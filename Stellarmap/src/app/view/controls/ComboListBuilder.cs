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
	/// Custom control that represents a function's parameters for LPC code generation.
	/// This control allows one to contruct a list of elements either for an array or a
	/// collection of numerical values that are logically ORed together.
	/// </summary>
	public partial class ComboListBuilder : AFunctionControl
		{
		List<string> Array = new List<string>();
		FuncParamType ParamType = FuncParamType.Array;
		
		public ComboListBuilder()
			{
			InitializeComponent();
			}
		
		public ComboListBuilder(string defaultLabel,string[] elements,string defaultText,FuncParamType paramType) : this()
			{
			this.ParameterType = paramType;
			this.LabelText = defaultLabel;
			this.EntryText = defaultText;			
			if(elements != null)
				{
				this.Entry.Items.AddRange(elements);
				}
			
			}
		
		
		#region properties
		public override ControlType FunctionControlType
			{
			get { return ControlType.ComboListBuilder; }
			}
		
		public override FuncParamType ParameterType
			{			
			set {
				if(value != FuncParamType.ORList
				&& value != FuncParamType.Array)
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
				}
			get {
				return this.Label.Text;
				}
			}
		
		public string EntryText
			{
			set	{
				this.Entry.Text = value;
				}
			get {
				return Entry.Text;
				}
			}
		
		public string[] EntryCollection
			{
			set {
				this.Entry.Items.Clear();
				this.Entry.Items.AddRange(value);
				
				Graphics g = this.Entry.CreateGraphics();
		        int maxwidth = this.Entry.Width;
		        foreach(string str in value)
					{
		        	maxwidth = Math.Max((int)g.MeasureString(str,this.Entry.Font).Width, maxwidth);
					}
				
				this.Entry.DropDownWidth = maxwidth + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
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
		#endregion
		
		
		#region event handlers
		private void AddEntry_Click(object sender, EventArgs e)
			{
			//add textbox contents to list
			if(Entry.Text.Length > 0)
				{
				string entry = ProcessOutputValue(Entry.Text);
				EntryList.Items.Add((object)entry);
				this.Array.Add(entry);
				
				Entry.Text = "";
				this.PostUpdate(this,null);
				}
			else{
				Utility.MessageBeep(Utility.MB_OK);
				}
			}

		private void RemoveEntry_Click(object sender, EventArgs e)
			{
			if(EntryList.SelectedItem != null)
				{
				//SLOW
				Array.RemoveAt(EntryList.SelectedIndex);
				
				EntryList.Items.Remove(EntryList.SelectedItem);
				this.PostUpdate(this,null);
				}
			else{
				Utility.MessageBeep(Utility.MB_OK);
				}
			}
		#endregion
		
		
		#region public interface
		/// <summary>
		/// Returns the contents of the control formated as a single string
		/// so that it can be used in code generation.
		/// </summary>
		public override string PullEntry()
			{
			//are we making an array or an ORed list?
			if(this.ParameterType == FuncParamType.Array)
				{
				return ControlIntoArray();
				}
			else{
				return ControlIntoORList();
				}
			}
		
		/// <summary>
		/// Parses a string into it's component parts for use in the control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{
			EntryList.Items.Clear();
			Entry.Text = "";
			
			if(this.ParameterType == FuncParamType.Array)
				{
				List<string> temp = ParserTools.StringIntoArray(entry,this.FunctionName);				
				ListIntoControl(temp);
				}
			
			if(this.ParameterType == FuncParamType.ORList)
				{
				List<string> temp = ParserTools.StringIntoORList(entry,this.FunctionName);
				ListIntoControl(temp);
				}
			}
		#endregion
		
		
		#region private methods
		private void ListIntoControl(List<string> list)
			{
			foreach(string str in list)
				{
				if(str != "")
					{
					EntryList.Items.Add((object)ProcessInputValue(str));
					}
				}
			}
		
		private string ControlIntoArray()
			{
			string temp = "({";
			//NOTE: we don't Process*() the text here because we expect it to be in
			//the proper format already
			foreach(object entry in this.EntryList.Items)
				{
				temp += (string)entry + ",";//ProcessOutputValue((string)entry) + ",";
				Array.Add((string)entry);
				}
			
			//remove trailing comma
			temp = temp.TrimEnd(',');
			
			temp += "})";
			return temp;
			}
		
		private string ControlIntoORList()
			{
			string temp = "";
			
			//NOTE: we don't Process*() the text here because we expect it to be in
				//the proper format already
			foreach(object entry in this.EntryList.Items)
				{
				temp += (string)entry + "|";//ProcessOutputValue((string)entry) + "|";
				}
			
			//remove trailing OR
			temp = temp.TrimEnd('|');
			
			return temp;
			}		
		#endregion	
		}
	}
