using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Stellarmap
{
	public enum ImageBoxRenderMode
		{
		Normal,
		Active,
		Selected,
		Error,
		}
	
	/// <summary>
	/// Used to represent a generic rectangular image that can be used as an avatar for rooms.
	/// </summary>
	public class ImageBox : IDisposable
		{
		#region memebers
		Rectangle		DestRect;
		Rectangle		SourceRect;
		Bitmap			refImage;
		SolidBrush		Brush = new SolidBrush(System.Drawing.Color.Black);
		public ImageBoxRenderMode Mode = ImageBoxRenderMode.Normal;
		public string RoomName; //yeah I know, not elegant but I didn't feel like adding more abstraction at 4 in the morning
		public string Icon;
		List<ConnectionNode> Connected= new List<ConnectionNode>();
		List<ConnectionNode> Nodes = new List<ConnectionNode>(10);
		Dictionary<ConnectionNode,bool> HasExit = new Dictionary<ConnectionNode,bool>();
		bool SuppressDisconnect = true;
		
		//used for transparent overlay rendering
		ColorMatrix		cm = new ColorMatrix();
		ImageAttributes ia = new ImageAttributes();
		
		bool Disposed = false;
		#endregion
		
		
		public ImageBox(Bitmap sourceImage,int width,int height)
			{
			System.Diagnostics.Debug.Assert(sourceImage != null);
			
			this.refImage = sourceImage;
			DestRect = new Rectangle(0,0,width,height);
			SourceRect = new Rectangle(0,0,width,height);
			
			//stuff used for transparent rendering
			cm.Matrix33 = Globals.ImageBoxProperties.ImageAlpha;		
			ia.SetColorMatrix(cm);
			
			
			foreach(string key in Globals.NodeProperties.NodeLocations.Keys)
				{
				Point point = new Point(Globals.NodeProperties.NodeLocations[key].X,Globals.NodeProperties.NodeLocations[key].Y);
				Nodes.Add(new ConnectionNode(this,point,key));
				}
			}
		
		~ImageBox()
			{
			Dispose(false);
			}
		
		public void Dispose()
			{
			Dispose(true);
			GC.SuppressFinalize(this);
			}
		
		
		#region private
		protected virtual void Dispose(bool disposing)
			{
			if(!Disposed)
				{
				if(disposing)
					{
					refImage.Dispose();
					}
				}
			
			Disposed = true;
			}
		
		private void Draw(Graphics gr,Point position)
			{
			if(gr == null)	return;
			DestRect.X = position.X;
			DestRect.Y = position.Y;
			
			switch(Mode)
				{
				case ImageBoxRenderMode.Selected:
					{
					Brush.Color = System.Drawing.Color.GhostWhite;
					break;
					}
				case ImageBoxRenderMode.Active:
					{
					Brush.Color = System.Drawing.Color.Yellow;
					break;
					}
				case ImageBoxRenderMode.Error:
					{
					Brush.Color = System.Drawing.Color.Red;
					break;
					}					
				default:
					{
					Brush.Color = System.Drawing.Color.DodgerBlue;
					break;
					}
				}
			
			//Render a color overlay, in actuality it is really
			//the box rendered first, followed by a transparent
			//version of the image.
			gr.FillRectangle(Brush,this.DestRect);
			
			
			gr.CompositingMode = CompositingMode.SourceOver;
			gr.CompositingQuality = CompositingQuality.HighSpeed;
			gr.DrawImage(refImage,DestRect,0,0,refImage.Width,refImage.Height,GraphicsUnit.Pixel,ia);
			gr.CompositingMode = CompositingMode.SourceCopy;			
			}	
		
		private bool IsPointInNode(string nodeKey,Point point)
			{
			if(!Globals.NodeProperties.NodeLocations.ContainsKey(nodeKey)) return false;
			
			
			Rectangle rect = new Rectangle(Globals.NodeProperties.NodeLocations[nodeKey].X + this.Location.X,
											Globals.NodeProperties.NodeLocations[nodeKey].Y + this.Location.Y,
											Globals.NodeProperties.width,
											Globals.NodeProperties.height);
			
			if(point.X > rect.Left && point.X < rect.Right &&
			   point.Y > rect.Top && point.Y < rect.Bottom)
				{
				return true;
				}
			return false;
			}	
		#endregion
		
		
		#region public interface
		public Point Location
			{
			set {
				DestRect.X = value.X;
				DestRect.Y = value.Y;
				}
			get {
				return new Point(DestRect.X,DestRect.Y);
				}
			}
		
		public Size Dimensions
			{
			get {
				return new Size(DestRect.Width,DestRect.Height);
				}
			}
		
		public void AddToConnections(ConnectionNode node)
			{
			Connected.Add(node);
			}
		
		public void RemoveFromConnections(ConnectionNode node)
			{
			if(Connected.Contains(node) && !SuppressDisconnect)
				{
				Connected.Remove(node);
				}
			}
		
		public void RemoveAllConnections()
			{
			SuppressDisconnect = true;
			foreach(ConnectionNode node in Connected)
				{
				node.SetTarget(null);
				}
			SuppressDisconnect = false;
			Connected.Clear();
			}
		
		public void Draw(Graphics gr)
			{
			Draw(gr,Location);
			}
		
		public void DrawTransform(Graphics gr,Point trans)
			{
			if(gr == null)	return;
			Rectangle destRect = DestRect;
			destRect.X += trans.X;
			destRect.Y += trans.Y;


			
			//gr.Transform = new Matrix(1,0,0,1,
			//SlimDX.Matrix projection = SlimDX.Matrix.Identity;
			//float right = 1.0f;
			//float left = -1.0f;
			//float bottom = -r;
			//float top = r;
			//float near = 0.0f;
			//float far = 1.0f;
			
			
			
			//projection = SlimDX.Matrix.Identity;
			//projection.M11 = (float)( (2.0f/(right - left)) / (float)refRenderTarget.ScreenResolution.X );
			//projection.M22 = (float)( (2.0f/(top - bottom)) / (float)refRenderTarget.ScreenResolution.X );
			//projection.M33 = 2.0f/(far - near);
			
			switch(Mode)
				{
				case ImageBoxRenderMode.Selected:
					{
					Brush.Color = System.Drawing.Color.GhostWhite;
					break;
					}
				case ImageBoxRenderMode.Active:
					{
					Brush.Color = System.Drawing.Color.Yellow;
					break;
					}
				case ImageBoxRenderMode.Error:
					{
					Brush.Color = System.Drawing.Color.Red;
					break;
					}					
				default:
					{
					Brush.Color = System.Drawing.Color.DodgerBlue;
					break;
					}
				}
			
			//Render a color overlay, in actuality it is really
			//the box rendered first, followed by a transparent
			//version of the image.
			gr.FillRectangle(Brush,destRect);
			
			
			gr.CompositingMode = CompositingMode.SourceOver;
			gr.CompositingQuality = CompositingQuality.HighSpeed;
			gr.DrawImage(refImage,destRect,0,0,refImage.Width,refImage.Height,GraphicsUnit.Pixel,ia);
			gr.CompositingMode = CompositingMode.SourceCopy;
			}			
		 
		public void DrawNodes(Graphics gr,Point transform)
			{
			if(gr == null)	return;
						
			foreach(ConnectionNode node in Nodes)
				{
				bool exitFlag = false;
				if(HasExit.ContainsKey(node))
					{exitFlag = HasExit[node];}
				node.Draw(gr,transform);
				}
			}
		
		public void SetNewImageSource(Bitmap sourceImage,string iconName)
			{
			System.Diagnostics.Debug.Assert(sourceImage != null);
			this.refImage = sourceImage;
			this.Icon = iconName;
			}
		
		public void SetNewImageSource(Bitmap sourceImage,string iconName,int width,int height)
			{
			System.Diagnostics.Debug.Assert(sourceImage != null);
			this.refImage = sourceImage;
			this.Icon = iconName;
			this.SourceRect.Width = width;
			this.SourceRect.Height = height;
			this.DestRect.Width = width;
			this.DestRect.Height = height;

			int count = 0; //nodes are reference in order of direcrtion names so this should work
			foreach(string key in Globals.NodeProperties.NodeLocations.Keys)
				{
				Point point = new Point(Globals.NodeProperties.NodeLocations[key].X,Globals.NodeProperties.NodeLocations[key].Y);
				Nodes[count].ResetConnectionNodeSizes(point);
				count++;
				}
			}
		
		public ConnectionNode PointInAnyNode(Point point)
			{
			int index = 0;
			foreach(string key in Globals.NodeProperties.NodeLocations.Keys)
				{
				//hack alert - we are deliberately avoiding contact with up/down/other nodes because
				//they are underneath the image and I haven't thought of a better way to display them.
				//In the future we will probably need some kind of shifted mode where the user
				//can then select the up/down nodes. This way they won't accidentally start dragging 
				//connection line when they really meant to move the imagebox.
				//UPDATE: We are now ignore all other nodes as well since we want to draw connection lines
				//for any exit/enter based on a static item. The node for these is dead center in the image
				//box so we *have* to ignore it
				if(IsPointInNode(key,point))
					{
					if(!Globals.NodeProperties.NodeLocations.ContainsKey(key))	{return null;}
					if(key == "up" || key == "down" || key == "other" || key == "out")	{return null;}
					return this.Nodes[index];
					}
				index++;
				}
			
			return null;
			}
		
		public ConnectionNode GetNode(string direction)
			{
			foreach(ConnectionNode node in Nodes)
				{
				if(node.Direction == direction) return node;
				}
			return null;
			}	
		
		public void SetExitDoorFlag(string direction,bool flag)
			{
			foreach(ConnectionNode node in Nodes)
				{
				if(node.Direction == direction)
					{
					HasExit[node] = flag;
					}
				}
			}	
		#endregion
		}
}
