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
	/// User control that represents either a string or number literal.
	/// Default values are provided but custom ones may still be entered
	/// in the text box.
	/// </summary>
	public partial class ComboSelectionTypeable : AFunctionControl
		{
		FuncParamType ParamType = FuncParamType.String;
		
		public ComboSelectionTypeable()
			{
			InitializeComponent();
			this.Entry.TextChanged += new EventHandler(PostUpdate);
			}
		
		public ComboSelectionTypeable(string lableText,string[] values,string defaultText,FuncParamType paramType) : this()
			{			
			this.ParameterType = paramType;
			this.LabelText = lableText;
			this.EntryText = defaultText;			
			
			if(values != null)
				{
				//this.Entry.Items.AddRange(values);
				ListTextCollection = values;
				}
			}
		
		
		#region properties
		public override ControlType FunctionControlType
			{
			get { return ControlType.ComboSelection; }
			}
		
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
			
				//Resize container control to hold all of the contents
				tempY = this.Height;
				this.Size = new System.Drawing.Size((int)Metrics.SpaceAfterLable + (int)Metrics.CustomControlBorder + this.Label.Location.X + this.Label.Width + this.Entry.Width,tempY);
				}
			get {
				return this.Label.Text;
				}
			}
		
		public new string EntryText
			{
			set	{
				NoPostback = true;
				Entry.Text = value;
				NoPostback = false;
				}
			get {
				return Entry.Text;
				}
			}
		
		public string[] ListTextCollection
			{
			set {
				this.Entry.Items.Clear();
				this.Entry.Items.AddRange(value);
				
				Graphics g = this.Entry.CreateGraphics();
		        int maxwidth = this.Entry.Width;
		        foreach(string str in value)
					{
		        	maxwidth = Math.Max((int)g.MeasureString(str, this.Entry.Font).Width, maxwidth);
					}
				
				this.Entry.DropDownWidth = maxwidth  + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
				g.Dispose();
				}
			get {
				List<string> values = new List<string>();
				foreach(object obj in this.Entry.Items)
					{
					values.Add(obj.ToString());					
					}
				return values.ToArray();
				}
			}
		
		public int SelectedIndex
			{
			get {return this.Entry.SelectedIndex;}
			set {
				if(value < this.Entry.Items.Count && value >= -1)
					{
					this.Entry.SelectedIndex = value;
					}
				}
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
		/// Parses a string into it's component parts for use in the control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{
			NoPostback = true;
			//if(entry.Length < 0)
			//    {
			//    this.SelectedIndex = -1;
			//    Entry.Text
			//    NoPostback = false;
			//    return;
			//    }
			//Entry.Text = ProcessInputValue(entry);
			int index = 0;
			Entry.SelectedIndex = -1;
			foreach(string item in Entry.Items)
				{
				if(this.ValueType == EntryType.Strings)
					{
					if(entry == "\"" + item + "\"")
						{
						Entry.SelectedIndex = index;
						break;
						}
					}
				if(this.ValueType == EntryType.Mixed)
					{
					if(entry == "\"" + item + "\"" || entry == Globals.Generator.FunctionParameterLiteral + item)
						{
						Entry.SelectedIndex = index;
						break;
						}
					}
				if(this.ValueType == EntryType.NonStrings)
					{
					if(entry == item)
						{
						Entry.SelectedIndex = index;
						break;
						}
					}
				
				index++;
				}
			NoPostback = false;
			}
		#endregion
		}
	}
