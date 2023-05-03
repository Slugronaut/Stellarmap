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
	/// <summary>
	/// A partially dynamic form used to generate LPC code files. The various
	/// form entries represent function calls that setup behaviors for the all
	/// base object being created.
	/// 
	/// The first few tabs are all standard function calls for all items derived 
	/// from the base object. The last tab, however, is genertaed based on an
	/// XML config file that specifies layout and components. This allows for
	/// derived objects with specific functionality to be added to the editor
	/// without extra code or recompilation.
	/// </summary>
	public partial class CreateItemForm : Form
		{
		#region members
		IDomainModelAdapter refDomain;
		ItemViewManager Manager;
		bool Saved = false;
		#endregion
		
		
		#region constructors
		public CreateItemForm(ItemsConfig ic,string itemClass,IDomainModelAdapter domain)
			{
			InitializeComponent();

#if STELLARMASS
            this.SetWieldLevel.Visible = true;
            this.SetWieldLevel.Enabled = true;
#endif
			CenterToParent();

			//this.SetDamageType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("damageTypes").ToArray();
            this.SetInventory.KeyCollection = domain.Inventory.ToArray();
			this.SetDamageType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("damageTypes").ToArray();
			this.SetWeaponType.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("weaponTypes").ToArray();
			this.SetVendorType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("vendorTypes").ToArray();
			
			
			refDomain = domain;
			this.Manager = new ItemViewManager(this,domain,this.OnFunctionControlUpdate,ItemLoadType.Item);
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
			
			if(control.FunctionName == "inherit LIB_STORAGE;")
				{
				Check box = (Check)sender;
				bool flag = box.Checked;
				this.SetCanClose.Enabled = flag;
				this.SetCanLock.Enabled = flag;
				this.SetClosed.Enabled = flag;
				this.SetLocked.Enabled = flag;
				this.SetMaxCarry.Enabled = flag;
				this.SetKey.Enabled = flag;
                this.SetInventory.Enabled = flag;
                this.SetOpacity.Enabled = flag;

                //set the inherit LIB_ITEM control that was added
                //dynamically when the form was created
                if(this.InheritTab.Controls != null && this.InheritContainer.Controls.Count > 0)
                    {
                    Check itemLib = this.InheritTab.Controls[0] as Check;
                    itemLib.Checked = (!flag);
                    }
				}
			}

		private void exportToolStripMenuItem_Click(object sender,EventArgs e)
			{
			if(!ValidateRequiredInput()) return;
			
			using(ExportItemForm export = new ExportItemForm(ItemSaveType.Object,this.Save))
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

            if(this.SetShort.Enabled)
                {
                string shortDesc = this.SetShort.PullEntry();
                if(shortDesc == null || shortDesc.Length < 1 || shortDesc == "\"\"")
				    {
				    MessageBox.Show("All objects must have a valid short description.");
				    return false;
				    }
                }
			return true;
			}		
		#endregion
		
		private string CheckParams(FunctionCallsCollection callsList,string name)
			{
			if( name.Contains("inherit") && callsList.CallList.ContainsKey(name) )
				{
				if(callsList.CallList[name] == "1") { return "1"; }
				else{return null;}
				}
			else{
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
			
			//is it a gun?
			if(CheckParams(inherits,"inherit LIB_FIREARM;") != null)
				{
				//check firearm damage
				string gunDamage = CheckParams(functions,"SetGunClass");
				if(gunDamage != null) { totalCost += 10 * Convert.ToInt32(gunDamage); }
				
				//reduced melee value
				string meleeDamage = CheckParams(functions,"SetClass");
				if(meleeDamage != null) { totalCost += 2 * Convert.ToInt32(meleeDamage); }
				
				//tally up number of damagetypes for gun
				string damageType = CheckParams(functions,"SetGunDamageType");
				if(damageType != null)
					{
					foreach(string value in ParserTools.StringIntoORList(damageType,agentName) )
						{
						//adds 12 for each type of damage it does
						totalCost += 12;
						}
					}
				
				string hp = CheckParams(functions,"SetDamagePoints");
				if(hp != null) { totalCost += (int)System.Math.Round((double)Convert.ToDouble(hp) * HpValue); }
				
				string accuracy = CheckParams(functions,"SetAccuracy");
				if(accuracy != null) {totalCost -= 5 * (100 - Convert.ToInt32(accuracy)); }
				
				string accuracyDrop = CheckParams(functions,"SetAccuracyDropoff");
				if(accuracyDrop != null) { totalCost -= Convert.ToInt32(accuracyDrop); }
				}
			
			
			
			//else, is it ammo?
			else if(CheckParams(inherits,"inherit LIB_MAGAZINE;") != null)
				{
				string damageType = CheckParams(functions,"SetDamageTypeBonus");
				if(damageType != null)
					{
					foreach(string value in ParserTools.StringIntoORList(damageType,agentName))
						{
						totalCost += 1;
						}
					}

				
				//we assume that if there is more than one for max ammo, that it is a clip
				//and if there is only one, it is a round
				string maxAmmo = CheckParams(functions,"SetMaxAmmo");
				if(maxAmmo != null)
					{
					int ammo = Convert.ToInt32(maxAmmo);
					//round based
					if(ammo == 1)
						{
						totalCost += 1;
						double AccuracyBonusValue = 0.10;
						double ClassBonusValue = 0.10;

						string classBonus = CheckParams(functions,"SetClassBonus");
						if(classBonus != null) { totalCost += (int)System.Math.Floor((double)Convert.ToDouble(classBonus) * ClassBonusValue); }

						string accuracyBonus = CheckParams(functions,"SetAccuracyBonus");
						if(accuracyBonus != null) { totalCost += (int)System.Math.Floor((double)Convert.ToDouble(accuracyBonus) * AccuracyBonusValue); }
				
						}
					//magazine based
					else{
						totalCost += (int)System.Math.Round( (double)ammo * (double)0.4f);
						double AccuracyBonusValue = 0.5;
						double ClassBonusValue = 0.5;

						string classBonus = CheckParams(functions,"SetClassBonus");
						if(classBonus != null) { totalCost += (int)System.Math.Floor((double)Convert.ToDouble(classBonus) * ClassBonusValue); }

						string accuracyBonus = CheckParams(functions,"SetAccuracyBonus");
						if(accuracyBonus != null) { totalCost += (int)System.Math.Floor((double)Convert.ToDouble(accuracyBonus) * AccuracyBonusValue); }
				
						}
					}
				}
			
			
			
			//must be a regular item, maybe a weapon?
			else{
				//otherwise check melee at regular value
				string meleeDamage = CheckParams(functions,"SetClass");
				if(meleeDamage != null)	{ totalCost += 8 * Convert.ToInt32(meleeDamage); }

				//tally up number of damagetypes for gun
				string damageType = CheckParams(functions,"SetDamageType");
				if(damageType != null)
					{
					foreach(string value in ParserTools.StringIntoORList(damageType,agentName))
						{
						//adds 12 for each type of damage it does
						totalCost += 12;
						}
					}
				
				//is it a weapon?
				if(Convert.ToInt32(meleeDamage) > 1)
					{
					string hp = CheckParams(functions,"SetDamagePoints");
					if(hp != null) { totalCost += (int)System.Math.Round((double)Convert.ToDouble(hp) * HpValue); }
					}
				}
			
			
			
			
			
			if(CheckParams(inherits,"inherit LIB_STORAGE;") != null)
				{
				string carry = CheckParams(functions,"SetMaxCarry");

                //divide by ten since weights are in hundreths of a pound
				if(carry != null) { totalCost += 2 * Convert.ToInt32(carry) / 10; }
				}
			
			
            //divide by ten since weights are in hundreths of a pound
			string mass = CheckParams(functions,"SetMass");
			if(mass != null) { totalCost -= Convert.ToInt32(mass) / 10; }
			
			
			
			this.SetBaseCost.EntryValue = totalCost;
			return;
			}

        private void SetDestroyOnSell_Load(object sender, EventArgs e)
            {

            }
		
		}
	}