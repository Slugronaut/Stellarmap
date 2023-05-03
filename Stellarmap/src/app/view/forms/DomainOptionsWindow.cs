using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	//HACK ALERT! ACCESSING AND MANIPULATING GLOBALS IN THIS CLASS
	public partial class DomainOptionsWindow : Form
		{
		VirtualDomain refDomainRenderer;
		Dictionary<string,Bitmap> refRoomIcons;
		
		public DomainOptionsWindow()
			{
			InitializeComponent();
			this.RoomImageWidth.Value = Globals.ImageBoxProperties.width;
			this.RoomImageHeight.Value = Globals.ImageBoxProperties.height;
			this.ExitNodeWidth.Value = Globals.NodeProperties.width;
			this.ExitNodeHeight.Value = Globals.NodeProperties.height;
			this.textBaseRoomName.Text = Globals.Model.BaseRoomName;
			this.textStartRoomName.Text = Globals.Model.CustomStartRoomName;
			this.ExitLineWidth.Value = Globals.ImageBoxProperties.ExitLineWidth;
			
			//create list of possible encodings in combo box
			//convert keys of encoding map (which are the names of the encodings) to a list of
			//strings that are then casted to an array of objects and added to the combo box. Yuck
			List<string> encodings = new List<string>(Globals.WorkspaceSave.EncodingMap.Keys);
			this.comboEncoding.Items.AddRange((object[])encodings.ToArray());
			
			//get currently chosen encoding
			this.comboEncoding.Text = Globals.WorkspaceSave.LPCEncoding.WebName;
			
			
			//set currently chosen line ending
			this.comboLineEndings.Text = Globals.WorkspaceSave.LineEndings;
			}
		public DomainOptionsWindow(VirtualDomain vd,ref Dictionary<string,Bitmap> roomIcons) : this()
			{
			this.refDomainRenderer = vd;
			this.refRoomIcons = roomIcons;
			}
	
		
		
		protected override void OnClosing(CancelEventArgs e)
			{
			if(this.DialogResult == DialogResult.OK)
				{
				Globals.ImageBoxProperties.width = (int)this.RoomImageWidth.Value;
				Globals.ImageBoxProperties.height = (int)this.RoomImageHeight.Value;
				Globals.NodeProperties.width = (int)this.ExitNodeWidth.Value;
				Globals.NodeProperties.height = (int)this.ExitNodeHeight.Value;
				Globals.Model.BaseRoomName = this.textBaseRoomName.Text;
				Globals.Model.CustomStartRoomName = this.textStartRoomName.Text;
				Globals.ImageBoxProperties.ExitLineWidth = (int)this.ExitLineWidth.Value;
				
				Globals.NodeProperties.RefreshNodeProperties();
				refDomainRenderer.ResetAllIcons(this.refRoomIcons);
				
				
				//NOTE: We are assuming that since the list was generated with available encoding info,
				//and can't be edited by the user, that this info is still valid and therfore will
				//not choke the encodingmap
				Globals.WorkspaceSave.LPCEncoding = Globals.WorkspaceSave.EncodingMap[this.comboEncoding.Text];
				Globals.WorkspaceSave.LineEndings = this.comboLineEndings.Text;
				
				
				EditConfigFile();
				}
			
			base.OnClosing(e);
			return;
			}
		
		
		private void EditConfigFile()
			{
			using(ProgressModal progress = new ProgressModal(20))
				{
				progress.Show(this);
				
				//yeah, reloading the config file just to mod it and save again. Derp!
				//System.Windows.Forms.
				//SUPER LAME PROGRESS BAR AHOY!
				try{
					Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
					Sluggy.Utility.Tag imageboxes = parser.DepthSeekFirst(null,"imageboxes");
					Sluggy.Utility.Tag nodeboxes = parser.DepthSeekFirst(null,"nodeboxes");
					Sluggy.Utility.Tag baseroom = parser.DepthSeekFirst(null,"baseroom");
					Sluggy.Utility.Tag startroom = parser.DepthSeekFirst(null,"startroom");
					Sluggy.Utility.Tag exitWidth = parser.DepthSeekFirst(null,"exitlinewidth");
					Sluggy.Utility.Tag lineending = parser.DepthSeekFirst(null,"lpclineending");
					Sluggy.Utility.Tag encoding = parser.DepthSeekFirst(null,"lpcencoding");
					progress.UpdateProgressBar();

					if(imageboxes == null || nodeboxes == null || exitWidth == null)
						{
						MessageBox.Show("The config file has been corrupted and cannot be altered. The problem may be solved by deleting the file 'config.xml' in the Stellarmap assets folder.");
						return;
						}
					progress.UpdateProgressBar();

					SetOrCreateAttribute(imageboxes,"width",System.Convert.ToString(Globals.ImageBoxProperties.width),progress);
					SetOrCreateAttribute(imageboxes,"height",System.Convert.ToString(Globals.ImageBoxProperties.height),progress);
					SetOrCreateAttribute(nodeboxes,"width",System.Convert.ToString(Globals.NodeProperties.width),progress);
					SetOrCreateAttribute(nodeboxes,"height",System.Convert.ToString(Globals.NodeProperties.height),progress);
					SetOrCreateAttribute(baseroom,"name",Globals.Model.BaseRoomName,progress);
					SetOrCreateAttribute(startroom,"name",Globals.Model.CustomStartRoomName,progress);
					SetOrCreateAttribute(exitWidth,"width",System.Convert.ToString(Globals.ImageBoxProperties.ExitLineWidth),progress);
					SetOrCreateAttribute(lineending,"type",Globals.WorkspaceSave.LineEndings,progress);
					SetOrCreateAttribute(encoding,"type",Globals.WorkspaceSave.LPCEncoding.WebName,progress);
					
					parser.Save(parser.FilePath);

					//TODO: need an option to save Parsed XML data in parser!!!
					}//end try
				catch(Sluggy.Utility.XMLParserException exc)
					{
					MessageBox.Show(exc.Message);
					}
				}//end progress modal
			
			return;
			}
		
		private void SetOrCreateAttribute(Sluggy.Utility.Tag tag,string attributeName,string stringizedValue,ProgressModal progress)
			{
			//TODO: add tag if it does not exist
			
			if(tag != null && tag.Attributes != null)
				{
				if(!tag.Attributes.ContainsKey(attributeName))
					{tag.Attributes.Add(attributeName,stringizedValue);}
				else{tag.Attributes[attributeName] = stringizedValue;}
				}
			
			if(progress != null) progress.UpdateProgressBar();
			}

		private void Ok_Click(object sender,EventArgs e)
			{
			DialogResult = DialogResult.OK;
			}

		private void Cancel_Click(object sender,EventArgs e)
			{

			}

		}
	}