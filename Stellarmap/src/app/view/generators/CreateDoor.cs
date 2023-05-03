using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class CreateDoor : Form
		{
		#region members
		ItemViewManager Manager;
		IDomainModelAdapter refDomain;
		bool Saved = false;
		Dictionary<string,List<FlexiFunction>> SideFunctions = new Dictionary<string,List<FlexiFunction>>();
		#endregion
		
		public CreateDoor(IDomainModelAdapter domain)
			{
			InitializeComponent();
			refDomain = domain;
			CenterToParent();
			this.Manager = new ItemViewManager(this,domain,this.OnFunctionControlUpdate,ItemLoadType.Door);
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

		private void exportToolStripMenuItem_Click(object sender,EventArgs e)
			{
			if(!ValidateRequiredInput()) return;

			using(ExportItemForm export = new ExportItemForm(ItemSaveType.Door,this.Save))
				{
				export.ShowDialog(this);
				}
			}
		
		/// <summary>
		/// Adds FlexiFunction controls for every function in door.c that uses a side name parameter.
		/// A list of all sides is kept so that they can be easily referenced later if they need
		/// to be removed all at once.
		/// 
		/// NOTE: The parameter for the side is always disabled. This is so the user doesn't screw with
		/// the consistency. This control will be enabled during saving so that the model doesn't skip it.
		/// (It skips disabled controls)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddSide_Click(object sender,EventArgs e)
			{
			string sideName = textNewSideName.Text;
            
			if(sideName == null || sideName.Length < 1)
				{
				MessageBox.Show("Please provide a name for the side you wish to add to this door.");
				return;
				}
			
			if(SideFunctions.ContainsKey(sideName))
				{
				MessageBox.Show("The name '" + sideName + "' is already in use.");
				return;
				}
			
			List<FlexiFunction> temp = new List<FlexiFunction>();
			Saved = false;
			
			//setup SetId()
			string func = "This Direction Id";
			FunctionControlParam setIdSideParam = new FunctionControlParam("side","Dir:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setIdNameParam = new FunctionControlParam("id","Id:",ControlType.ListBuilder,FuncParamType.Array,EntryType.Strings,EntryType.Strings);
			FlexiFunction setId = new FlexiFunction("SetId" + Globals.Generator.FunctionHashSymbol + sideName,func,new FunctionControlParam[] {setIdSideParam,setIdNameParam});
			setId.Parameters[0].PushEntry(sideName);
			setId.Parameters[0].Enabled = false;
			
			StringBuilder s = new StringBuilder("({");
			s.Append("\"a door leading " + sideName + "\",\"" + sideName + " door\",\"door\"})");
			setId.Parameters[1].PushEntry(s.ToString());
			temp.Add(setId);
			
			
			//setup SetShort()
			func = "This Direction Short Desc";
			FunctionControlParam setShortSideParam = new FunctionControlParam("side","Dir:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setShortNameParam = new FunctionControlParam("short","Short:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FlexiFunction setShort = new FlexiFunction("SetShort" + Globals.Generator.FunctionHashSymbol + sideName,func,new FunctionControlParam[] { setShortSideParam,setShortNameParam });
			setShort.Parameters[0].PushEntry(sideName);
			setShort.Parameters[0].Enabled = false;
            setShort.Parameters[1].PushEntry("\"a door leading " + sideName + "\"");
			temp.Add(setShort);
			
			
			//SetLong()
			func = "This Direction Long Desc";
			FunctionControlParam setLongSideParam = new FunctionControlParam("side","Dir:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setLongNameParam = new FunctionControlParam("long","Long:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FlexiFunction setLong = new FlexiFunction("SetLong" + Globals.Generator.FunctionHashSymbol + sideName,func,new FunctionControlParam[] { setLongSideParam,setLongNameParam });
			setLong.Parameters[0].PushEntry(sideName);
			setLong.Parameters[0].Enabled = false;
            setLong.Parameters[1].PushEntry("\"A door leading " + sideName + ".\"");
			temp.Add(setLong);
			
			
			//SetLockable()
			func = "This Direction Lockable";
			FunctionControlParam setLockableSideParam = new FunctionControlParam("side","Dir:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setLockableNameParam = new FunctionControlParam("lockable","Lockable:",ControlType.CheckBox,FuncParamType.Property,EntryType.NonStrings,EntryType.NonStrings);
			FlexiFunction setLockable = new FlexiFunction("SetLockable" + Globals.Generator.FunctionHashSymbol + sideName,func,new FunctionControlParam[] { setLockableSideParam,setLockableNameParam });
			setLockable.Parameters[0].PushEntry(sideName);
			setLockable.Parameters[0].Enabled = false;
			temp.Add(setLockable);
			
			
			//SetKeys()
			func = "This Direction Acceptable Keys";
			FunctionControlParam setKeysSideParam = new FunctionControlParam("side","Dir:",ControlType.TextEntry,FuncParamType.String,EntryType.Strings,EntryType.Strings);
			FunctionControlParam setKeysNameParam = new FunctionControlParam("keys","Key Names:",ControlType.ListBuilder,FuncParamType.Array,EntryType.Strings,EntryType.Strings);
			FlexiFunction setKeys = new FlexiFunction("SetKeys" + Globals.Generator.FunctionHashSymbol + sideName,func,new FunctionControlParam[] { setKeysSideParam,setKeysNameParam });
			setKeys.Parameters[0].PushEntry(sideName);
			setKeys.Parameters[0].Enabled = false;
			temp.Add(setKeys);
			
			
			
			
			
			//when all is said and done, place controls into proper layouts
			SideFunctions.Add(sideName,temp);
			this.comboSideList.Items.Add(sideName);
			
			this.flowSetId.Controls.Add(setId);
			this.flowSetShort.Controls.Add(setShort);
			this.flowSetLong.Controls.Add(setLong);
			this.flowSetLockable.Controls.Add(setLockable);
			this.flowSetKeys.Controls.Add(setKeys);
			}
		
		private void buttonRemoveLastSide_Click(object sender,EventArgs e)
			{
			string sideName = (string)this.comboSideList.SelectedItem;
			if(sideName == null || sideName.Length < 1)
				{
				MessageBox.Show("Please provide a name for the side you wish to remove from this door.");
				return;
				}
			if(!SideFunctions.ContainsKey(sideName))
				{
				MessageBox.Show("This door does not have a side with that name.");
				return;
				}
			Saved = false;
			
			//HACK ALERT!!! we are assuming the indicies of the array in SideFunctions[sideName]
			//correspond to the layout controls they were added to
			this.flowSetId.Controls.Remove(SideFunctions[sideName][0]);
			this.flowSetShort.Controls.Remove(SideFunctions[sideName][1]);
			this.flowSetLong.Controls.Remove(SideFunctions[sideName][2]);
			this.flowSetLockable.Controls.Remove(SideFunctions[sideName][3]);
			this.flowSetKeys.Controls.Remove(SideFunctions[sideName][4]);
			
			SideFunctions.Remove(sideName);
			this.comboSideList.Items.Remove(sideName);
			
			}
		#endregion
		
		
		#region private
		private bool Save(ItemSaveType path,string fileName)
			{
			if(fileName == null || fileName.Length < 1)
				{
				MessageBox.Show("Please provide a name for the file to be saved.");
				return false;
				}
			
			Manager.DeleteFunctionsInCode();
			Manager.AddLine("\n::create();\n");
			Saved = Manager.GenerateFile(path,fileName);
			if(Saved && refDomain != null)
				{
				//StringBuilder name = new StringBuilder("$" + ChoosePath(path) + " \"/" + fileName + Globals.Model.RoomExtension + "\"");
				//refDomain.AddDoor(name.ToString(),path);
				//refDomain.TriggerCurrentViewControlsUpdate();
				refDomain.AddDoor(fileName,path);
				refDomain.TriggerCurrentViewControlsUpdate();
				}
			return true;
			}
		
		private bool ValidateRequiredInput()
			{
			return true;
			}
		#endregion

		
		}
	}