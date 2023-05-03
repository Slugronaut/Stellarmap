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
	public partial class MapBuilder : AFunctionControl
		{
		Dictionary<string,string> Mapping = new Dictionary<string,string>();
		FuncParamType ParamType = FuncParamType.Mapping;
		
		public MapBuilder()
			{
			InitializeComponent();
			}
		
		public MapBuilder(string groupText) : this()
			{			
			this.Label.Text = groupText;			
			}
		
		
		#region properties
		public override ControlType FunctionControlType
			{
			get { return ControlType.MapBuilder; }
			}
		
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
		
		public string[] ListTextCollection
			{
			set {
				this.EntryList.Items.Clear();
				this.EntryList.Items.AddRange(value);
								
				//crap, looks like we need to manually combine every element :<
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
					
					this.PostUpdate(this,null);
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
				Mapping.Remove(iter.Current);
				
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
			return ControlIntoMap();
			}
		
		/// <summary>
		/// Parses a string into it's component parts for use in the control's native format.
		/// </summary>
		public override void PushEntry(string entry)
			{
            object[] tempCol = new object[EntryList.Items.Count];
            Dictionary<string,string> tempMap = Mapping;
            EntryList.Items.CopyTo(tempCol,0);

            Key.Text = "";
			Value.Text = "";
            EntryList.Items.Clear();
            Mapping.Clear();

            try{
			    Mapping = ParserTools.StringIntoMap(entry,this.FunctionName);			
			    PairsIntoControl(Mapping);
                }
            catch(ParserException e)
                {
                EntryList.Items.AddRange(tempCol);
                throw e;
                }

           
			
			}
		#endregion
		
		
		#region private methods
		private void PairsIntoControl(Dictionary<string,string> list)
			{
			foreach(string str in list.Keys)
				{
				//NOTE: we don't Process*() the text here because we expect it to be in
				//the proper format already
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

		private void button1_Click(object sender,EventArgs e)
			{
			using(BasicTextEditor editor = new BasicTextEditor(OnCallbackKey,this.Key.Text))
				{
				editor.ShowDialog(this.Parent);
				}
			}

		private void OnCallbackKey(object sender,EventArgs e)
			{
			StringEventArgs text = (StringEventArgs)e;
			this.Key.Text = text.InputString;
			}

		private void button2_Click(object sender,EventArgs e)
			{
			using(BasicTextEditor editor = new BasicTextEditor(OnCallbackValue,this.Value.Text))
				{
				editor.ShowDialog(this.Parent);
				}
			}

		private void OnCallbackValue(object sender,EventArgs e)
			{
			StringEventArgs text = (StringEventArgs)e;
			this.Value.Text = text.InputString;
			}
		}
	}
