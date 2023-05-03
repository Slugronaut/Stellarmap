using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sluggy.Utility;

namespace Stellarmap
	{
	internal enum FunctionCallId
		{
		SetSkill,
		SetStat,
		SetAction,
		SetCombatAction,
		AddLanguagePoints,
		}
	
	public partial class CreateNpcForm : Form
		{
		#region members
		ItemViewManager Manager;
		IDomainModelAdapter refDomain;
		bool Saved = false;
		Dictionary<string,FlexiFunction> StatFunctions = new Dictionary<string,FlexiFunction>();
		Dictionary<string,FlexiFunction> SkillFunctions = new Dictionary<string,FlexiFunction>();
		Dictionary<string,FlexiFunction> LanguageFunctions = new Dictionary<string,FlexiFunction>();
		Dictionary<int,FlexiFunction> ActionFunctions = new Dictionary<int,FlexiFunction>();
		Dictionary<int,FlexiFunction> CombatFunctions = new Dictionary<int,FlexiFunction>();
		int ActionCount = 0;
		int CombatActionCount = 0;
		#endregion



		public CreateNpcForm(ItemsConfig ic,string itemClass,IDomainModelAdapter domain)
			{
			InitializeComponent();
			
			//this.SetNativeLanguage.SelectedIndex = 0;
			this.comboStatRemove.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboSkillRemove.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboStatAdd.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboSkillAdd.DropDownStyle = ComboBoxStyle.DropDownList;

			this.SetInventory.KeyCollection = domain.Inventory.ToArray();
			this.SetRace.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("races").ToArray();
			this.SetClass.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("classes").ToArray();
			this.SetGender.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("genders").ToArray();
			this.SetCurrency.KeyCollection = DeadSouls.Globals.DefinedLists.GetList("currencies").ToArray();
			this.SetDefaultLanguage.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("languages").ToArray();
			this.SetNativeLanguage.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("languages").ToArray();
			this.comboSkillAdd.Items.AddRange((object[])DeadSouls.Globals.DefinedLists.GetList("skills").ToArray());
			this.comboStatAdd.Items.AddRange((object[])DeadSouls.Globals.DefinedLists.GetList("stats").ToArray());
			this.comboLanguageAdd.Items.AddRange((object[])DeadSouls.Globals.DefinedLists.GetList("languages").ToArray());
			this.SetItemDropRates.KeyCollection = domain.Inventory.ToArray();
			
			#if STELLARMASS
			this.groupInventoryDrops.Visible = true;
			this.SetItemDropRates.Enabled = true;
			this.SetDropInventoryOnDeath.Enabled = true;
			#endif
			
			CenterToParent();

			refDomain = domain;
			this.Manager = new ItemViewManager(this,domain,this.OnFunctionControlUpdate,ItemLoadType.Npc);
			DynamicControlsSetup(ic,itemClass);
            ic.DisableListedFunctionControls(this,itemClass);

            AddAction(FunctionCallId.SetAction,"SetAction",this.flowActions);
            AddAction(FunctionCallId.SetCombatAction,"SetCombatAction",this.flowCombatActions);
			}
		
		
		#region event handlers
		protected override void OnClosing(CancelEventArgs e)
			{
			if(!Saved)
				{
				if(MessageBox.Show("You have made changes to this door that have not been saved. If you exit now they will be lost. Are you sure you want to close this editor?","Warning: Changes Not Saved",MessageBoxButtons.YesNo) == DialogResult.No)
					{
					e.Cancel = true;
					}
				}

			base.OnClosing(e);
			}

		private void OnFunctionControlUpdate(object sender,EventArgs args)
			{
			Saved = false;
			}
		
		private void buttonAddNewStat_Click(object sender,EventArgs e)
			{
            AddStatOrSkillFunctionCall(FunctionCallId.SetStat,"SetStat",this.comboStatAdd.Text,this.flowStats,this.comboStatRemove);
			this.comboStatRemove.Text = "";
			}

		private void buttonRemoveStat_Click(object sender,EventArgs e)
			{
			//(string)this.comboSideList.SelectedItem;
			RemoveStatOrSkillFunctionCall(FunctionCallId.SetStat,(string)this.comboStatRemove.SelectedItem,this.flowStats,this.comboStatRemove);
			this.comboStatRemove.Text = "";
			}

		private void buttonAddNewSkill_Click(object sender,EventArgs e)
			{
			AddStatOrSkillFunctionCall(FunctionCallId.SetSkill,"SetSkill",this.comboSkillAdd.Text,this.flowSkills,this.comboSkillRemove);
			this.comboSkillRemove.Text = "";
			}

		private void buttonRemoveSkill_Click(object sender,EventArgs e)
			{
			RemoveStatOrSkillFunctionCall(FunctionCallId.SetSkill,(string)this.comboSkillRemove.SelectedItem,this.flowSkills,this.comboSkillRemove);
			this.comboSkillRemove.Text = "";
			}

		private void buttonAddNewAction_Click(object sender,EventArgs e)
			{
			//AddAction(FunctionCallId.SetAction,"SetAction",this.flowActions);
			}

		private void buttonRemoveLastAction_Click(object sender,EventArgs e)
			{
			//RemoveAction(FunctionCallId.SetAction,this.flowActions);
			}

		private void buttonAddNewCombatAction_Click(object sender,EventArgs e)
			{
			//AddAction(FunctionCallId.SetCombatAction,"SetCombatAction",this.flowCombatActions);
			}

		private void buttonRemoveLastCombatAction_Click(object sender,EventArgs e)
			{
			//RemoveAction(FunctionCallId.SetCombatAction,this.flowCombatActions);
			}

		private void buttonAddLanguage_Click(object sender,EventArgs e)
			{
			AddStatOrSkillFunctionCall(FunctionCallId.AddLanguagePoints,"AddLanguagePoints",this.comboLanguageAdd.Text,this.flowLanguages,this.comboLanguageRemove);
			this.comboLanguageRemove.Text = "";
			}

		private void buttonRemoveLanguage_Click(object sender,EventArgs e)
			{
			RemoveStatOrSkillFunctionCall(FunctionCallId.AddLanguagePoints,(string)this.comboLanguageRemove.SelectedItem,this.flowLanguages,this.comboLanguageRemove);
			this.comboLanguageRemove.Text = "";
			}

		private void exportToolStripMenuItem_Click(object sender,EventArgs e)
			{
			if(!ValidateRequiredInput()) return;

			using(ExportItemForm export = new ExportItemForm(ItemSaveType.NPC,this.Save))
				{
				export.ShowDialog(this);
				}
			}
		#endregion
		
		
		#region private
		private void AddStatOrSkillFunctionCall(FunctionCallId funcid,string functionName,string firstParam,FlowLayoutPanel target,ComboBox listaddition)
			{
            firstParam = firstParam.Trim();
            if(firstParam == null || firstParam.Length < 1 || firstParam == string.Empty)	{return;}
			
			Dictionary<string,FlexiFunction> functions = new Dictionary<string,FlexiFunction>();
			string funcText = "";
			
			
			#region verify firstParam
			switch(funcid)
				{
				case FunctionCallId.SetStat:
					{
					if(StatFunctions.ContainsKey(firstParam))
						{
						MessageBox.Show("The name '" + firstParam + "' is already in use.");
						return;
						}
					functions = this.StatFunctions;
					funcText = "Stat";
					break;
					}
				case FunctionCallId.SetSkill:
					{
					if(SkillFunctions.ContainsKey(firstParam))
						{
						MessageBox.Show("The name '" + firstParam + "' is already in use.");
						return;
						}
					functions = this.SkillFunctions;
					funcText = "Skill";
					break;
					}
				case FunctionCallId.AddLanguagePoints:
					{
					if(LanguageFunctions.ContainsKey(firstParam))
						{
						MessageBox.Show("The name '" + firstParam + "' is already in use.");
						return;
						}
					functions = this.LanguageFunctions;
					funcText = "Language";
					break;
					}
				}
			#endregion
			
			
			Saved = false;

			//setup the multi-paramed function call
			FunctionControlParam setNameParam = new FunctionControlParam("name","Name:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setValueParam = new FunctionControlParam("value","Value:",ControlType.NumberEntry,FuncParamType.Number,EntryType.NonStrings,EntryType.NonStrings);
			FlexiFunction setSkillOrStat = new FlexiFunction(functionName + Globals.Generator.FunctionHashSymbol + firstParam,funcText,new FunctionControlParam[] { setNameParam,setValueParam });
			setSkillOrStat.Parameters[0].PushEntry(firstParam);
			setSkillOrStat.Parameters[0].Enabled = false;
			
			
			//when all is said and done, place controls into proper layouts
			functions.Add(firstParam,setSkillOrStat);
			target.Controls.Add(setSkillOrStat);
			if(listaddition != null)
				{listaddition.Items.Add(firstParam);}
			
			return;
			}
		
		private void RemoveStatOrSkillFunctionCall(FunctionCallId funcid,string firstParam,FlowLayoutPanel target,ComboBox listsubtraction)
			{
			if(firstParam == null || firstParam.Length < 1)	{return;}

			Dictionary<string,FlexiFunction> functions = new Dictionary<string,FlexiFunction>();
			string funcText = "";


			#region verify firstParam
			switch(funcid)
				{
				case FunctionCallId.SetStat:
					{
					if(!StatFunctions.ContainsKey(firstParam))
						{return;}
					
					functions = this.StatFunctions;
					funcText = "Stat";
					break;
					}
				case FunctionCallId.SetSkill:
					{
					if(!SkillFunctions.ContainsKey(firstParam))
						{return;}
					
					functions = this.SkillFunctions;
					funcText = "Skill";
					break;
					}
				case FunctionCallId.AddLanguagePoints:
					{
					if(!LanguageFunctions.ContainsKey(firstParam))
						{ return; }

					functions = this.LanguageFunctions;
					funcText = "Language";
					break;
					}
					
				}
			#endregion
			
			Saved = false;

			target.Controls.Remove(functions[firstParam]);
			functions.Remove(firstParam);
			listsubtraction.Items.Remove(firstParam);
			}

		private void AddAction(FunctionCallId funcid,string functionName,FlowLayoutPanel target)
			{
			string funcText = "";
			int count = 0;
			
			
			#region verify firstParam
			switch(funcid)
				{
				case FunctionCallId.SetAction:
						{
						ActionCount++;
						count = ActionCount;
						funcText = "Actions";
						break;
						}
				case FunctionCallId.SetCombatAction:
						{
						CombatActionCount++;
						count = CombatActionCount;
						funcText = "Combat Actions";
						break;
						}
				}
			#endregion


			Saved = false;
			
			//setup the multi-paramed function call
			FunctionControlParam setChanceParam = new FunctionControlParam("chance","Chance / Heartbeat:",ControlType.NumberEntry,FuncParamType.Number,EntryType.NonStrings,EntryType.NonStrings);
			FunctionControlParam setResultParam = new FunctionControlParam("value","Value:",ControlType.ListBuilder,FuncParamType.Array,EntryType.Mixed,EntryType.Mixed);
			//removed the instance number for the funtion: FlexiFunction setSkillOrStat = new FlexiFunction(functionName + Globals.Generator.FunctionHashSymbol + Convert.ToString(count),funcText,new FunctionControlParam[] { setChanceParam,setResultParam });
			FlexiFunction setSkillOrStat = new FlexiFunction(functionName,funcText,new FunctionControlParam[] { setChanceParam,setResultParam });
			
			target.Controls.Add(setSkillOrStat);
			return;
			}
		
		private void RemoveAction(FunctionCallId funcid,FlowLayoutPanel target)
			{
			if(target.Controls == null || target.Controls.Count < 1)	{ return; }
			
			#region verify firstParam
			switch(funcid)
				{
				case FunctionCallId.SetAction:
						{
						break;
						}
				case FunctionCallId.SetSkill:
						{
						break;
						}
				}
			#endregion

			Saved = false;
			
			//HACK ALERT! I am assuming that the last control was the last one added as well.
			//This could very well change dynamically. DO SOME RESERACH!
			target.Controls.RemoveAt(target.Controls.Count-1);
			}
		
		

		/// <summary>
		/// Creates dynamic controls based on config file. The controls are applied to
		/// the parent item creation form passed to this object at construction.
		/// </summary>
		/// <param name="ic"></param>
		/// <param name="itemClass"></param>
		private void DynamicControlsSetup(ItemsConfig ic,string itemClass)
			{
			List<Control> InheritTabControls = new List<Control>(10);
			Positioner post = new Positioner();

			List<Tag> itemDecls = ic.GetItemClassDecls();
			foreach(Tag t in itemDecls)
				{
				if(t.Attributes["id"] == itemClass)
					{
					InheritTabControls = ic.GenerateControlsList(t.Value);
					}
				}

			//initialize all of the loaded custom controls
			Control controlRef = null;
			foreach(Control control in InheritTabControls)
				{
				post.AddControlBelow(this.InheritTab,controlRef,control);
				controlRef = control;
				}
			controlRef = null;
			}
		
		private bool Save(ItemSaveType path,string fileName)
			{
			if(fileName == null || fileName.Length < 1)
				{
				MessageBox.Show("Please provide a name for the file to be saved.");
				return false;
				}

			Saved = Manager.GenerateFile(path,fileName);
			if(Saved && refDomain != null)
				{
				//StringBuilder name = new StringBuilder("$" + refDomain.IncludeFile.Door + " \"/" + FileSaveName + Globals.Model.RoomExtension + "\"");
				refDomain.AddInventory(fileName,path);
				refDomain.TriggerCurrentViewControlsUpdate();
				}
			return true;
			}
		
		private bool ValidateRequiredInput()
			{
            if(this.SetId.Enabled)
                {
                string id = this.SetId.PullEntry();
			    if(id == null || id.Length < 1 || id == "({})")
				    {
				    MessageBox.Show("All objects must have at least on valid id.");
				    return false;
				    }
			    if(id.ToLower() != id)
				    {
				    MessageBox.Show("All id's must be in lower case letters.");
				    return false;
				    }
                }

            if(this.SetKeyName.Enabled)
			    {
                string keyName = this.SetKeyName.PullEntry();
                if(keyName == null || keyName.Length < 1 || keyName == "\"\"")
				    {
				    MessageBox.Show("All npcs must have a valid key name.");
				    return false;
				    }
                }

            if(this.SetRace.Enabled)
                {
                string race = this.SetRace.PullEntry();
			    if(race == null || race.Length < 1 || race == "\"\"")
				    {
				    MessageBox.Show("All npcs must have a race.");
				    return false;
				    }
                }
			#if STELLARMASS
            if(this.SetClass.Enabled)
                {
                string cls = this.SetClass.PullEntry();
			    if(cls == null || cls.Length < 1 || cls == "\"\"")
				    {
				    MessageBox.Show("All npcs must have a class.");
				    return false;
				    }
                }
			#endif
            if(this.SetNativeLanguage.Enabled)
                {
                string native = this.SetNativeLanguage.PullEntry();
			    if(native == null || native.Length < 1 || native == "\"\"")
				    {
				    MessageBox.Show("All npcs must have a native language.");
				    return false;
				    }
                }

            if(this.SetDefaultLanguage.Enabled)
                {
                string defaultlan = this.SetDefaultLanguage.PullEntry();
			    if(defaultlan == null || defaultlan.Length < 1 || defaultlan == "\"\"")
				    {
				    MessageBox.Show("All npcs must have a default lanuage.");
				    return false;
				    }
                }
			return true;
			}
		#endregion

		
		
		}
	}