using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class ExportItemForm : Form
		{
		private event ExportItemEvent Export;
		
		public ExportItemForm(ItemSaveType defaultType,ExportItemEvent saveEvent)
			{
			InitializeComponent();
			
			//radio defaults to 'Obj'
			if(defaultType == ItemSaveType.Armor)	{this.radioArmor.Checked = true;}
			if(defaultType == ItemSaveType.Weapon)	{this.radioWeap.Checked = true;}
			if(defaultType == ItemSaveType.Door)	{this.radioDoor.Checked = true;}
			if(defaultType == ItemSaveType.NPC)		{this.radioNpc.Checked = true;}
            if(defaultType == ItemSaveType.Meal)    {this.radioMeal.Checked = true;}
			Export = saveEvent;
			}
		
		private void buttonOk_Click(object sender,EventArgs e)
			{
			string fileName = this.textFileName.Text;
			if(fileName == null || fileName.Length < 1)
				{
				MessageBox.Show("Please provide a name for the file to be saved.");
				}
			else{
				ItemSaveType path = ItemSaveType.Object;
				if(this.radioObj.Checked)	{ path = ItemSaveType.Object; }
				if(this.radioWeap.Checked)	{ path = ItemSaveType.Weapon; }
				if(this.radioArmor.Checked) { path = ItemSaveType.Armor; }
				if(this.radioDoor.Checked)	{ path = ItemSaveType.Door; }
				if(this.radioNpc.Checked)	{ path = ItemSaveType.NPC; }
                if(this.radioMeal.Checked)  { path = ItemSaveType.Meal; }
				if(Export != null)	{Export(path,fileName);}
				this.Close();
				return;
				}
			}

		private void buttonCancel_Click(object sender,EventArgs e)
			{
			this.Close();
			}

		
		}
	}