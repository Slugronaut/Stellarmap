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
	/// This control allows a user to contruct a map of elements that use strings for keys
	/// and may contain numbers, strings, or variables for entries.
	/// 
	/// NOTE: The ProcessInput* and ProcessOutput* functions are reversed from normal
	/// due to the nature of the control converting and displaying the format in which
	/// it will be stored rather than what the player types.
	/// </summary>
	public partial class DoubleComboMap : AFunctionControl
		{		
		Dictionary<string,string> Mapping = new Dictionary<string,string>();
		FuncParamType ParamType = FuncParamType.Mapping;
				
		public DoubleComboMap()
			{
			InitializeComponent();
			}
		
		public DoubleComboMap(string defaultLabel,string[] keys,string[] values,string defaultKey,string defaultValue,FuncParamType paramType) : this()
			{
			this.ParameterType = paramType;
			this.LabelText = defaultLabel;
			this.KeyEntry = defaultKey;
			this.ValueEntry = defaultValue;	
			if(values != null)
				{
				//this.Key.Items.AddRange(keys);
				KeyCollection = keys;
				}
			
			if(values != null)
				{
				//this.Value.Items.AddRange(values);
				ValueCollection = values;
				}
			}

		//public DoubleComboMap(string defaultLabel,string[] keys,string defaultText,FuncParamType paramType)
		//    : this(defaultLabel,keys,null,defaultText,paramType)
		//{ }
		
		
		#region properties
		public override FuncParamType ParameterType
			{
			set {
				if(value != FuncParamType.Mapping)
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
		
		public string KeyEntry
			{
			set	{
				this.Key.Text = value;
				}
			get {
				return Key.Text;
				}
			}
		
		public string ValueEntry
			{
			set {
				this.Value.Text = value;				
				}
			get {
				return Value.Text;
				}
			}
				
		public string[] KeyCollection
		    {
		    set {
		        this.Key.Items.Clear();
		        this.Key.Items.AddRange(value);
		        
		        Graphics g = this.Key.CreateGraphics();
		        int maxwidth = this.Key.Width;
		        foreach(string str in value)
					{
		        	maxwidth = Math.Max((int)g.MeasureString(str, this.Key.Font).Width, maxwidth);
					}
				
				this.Key.DropDownWidth = maxwidth + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
				g.Dispose();
				}
		    get {
		        List<string> values = new List<string>();
		        foreach(object obj in this.Key.Items)
		            {
		            values.Add(obj.ToString());					
		            }
		        return values.ToArray();
				}
			}
		
		public string[] ValueCollection
		    {
		    set {
		        this.Value.Items.Clear();
		        this.Value.Items.AddRange(value);
		        
		        Graphics g = this.Value.CreateGraphics();
		        int maxwidth = this.Value.Width;
		        foreach(string str in value)
					{
		        	maxwidth = Math.Max((int)g.MeasureString(str, this.Value.Font).Width, maxwidth);
					}
				
				this.Value.DropDownWidth = maxwidth + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
				g.Dispose();
		        }
		    get {
		        List<string> values = new List<string>();
		        foreach(object obj in this.Value.Items)
		            {
		            values.Add(obj.ToString());					
		            }
		        return values.ToArray();
		        }
		    }
		    
		public string[] ListTextCollection
			{
			set {
				this.EntryList.Items.Clear();
				this.EntryList.Items.AddRange(value);
								
				
				Mapping.Clear();
				foreach(object entry in this.EntryList.Items)
					{
					List<StringBuilder> temp = ParserTools.ParseCode((string)entry,':');
					if(temp.Count < 2)
						{
						throw new FunctionControlException("Error parsing map entries passed to ListCollection property of " + this.FunctionName);
						}
					Mapping.Add(temp[0].ToString(),temp[1].ToString());
					}
				}
			get {
				List<string> values = new List<string>();
				foreach(object obj in this.EntryList.Items)
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
			if(Key.Text.Length > 0 && Value.Text.Length > 0)
				{
				string key = ProcessOutputKey(Key.Text);
				string value = ProcessOutputValue(Value.Text);
				
				if(Mapping.ContainsKey(key))
					{
					MessageBox.Show("This map already contains the key " + key);
					}
				else{
					EntryList.Items.Add((object)key + " : " + (object)value);
					Mapping.Add(key,value);
					Key.Text = "";
					Value.Text = "";
					
					RoomConnectionArgs args = new RoomConnectionArgs(null,value,ProcessInputKey(key),ConnectionType.Connect);
					this.PostUpdate(this,args);
					}
				}
			else{
				Utility.MessageBeep(Utility.MB_OK);
				}
			}
		
		private void RemoveEntry_Click(object sender, EventArgs e)
			{
			if(EntryList.SelectedItem != null)
				{
				string entry = (string)EntryList.SelectedItem;
				Dictionary<string,string> temp = ParserTools.StringIntoMap("([" + entry + "])",this.FunctionName + "Control.RemoveEntry()");
				Dictionary<string,string>.KeyCollection.Enumerator iter = temp.Keys.GetEnumerator();
				iter.MoveNext();				
				RoomConnectionArgs args = new RoomConnectionArgs(null,temp[iter.Current],ProcessInputKey(iter.Current),ConnectionType.Disconnect);
				Mapping.Remove(iter.Current);
				
				EntryList.Items.Remove(EntryList.SelectedItem);
				
				this.PostUpdate(this,args);
				}
			else{
				Utility.MessageBeep(Utility.MB_OK);
				}
			}
		#endregion
		
		
		#region public interface
		public override ControlType FunctionControlType
			{
			get { return ControlType.DoubleComboMap; }
			}
		
		/// <summary>
		/// Returns the contents of the control formated as a single string
		/// so that it can be used in code generation.
		/// </summary>
		public override string PullEntry()
			{
			return ControlIntoMap();
			}
		
		/// <summary>
		/// Parses a string into it's component parts for use in the control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{			
			Key.Text = "";
			Value.Text = "";
			EntryList.Items.Clear();
			Mapping.Clear();
			Mapping = ParserTools.StringIntoMap(entry,this.FunctionName);				
			PairsIntoControl(Mapping);
			}
		
		public void AddToListCollection(string value)
			{
			this.EntryList.Items.Add(value);
			
			List<StringBuilder> temp = ParserTools.ParseCode((string)value,':');
			if(temp.Count < 2)
				{
				throw new FunctionControlException("Error parsing map entries passed to AddListCollection() function of " + this.FunctionName);
				}
			Mapping.Add(temp[0].ToString(),temp[1].ToString());
			}	
		
		public void RemoveKey(string key)
			{
			//string text = ControlIntoMap();
			if(Mapping.ContainsKey(key))
				{
				//remove any values based on this key
				Mapping.Remove(key);
				PairsIntoControl(Mapping);
				}
			}
		
		public string GetValue(string key)
			{
			if(Mapping.ContainsKey(key)) return Mapping[key];
			
			return null;
			}
		
		public void RemoveValueFromList(string value)
			{
			List<string> list = new List<string>();
			
			foreach(string str in ValueCollection)
				{
				if(str != value)
					{list.Add(str);}
				}
			
			this.ValueCollection = list.ToArray();
			}	
		#endregion
		
		
		#region private methods
		private void PairsIntoControl(Dictionary<string,string> list)
			{
			foreach(string str in list.Keys)
				{
				string key = str;//ProcessOutputKey(str);
				string value = list[str];//ProcessOutputValue(list[str]);
				
				EntryList.Items.Add((string)key + " : " + (string)value);
				if(!Mapping.ContainsKey((string)key))
					{ Mapping.Add((string)key,(string)value); }
				//else { Mapping[(string)key] = (string)value; }
				}
			}
		
		private string ControlIntoMap()
			{
			return ParserTools.MapIntoString(Mapping);
			}
		#endregion
		}
	}
