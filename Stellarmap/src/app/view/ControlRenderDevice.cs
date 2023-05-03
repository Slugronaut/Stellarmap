using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Stellarmap
	{
	public class RenderDeviceException : Exception
		{
		public RenderDeviceException():base() {}
		public RenderDeviceException(string message):base(message){}
		public RenderDeviceException(string message,Exception inner):base(message,inner){}
		}
	
	/// <summary>
	/// Mechanism for rendering ImageBoxes to a control.
	/// </summary>
	public class ControlRenderDevice
		{
		Control					refRenderTarget;
		BufferedGraphics		Buffer;
		BufferedGraphicsContext	BufferContext;
		
		public ControlRenderDevice(IRenderTarget renderTarget)
			{
			System.Diagnostics.Debug.Assert(renderTarget != null && renderTarget is Control);
			this.refRenderTarget = (Control)renderTarget;
			BufferContext = BufferedGraphicsManager.Current;
			
			//make it larger than control in order to create temp buffer
			BufferContext.MaximumBuffer = new Size(refRenderTarget.Width + 1,refRenderTarget.Height + 1);
			Buffer = BufferContext.Allocate(refRenderTarget.CreateGraphics(), new Rectangle(0, 0, refRenderTarget.Width, refRenderTarget.Height));
			
			//allow alpha
			Buffer.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
			}
		
		public Graphics Graphics
			{
			get {
				//assert that buffer was created after resize
				if(Buffer == null)	return null;
				
				return Buffer.Graphics;
				}
			}
		
		public void RenderBitmap(Bitmap image,Rectangle destRect)
			{
			//assert that buffer was created after resize
			if(Buffer == null)	return;
			
			Buffer.Graphics.DrawImage(image,destRect,0,0,image.Width,image.Height,GraphicsUnit.Pixel);
			}
		
		public void Flip()
			{
			//assert that buffer was created after resize
			if(Buffer == null)	return;
			
			Buffer.Render();
			}
		
		public void Clear(Color backgroundColor)
			{
			//assert that buffer was created after resize
			if(Buffer == null)	return;
			
			Buffer.Graphics.Clear(backgroundColor);
			}
		
		public void ResizeBuffer()
			{
			//assert that buffer was created after resize
			if(Buffer != null)
				{
				Buffer.Dispose();
				BufferContext.Dispose();
				Buffer = null;
				BufferContext = null;
				}
			
			
			//don't resize if less than 1x1
			if(refRenderTarget.Width > 0 && refRenderTarget.Height > 0)
				{			
				//make it larger than control in order to create temp buffer
				BufferContext = BufferedGraphicsManager.Current;
				BufferContext.MaximumBuffer = new Size(refRenderTarget.Width + 1,refRenderTarget.Height + 1);
				Buffer = BufferContext.Allocate(refRenderTarget.CreateGraphics(), new Rectangle(0, 0, refRenderTarget.Width, refRenderTarget.Height));
				}
			else {
				Buffer = null;
				//throw new RenderDeviceException("Buffer could not be resized!");
				}
			}			
		}
	}
