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
	/// The control layout is desined for large amounts of text.
	/// </summary>
	public partial class TextDump : AFunctionControl
		{
		FuncParamType ParamType = FuncParamType.String;
		
		public TextDump()
			{
			InitializeComponent();
			this.TextEntry.Validated += new EventHandler(PostUpdate);
			}
		
		public TextDump(string groupText,string defaultText,FuncParamType paramType) : this()
			{
			this.ParameterType = paramType;
			this.LabelText = groupText;
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
					throw new Stellarmap.InvalidFunctionControlParamException(this.ToString() + " Control \"" + this.Label.Text + "\" did not receive a valid FuncParamType."); 		
					}
				this.ParamType = value;
				}
			get {
				return this.ParamType;
				}
			}

		public override ControlType FunctionControlType
			{
			get { return ControlType.TextDump; }
			}
		
		public override string LabelText
			{
			set {
				this.Label.Text = value;
				}
			get {
				return this.Label.Text;
				}
			}
		
		public string EntryText
			{
			set	{
				TextEntry.Text = value;
				}
			get {
				return TextEntry.Text;
				}
			}
		#endregion

		#region public interface
		public override string PullEntry()
			{
			return ProcessOutputValue(this.EntryText);
			}
		
		public override void PushEntry(string entry)
			{
			TextEntry.Text = ProcessInputValue(entry);
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
