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
	/// User control that represents a series of ORed values or an array literal.
	/// Only elements that are ticked are pulled.
	/// </summary>
	public partial class RadioList : AFunctionControl
		{
		//NOTE: This will probably be needed later or to properly preserve
		//      that actual values sotred so that we don't have just true/false
		//      from the checkbox. It will probably need to be a dictionary though
		Dictionary<string,string> Mapping = new Dictionary<string,string>();
		FuncParamType ParamType = FuncParamType.Array;
		bool DoNotRefire = false;
		
		
		public RadioList()
			{
			InitializeComponent();
			//this.CheckedList.CheckOnClick = true;
			
			//none of the data is typed by the user but we still need to know how
			//to process strings for a map-key. For maps, we always assume the 
			//values are 1 or 0 here. And sice we aren't allowing the user to type
			//anything we are going to assume the key is a string (the control's
			//LabelText to be exact).
			if(ParamType == FuncParamType.Mapping)
				{
				this.KeyType = EntryType.Strings;
				this.ValueType = EntryType.NonStrings;
				}
			
			//OR-list are going to assume that data is always code identifiers
			if(ParamType == FuncParamType.ORList)
				{
				this.KeyType = EntryType.NonStrings;
				this.ValueType = EntryType.NonStrings;
				}
			
			//Arrays could be either as usual :P  The settings can always be overriden
			//in code for special cases.
			if(ParamType == FuncParamType.Array)
				{
				this.KeyType = EntryType.Mixed;
				this.ValueType = EntryType.Mixed;
				}
			
			}
		
		public RadioList(string groupText,string[] elements,FuncParamType paramType) : this()
			{			
			this.LabelText = groupText;
			this.ParameterType = paramType;
			
			if(elements != null)
				{
				this.CheckedList.Items.AddRange(elements);
				}	
			
			}
		
				
		
		#region properties
		public override ControlType FunctionControlType
			{
			get { return ControlType.CheckList; }
			}
		
		public override FuncParamType ParameterType
			{
			set {
				if(value != FuncParamType.Properties
				&& value != FuncParamType.ORList
				&& value != FuncParamType.Array
				&& value != FuncParamType.Mapping
				&& value != FuncParamType.Inherit)
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
		
		public string[] CheckListStrings
			{
			set {
				this.CheckedList.Items.Clear();
				this.CheckedList.Items.AddRange(value);
				
				
				Mapping.Clear();
				foreach(object entry in this.CheckedList.Items)
					{
					//we process the text to ensure entires are stored
					//with LPC code formatting
					Mapping.Add(ProcessOutputKey((string)entry),"0");
					int i = 0;
					}
				}
			get {
				List<string> values = new List<string>();
				foreach(object obj in this.CheckedList.Items)
					{
					values.Add((string)obj.ToString() );					
					}
				return values.ToArray();
				}
			}
		#endregion
		
		
		#region event handlers
		private void CheckedList_ItemChecked(object sender, EventArgs e)
			{
			if(!DoNotRefire)
				{
				DoNotRefire = true;
				
				ItemCheckEventArgs args = (ItemCheckEventArgs)e;
				string name = (string)this.CheckedList.Items[args.Index];
								
				if(args.NewValue == CheckState.Checked)
					{
					Mapping[name] = "1";
					}
				else{
					Mapping[name] = "0";
					}
				
				PostUpdate(this,null);
				}
			
			DoNotRefire = false;
			}

		private void CheckedList_SelectedIndexChanged(object sender, EventArgs e)
			{
			}
		#endregion
		
		
		#region public interface
		public override string PullEntry()
			{
			switch(this.ParamType)
				{
				case FuncParamType.Array: {return ControlIntoArray();}
				case FuncParamType.ORList: {return ControlIntoORList();}
				case FuncParamType.Mapping: {return ControlIntoMap();}
				}
			
			return "";
			}
		
		public override void PushEntry(string entry)
			{
			List<string> list;
			
			switch(this.ParamType)
				{
				case FuncParamType.Array:
					{
					list = ParserTools.StringIntoArray(entry,this.FunctionName);
					ListIntoControl(list);
					break;
					}
				case FuncParamType.ORList:
					{
					list = ParserTools.StringIntoORList(entry,this.FunctionName);
					ListIntoControl(list);
					break;
					}
				case FuncParamType.Mapping:
					{
					Dictionary<string,string> pairs = ParserTools.StringIntoMap(entry,this.FunctionName);
					PairsIntoControl(pairs);
					break;
					}
				}
			}
		#endregion
		
		
		#region private methods
		private void ListIntoControl(List<string> list)
			{
			//Dictionary<string,string>.KeyCollection.Enumerator iter = Mapping.Keys.GetEnumerator();
			
			//now check the ones that apply
			for(int index = 0; index < this.CheckedList.Items.Count; index++)
				{
				bool flag = false;
				string val = "0";
				string element = "";
				//iter.MoveNext();
				
				//yuck, inner loops :P
				foreach(string str in list)
					{
					//since we are listing collections of elements, we can check the
					//box solely based on the presence of the string
					flag = false;
					if((string)this.CheckedList.Items[index] == str)
						{
						flag = true;
						val = "1";
						element = str;
						break;
						}
					}
				
				this.CheckedList.SetSelected(index,flag);//SetItemChecked(index,flag);
				//Mapping[iter.Current] = val;
				if(element != null && element.Length > 0 && Mapping.ContainsKey(element))
					{
					Mapping[element] = val;
					}
				}
			
			}
		
		private void PairsIntoControl(Dictionary<string,string> pairs)
			{
			//loop through all checkboxes and check/uncheck appropriately
			//for the value passed in the pair (WARNING WILL ROBINSON! We
			//are assuming sanitized data by this point so the values had
			//better be numbers and the keys better be strings!!! )
			//Also note that all values will be converted to 1 or 0!!
			for(int index = 0; index < this.CheckedList.Items.Count; index++)
				{
				bool flag = false;
				string val = "0";
				
				
				foreach(string str in pairs.Keys)
					{
					flag = false;
					if( (string)this.CheckedList.Items[index] == str)
			            {
			            //ok I lied, the value doesn't *have* to be a number, but still...
			            try {
							if(Convert.ToInt32(pairs[str]) != 0)
								{
								flag = true;
								val = "1";
								break;
								}
							}
						catch(System.FormatException e)
							{
							throw new FunctionControlException("Invalid map value for entry \"" + str + " : " + pairs[str] + "\" passed to " + this.FunctionName + ". It should be a boolean value.");
							}
						}
					}
				
				this.CheckedList.SetSelected(index,flag);//SetItemChecked(index,flag);
				//Mapping[(string)this.CheckedList.Items[index]] = val;
				Mapping[(string)this.CheckedList.Items[index]] = val;
				}
			}
		
		private string ControlIntoArray()
			{
			List<string> array = new List<string>();
			
			foreach(string key in Mapping.Keys)
				{
				if(Mapping[key] == "1")
					{
					array.Add(key);
					}
				}
			
			return ParserTools.ArrayIntoString(array);
			}
		
		private string ControlIntoORList()
			{
			List<string> array = new List<string>();
			
			foreach(string key in Mapping.Keys)
				{
				if(Mapping[key] == "1")
					{
					array.Add(key);
					}
				}
			
			return ParserTools.ORListIntoString(array);
			}
		
		private string ControlIntoMap()
			{
			return ParserTools.MapIntoString(Mapping);
			}	
		#endregion	
		}
	}
