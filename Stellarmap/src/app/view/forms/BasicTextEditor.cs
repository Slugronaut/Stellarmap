using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Stellarmap.Spellcheck;

namespace Stellarmap
	{
	public partial class BasicTextEditor : Form
		{
		private const int WORKERTHREAD_END_TIMEOUT = 8000;
		private const int WORKERTHREAD_DELAY = 3000;
		private int MousedWordIndex = 0;
		
		private TypingSpellChecker SpellChecker;
		private List<SpellCheckRange> Ranges;
		private EventHandler EditCallback;		
		private Thread SpellCheckerThread;
		private bool ThreadClosing = false;
		
		private int TextBorder = 12;
		
		
		public BasicTextEditor(EventHandler callback,string data)
			{
			try {
				SpellChecker = new TypingSpellChecker(Globals.WorkspaceSave.AffFile, Globals.WorkspaceSave.DicFile);
				}
			catch(Exception exception)
				{
				MessageBox.Show(exception.Message + "\nCannot enable automatic spell check.");
				}
			
			InitializeComponent();
			CenterToScreen();
            //MessageBox.Show(data);
			
			EditCallback += new EventHandler(callback);

            data = data.Replace("\\\\'","'");
            data = data.Replace("\\'","'");
            
            data = data.Replace("\\\\\"","'");
            data = data.Replace("\\\"","'");

            data = data.Replace("\\n","\n");
           
			this.richTextBox1.Text = data;
			
			//gotta give the text editor focus and set a cursor index in order for
			//the spellchecker to work cirrectly with first right click
			richTextBox1.Focus();
			richTextBox1.Select();

			
			this.labelCharacterSizesChart.Font = richTextBox1.Font;
			
			this.richTextBox1.Location = new Point(TextBorder,this.richTextBox1.Location.Y);
			this.richTextBox1.Width = labelCharacterSizesChart.Width;
			this.richTextBox1.Select(richTextBox1.Text.Length,0);
			this.Width = richTextBox1.Width + (2 * TextBorder);
			
			this.buttonOk.Location = new Point(buttonOk.Location.X, richTextBox1.Location.Y + richTextBox1.Height + (2*TextBorder));
			this.buttonCancel.Location = new Point(buttonCancel.Location.X, buttonOk.Location.Y);
			
			if(SpellChecker != null)
				{
				SpellCheckerThread = new Thread(new ThreadStart(UpdateSpellCheckThread));
				SpellCheckerThread.Start();
				}
			}

		protected override void OnClosing(CancelEventArgs e)
			{
			ThreadClosing = true;
			
			//NOTE: I've decided to remove this block since it shouldn't matter (I think!)
			//      and it's causing an annoying lag when closing the text editor window.
			//SpellCheckerThread.Join(WORKERTHREAD_END_TIMEOUT);
			//if(SpellCheckerThread.IsAlive)
			//    {
			//    SpellCheckerThread.Abort();
			//    }
			if(SpellChecker != null)	{SpellChecker.Dispose();}
			SpellChecker = null;
			
			base.OnClosing(e);
			}
		
		
		private void buttonOk_Click(object sender,EventArgs e)
			{
            string output = this.richTextBox1.Text;
			if(EditCallback != null)
				{
                //transform all newlines into '\n' strings
                output = output.Replace("\n","\\n");
                output = output.Replace("\r","\\n");
                output = output.Replace(Environment.NewLine,"\\n");

                output = output.Replace("\\'","%^TEMPQUOTE%^"); //make sure quotes that are already escaped aren't accendentally given an extra backslash
                output = output.Replace("'","\\'");
                output = output.Replace("%^TEMPQUOTE%^","\\'"); 

                //we want the ability to print double quates so we don't change that
                output = output.Replace("\\\"","\\'");
                //output = output.Replace("\"","\\'");
                //output = output.Replace("%^TEMPDOUBLEQUOTE%^","\\'"); 

                EditCallback(this,new StringEventArgs(output));
				}
			this.Close();
			}
		
		private void buttonCancel_Click(object sender,EventArgs e)
			{
			this.Close();
			}
		
		private void buttonSpellCheck_Click(object sender,EventArgs e)
			{
			HilightEditorText(richTextBox1,SpellChecker);
			return;
			}
		
		private void UpdateSpellCheckThread()
			{
			while(!ThreadClosing)
				{
				richTextBox1.BeginInvoke(new MethodInvoker(delegate {
															HilightEditorText(richTextBox1,SpellChecker);
															}
														)
										);
				Application.DoEvents();
				Thread.Sleep(WORKERTHREAD_DELAY);
				}
			}

		private List<SpellCheckRange> HilightEditorText(Stellarmap.FlickerFreeRichEditTextBox control,TypingSpellChecker checker)
			{
			List<SpellCheckRange> ranges;
			//Hack Alert, this isn't entirely thread safe because I could dispose of the SpellChecker
			// but not yet make the referece invalid
			lock(control)
				{
				if(SpellChecker == null || SpellChecker.DataDisposed) return null;
				if(Control.MouseButtons == MouseButtons.Left || Control.MouseButtons == MouseButtons.Right)
					{
					return null;
					}
				
				bool hadFocus = control.Focused;
				int selStart = control.SelectionStart;
				int selLen = control.SelectionLength;

				richTextBox1.EnablePainting = false;
				ranges = checker.ProofRead(control.Text);
				control.Select(0,control.Text.Length);
				control.SelectionFont = new Font(control.Font,FontStyle.Regular);
				control.SelectionColor = System.Drawing.SystemColors.ControlText;
				
					
				foreach(SpellCheckRange r in ranges)
					{
					control.Select(r.Start,r.End-r.Start);
					control.SelectionFont = new Font(control.Font,FontStyle.Underline);
					control.SelectionColor = System.Drawing.Color.Red;
					}
				
							
				//richTextBox1.Enabled = true;
				if(hadFocus)
					{
					control.Focus();
					control.Select(selStart,selLen);
					}
				control.EnablePainting = true;
				}//end crit section
			
			return ranges;
			}

		
		private void contextRecommendSpelling_Opening(object sender,CancelEventArgs e)
			{
			MousedWordIndex = richTextBox1.SelectionStart;
			this.toolstripRecommendSpelling.DropDownItems.Clear();
			EventHandler SpellingEventHandler = new EventHandler(SpellingRecommendationEventHandler);

			List<SpellCheckRange> ranges = SpellChecker.ProofRead(richTextBox1.Text);

			for(int count = 0; count < ranges.Count; count++)
				{
				if(ranges[count].IsWithinRange(MousedWordIndex))
					{
					foreach(string s in SpellChecker.Recommended(ranges,count))
						{
						this.toolstripRecommendSpelling.DropDownItems.Add(new ToolStripMenuItem(s,null,SpellingEventHandler));
						}
					break;
					}
				}
			}

			
		private void titleSpelling_Click(object sender,EventArgs e)
			{
			
			}

		private void toolstrinpCut_Click(object sender,EventArgs e)
			{
			richTextBox1.Cut();
			}
		
		private void toolstripCopy_Click(object sender,EventArgs e)
			{
			richTextBox1.Copy();
			}

		private void toolstripPaste_Click(object sender,EventArgs e)
			{
			richTextBox1.Paste();
			}

		private void selectAllCtrAToolStripMenuItem_Click(object sender,EventArgs e)
			{
			this.richTextBox1.Select(0,richTextBox1.Text.Length);
			}
		
		private void SpellingRecommendationEventHandler(object sender,EventArgs e)
			{
			//get selected
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			FlickerFreeRichEditTextBox control = richTextBox1;

			//yeah, yeah, I know. We're calling this function a lot. But i really need that list again ;)
			List<SpellCheckRange> ranges = SpellChecker.ProofRead(control.Text);
			MousedWordIndex = control.SelectionStart;

			//select whole word from text box so that it can be replaced with the one needed
			foreach(SpellCheckRange range in ranges)
				{
				if(range.IsWithinRange(MousedWordIndex))
					{
					control.Select(range.Start,range.End - range.Start);
					control.SelectedText = item.Text;
					break;
					}
				}
			}

		}
	}