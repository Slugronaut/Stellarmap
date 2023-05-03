using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	/// <summary>
	/// Wrapper for a Virtual Domain's visible area. Handles the control that is
	/// rendered to, as well as the viewing range it represents and the scorllbars
	/// that can control it.
	/// </summary>
	public partial class RenderTarget : UserControl,IViewPort,IRenderTarget
		{
		private VirtualDomain refVirtualDomain;
		private event PaintEventHandler RenderEvent;
		bool DonotHScroll = false;
		bool DonotVScroll = false;
		int DefaultHeight;
		Rectangle ViewRect = new Rectangle();
		
		
		public RenderTarget()
			{
			InitializeComponent();
			}
		
		
		#region public interface
		public void Initialize(VirtualDomain vDomain,PaintEventHandler renderEvent)
			{
			System.Diagnostics.Debug.Assert(vDomain != null && renderEvent != null);
			refVirtualDomain = vDomain;
			RenderEvent += new PaintEventHandler(renderEvent);
			}
		
		public void SetViewport(Rectangle area)
			{
			this.ViewX = area.X;
			this.ViewY = area.Y;
			this.ViewWidth = area.Width;
			this.ViewHeight = area.Height;
			}
		
		public void SetViewPosition(Point location)
			{
			this.ViewX = location.X;
			this.ViewY = location.Y;
			}
		
		public void SetViewPortDimensions(Size size)
			{
			this.ViewWidth = size.Width;
			this.ViewHeight = size.Height;
			}
		
		
		
		public Rectangle GetViewport()
			{
			return new Rectangle(this.ViewX,this.ViewY,this.ViewWidth,this.ViewHeight);
			}
		
		public Point GetViewPosition()
			{
			return new Point(this.ViewX,this.ViewY);
			}
		
		public Size GetViewDimensions()
			{
			return new Size(this.ViewWidth,this.ViewHeight);
			}
		
		
		
		public int ViewX
			{
			get{return this.ViewRect.X;}
			set{this.ViewRect.X = value;}
			}
		
		public int ViewY
			{
			get{return this.ViewRect.Y;}
			set{this.ViewRect.Y = value;}
			}
		
		public int ViewWidth
			{
			get {return this.Width;}
			set {this.Width = Math.Max(value,0);}
			}
		
		public int ViewHeight
			{
			get {return this.Height;}
			set {this.Height = Math.Max(value,0);}
			}

		public int RegionWidth
			{
			get {return this.refVirtualDomain.GetMaxAreaDimensions().Width;}// + this.ViewWidth - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth; }
			}

		public int RegionHeight
			{
			get {return this.refVirtualDomain.GetMaxAreaDimensions().Height;}// + this.ViewHeight - System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight; }
			}
		
		public void TriggerResize(int xSize,int ySize)
			{
			this.ViewWidth = xSize;
			this.ViewHeight = ySize;
			}		
		#endregion
		
		
		}
	}
