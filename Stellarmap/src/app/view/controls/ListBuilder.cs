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
	public partial class ListBuilder : AFunctionControl
		{
		List<string> Array = new List<string>();
		FuncParamType ParamType = FuncParamType.Array;
		
		
		public ListBuilder()
			{
			InitializeComponent();
			}
		
		public ListBuilder(string groupText,FuncParamType paramType) : this()
			{			
			this.ParameterType = paramType;
			this.LabelText = groupText;			
			}
		
		
		#region properties
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
		
		public string[] ListTextCollection
			{
			set {
				this.EntryList.Items.Clear();
				this.EntryList.Items.AddRange(value);
				
				Array.AddRange(value);
				}
			get {
				/*
				List<string> values = new List<string>();
				foreach(object obj in this.EntryList.Items)
					{
					values.Add(obj.ToString());					
					}
				return values.ToArray();
				*/
				return this.Array.ToArray();
				}
			}
		#endregion
		
		
		#region event handlers
		private void AddEntry_Click(object sender, EventArgs e)
			{
			//add textbox contents to list
			if(Entry.Text.Length > 0)
				{
				if(this.ParameterType == FuncParamType.ORList)
					{
					//we are going to assume that all data is passed as variable names so there is
					//no need for the preceeding '$'. This also means that if we find signs of
					//non-variable name data (quotes or starting with a number) we should throw an
					//exception.
					//NOTE: Decided to let number literals pass
					if(Entry.Text[0] == '\"')
						{
						throw new FunctionControlException("String literal passed to an ORed list of variables in " + this.FunctionName + ".");			
						}
					if(Entry.Text[0] == Stellarmap.Globals.Generator.FunctionParameterLiteralChar)
						{
						throw new FunctionControlException("Variable signifier '~' not needed in " + this.FunctionName + ".");
						}
					}
				
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
		public override ControlType FunctionControlType
			{
			get { return ControlType.ListBuilder; }
			}
		
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
			//NOTE: we don't Process*() the text here because we expect it to be in
			//the proper format already
			foreach(string str in list)
				{
				if(str != "")
					{
					EntryList.Items.Add((object)str);//ProcessInputValue(str));
					Array.Add(str);
					}
				}
			}
		
		private string ControlIntoArray()
			{
			return ParserTools.ArrayIntoString(this.Array);
			}
		
		private string ControlIntoORList()
			{
			return ParserTools.ORListIntoString(this.Array);
			}
		#endregion	

		private void button1_Click(object sender,EventArgs e)
			{
			using(BasicTextEditor editor = new BasicTextEditor(OnCallback,this.Entry.Text))
				{
				editor.ShowDialog(this.Parent);
				}
			}

		private void OnCallback(object sender,EventArgs e)
			{
			StringEventArgs text = (StringEventArgs)e;
			this.Entry.Text = text.InputString;
			}
		}
	}
