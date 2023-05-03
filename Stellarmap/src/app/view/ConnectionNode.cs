using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Stellarmap
	{
	/// <summary>
	/// An avatar for room's exits that connecto to other rooms.
	/// </summary>
	public class ConnectionNode
		{
		ImageBox refSource;
		ImageBox refTarget;
		Point Offset;
		ConnectionLine Line = new ConnectionLine();
		public string Direction;
		public bool FailedConnection = false;
		public bool HasDoor = false;
		
		public ConnectionNode(ImageBox image,Point offset,string direction)
			{
			refSource = image;
			refTarget = refSource;
			Offset = offset;
			Direction = direction;
			}
		
		public void ResetConnectionNodeSizes(Point offset)
			{
			Offset = offset;
			}
		
		public ImageBox Source
			{
			get {return refSource;}
			}
		
		public ImageBox Target
			{
			get {return refTarget;}
			}
		
		public void SetTarget(ImageBox target)
			{
			if(refTarget != null && refTarget != refSource)
				{
				refTarget.RemoveFromConnections(this);
				}
			
			if(target == null)
				{refTarget = refSource;}
			else{
				refTarget = target;
				refTarget.AddToConnections(this);
				}
			}
		
		public void Draw(Graphics graphics,Point transform)
			{
			Point trans = Offset;
			trans.X += transform.X;
			trans.Y += transform.Y;
			
			//translate node-rect by image position
			Rectangle rect = new Rectangle(trans.X,trans.Y,Globals.NodeProperties.width,Globals.NodeProperties.height);
			rect.X += refSource.Location.X;
			rect.Y += refSource.Location.Y;


			
			//find center of noderect
			Point start = new Point(rect.X,rect.Y);
			start.X += (rect.Width/2);
			start.Y += (rect.Height/2);
			
			//change end of line to target if available
			Point end = refTarget.Location;
			end.X += transform.X + refTarget.Dimensions.Width / 2;
			end.Y += transform.Y + refTarget.Dimensions.Height / 2;
			
			//hack alert, if we don't have on of the main 10 directions
			//(n, s, e,w, u, d etc...) we assign this to 'other' with the
			//assumption that it is an 'enter' or some exit based on a static item
			if(!Globals.NodeProperties.NodeLocations.ContainsKey(Direction))
				{Line.Draw(graphics,start,end,"other");}
			else{Line.Draw(graphics,start,end,Direction);}
			
			if(refSource != refTarget)
				{
				graphics.FillRectangle(Globals.NodeProperties.ConnectedBrush,rect);
				if(HasDoor) { graphics.DrawRectangle(Globals.NodeProperties.DoorPen,rect); }
				else { graphics.DrawRectangle(Globals.NodeProperties.NoDoorPen,rect); }
				}
			else{
				if(this.FailedConnection)
					{graphics.FillRectangle(Globals.NodeProperties.FailedConnectionBrush,rect);}
				else{
					graphics.FillRectangle(Globals.NodeProperties.DefaultBrush,rect);
					if(HasDoor) {graphics.DrawRectangle(Globals.NodeProperties.DoorPen,rect);}
					else{graphics.DrawRectangle(Globals.NodeProperties.NoDoorPen,rect);}
					}
				}
			}		
		}
	}
