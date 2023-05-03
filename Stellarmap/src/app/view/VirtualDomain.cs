using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

//TODO: VirtualDomain tends to loop through a linkedlist of all room icons for a lot of operations.
//		It should just have a Dictionary added at some point to help pinpoint extact objects more quickly.
//UPDATED: Finally got around to adding that map reference but I still haven't implemented it in much

namespace Stellarmap
	{
	public class StringEventArgs : EventArgs
		{
		public string InputString;
		
		public StringEventArgs(string str)
			{
			InputString = str;
			}
		}
	
	/// <summary>
	/// The rendering and positioning state system for the GUI domain map.
	/// 
	/// NOTE: In an ideal world I would have seperated the actual rendering and
	/// positioning system but I just don't have the time or energy :/
	/// </summary>
	public class VirtualDomain
		{
		#region members
		Dictionary<string,ImageBox> AvatarMap = new Dictionary<string,ImageBox>();
		LinkedList<ImageBox>	RoomAvatars = new LinkedList<ImageBox>();
		LinkedList<ImageBox>	refSelectedImages = new LinkedList<ImageBox>();		
		ImageBox				refActiveImage;
		int GridSize = 45;
		bool GridLock = false;
		//can either be the total area for all images or the viewport depending
		Size					MaxArea = new Size();
		//Represents both the render target intrface as well as the camera.
		IViewPort				ViewPort = null;
		
		public event EventHandler<StringEventArgs> RoomSelectEvent;
		#endregion
		
		
		#region constructor
		public VirtualDomain(IViewPort viewPort)
			{
			System.Diagnostics.Debug.Assert(viewPort != null);
			
			ViewPort = viewPort;
			MaxArea = ViewPort.GetViewDimensions();
			}
		#endregion
		
		
		#region public interface
		/// <summary>
		/// Offsets the camera from its current position by the given vector.
		/// </summary>
		public void MoveCamera(int xVector,int yVector)
			{			
			ViewPort.ViewX += xVector;
			ViewPort.ViewY += yVector;
			}
		
		public void ResetCameraToOrigin()
			{
			ViewPort.ViewX = 0;
			ViewPort.ViewY = 0;
			//ViewPort.ViewX = -ViewPort.ViewWidth/2;
			//ViewPort.ViewY = -ViewPort.ViewHeight/2;
			}
		
		/// <summary>
		/// Offsets the selected images in the list by the specified vector.
		/// </summary>
		public void MoveSelectedImages(Point vector)
			{
			//this gets reused a couple times in this function
			Point point = new Point();
			
			//make sure the vector doesn't place them outside of the
			//viewport. Yes, I said VIEWPORT! I'm making the assumption
			//that the user is moving the images with the mouse and not
			//the programmer.
			Size s = CalculateContainmentSize(refSelectedImages);
			Point p = CalculateStartingPoint(refSelectedImages);


			if((p.X + vector.X + Globals.ImageBoxProperties.width) > (ViewPort.ViewWidth + ViewPort.ViewX) && (vector.X > 0))
				{
				vector.X = 0;
				}
			if((p.X + vector.X) < ViewPort.ViewX && (vector.X < 0))
				{
				//subtracting negative number
				vector.X = 0;//-= (0 + vector.X);
				}
			if((p.Y + vector.Y + Globals.ImageBoxProperties.height) > (ViewPort.ViewHeight + ViewPort.ViewY) && (vector.Y > 0))
				{
				vector.Y = 0;
				}
			if((p.Y + vector.Y) < ViewPort.ViewY && (vector.Y < 0))
				{
				//subtracting negative number
				vector.Y = 0;//-= (0 + vector.Y);
				}
			
			
			foreach(ImageBox image in refSelectedImages)
				{
				point.X = image.Location.X + vector.X;
				point.Y = image.Location.Y + vector.Y;
				
				image.Location = point;				
				}
						
			ResizeMaxAreaDimensions();
			}	
		
		/// <summary>
		/// Sets the internal rendering output size by resizing the
		/// rendertarget control.
		/// </summary>
		public void UpdateViewDimensions()
		    {
		    ResizeMaxAreaDimensions();
		    }
		
		public Size GetViewDimensions()
			{
			return ViewPort.GetViewDimensions();
			}
		
		public Point GetCameraLocation()
			{
			return new Point(ViewPort.ViewX,ViewPort.ViewY);
			}
		
		public Size GetMaxAreaDimensions()
			{
			return MaxArea;
			}
		
		public bool IsPointInSelectionGroup(int xPos,int yPos)
			{
			foreach(ImageBox image in refSelectedImages)
				{
				if(IsPointInImage(image,xPos,yPos) == true)	return true;
				}
			return false;
			}		
			
		public void Reset(object sender,object thing)
			{
			RoomAvatars.Clear();
			AvatarMap.Clear();
			refSelectedImages.Clear();
			refActiveImage = null;
			}			
		
			
		
		public void OnUpdateCamera(object sender,PointEventArgs e)
			{			
			ViewPort.ViewX = e.Location.X;
			ViewPort.ViewY = e.Location.Y;
			
			//trigger rendering
			}
		
		public void AddImage(Point position,Bitmap image,string roomName,string iconDesc)
			{
			//RoomAvatars.AddFirst(new ImageBox(ImageBoxProperties.RoomIcons[0],ImageBoxProperties.width,ImageBoxProperties.height));
			ImageBox imageBox = new ImageBox(image,Globals.ImageBoxProperties.width,Globals.ImageBoxProperties.height);
			imageBox.RoomName = roomName;
			imageBox.Icon = iconDesc;
			RoomAvatars.AddFirst(imageBox);
			AvatarMap.Add(roomName,imageBox);
			position.X += ViewPort.ViewX;
			position.Y += ViewPort.ViewY;
			RoomAvatars.First.Value.Location = position;
			//ImageBox image = RoomImages.First.Value; //UUUM, WHY DID I PUT THIS HERE?
			ResizeMaxAreaDimensions();
			}	
		
		public List<string> RemoveImage()
			{
			List<string> RoomsRemoved = new List<string>();
			
			//removes selected image or images
			foreach(ImageBox image in refSelectedImages)
				{
				AvatarMap.Remove(image.RoomName);
				image.RemoveAllConnections();
				RoomsRemoved.Add(image.RoomName);
				RoomAvatars.Remove(image);
				}
			
			refSelectedImages.Clear();
			ResizeMaxAreaDimensions();
			
			return RoomsRemoved;
			}
		
		public void SetImageSource(string roomName,Bitmap image,string iconName)
			{
			foreach(ImageBox box in RoomAvatars)
				{
				if(box.RoomName == roomName)
					{
					box.SetNewImageSource(image,iconName);
					}
				}
			}
		
		public string GetIconDesc(string roomName)
			{
			foreach(ImageBox box in RoomAvatars)
				{
				if(box.RoomName == roomName)
					{
					return box.Icon;
					}
				}
			return Globals.ImageBoxProperties.DefaultIconDesc;
			}

		public Point GetIconPosition(string roomName)
			{
			foreach(ImageBox box in RoomAvatars)
				{
				if(box.RoomName == roomName)
					{
					return box.Location;
					}
				}
			return new Point(0,0);
			}

		public Point GetTransformedIconPosition(string roomName)
			{
			foreach(ImageBox box in RoomAvatars)
				{
				if(box.RoomName == roomName)
					{
					return new Point(box.Location.X + ViewPort.ViewX, box.Location.Y + ViewPort.ViewY);
					}
				}
			return new Point(0,0);
			}
		
		/// <summary>
		/// Renders the rooms of the domain as 'ImageBox'es within a
		/// control using a given 'ControlRenderDevice'.
		/// </summary>
		public void OnRenderDomainMap(ControlRenderDevice device)
			{
			//here, we're using a stack to easily access them in the correct rendering order
			Stack<ImageBox> stack = new Stack<ImageBox>(RoomAvatars);
			
			Point p = new Point(-ViewPort.ViewX,-ViewPort.ViewY);
			
			//Yup, this is ugly but I want it to just work. All connection
			//lines need to be drawn behind the imageboxes
			foreach(ImageBox image in stack)
				{
				Point vector = new Point(p.X + image.Location.X,p.Y + image.Location.Y);

				if(GridLock)
					{
					int dx = vector.X % GridSize;
					if(dx != 0) { vector.X -= dx; }

					int dy = vector.Y % GridSize;
					if(dy != 0) { vector.Y -= dy; }
					}

				vector.X -= image.Location.X;
				vector.Y -= image.Location.Y;
				image.DrawNodes(device.Graphics,vector);
				}
			
			foreach(ImageBox image in stack)
				{
				Point vector = new Point(p.X+image.Location.X,p.Y+image.Location.Y);
				
				if(GridLock)
					{
					int dx = vector.X % GridSize;
					if(dx != 0) { vector.X -= dx; }
					
					int dy = vector.Y % GridSize;
					if(dy != 0) { vector.Y -= dy; }
					}
				
				vector.X -= image.Location.X;
				vector.Y -= image.Location.Y;
				image.DrawTransform(device.Graphics,vector);
				}
			}
		
		public ImageBox OnHilightImage(int xPos,int yPos)
			{			
			//After the FIRST image within the passed coords is
			//found, we need to ensure all the rest are deselected
			ImageBox selected = null;
			refSelectedImages.Clear();
			
			foreach(ImageBox image in RoomAvatars)
				{
				if(selected == null && IsPointInImage(image,xPos,yPos))
					{
					image.Mode = ImageBoxRenderMode.Selected;					
					refSelectedImages.AddFirst(image);					
					selected = image;
					}
				else{
					//return anything else to its previous state
					if(image != refActiveImage)
						{
						image.Mode = ImageBoxRenderMode.Normal;
						}
					else{
						image.Mode = ImageBoxRenderMode.Active;
						}
					}
				}
			
			return selected;
			}
		
		public ImageBox OnHilightImage(string roomName)
			{
			//After the FIRST image within the passed coords is
			//found, we need to ensure all the rest are deselected
			ImageBox selected = null;
			refSelectedImages.Clear();
			
			foreach(ImageBox image in RoomAvatars)
				{
				if(selected == null && roomName == image.RoomName)
					{
					image.Mode = ImageBoxRenderMode.Selected;					
					refSelectedImages.AddFirst(image);					
					selected = image;
					}
				else{
					//return anything else to its previous state
					if(image != refActiveImage)
						{
						image.Mode = ImageBoxRenderMode.Normal;
						}
					else{
						image.Mode = ImageBoxRenderMode.Active;
						}
					}
				}
			
			return selected;
			}
		
		public bool OnSelectImageGroup(Rectangle region)
			{
			bool selected = false;
			refSelectedImages.Clear();
						
			foreach(ImageBox image in RoomAvatars)
				{
				if(IsRoomInRect(image,region))
					{
					image.Mode = ImageBoxRenderMode.Selected;
					refSelectedImages.AddFirst(image);
					selected = true;
					}
				else{
					image.Mode = ImageBoxRenderMode.Normal;					
					}
				}
			
			//now bring all the selected images to the top			
			foreach(ImageBox image in refSelectedImages)
				{
				ImageBox temp = image;
				RoomAvatars.Remove(image);
				RoomAvatars.AddFirst(temp);
				}
			return selected;
			}
		
		public bool OnActivateImage(int xPos,int yPos)
			{
			bool selected = false;
			ImageBox temp = null;
			
			foreach(ImageBox image in RoomAvatars)
				{
				if(!selected && IsPointInImage(image,xPos,yPos))
					{
					image.Mode = ImageBoxRenderMode.Active;
					selected = true;
					temp = image;
					}
				else{
					image.Mode = ImageBoxRenderMode.Normal;					
					}
				}
			
			refActiveImage = temp;
			
			//swap the image to the front of the list
			//that way it gets rendered on top
			if(selected)
				{							
				RoomAvatars.Remove(temp);
				RoomAvatars.AddFirst(refActiveImage);
				
				if(RoomSelectEvent != null) RoomSelectEvent(this,new StringEventArgs(refActiveImage.RoomName));
				}
			else{
				if(RoomSelectEvent != null) RoomSelectEvent(this,new StringEventArgs(null));
				}
			return selected;
			}

		public bool OnActivateImage(string roomName)
			{
			bool selected = false;
			ImageBox temp = null;

			foreach(ImageBox image in RoomAvatars)
				{
				if(!selected && roomName == image.RoomName)
					{
					image.Mode = ImageBoxRenderMode.Active;
					selected = true;
					temp = image;
					}
				else
					{
					image.Mode = ImageBoxRenderMode.Normal;
					}
				}

			refActiveImage = temp;

			//swap the image to the front of the list
			//that way it gets rendered on top
			if(selected)
				{
				RoomAvatars.Remove(temp);
				RoomAvatars.AddFirst(refActiveImage);

				if(RoomSelectEvent != null) RoomSelectEvent(this,new StringEventArgs(refActiveImage.RoomName));
				}
			else
				{
				if(RoomSelectEvent != null) RoomSelectEvent(this,new StringEventArgs(null));
				}
			return selected;
			}	
		
		public ConnectionNode OnHoverNode(int xPos,int yPos)
			{
			Point p = new Point(xPos + ViewPort.ViewX, yPos + ViewPort.ViewY);
			
			foreach(ImageBox image in RoomAvatars)
				{
				ConnectionNode node = image.PointInAnyNode(p);				
				if(node != null)	return node;
				}
			
			return null;
			}
		
		public void ConnectNodeToAvatar(string ownerRoom,string targetRoom,string nodeDirection)
			{
			ImageBox target = null;
			ImageBox owner = null;

			if(ownerRoom == null || ownerRoom == "" || targetRoom == "" || nodeDirection == null || nodeDirection == "")
				{ return; }
			
			if(targetRoom == null)
				{target = null;}
			else{
				if(AvatarMap.ContainsKey(targetRoom))
					{target =  AvatarMap[targetRoom];}
				}
			if(AvatarMap.ContainsKey(ownerRoom))
				{owner = AvatarMap[ownerRoom];}
			
					
			//hack alert! we are setting this up for static item based exits/enters
			if(!Globals.NodeProperties.NodeLocations.ContainsKey(nodeDirection))
				{
				nodeDirection = "other";
				}
			
			if(targetRoom != null && targetRoom.Length > 0 && target == null && owner != null)
				{
				//failed connection
				
				ConnectionNode node = owner.GetNode(nodeDirection);
				if(node != null)
					{
					node.FailedConnection = true;
					node.SetTarget(target);
					}
				}
			else{
				//connect / disconnect
				if(owner != null)
					{
					ConnectionNode node = owner.GetNode(nodeDirection);
					if(node != null)
						{
						node.FailedConnection = false;
						node.SetTarget(target);
						}
					return;
					}
				}
			return;
			}
		
		public void SetNodeExitDoor(string ownerRoom,string nodeDirection,bool exitFlag)
			{
			if(ownerRoom == null || ownerRoom == "" || nodeDirection == null || nodeDirection == "")
			{ return; }

			foreach(ImageBox image in RoomAvatars)
				{
				if(image.RoomName == ownerRoom)
					{
					nodeDirection = nodeDirection.Replace("\"","");
					ConnectionNode node = image.GetNode(nodeDirection);
					if(node != null)
						{node.HasDoor = exitFlag;}
					}
				}
			}
		
		public void ResetAllIcons(Dictionary<string,Bitmap> roomIcons)
			{
			foreach(ImageBox box in this.RoomAvatars)
				{
				if(roomIcons.ContainsKey(box.Icon))
					{
					box.SetNewImageSource(roomIcons[box.Icon],
										   box.Icon,
										   Globals.ImageBoxProperties.width,
										   Globals.ImageBoxProperties.height);
					}
				else{
					box.SetNewImageSource(Globals.ImageBoxProperties.DefaultImage,
									  Globals.ImageBoxProperties.DefaultIconDesc,
									  Globals.ImageBoxProperties.width,
									  Globals.ImageBoxProperties.height);
					}
				}
			}	
		#endregion
		
		
		#region private methods
		/// <summary>
		/// Returns true if the given point is within the given ImageBox.
		/// </summary>
		private bool IsPointInImage(ImageBox image,int xPos,int yPos)
			{
			xPos += ViewPort.ViewX;
			yPos += ViewPort.ViewY;
			
			if(	xPos > image.Location.X && xPos < image.Location.X + Globals.ImageBoxProperties.width &&
			    yPos > image.Location.Y && yPos < image.Location.Y + Globals.ImageBoxProperties.height)
				{
				return true;
				}
			return false;
			}
		
		/// <summary>
		/// Returns true is the given ImageBox's center is within the given rect.
		/// </summary>
		private bool IsRoomInRect(ImageBox image,Rectangle rect)
			{
			rect.X += ViewPort.ViewX;
			rect.Y += ViewPort.ViewY;
			int xMidPoint = image.Location.X + (Globals.ImageBoxProperties.width / 2);
			int yMidPoint = image.Location.Y + (Globals.ImageBoxProperties.height / 2);
			
			//basically all we need to find is if the center point of the image
			//is within the given rect
			if(	xMidPoint > rect.Left && xMidPoint < rect.Right &&
				yMidPoint > rect.Top && yMidPoint < rect.Bottom)
				{
				return true;
				}
			return false;
			}
		
		/// <summary>
		/// Combines the largest dimensions from the given rects.
		/// </summary>
		private Size MakeBiggestSize(Size size1,Size size2)
			{
			Size size = new Size();
			size.Width = System.Math.Max(size1.Width,size2.Width);
			size.Height = System.Math.Max(size1.Height,size2.Height);
			return size;
			}
		
		/// <summary>
		/// Returns the minimum region needed to contain all of the
		/// imageboxes. This can be used for relating the max area
		/// to the visible area.
		/// 
		/// NOTE: Yeah so it's not the best thing in the world to repeatedly
		/// run through the same list but I don't think there will be that many
		/// rooms and I'd rather just get this working ;)
		/// </summary>
		private Size CalculateContainmentSize(LinkedList<ImageBox> imageList)
			{
			Size size = new Size(0,0);			
			
			foreach(ImageBox image in imageList)
				{
				//I need to find which image has the furthest x position and which has the
				//furthest y position and combine them into one value.
				size.Width = System.Math.Max(image.Location.X + Globals.ImageBoxProperties.width,size.Width);
				size.Height = System.Math.Max(image.Location.Y + Globals.ImageBoxProperties.height,size.Height);
				}
			
			return size;
			}
		
		/// <summary>
		/// Runs through a list of all given images and combines the smaller positions
		/// </summary>
		private Point CalculateStartingPoint(LinkedList<ImageBox> imageList)
			{
			Point point = new Point(MaxArea.Width,MaxArea.Height);			
			
			foreach(ImageBox image in imageList)
				{
				//I need to find which image has the smallest x position and which has the
				//smallest y position and combine them into one value.
				point.X = System.Math.Min(image.Location.X,point.X);
				point.Y = System.Math.Min(image.Location.Y,point.Y);
				}
			
			return point;
			}	
		
		/// <summary>
		/// Sets the MaxArea to accomodate both the total area that the images inhabit
		/// as well as the maximum viewing area.
		/// </summary>
		private void ResizeMaxAreaDimensions()
			{
			//in case we are dragging an image that is at the bounds of space, we don't want
			//to shrink the MaxArea to anything less than the outer bound of the current viewing rect.
			//At least, not until the camera has been moved back in.
			Size temp = new Size(ViewPort.ViewX + ViewPort.ViewWidth, ViewPort.ViewY + ViewPort.ViewHeight);
			MaxArea = MakeBiggestSize(temp,CalculateContainmentSize(RoomAvatars));
			
			//MaxArea = MakeBiggestSize(ViewPort.GetViewDimensions(),CalculateContainmentSize(RoomAvatars));
			}	
		#endregion
		}
	}
