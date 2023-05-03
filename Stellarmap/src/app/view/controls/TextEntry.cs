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
	/// Represents a basic function call with a single string as a parameter.
	/// </summary>
	public partial class TextEntry : AFunctionControl
		{
		FuncParamType ParamType = FuncParamType.String;
		
		public TextEntry()
			{
			InitializeComponent();
			this.buttonEditWindow.Size = new System.Drawing.Size(12,20 - (System.Windows.Forms.SystemInformation.Border3DSize.Height * 2));
			
			this.Entry.Validated += new EventHandler(PostUpdate);
			}
		
		public TextEntry(string labelText,string defaultText,FuncParamType paramType) : this()
			{			
			this.ParameterType = paramType;
			this.LabelText = labelText;
			this.EntryText = defaultText;
			}
		
		
		#region properties
		public override FuncParamType ParameterType
			{
			set {
				if(value != FuncParamType.String
				&& value != FuncParamType.Number
				&& value != FuncParamType.Raw)
					{
					throw new Stellarmap.InvalidFunctionControlParamException(this.ToString() + " Control \"" + Label.Text + "\" did not receive a valid FuncParamType."); 		
					}
				this.ParamType = value;
				}
			get {
				return this.ParamType;
				}
			}
	
		public override string LabelText
			{
			set {
				this.Label.Text = value;
				
				//position entry text box just after label (it may have been resized for text)
				int tempY = Entry.Location.Y;
				this.Entry.Location = new System.Drawing.Point(this.Label.Location.X + this.Label.Width + (int)Metrics.SpaceAfterLable,tempY);
				this.buttonEditWindow.Location = new System.Drawing.Point(this.Entry.Location.X + this.Entry.Width - this.buttonEditWindow.Width - (int)SystemInformation.Border3DSize.Width,tempY + System.Windows.Forms.SystemInformation.Border3DSize.Height);
				//Resize container control to hold all of the contents
				tempY = this.Height;
				this.Size = new System.Drawing.Size((int)Metrics.SpaceAfterLable + (int)Metrics.CustomControlBorder + this.Label.Location.X + this.Label.Width + this.Entry.Width + (int)Metrics.SpaceAfterLable + (this.buttonEditWindow.Width),tempY);
				
				}
			get {
				return this.Label.Text;
				}
			}

		public override ControlType FunctionControlType
			{
			get { return ControlType.TextEntry; }
			}	
		
		public string EntryText
			{
			get {return this.Entry.Text;}
			set {this.Entry.Text = value;}
			}
		#endregion
		
		
		#region public interface
		/// <summary>
		/// Returns the contents of the control formatted as a single string
		/// so that it can be used in code generation.
		/// </summary>
		public override string PullEntry()
			{
			return ProcessOutputValue(this.EntryText);
			}
		
		/// <summary>
		/// Parses a string into it's component parts for use in the
		/// control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{
			Entry.Text = ProcessInputValue(entry);
			}
		#endregion


		private void buttonEditWindow_Click(object sender,EventArgs e)
			{
			using(BasicTextEditor editor = new BasicTextEditor(OnCallback,this.EntryText))
				{
				editor.ShowDialog(this.Parent);
				}
			}

		private void OnCallback(object sender,EventArgs e)
			{
			StringEventArgs text = (StringEventArgs)e;
			this.EntryText = text.InputString;
			}
		
		}
	}
