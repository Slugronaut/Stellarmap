using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Stellarmap
	{
	partial class Positioner
		{
		//protected Control Parent = null;
		//protected Control LastAdded = null;
			
		//dimensions of parent in order to contain all added controls		
		//private System.Drawing.Size MaxSize = new System.Drawing.Size();
				
		public Positioner()
			{			
			}		
		
		public void AddControlBelow(Control parent,Control anchor,Control toPosition,System.Drawing.Point offset)
			{			
			parent.Controls.Add(toPosition);			
			if(anchor != null)
				{
				toPosition.Location = new System.Drawing.Point(anchor.Location.X + offset.X, anchor.Location.Y + anchor.Height + (int)Metrics.VerticalMargin + offset.Y);
				//parent.AutoScroll = true;
				}
			if(anchor == null)
				{
				toPosition.Location = new System.Drawing.Point(offset.X,offset.Y);
				}
			}

		public void AddControlNextTo(Control parent,Control anchor,Control toPosition,System.Drawing.Point offset)
			{			
			parent.Controls.Add(toPosition);
			if(anchor != null)
				{
				toPosition.Location = new System.Drawing.Point(anchor.Location.X + anchor.Width + (int)Metrics.HorizontalMargin + offset.X, anchor.Location.Y + offset.Y);
				//parent.AutoScroll = true;
				}
			if(anchor == null)
				{
				toPosition.Location = new System.Drawing.Point(offset.X,offset.Y);
				}
			}

		public void AddControlBelow(Control parent,Control anchor,Control toPosition)
			{
			parent.Controls.Add(toPosition);
			if(anchor != null)
				{
				toPosition.Location = new System.Drawing.Point(anchor.Location.X,anchor.Location.Y + anchor.Height + (int)Metrics.VerticalMargin);
				//parent.AutoScroll = true;
				}
			}

		public void AddControlNextTo(Control parent,Control anchor,Control toPosition)
			{
			parent.Controls.Add(toPosition);
			if(anchor != null)
				{
				toPosition.Location = new System.Drawing.Point(anchor.Location.X + anchor.Width + (int)Metrics.HorizontalMargin,anchor.Location.Y);
				//parent.AutoScroll = true;
				}
			}
		
		}
	}
