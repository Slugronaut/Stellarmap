using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Stellarmap
	{
	public interface IViewPort
		{
		void SetViewport(Rectangle area);
		void SetViewPortDimensions(Size size);
		void SetViewPosition(Point location);
		
		Rectangle GetViewport();
		Size GetViewDimensions();
		Point GetViewPosition();
		
		void TriggerResize(int x,int y);
		
		int ViewX
			{
			get;
			set;
			}
					
		int ViewY
			{
			get;
			set;
			}
		
		int ViewWidth
			{
			get;
			set;
			}
		
		int ViewHeight
			{
			get;
			set;
			}

		int RegionWidth
			{
			get;
			}

		int RegionHeight
			{
			get;
			}	
		}
	}
