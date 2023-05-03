using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Stellarmap
	{
	public class PointEventArgs : EventArgs
		{
		public Point Location;
		
		public PointEventArgs(Point point)
			{
			Location = point;
			}
		
		public PointEventArgs(int x,int y)
			{
			Location = new Point(x,y);
			}
		}

	public class RectEventArgs : EventArgs
		{
		public Rectangle Rect;
		
		public RectEventArgs(Rectangle rect)
			{
			Rect = rect;
			}
		}
	
	public class MouseMoveArgs : EventArgs
		{
		public Point Current;
		public Point Start;
		public Point End;
		public Point Offset;	
		public int Delta;
		
		public MouseMoveArgs(Point current,Point start,Point end,Point offset)
			{
			Current = current;
			Start = start;
			End = end;
			Offset = offset;
			}
		
		public MouseMoveArgs()
			{
			}
		}
	
	public enum ConnectionType
		{
		Connect,
		Disconnect,
		}
	
	public class RoomConnectionArgs : EventArgs
		{
		public string Room;
		public string ExitPath;
		public string Direction;
		public ConnectionType Type;
		
		public RoomConnectionArgs(string room,string exit,string direction,ConnectionType type)
			{
			Room = room;
			ExitPath = exit;
			Direction = direction;
			Type = type;
			}
		
		public RoomConnectionArgs()
			{
			}
		}
	
	}
