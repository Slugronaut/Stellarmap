using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sluggy.Utility;
using System.Runtime.InteropServices;


namespace Stellarmap
	{
	/// <summary>
	/// 
	/// </summary>
	public partial class CreateArmorForm : Form
		{
		#region members
		IDomainModelAdapter refDomain;
		ItemViewManager Manager;
		bool Saved = false;
		#endregion
		
		
		#region constructors
		public CreateArmorForm(ItemsConfig ic,string itemClass,IDomainModelAdapter domain)
			{
			InitializeComponent();

#if STELLARMASS
            this.SetWearLevel.Visible = true;
            this.SetWearLevel.Enabled = true;
#endif

			this.SetVendorType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("vendorTypes").ToArray();
			this.SetRestrictLimbs.EntryCollection = DeadSouls.Globals.DefinedLists.GetList("limbs").ToArray();
			this.SetProtections.KeyCollection = DeadSouls.Globals.DefinedLists.GetList("damageTypes").ToArray();
			this.SetArmorType.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("armorTypes").ToArray();
			
			CenterToParent();
			refDomain = domain;
			this.Manager = new ItemViewManager(this,domain,this.OnFunctionControlUpdate,ItemLoadType.Armor);
            DynamicControlsSetup(ic,itemClass);
            ic.DisableListedFunctionControls(this,itemClass);
			}
		#endregion
		
		
		#region event handlers
		protected override void OnClosing(CancelEventArgs e)
			{
			if(!Saved)
				{
				if(MessageBox.Show("You have made changes to this item that have not been saved. If you exit now they will be lost. Are you sure you want to close this editor?","Warning: Changes Not Saved",MessageBoxButtons.YesNo) == DialogResult.No)
					{
					e.Cancel = true;
					}
				}

			base.OnClosing(e);
			}

		/// <summary>
		/// Receives input from function controls (after being wired up through the ItemViewManager)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void OnFunctionControlUpdate(object sender,EventArgs args)
			{
			Saved = false;
			IFunctionControl control = (IFunctionControl)sender;

			}

		private void exportToolStripMenuItem_Click(object sender,EventArgs e)
			{
			if(!ValidateRequiredInput())	return;
			
			using(ExportItemForm export = new ExportItemForm(ItemSaveType.Armor,this.Save))
				{
				export.ShowDialog(this);
				}
			}	
		#endregion
		
		
		#region private
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
			string id = this.SetId.PullEntry();
			string shortDesc = this.SetShort.PullEntry();
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
			if(shortDesc == null || shortDesc.Length < 1 || shortDesc == "\"\"")
				{
				MessageBox.Show("All objects must have a valid short description.");
				return false;
				}
			
			return true;
			}	
		#endregion	
		
		private string CheckParams(FunctionCallsCollection callsList,string name)
			{
			if(name.Contains("inherit") && callsList.CallList.ContainsKey(name))
				{
				if(callsList.CallList[name] == "1") { return "1"; }
				else { return null; }
				}
			else
				{
				if(callsList.CallList.ContainsKey(name))
					{
					return callsList.CallList[name];
					}
				}

			return null;
			}

		private void CalculateCost_Click(object sender,EventArgs e)
			{
			string agentName = "CreateItemForm->CalculatCost_Click";
			List<IFunctionControl> functionControls = this.Manager.CompileFunctionControlsList(this);
			FunctionCallsCollection functions = this.Manager.CompileFunctioCallsList(functionControls);
			FunctionCallsCollection inherits = this.Manager.CompileIheritList(functionControls);
			double HpValue = 0.15;


			int totalCost = 0;
			
			string protections = CheckParams(functions,"SetProtections");
			if(protections != null)
				{
				foreach(string v in ParserTools.StringIntoMap(protections,agentName).Values)
					{
					totalCost += 8 * Convert.ToInt32(v);
					}
				}
			
			//if total cost is already over 8 (due to armor costs) we know this is an armor so at that
			//point we will start charging for the armor's hp as well
			if(totalCost > 8)
				{
				string hp = CheckParams(functions,"SetDamagePoints");
				if(hp != null) { totalCost += (int)System.Math.Round((double)Convert.ToDouble(hp) * HpValue); }
				}

			if(CheckParams(inherits,"inherit LIB_WORN_STORAGE;") != null)
				{
				string carry = CheckParams(functions,"SetMaxCarry");
                //divide by ten since weights are in hundreths of a pound
				if(carry != null) { totalCost += 3 * Convert.ToInt32(carry) / 10; }
				}

            //divide by ten since weights are in hundreths of a pound
			string mass = CheckParams(functions,"SetMass");
			if(mass != null) { totalCost -= Convert.ToInt32(mass) / 10; }
			
			
			if(CheckParams(inherits,"inherit LIB_SPACESUIT;") != null)
				{
				totalCost += 5000;
				}
			
			if(CheckParams(inherits,"inherit LIB_ZURAKIGLOVES;") != null)
				{
				totalCost += 250;
				}
			
			
			this.SetBaseCost.EntryValue = totalCost;
			
			return;
			}
		
		}
	}