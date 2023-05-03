using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Stellarmap
	{
	/// <summary>
	/// Summary description for FlickerFreeRichEditTextBox - Subclasses the
	/// RichTextBox to allow control over flicker
	/// </summary>
	public class FlickerFreeRichEditTextBox : RichTextBox
		{
		const short WM_PAINT = 0x00f;
		public bool EnablePainting = true;
		
		public FlickerFreeRichEditTextBox()
			{
			}
		
		protected override void WndProc(ref System.Windows.Forms.Message m)
			{
			// Code courtesy of Mark Mihevc
			// sometimes we want to eat the paint message so we don't have to see all the
			// flicker from when we select the text to change the color.
			if (m.Msg == WM_PAINT)
				{
				if (EnablePainting)base.WndProc(ref m); // if we decided to paint this control, just call the RichTextBox WndProc
				else{
					m.Result = IntPtr.Zero; // not painting, must set this to IntPtr.Zero if not painting otherwise serious problems.
					}
				}
			else{
				base.WndProc(ref m); // message other than WM_PAINT, jsut do what you normally do.
				}
			
			}

		protected override void OnMouseDown(MouseEventArgs e)
			{
			if(e.Button == MouseButtons.Right && this.SelectionLength < 1)
				{
				int index = this.GetCharIndexFromPosition(e.Location);
				if(index == this.Text.Length - 1)
					{
					string last = this.Text.Substring(this.Text.Length - 1,1);
					Size size = TextRenderer.MeasureText(last,this.Font);
					Point point = this.GetPositionFromCharIndex(this.Text.Length - 1);
					if(e.X > point.X + size.Width / 4)
						{
						this.SelectionStart = this.Text.Length;
						}
					else{this.SelectionStart = index;}
					}
				else{this.SelectionStart = index;}
				//int index = this.GetCharIndexFromPosition(PointToClient(MousePosition));
				//if(index >= this.Text.Length)	{index = this.Text.Length;}
				//this.Select(index,0);
				
				//HACK ALERT
				//NOTE: We are setting the focus here so that any spellchecker will properly
				//pick up the cursor position. Without the focus, the cursor will default to the
				//last word in the text box and the spellchecker will change the wrong word (I think it
				//only happens if the last word happens to be misspelled, haven't bothered testing this though)
				this.Focus();
				}
			base.OnMouseDown(e);
			}
		
	}
}