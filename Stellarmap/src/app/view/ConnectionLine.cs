using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Stellarmap
	{
	enum Direction
		{
		Up,
		Down,
		Left,
		Right,
		}
	
	public class ConnectionLine
		{
		//stuff for connection lines
		Pen Pen = new Pen(System.Drawing.Color.White);
		
		
		public void Draw(Graphics graphics,Point start,Point end,string direction)
			{
			Pen.Width = Globals.ImageBoxProperties.ExitLineWidth;
			//hack alert - we are hardcoding more stuff for both colors and the functionality of
			//the up/down connection lines
			
			//more hacks - now we are checking again all directions, any direction that is not
			//one of the main
			Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			
			if(direction == "up")
				{Pen.Color = Globals.GUI.ConnectionLineUp;}
			else if(direction == "down")
				{Pen.Color = Globals.GUI.ConnectionLineDown;}
			else if(direction == "other")
				{
				Pen.Color = Globals.GUI.ConnectionLineOther;
				Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				Pen.DashOffset = 4;
				}
			else if(direction == "out")
				{
				Pen.Color = Globals.GUI.ConnectionLineOut;
				Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				Pen.DashOffset = 0;
				}
			else{Pen.Color = Globals.GUI.ConnectionLineCardinal;}
				
			graphics.DrawLine(Pen,start,end);
			}
		
		private Direction CalculateDirection(Point start,Point stop)
			{
			return Direction.Up;
			}
		}
	}
