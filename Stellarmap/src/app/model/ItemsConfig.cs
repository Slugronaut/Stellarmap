using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using System.Windows.Forms;

using Sluggy.Utility;


namespace Stellarmap
	{
	/// <summary>
	/// Parses an XML file that represents various form layouts for
	/// LPC items, weapons, NPCs, etc. 
	/// </summary>
	public class ItemsConfig : Sluggy.Utility.XmlParser
		{
		Dictionary<string,List<string>>	AdditionalLists;
        //List<string> RoomNames;
		//List<string> ItemNames;
		
		public ItemsConfig(string fileName,Dictionary<string,List<string>> additionalLists) : base(fileName)
			{
			if(additionalLists != null)
				{AdditionalLists = additionalLists;}
			else{AdditionalLists = new Dictionary<string,List<string>>();}
			object t = (object)this;
			}
		
		public List<Tag> GetItemClassDecls()
			{
			return FindAll(this.Root,"itemclassdeclaration");
			}
		
		//given a dialog type (based on the menu name) get its class id
		public string GetClassID(string dialogType)
			{
			foreach(Tag t in Root.Children)
				{
				if(t.Name == "itemclassdeclaration" && t.Attributes[dialogType] == dialogType)
					{					
					return t.Value;
					}
				}
			
			return null;
			}
		
		public Tag GetItemClassRoot(string cls)
			{
			List<Tag> tags = FindAll(this.Root,"itemclass");
			if(tags == null)	return null;
			
			foreach(Tag t in tags)
				{				
				if(t.Attributes["class"] == cls)
					{
					return t;
					}
				}			
			return null;
			}

        /*
         * Disables any controls in the generator based on function name. Useful for when certain
         * inherited items don't share a common inheritence chain. (Like meals, they don't really need
         * SetClass() because they aren't supposed to be weapons.)
         */
        public void DisableListedFunctionControls(Form ownerWindow,string itemClass)
            {
            if(this.Root == null || this.Root.Children == null || this.Root.Children.Count < 0)
                {
                MessageBox.Show("There were no elements found in the items config file.");
                }
            Tag root = null;
            foreach(Tag t in this.Root.Children[0].Children)
                {
                if(t.Attributes.ContainsKey("class") && t.Attributes["class"].ToLower() == itemClass.ToLower())
                    {root = t;}
                }
            if(root == null || root.Children == null || root.Children.Count < 1) {return;}

            foreach(Tag t in root.Children)
                {
                if(t.Name == "disable")
                    {
                    
                    //yuck, inner loops. Just loop through every control and child-control
                    //in this window until we find the one with the matching function name. Speed isn't
                    //all that important unless this is going to run on an old Pentium II
                    SearchForControlToDisable((Control)ownerWindow as Control,t.Value);
                    }
                }
            }

        private bool SearchForControlToDisable(Control parent,string functionName)
            {
            if(parent == null || !parent.HasChildren || parent.Controls == null)   
                {return false;}

            foreach(Control con in parent.Controls)
                {
                IFunctionControl funcControl = con as IFunctionControl;
				if(funcControl != null && funcControl.FunctionName == functionName)
                    {
                    con.Enabled = false;
                    return true;
                    }
                else{
                    if(SearchForControlToDisable(con,functionName) == true) {return true;}
                    }
                }
            return false;
            }
		
		public List<Tag> GetItemClassControls(Tag root)
			{			
			List<Tag> controlTags = new List<Tag>();			
			foreach(Tag t in root.Children)
				{
				if(t.Name == "control")
					{
					controlTags.Add(t);
					//List<Tag> childTags = GetItemClassControls(t);
					//if(childTags != null)
					//    {
					//    t.Children = childTags;
					//    }
					}
				}
			return controlTags;
			}
		
		public List<string> GetControlValues(Tag root,string index)
			{
			List<string> values = new List<string>(10);
			string tagName;
			string attribute;
			
			foreach(Tag t in root.Children)
				{
				tagName = t.Name + index;
				
				//create list based on value present			
				if(tagName == "value" && t.Value != null)
					{
					values.Add(t.Value);
					}
				
								
				//use a list from the config file or lists passed in constructor
				if(t.Value != null)
					{
					if(tagName == "list" || tagName == "list2")
						{
						//try additional lists passed to constructor first...
						if(AdditionalLists.ContainsKey(t.Value.ToLower()) )
							{
							List<string> list = AdditionalLists[t.Value.ToLower()];
							if(list != null && list.Count > 0)
								{values.AddRange(list);}
							}
						//...then try global deadsouls lists
						else{
							List<string> l = Stellarmap.DeadSouls.Globals.DefinedLists.GetList(t.Value.ToLower());
							if(l != null)
								{
								values.AddRange(l);
								}
							
							/*//try defined config lists
							Type configType = typeof(Stellarmap.DeadSouls.Globals.DefinedLists);
							System.Reflection.FieldInfo[] fields = configType.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
							
							foreach(System.Reflection.FieldInfo field in fields)
								{
								//loop through all lists in this global class. If we find
								//a list that matches the tag's value we have our variable
								//that we can copy strings from
								if(t.Value.ToLower() == field.Name.ToLower())
									{
									values.AddRange((List<string>)field.GetValue(null));
									}
								}
							 **/
							}//end else
						}//end if
					}
				
				}
			
			return values;
			}
		
		public string GetControlInit(Tag root,string index)
			{
			foreach(Tag t in root.Children)
				{
				string tagName = t.Name + index;
				if(t.Value != null)
					{
					if(tagName == "init" || tagName == "init2")	return t.Value;
					}
				}
			return "";
			}
			
		public List<Control> GenerateControlsList(string cls)
			{
			//loop through all the tags of the given 'itemclass'
			//and use them to produce controls	
			Tag ItemRoot = GetItemClassRoot(cls);
			
			//get all control descriptions
            List<Tag> controlRoots = GetItemClassControls(ItemRoot);
			if(controlRoots == null)	return new List<Control>();
			
			
			List<Control> l = new List<Control>();
			foreach(object obj in CreateControlsList(controlRoots))
				{
				Control c = obj as Control;
				if(c != null) { l.Add(c); }
				}
			return l;
			}
		
		public List<object> CreateControlsList(List<Tag> controlRoots)
			{
			System.Collections.Generic.List<object> controls = new System.Collections.Generic.List<object>(10);
			if(controlRoots == null || controlRoots.Count < 0) return null;
			
			foreach(Tag c in controlRoots)
				{
				object con = CreateControl(c);
				if(con as Control != null)
					{controls.Add((object)con);}
				}
			
			return controls;
			}
		
		public object CreateControl(Tag c)
			{
			Control control = null;
			
			//get the list of initial values that a control might have
			List<string> Values = GetControlValues(c,"");
			List<string> Values2 = GetControlValues(c,"2");
			string Init = GetControlInit(c,"");
			string Init2 = GetControlInit(c,"2");
			string subType = "";
			string displayName = "";
            string header = "";
			FuncParamType paramType = FuncParamType.None;
            EntryType keyType = EntryType.NA;
            EntryType valueType = EntryType.NA;
			bool enabled = true;
			
			//check if this control has paramtype specified. If not,
			//either it doesn't need one or someone forgot it!
			if(c.Attributes.ContainsKey("paramtype"))
				{
				foreach(Stellarmap.FuncParamType check in Stellarmap.FuncParamTypesList.List)
					{
					//yeah, the ToLower() was 'cause I was too lazy to
					//go back and change either the tags or the enums.
					//Bite me >:B
					string attribute = c.Attributes["paramtype"].ToLower();
					string converted = check.ToString().ToLower();
					if(attribute == converted)
						{
						paramType = check;
						break;
						}
					}
				//MessageBox.Show("XMLFile: " + c.Attributes["paramtype"] + "\nResult: " + paramType.ToString().ToLower());
				}
            if(c.Attributes.ContainsKey("keytype"))
				{
				foreach(Stellarmap.EntryType check in Stellarmap.EntryTypesList.List)
					{
					//yeah, the ToLower() was 'cause I was too lazy to
					//go back and change either the tags or the enums.
					//Bite me >:B
					string attribute = c.Attributes["keytype"].ToLower();
					string converted = check.ToString().ToLower();
					if(attribute == converted)
						{
						keyType = check;
						break;
						}
					}
				}
            if(c.Attributes.ContainsKey("valuetype"))
				{
				foreach(Stellarmap.EntryType check in Stellarmap.EntryTypesList.List)
					{
					//yeah, the ToLower() was 'cause I was too lazy to
					//go back and change either the tags or the enums.
					//Bite me >:B
					string attribute = c.Attributes["valuetype"].ToLower();
					string converted = check.ToString().ToLower();
					if(attribute == converted)
						{
						valueType = check;
						break;
						}
					}
				}
			if(c.Attributes.ContainsKey("enable"))
				{
				if(c.Attributes["enable"] == "false")	{enabled = false;}
				}
			if(c.Attributes.ContainsKey("text"))
				{
				displayName = c.Attributes["text"];
				}
			if(c.Attributes.ContainsKey("subtype"))
				{
				subType =  c.Attributes["subtype"];
				}
            if(c.Attributes.ContainsKey("header"))
                {
                header = c.Attributes["header"];
                }
			
			
			try {
				//process each control tag
				#region control creation switch
				switch(c.Attributes["type"].ToLower())
					{
					case "checklist":
						{
						control = new CheckList(displayName,Values.ToArray(),paramType);
						break;
						}
					case "comboselection":
						{						
						control = new ComboSelection(displayName,Values.ToArray(),Init,paramType);
						break;
						}
                    case "comboselectiontypeable":
						{						
						control = new ComboSelectionTypeable(displayName,Values.ToArray(),Init,paramType);
						break;
						}
					case "listbuilder":
						{
						control = new ListBuilder(displayName,paramType);
						break;
						}
					case "mapbuilder":
						{
						control = new MapBuilder(displayName);
						break;
						}
					case "numberentry":
						{
                        decimal temp = 0;
                        NumberEntry numCon;

                        if(Init != "")
                            {
                            temp = Convert.ToDecimal(Init);
                            }
                        control = new NumberEntry(displayName,temp,0,10000);
                        numCon = control as NumberEntry;
                        if(numCon != null)
                            {
                            if(c.Attributes.ContainsKey("min"))
				                {
                                numCon.EntryMinimum = Convert.ToDecimal(c.Attributes["min"]);
                                }
                            if(c.Attributes.ContainsKey("max"))
				                {
                                numCon.EntryMaxmimum = Convert.ToDecimal(c.Attributes["max"]);
                                }
                            if(c.Attributes.ContainsKey("inc"))
				                {
                                numCon.EntryIncrement = Convert.ToDecimal(c.Attributes["inc"]);
                                }
                            if(c.Attributes.ContainsKey("decimalplaces"))
				                {
                                numCon.DecimalPlaces = Convert.ToInt32(c.Attributes["decimalplaces"]);
                                }
                            }
                        break;
						}
					case "textentry":
						{
						control = new TextEntry(displayName,Init,paramType);
						break;
						}
					case "textdump":
						{
						control = new TextDump(displayName,Init,paramType);
						break;
						}
					case "checkbox":
						{
						int val = 0;
						bool temp = false;
						if(Init != "")
							{
							val = Convert.ToInt32(Init);
							if(val >= 1)	temp = true;
							else temp = false;
							//temp = Convert.ToBoolean(Init);
							}
						control = new Stellarmap.Check(displayName,temp,paramType,"");
						break;
						}
					case "combomapbuilder":
						{
						control = new Stellarmap.ComboMapBuilder(displayName,Values.ToArray(),Init,paramType);
						break;
						}
					case "rightcombomapbuilder":
						{
						control = new Stellarmap.RightComboMapBuilder(displayName,Values.ToArray(),Init,paramType);
						break;
						}
					case "doublecombomapbuilder":
						{
						control = new Stellarmap.DoubleComboMap(displayName,Values.ToArray(),Values2.ToArray(),Init,Init2,paramType);
						break;
						}
					case "combolistbuilder":
						{
						control = new Stellarmap.ComboListBuilder(displayName,Values.ToArray(),Init,paramType);
						break;
						}
					case "param":
						{
						break;
						}
					case "multiparam":
						{
						if(c.Children != null)
							{
							List<FunctionControlParam> paramList = new List<FunctionControlParam>();
							foreach(object obj in CreateControlsList(c.Children))
								{
								IFunctionControl funcControl = obj as IFunctionControl;
								if(funcControl != null)
									{
									string init = funcControl.PullEntry();
									if(init.Length < 1 || init == "({})" || init == "([])" || init == "")
										{
										init = null;
										}
									paramList.Add(new FunctionControlParam(funcControl.FunctionName,funcControl.LabelText,
																						  funcControl.FunctionControlType,funcControl.ParameterType,
																						  funcControl.KeyType,funcControl.ValueType,init));
									}
								}
													
							control = (Control)new FlexiFunction(displayName,c.Attributes["function"],(List<FunctionControlParam>)paramList);
							}
						else { break; }
						//MessageBox.Show("Failed to load a dynamic control. No support for 'Multiparam' yet.");

						IFunctionControl func = control as IFunctionControl;
						if(func != null)
							{
							func.FunctionName = c.Attributes["function"];
                            func.Enabled = enabled;
                            func.RequiredHeader = header;
							//TODO: set tool tip here as well
							}
						return control;
						}
					//case "window":
					//    {
					//    break;
					//    }
					case "tab":
						{
						TabControl tab = new TabControl();
						tab.Name = displayName;
						
						//List<Control> subcons = this.CreateControlsList(c.Children);
						List<Control> subcons = new List<Control>();
						foreach(object obj in CreateControlsList(c.Children))
							{
							Control c2 = obj as Control;
							if(c2 != null) { subcons.Add(c2); }
							}
						if(subcons != null)	tab.Controls.AddRange(subcons.ToArray());
						break;
						}
					}
				#endregion
				}//end try
			
			catch(Stellarmap.InvalidFunctionControlParamException e)
				{
				MessageBox.Show("Could not generate dynamic controls. You hosed one of the xml file's ParamTypes.\n" + e.Message);
				return null;
				}
			
			if(control as Control != null)
				{
				IFunctionControl func = control as IFunctionControl;
				if(func != null)
					{
					func.FunctionName = c.Attributes["function"];
					func.Enabled = enabled;
					func.RequiredHeader = header;
                    if(keyType != EntryType.NA)     {func.KeyType = keyType;}
                    if(valueType != EntryType.NA)   {func.ValueType = valueType;}
					//TODO: set tool tip here as well
					}
				
                keyType = EntryType.NA;
                valueType = EntryType.NA;
				Values = null;
				Values2 = null;
				Init = null;
				Init2 = null;
				displayName = "";
				paramType = FuncParamType.None;
				}
			
			return control;
			}
		
		
		
		}
	}
