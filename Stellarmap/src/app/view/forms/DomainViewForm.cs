using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using Stellarmap.Properties;

namespace Stellarmap
	{
	/// <summary>
	/// The main window for Domain room layout and room editing.
	/// </summary>
	public partial class DomainViewForm : Form, IDomainView
		{		
		#region members
		
		//External References
		IDomainModelAdapter refMVCAdapter; //interface to the model through the controller
		public event AdapterUpdateSendEvent UpdateModelEvent;
		List<IFunctionControl> refFunctionControls; //ok, I lied. This isn't external. But it *is* related!
		string CurrentlySelectedRoom = "";
		RoomEditorState CurrentRoomEditorState = new RoomEditorState(false);
		
		//GUI Elements
		readonly VirtualDomain DomainRenderer;
		readonly ControlRenderDevice RenderDevice;
		Rectangle SelectionRect = new Rectangle();
		Point DragLineStart = new Point();
		Point DragLineEnd = new Point();
		bool SelectionRectActive = false;
		bool DragLineActive = false;
		Dictionary<string,Bitmap> RoomIcons = new Dictionary<string,Bitmap>();
		int MinRoomTabHeight;
				
		//Workspace State
		public static class WorkSpace
			{
			public static DomainViewForm master;
			public static bool Saved = true;
			public static bool Loading = false;
			public static int RoomNameCount = 0;
			public static bool Resetting = false;
			
			public static List<string> InvenoryNames;
			public static List<string> RoomNames;
			
			
			
			public static void EnableWorkspaceControls(bool flag)
				{
				//master.domainSettingsToolStripMenuItem.Enabled = flag;
				//master.menuOptions.Enabled = flag;
				master.menuEdit.Enabled = flag;
				master.menuFileSave.Enabled = flag;
				//master.
				}
			
			public static void ResetBegin()
				{
				Resetting = true;
				Saved = false;
				RoomNameCount = 0;
				}
			
			public static void ResetEnd()
				{
				Resetting = false;
				}
			
			public static bool SavedWarning()
				{
				if(!Saved)
					{
					if(MessageBox.Show("Updates have been made that have not been saved.\nContinuing with this operation will cause all updates to be lost.","Updates Not Saved",MessageBoxButtons.OKCancel) == DialogResult.OK)
						{return true;}
					else{return false;}
					}
				return true;
				}
			
			public static bool SaveWorkSpaceInfo(string domainRoot)
				{
				StringBuilder str = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
				str.Append("\n<workspace roomcount=\"" + RoomNameCount + "\">\n");
				str.Append("</workspace>\n");

				System.IO.Directory.SetCurrentDirectory(domainRoot);
				using(System.IO.StreamWriter stream = new System.IO.StreamWriter("workspacedat.xml"))
					{
					stream.Write(str);
					stream.Flush();
					}
				
				return true;
				}
			
			public static bool LoadWorkSpaceInfo(string domainRoot)
				{
				string warning = "Attempting to load and modify the domain without this file could cause corruption of the domain or crashing of Stellapmap. Do you wish to continue anyway?";
				string masterFile = domainRoot + "\\" + "workspacedat.xml";
				if(!System.IO.File.Exists(masterFile))
					{
					if(MessageBox.Show("No domain workspace file found. " + warning,"Could not locate workspace file.",MessageBoxButtons.YesNo) == DialogResult.No)
						{return false;}
					else{return true;}
					}
				
				Sluggy.Utility.XmlParser xml;
				try{
					bool validFile = false;
					xml = new Sluggy.Utility.XmlParser(masterFile);
					
					//check to make sure there is actually contents in this file
					if(xml.Contents != null && xml.Contents.Children != null && xml.Contents.Children.Count > 0)
						{
						//gret! now, do we have a workspace tag?
						Sluggy.Utility.Tag mainTag = xml.Contents.Children[0];
						
						if(mainTag.Name == "workspace")
							{
							//how about the room count?
							if(mainTag.Attributes.ContainsKey("roomcount"))
								{
								RoomNameCount = System.Convert.ToInt32(mainTag.Attributes["roomcount"]);
								validFile = true;
								}
							}
						}
					if(!validFile)
						{
						if(MessageBox.Show("Important information was missing from the workspace file. " + warning,"Missing workspace information",MessageBoxButtons.YesNo) == DialogResult.No)
							{
							return false;
							}
						else { return true; }
						}
					}
				catch(Sluggy.Utility.XMLParserException exception)
					{
					if(MessageBox.Show("An error occured while trying to parse the workspace xml file.\n\n" + exception.Message + "\n\n" + warning,"Failed to load xml file properly",MessageBoxButtons.YesNo) == DialogResult.No)
						{
						return false;
						}
					else { return true; }
					}
				return true;
				}	
			}
		
		
		//Mouse States
		GUIMouseStateMachine MouseState;
		MouseButtons MouseButton = MouseButtons.None;		
		Point MousePos = new Point();		
		Point ClickPoint = new Point();
		Point DragEnd = new Point();
		Point Offset = new Point();
		
		//other states
		bool DisableViewTriggerFeedback = false; //used to stop certain model updates from feeding back to the view
		bool DisableModelTriggerFeedback = false; //same as above but in reverse
		#endregion
		
		
		#region constructor
		public DomainViewForm()
			{
			InitializeComponent();

#if STELLARMASS
            this.PropertiesTab.Controls.Add(this.SetTracksAllowed);
            this.PropertiesTab.Controls.Add(this.SetBlockNPCs);
#endif
			
			//make sure rendertarget is at least big enough to not cause a crash
			RenderTarget.Size = new Size(10,10);
			this.MinRoomTabHeight = this.RoomTabPanel.Height;
			
			//INIT
			DomainRenderer = new VirtualDomain(this.RenderTarget);
			RenderTarget.Initialize(DomainRenderer,this.PaintFrame);
			RenderDevice = new ControlRenderDevice(this.RenderTarget);
			MouseState = new GUIMouseStateMachine(DomainRenderer,RenderTarget,RenderTarget);
			DomainRenderer.RoomSelectEvent += new EventHandler<StringEventArgs>(OnSelectRoom);
			WorkSpace.master = this;
			WorkSpace.EnableWorkspaceControls(false);
			
			
			//EVENTS
			this.KeyDown += new KeyEventHandler(OnInputDown);
			this.KeyUp += new KeyEventHandler(OnInputUp);
			this.Activated += new EventHandler(OnActivated);
			this.Deactivate += new EventHandler(OnDeactivated);
			this.RenderTarget.Paint += new PaintEventHandler(PaintFrame);			
			this.RenderTarget.MouseDown += new MouseEventHandler(this.eventMouseDown);
            this.RenderTarget.MouseUp += new MouseEventHandler(this.eventMouseUp);
            this.RenderTarget.MouseMove += new MouseEventHandler(this.eventMouseMove);
            this.RenderTarget.MouseWheel += new MouseEventHandler(this.eventMouseWheel);
			this.Resize += new EventHandler(OnResizeWindow);
			this.SizeChanged += new EventHandler(OnSizeChanged);
			this.OnResizeWindow(this,null); //fire the window size setup for the first time
			this.Closing += new CancelEventHandler(MainForm_Closing);
			Application.Idle += new EventHandler(OnIdle);
			this.KeyPreview = true;
						
			this.RoomIconComboBox.Validating += new CancelEventHandler(OnRoomIconChange);
			
			//gui statemachine
			this.MouseState.WorkspaceContextMenu += new EventHandler(WorkSpaceContextOpen);
			this.MouseState.RoomContextMenu += new EventHandler(RoomContextOpen);
			this.MouseState.ActivateSelectionRect += new EventHandler(ActivateSelectionRect);
			this.MouseState.DeactivateSelectionRect += new EventHandler(DeactivateSelectionRect);
			this.MouseState.SetConnection += new EventHandler(OnConnectRoom);
			this.MouseState.ActivateDragLine += new EventHandler(ActivateDragLine);
			this.MouseState.DeactivateDragLine += new EventHandler(DeactivateDragLine);
			this.MouseState.RemoveConnection += new EventHandler(OnDisconnectRoom);
			
			//DYNAMIC MENU
			GenerateItemCreationMenu();
			GenerateArmorCreationMenu();
			GenerateNpcCreationMenu();

            ////group the room radio buttons
            //foreach (Control control in this.RoomType.Controls)
            //    {
            //    //RadioButton b;
            //    //b.Check
            //    Radio radioButton = control as Radio;
            //    if (radioButton != null)
            //        {
            //        radioButton.SubscribeCheckedChanged(new EventHandler(customRadioButton_CheckedChanged) );
            //        }
            //    }
			
			//prep and cache
			refFunctionControls = this.CompileFunctionControlsList(this.RoomTabPanel);
			LoadIconsFromConfig();
			SaveForm(this.RoomTabPanel);

			//setup controls that use lists from the config file
			this.checkList1.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("roomproperties").ToArray();
			this.SetTerrainType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("terraintypes").ToArray();
			this.SetMedium.ListTextCollection = DeadSouls.Globals.DefinedLists.GetList("mediums").ToArray();
            this.SetRespirationType.CheckListStrings = DeadSouls.Globals.DefinedLists.GetList("respirationtypes").ToArray();
			//this.SetMedium.SelectedIndex = 0;

            this.Activate();
			}
		#endregion
		
		
		#region public view interface
		/// <summary>
		/// Allows the controller to subscribe to this object. When the view is updated
		/// by the user (buttons, drags, typing, etc) it will publish an update
		/// that propgates to the model through the controller. Only one controller
		/// may be subscribed at a time.
		/// </summary>
		public bool SetSubscriber(IDomainModelAdapter adapter)
			{
			//we can only have one model attached to this view at any time
			System.Diagnostics.Debug.Assert(adapter != null);
			if(refMVCAdapter != null) return false;
			
			refMVCAdapter = adapter;			
			this.UpdateModelEvent += new AdapterUpdateSendEvent(adapter.UpdateModel);
			return true;
			}
		
		/// <summary>
		/// Removes a subscriber only if has already been subscribed.
		/// </summary>		
		public bool RemoveSubscriber(IDomainModelAdapter adapter)
			{
			System.Diagnostics.Debug.Assert(adapter != null);
			if(refMVCAdapter == adapter)
				{
				this.UpdateModelEvent -= new AdapterUpdateSendEvent(adapter.UpdateModel);
				refMVCAdapter = null;
				return true;
				}
			
			return false;
			}
		
		/// <summary>
		/// The method through which the controller can make a particular model's
		/// data visible. Hooked upon subscription to the controller.
		/// </summary>
		public void DisplayModel(string roomName,RoomUpdateInfo info)
			{
			if(roomName.Length < 1 || roomName == null)	return;
			
			//loop through all controls in the form looking for the ones
			//named after the functions passed. update them with the
			//provided paramters. (The controls will do the translation from
			//strings to actual form data)
			foreach(IFunctionControl control in refFunctionControls)
				{				
				//WARNING! I'M NOT CONFIRMING THAT THE ENTRY EXISTS
				//IN THE COLLECTION VERY WELL HERE
				
				//SURE ENOUGH: 6 months later I have a bug. I removed empty parametered functions 
				//during save but now there is no default info to fill in with when we reload. So
				//I have to set default values here. Here is where that handy little 'Tag' property I added
				//just for such a purpose comes into play ;D
				if(info.Functions == null)	break;
				
				if(info.Functions.CallList.ContainsKey(control.FunctionName))
					{
					try {
						control.PushEntry(info.Functions.CallList[control.FunctionName]);
						}
					catch(FunctionControlException e)
						{
						MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
						}
					catch(ParserException e)
						{
						MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
						}
					}
				else{
					control.PushEntry((string)control.Tag);
					}
				}
			
			this.CurrentRoomEditorState = info.EditorStates;
			
			
			string rawName = this.refMVCAdapter.GetRoom(roomName).RawFileName;
			this.FileName.Text = rawName;
			this.textboxDomainName.Text = roomName.Replace(" \"/" + rawName + "\"","");
			this.RoomIconComboBox.Text = DomainRenderer.GetIconDesc(roomName);

            		
			
			//set the rest manually   yuck XP 
			this.EnableDayNight.Checked = info.EditorStates.UseDayNight;
			//this.InheritRestaurant.Checked = info.EditorStates.Restaurant;			

			//HACK ALERT
			//need to make sure the list box for mediums stays empty when
			//switch between a room that has a value set and one that doesn't
			
			//this was fixed with the update to the ComboSelection function control's PushEntry()
			RoomModel room = this.refMVCAdapter.GetRoom(CurrentlySelectedRoom);
			if(room != null)
				{
				if(!room.FunctionCalls.CallList.ContainsKey("SetMedium") || room.FunctionCalls.CallList["SetMedium"].Length < 1)
					{
					//this.SetMedium.SelectedIndex = -1;
					//this.SetMedium.EntryText = "";
					}
				}
			
			SetupDummyItemLists();			
			}
		
		/// <summary>
		/// Interface through which a controller can obtain view state in order to
		/// update the model with it.
		/// </summary>
		/// <param name="?"></param>
		public RoomUpdateInfo PropogateViewToModel(string roomFileName)
			{
			RoomUpdateInfo info = new RoomUpdateInfo();
			info.Functions = CompileFunctionCallsList(this.refFunctionControls);			
			info.EditorStates = this.CurrentRoomEditorState;	
			return info;
			}
		
		public string GetCurrentModelViewed()
			{
			return this.CurrentlySelectedRoom;
			}
		
		public void TriggerCurrentViewControlsUpdate()
			{
			this.SetupDummyItemLists();
			}
		
		#endregion
		
		
		#region windows event handlers
		private void OnIdle(object sender,EventArgs args)
			{
			RenderUpdate();
			System.Threading.Thread.Sleep(1);
			}
		
		private void OnContextClose(object sender,ToolStripDropDownClosedEventArgs e)
			{			
			this.RenderTarget.ContextMenuStrip.Closed -= new ToolStripDropDownClosedEventHandler(OnContextClose);
			
			//this.RenderTargetControl.ContextMenuStrip.Dispose();
			//this.RenderTargetControl.ContextMenuStrip = null;
			MouseState.SignalContextMenuClose();			
			}
						
		private void WorkSpaceContextOpen(object sender,EventArgs e)
			{
			if(refMVCAdapter.Initialized)
				{				
				this.RenderTarget.ContextMenuStrip = this.WorkAreaContextMenu;
							
				//this is the only way the mouse statemachine can know when to return to the normal
				//state after a context menu is opened
				this.RenderTarget.ContextMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(OnContextClose);			
				}
			else{
				MouseState.SignalContextMenuClose();
				}
			}
		
		private void RoomContextOpen(object sender,EventArgs e)
			{
			this.RenderTarget.ContextMenuStrip = this.RoomContextMenu;
			
			//this is the only way the mouse statemachine can know when to return to the normal
			//state after a context menu is opened
			this.RenderTarget.ContextMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(OnContextClose);
			}
		
		void MainForm_Closing(object sender, CancelEventArgs e)
			{
			if(!WorkSpace.SavedWarning())
				{
				e.Cancel = true;
				return;
				}
			
			//save config file one last time
			EditConfigFile();
			}
		
		private void PaintFrame(object sender,PaintEventArgs e)
			{
			RenderUpdate();
			}
		
		private void OnActivated(object sender,EventArgs args)
			{
			RenderUpdate();
			}
		
		private void OnDeactivated(object sender,EventArgs args)
			{
			this.MouseState.ResetStateMachine();
			}	
		
		private void OnResizeWindow(object sender, EventArgs e)
			{
			Control window = (Control)sender;
			
			//make sure the room controls are always located at the edge of the window
			//this.RoomTabPanel.Location = new Point((window.ClientRectangle.X + window.ClientRectangle.Width) - (this.RoomTabPanel.Width + (int)Metrics.RoomControlPanelMarginX),
			//										window.ClientRectangle.Y + (int)Metrics.RoomControlPanelMarginY);

			this.RoomTabPanel.Location = new Point(-this.HorizontalScroll.Value + window.ClientRectangle.Width - this.RoomTabPanel.Width - (int)Metrics.RoomControlPanelMarginX,
												   -this.VerticalScroll.Value + (int)Metrics.RoomControlPanelMarginY);
			
			this.RenderTarget.Location = new Point(-this.HorizontalScroll.Value + (int)Metrics.RoomLayoutLeftMargin,
												   -this.VerticalScroll.Value + (int)Metrics.RoomLayoutTopMargin);
			ResizeView((window.ClientRectangle.Width - this.RoomTabPanel.Width), window.ClientRectangle.Height);		
			
			//move the roomname box
			int border = 4;
			this.FileName.Location = new Point((RoomTabPanel.Location.X - FileName.Width - 10), (RoomTabPanel.Location.Y));
			this.textboxDomainName.Location = new Point(FileName.Location.X - textboxDomainName.Width - border, this.FileName.Location.Y);
			this.lable_filename.Location = new Point(textboxDomainName.Location.X - lable_filename.Width - border,(RoomTabPanel.Location.Y + (FileName.Height / 4)));
			
			}

		private void OnSizeChanged(object sender,EventArgs args)
			{
			if(this.WindowState == FormWindowState.Maximized)
				{
				ResizeView((this.ClientRectangle.Width - this.RoomTabPanel.Width), this.ClientRectangle.Height);		
				}
			this.RoomTabPanel.Refresh();
			}		
		
		private void OnSelectRoom(object sender,StringEventArgs args)
			{
			if (!this.refMVCAdapter.Initialized) return;
			
			//If there was a previously selcted room, trigger its data to be
			//sent to the model before the view is overwriten by the newly
			//selected room
			TriggerModelUpdate(CurrentlySelectedRoom);
			
			if(args.InputString != null && args.InputString != "")
				{
				this.RoomTabPanel.Enabled = true;
				this.CurrentlySelectedRoom = args.InputString;
				this.refMVCAdapter.TriggerViewUpdate(args.InputString);				
				}
			else{
				this.RoomTabPanel.Enabled = false;
				}

			}	
		
		private void EnableDayNight_CheckedChanged(object sender, EventArgs e)
			{
			System.Windows.Forms.CheckBox box = (System.Windows.Forms.CheckBox)sender;
			
			this.CurrentRoomEditorState.UseDayNight = box.Checked;
			
			//box was checked
			if(box.Checked)
				{
				//only outdoor area can have day/night descriptions
				if(this.SetClimate.EntryText == "indoors")
					{
					MessageBox.Show("Indoor areas do not have day/night cylces.");
					box.CheckState = CheckState.Unchecked;
					return;
					}
				
				this.SetDayLong.Enabled = true;
				this.SetNightLong.Enabled = true;
                this.SetDayLight.Enabled = true;
                this.SetNightLight.Enabled = true;
				}
			
			//box was unchecked
			if(!box.Checked)
				{
				this.SetDayLong.Enabled = false;
				this.SetNightLong.Enabled = false;
                this.SetDayLight.Enabled = false;
                this.SetNightLight.Enabled = false;
				}

			WorkSpace.Saved = false;
			}	
				
		private void RoomIcon_SelectedIndexChanged(object sender, EventArgs e)
			{
			ComboBox combo = (ComboBox)sender;
			
			if(combo.Text == "default")
				{
				DomainRenderer.SetImageSource(CurrentlySelectedRoom,Globals.ImageBoxProperties.DefaultImage,combo.Text);
				return;
				}	
			if(RoomIcons.ContainsKey(combo.Text))
				{
				DomainRenderer.SetImageSource(CurrentlySelectedRoom,RoomIcons[combo.Text],combo.Text);
				}
			
			
			}
		
		private void OnRoomIconChange(object sender, CancelEventArgs e)
			{
			ComboBox combo = (ComboBox)sender;
			
			if(combo.Text != "default" && !RoomIcons.ContainsKey(combo.Text))
				{
				MessageBox.Show("That icon is not available.");
				combo.Text = this.DomainRenderer.GetIconDesc(CurrentlySelectedRoom);
				e.Cancel = true;
				}

			WorkSpace.Saved = false;
			}
		#endregion
		
		
		#region menu events
		private void menuFileQuit_Click(object sender, EventArgs e)
			{
			this.Close();
			}
		
		private void menuFileNew_Click(object sender, EventArgs e)
			{
			//ensure that we want to erase the old workspace
			if(!WorkSpace.SavedWarning())	return;
			this.DomainRenderer.ResetCameraToOrigin();
			
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select a folder where your workspace will be saved";
			dialog.SelectedPath = Globals.WorkspaceSave.LastDirectory;			
			if(dialog.ShowDialog() == DialogResult.OK)
				{
				Globals.WorkspaceSave.LastDirectory = dialog.SelectedPath;
				ResetWorkspace(dialog.SelectedPath);
				
				this.AddDefaultRoom();
				RenderUpdate();
				}
			}

		private void menuFileSave_Click(object sender,EventArgs e)
			{
			using(ProgressModal progress = new ProgressModal(2))
				{
				progress.Show(this);
				//create a folder structure, create all required files, then fill each file with
				//the info available in
				this.refMVCAdapter.UpdateModel(this.CurrentlySelectedRoom);
				progress.UpdateProgressBar();
				
				WorkSpace.SaveWorkSpaceInfo(this.refMVCAdapter.DomainRootDir);
				WorkSpace.Saved = this.refMVCAdapter.SaveModelToDisk(this.DomainRenderer);
				progress.UpdateProgressBar();
				}
			}

		private void menuFileOpen_Click(object sender,EventArgs e)
			{
			//ensure that we want to erase the old workspace
			if(!WorkSpace.SavedWarning()) return;
			
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.Description = "Select a folder containing a previous workspace to open it";
			dialog.SelectedPath = Globals.WorkspaceSave.LastDirectory;
			
			using(ProgressModal progress = new ProgressModal(7))
				{
				if(dialog.ShowDialog() == DialogResult.OK)
					{
					Globals.WorkspaceSave.LastDirectory = dialog.SelectedPath;
					progress.Show(this);
					ResetWorkspace(dialog.SelectedPath);
					
					CurrentlySelectedRoom = ""; //this needs to be unset so that a loopback call to updating the view doesn't first clear the model that is supposedly being hidden
					progress.UpdateProgressBar();
					this.DomainRenderer.ResetCameraToOrigin();
					WorkSpace.Loading = true;
					try{
						this.refMVCAdapter.LoadModelFromDisk(this.DomainRenderer);
						if(!WorkSpace.LoadWorkSpaceInfo(dialog.SelectedPath))
							{
							ResetWorkspace("");
							this.DisableWorkspace();
							}
						progress.UpdateProgressBar();
						
						//populate exits and enters list boxes
						List<string> temp = new List<string>();
						if(this.refMVCAdapter.Rooms != null && this.refMVCAdapter.Rooms.Count > 0)
							{
							this.DisableModelTriggerFeedback = true;
							foreach(RoomModel room in this.refMVCAdapter.Rooms)
								{
								temp.Add(Stellarmap.Globals.Generator.FunctionParameterLiteral + room.FileName);
								
								//set the icons, couldn't do this in the domain loader because it
								//doesn't have access to the icons list loaded from the config files
								string roomIcon = this.DomainRenderer.GetIconDesc(room.FileName);
								if(this.RoomIcons.ContainsKey(roomIcon))
									{
									this.DomainRenderer.SetImageSource(room.FileName,this.RoomIcons[roomIcon],roomIcon);
									}
								}
							progress.UpdateProgressBar();
							CurrentlySelectedRoom = this.refMVCAdapter.Rooms[0].FileName;
							this.DomainRenderer.OnActivateImage(CurrentlySelectedRoom);
							this.DisableModelTriggerFeedback = false;
							progress.UpdateProgressBar();
							
							this.SetExits.ValueCollection = temp.ToArray();
							this.SetEnters.ValueCollection = temp.ToArray();
							this.SyncViewWithRooms(this.refMVCAdapter.Rooms);
							progress.UpdateProgressBar();
							
							}
						}
					catch(DomainModelException exception)
						{
						MessageBox.Show(exception.Message);
						this.DisableWorkspace();
						}
					WorkSpace.Loading = false;
					WorkSpace.Saved = true;
					RenderUpdate();
					progress.UpdateProgressBar();
					
					}
				}//end using ProgressModal
			
			SetupDummyItemLists();
			return;
			}
		
		private void AddRoomContextMenuItem_Click(object sender,EventArgs e)
			{
			MouseMoveArgs mma = new MouseMoveArgs(this.MousePos,this.ClickPoint,this.DragEnd,this.Offset);			
			this.AddRoom(mma.Current,null,Stellarmap.RoomType.Normal);
			WorkSpace.Saved = false;
			RenderUpdate();
			}
		
		private void removeRoomToolStripMenuItem_Click(object sender, EventArgs e)
			{
			this.RemoveRoom();
			WorkSpace.Saved = false;
			RenderUpdate();
			}

		private void CreateItemMenuSelection_Clicked(object sender,EventArgs args)
			{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			ItemsConfig ig = null;
			
			try{
				//List<string> rooms = new List<string>(this.SetExits.ValueCollection);
				//List<string> items = new List<string>(this.SetInventory.KeyCollection);
				Dictionary<string,List<string>> lists = new Dictionary<string,List<string>>();
				lists.Add("rooms",new List<string>(this.SetExits.ValueCollection));
				lists.Add("inventory",new List<string>(this.SetInventory.KeyCollection));
				
				ig = new ItemsConfig(Globals.Files.ItemsConfigFile,lists);
				//make a 'Create item' dialog
				using(CreateItemForm ItemForm = new CreateItemForm(ig,item.Text,this.refMVCAdapter))
					{
					//using the xml file loaded at startup, create the appropriate
					//user controls for the last tab based on the item being created
					//(FOR NOW THIS IS HARDCODED TO MATCH STRING :P )

					ItemForm.Text = "Create " + item.Text;
					ItemForm.ShowDialog(this);
					}
				}
			catch(Sluggy.Utility.XMLParserException e)
				{
				MessageBox.Show("Error:" + e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the 'itemsconfig.xml' file needed to generate the control.");
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the directory that contains the 'itemsconfig.xml' file needed to generate the control.");
				}
			}

		private void armorToolStripMenuItem_Click(object sender,EventArgs args)
			{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			ItemsConfig ig = null;
			
			try
				{
				Dictionary<string,List<string>> lists = new Dictionary<string,List<string>>();
				lists.Add("rooms",new List<string>(this.SetExits.ValueCollection));
				lists.Add("inventory",new List<string>(this.SetInventory.KeyCollection));
				
				ig = new ItemsConfig(Globals.Files.ArmorsConfigFile,lists);
				//make a 'Create armor' dialog
				if(ig == null)
					{
					MessageBox.Show("Failed to load the armorsconfig.xml file");
					return;
					}
				
				using(CreateArmorForm ArmorForm = new CreateArmorForm(ig,item.Text,this.refMVCAdapter))
					{
					//using the xml file loaded at startup, create the appropriate
					//user controls for the last tab based on the item being created
					//(FOR NOW THIS IS HARDCODED TO MATCH STRING :P )

					ArmorForm.Text = "Create " + item.Text;
					ArmorForm.ShowDialog(this);
					}
				}
			catch(Sluggy.Utility.XMLParserException e)
				{
				MessageBox.Show("Error:" + e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the 'armorsconfig.xml' file needed to generate the control.");
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the directory that contains the 'armorsconfig.xml' file needed to generate the control.");
				}
			}

		private void npcToolStripMenuItem_Click(object sender,EventArgs e)
			{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			ItemsConfig ig = null;

			try
				{
				Dictionary<string,List<string>> lists = new Dictionary<string,List<string>>();
				lists.Add("rooms",new List<string>(this.SetExits.ValueCollection));
				lists.Add("inventory",new List<string>(this.SetInventory.KeyCollection));
				
				ig = new ItemsConfig(Globals.Files.NpcsConfigFile,lists);
				//make a 'Create armor' dialog
				if(ig == null)
					{
					MessageBox.Show("Failed to load the inheritednpcs.xml file");
					return;
					}
				
				using(CreateNpcForm NpcForm = new CreateNpcForm(ig,item.Text,this.refMVCAdapter))
					{
					//using the xml file loaded at startup, create the appropriate
					//user controls for the last tab based on the item being created
					//(FOR NOW THIS IS HARDCODED TO MATCH STRING :P )

					NpcForm.Text = "Create " + item.Text;
					NpcForm.ShowDialog(this);
					}
				}
			catch(Sluggy.Utility.XMLParserException exception)
				{
				MessageBox.Show("Error:" + exception.Message);
				}
			catch(System.IO.FileNotFoundException exception)
				{
				MessageBox.Show(exception.Message + "\n\nCould not locate the 'inheritnpcs.xml' file needed to generate the control.");
				}
			catch(System.IO.DirectoryNotFoundException exception)
				{
				MessageBox.Show(exception.Message + "\n\nCould not locate the directory that contains the 'inheritnpcs.xml' file needed to generate the control.");
				}
			}
		
		private void domainSettingsToolStripMenuItem_Click(object sender,EventArgs e)
			{
			using(DomainOptionsWindow form = new DomainOptionsWindow(this.DomainRenderer,ref this.RoomIcons))
				{
				form.ShowDialog(this);		
				}
			}

		private void menuCreateDoor_Click(object sender,EventArgs e)
			{
			using(CreateDoor form = new CreateDoor(this.refMVCAdapter))
				{
				form.ShowDialog(this);
				}
			}

		private void moveToOriginToolStripMenuItem_Click(object sender,EventArgs e)
			{
			this.DomainRenderer.ResetCameraToOrigin();
			}

		private void menuHelpTips_Click(object sender,EventArgs e)
			{
			if(System.IO.File.Exists(Globals.Files.HelpFile))
				{
				System.Diagnostics.Process.Start(Globals.Files.HelpFile);
				}
			}
		
		private void menuHelpAbout_Click(object sender,EventArgs e)
			{
			using(About about = new About())
				{
				about.ShowDialog(this);
				}
			}

		private void RoomContextMenu_Opening(object sender,CancelEventArgs e)
			{

			}

		private void menuCreateRoomQuick_Click(object sender,EventArgs e)
			{
			MouseMoveArgs mma = new MouseMoveArgs(new System.Drawing.Point(100,100),new System.Drawing.Point(100,100),this.DragEnd,this.Offset);
			this.AddRoom(mma.Current,null,Stellarmap.RoomType.Normal);
			WorkSpace.Saved = false;
			RenderUpdate();
			}

		private void menuCreateRoomComplete_Click(object sender,EventArgs e)
			{
			using(CreateNewRoomForm form = new CreateNewRoomForm(this.AddRoom,new Point(100,100)))
				{
				form.ShowDialog(this);
				}

			WorkSpace.Saved = false;
			RenderUpdate();
			}

		private void domainToolStripMenuItem_Click(object sender,EventArgs e)
			{
			using(ImportExternalDomainForm import = new ImportExternalDomainForm(this.refMVCAdapter))
				{
				import.ShowDialog();
				}

			SetupDummyItemLists();
			}

		private void AddRoomCompleteContextMeruItem_Click(object sender,EventArgs e)
			{
			using(CreateNewRoomForm form = new CreateNewRoomForm(this.AddRoom,this.MousePos))
				{
				form.ShowDialog(this);
				}

			WorkSpace.Saved = false;
			RenderUpdate();
			}

		private void itemToolStripMenuItem_Click(object sender,EventArgs e)
			{
			string[] validDirs = { "armor","meals","npc","obj","weap" };

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AutoUpgradeEnabled = true;
			dialog.InitialDirectory = this.refMVCAdapter.DomainRootDir;

			if(dialog.ShowDialog() == DialogResult.OK)
				{
				//TODO: confirm directory is within domain bounds!!!
				if(!dialog.FileName.Contains(this.refMVCAdapter.DomainRootDir))
					{
					MessageBox.Show("The selected file was not within the active domain's directory. Move it to a location within the directory '" + this.refMVCAdapter.DomainRootDir + "' in order to import it.");
					return;
					}

				//ensure we really are loading a valid object
				bool pass = false;
				foreach(string s in validDirs)
					{
					if(dialog.FileName.Contains(s)) { pass = true; break; }
					}
				if(!pass)
					{
					MessageBox.Show("Invalid directory for loading items. In order to associate the item with on the the defined directories in the master include file, it must be stored in one of the following sub-directories of the domain: armor, meals, npc, obj, weap");
					return;
					}

				if(!this.refMVCAdapter.AddInventory(dialog.FileName,ItemSaveType.Unknown))
					{
					MessageBox.Show("Could not determine the type of item being loaded. Make sure it is stored within the proper sub-directory (armor, meals, npc, obj, or weap) of the domain.");
					return;
					}
				}

			this.SetupDummyItemLists();
			return;
			}

		private void roomToolStripMenuItem_Click(object sender,EventArgs e)
			{
			string[] validDirs = { "room" };
			this.DisableModelTriggerFeedback = true;

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AutoUpgradeEnabled = true;
			//HACK ALERT: hardcoding the room directory here and below
			dialog.InitialDirectory = this.refMVCAdapter.DomainRootDir + "\\room";

			if(dialog.ShowDialog() == DialogResult.OK)
				{
				//TODO: confirm directory is within domain bounds!!!
				if(!dialog.FileName.Contains(this.refMVCAdapter.DomainRootDir + "\\room"))
					{
					MessageBox.Show("The selected file was not within the active domain's directory. Move it to a location within the directory '" + this.refMVCAdapter.DomainRootDir + "\\room\\' in order to import it.");
					return;
					}

				//ensure we really are loading a valid object
				bool pass = false;
				foreach(string s in validDirs)
					{
					if(dialog.FileName.Contains(s)) { pass = true; break; }
					}
				if(!pass)
					{
					MessageBox.Show("Invalid directory for loading items. In order to associate the item with on the the defined directories in the master include file, it must be stored in one of the following sub-directories of the domain: room");
					return;
					}

				//if(!this.AddRoom(new Point(100,100),
				//if(!this.refMVCAdapter.ImportRoom(dialog.FileName,ItemSaveType.Unknown))
				if(!this.ImportRoom(new Point(100,100),dialog.FileName))
					{
					MessageBox.Show("Failed to load the room file " + dialog.FileName + ". Make sure that there are no other rooms with this name already loaded into the workspace.");
					return;
					}
				}

			this.DisableModelTriggerFeedback = false;

			this.SetupDummyItemLists();
			return;
			}

		private void ImportRoomContextMenuItem_Click(object sender,EventArgs e)
			{
			string[] validDirs = { "room" };
			this.DisableModelTriggerFeedback = true;

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.AutoUpgradeEnabled = true;
			//HACK ALERT: hardcoding the room directory here and below
			dialog.InitialDirectory = this.refMVCAdapter.DomainRootDir + "\\room";

			if(dialog.ShowDialog() == DialogResult.OK)
				{
				//TODO: confirm directory is within domain bounds!!!
				if(!dialog.FileName.Contains(this.refMVCAdapter.DomainRootDir + "\\room"))
					{
					MessageBox.Show("The selected file was not within the active domain's directory. Move it to a location within the directory '" + this.refMVCAdapter.DomainRootDir + "\\room\\' in order to import it.");
					return;
					}

				//ensure we really are loading a valid object
				bool pass = false;
				foreach(string s in validDirs)
					{
					if(dialog.FileName.Contains(s)) { pass = true; break; }
					}
				if(!pass)
					{
					MessageBox.Show("Invalid directory for loading items. In order to associate the item with on the the defined directories in the master include file, it must be stored in one of the following sub-directories of the domain: room");
					return;
					}

				//if(!this.AddRoom(new Point(100,100),
				//if(!this.refMVCAdapter.ImportRoom(dialog.FileName,ItemSaveType.Unknown))
				if(!this.ImportRoom(this.MousePos,dialog.FileName))
					{
					MessageBox.Show("Failed to load the room file " + dialog.FileName + ". Make sure that there are no other rooms with this name already loaded into the workspace.");
					return;
					}
				}

			this.DisableModelTriggerFeedback = false;

			this.SetupDummyItemLists();
			return;
			}	

		#endregion
		
		
		#region mouse events
		public void eventMouseMove(object sender,MouseEventArgs e)
			{
			MousePos.X = e.X;
			MousePos.Y = e.Y;
			MouseButton = e.Button;
			
			
			//setup the action
			switch(e.Button)
				{
				case MouseButtons.Left:
					{
					//find the movement vector for this 'frame'					
					MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);					
					this.MouseState.PerformAction(MouseEvents.MoveMouse,(EventArgs)mma);
					break;
					}
				case MouseButtons.Middle:
					{
					//find the movement vector for this 'frame'					
					MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
					this.MouseState.PerformAction(MouseEvents.MoveMouse,(EventArgs)mma);
					break;
					}
				case MouseButtons.None:
					{
					MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
					this.MouseState.PerformAction(MouseEvents.MoveMouse,(EventArgs)mma);					
					break;
					}
				}
			
			Offset = MousePos;
			
			RenderUpdate();
			}

		public void eventMouseWheel(object sender,MouseEventArgs e)
			{
			MousePos.X = e.X;
			MousePos.Y = e.Y;
			MouseButton = e.Button;
			ClickPoint = MousePos;
			
			//don't move camera unless the mouse is over the rendertarget control
			if(MousePos.X < this.RenderTarget.Location.X ||
			   MousePos.X > this.RenderTarget.Right ||
			   MousePos.Y < this.RenderTarget.Location.Y ||
			   MousePos.Y > this.RenderTarget.Bottom)
				{return;}
			
			MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
			mma.Delta = e.Delta;

			if(e.Delta != 0) this.MouseState.PerformAction(MouseEvents.MouseWheel,(EventArgs)mma);
			RenderUpdate();
			}
			
		public void eventMouseDown(object sender,MouseEventArgs e)
			{
			MousePos.X = e.X;
			MousePos.Y = e.Y;
			MouseButton = e.Button;
			ClickPoint = MousePos;
			
			MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
			
			if(e.Button == MouseButtons.Left)	this.MouseState.PerformAction(MouseEvents.LeftClick,(EventArgs)mma);
			if(e.Button == MouseButtons.Right)	this.MouseState.PerformAction(MouseEvents.RightClick,(EventArgs)mma);
			if(e.Button == MouseButtons.Middle) this.MouseState.PerformAction(MouseEvents.MiddleClick,(EventArgs)mma);
			RenderUpdate();	
			}
		
		public void eventMouseUp(object sender,MouseEventArgs e)
			{
			MousePos.X = e.X;
			MousePos.Y = e.Y;
			MouseButton = e.Button;
			DragEnd = MousePos;
			MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
			
			if(e.Button == MouseButtons.Left)	this.MouseState.PerformAction(MouseEvents.LeftRelease,(EventArgs)mma);
			if(e.Button == MouseButtons.Middle) this.MouseState.PerformAction(MouseEvents.MiddleRelease,(EventArgs)mma);
			
			RenderUpdate();
			}
		
		private void OnInputDown(object sender,KeyEventArgs e)
			{
			MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);			
			if(e.KeyCode == Keys.ControlKey)
				{				
				this.MouseState.PerformAction(MouseEvents.CtrlDown,(EventArgs)mma);				
				}
			if(e.KeyCode == Keys.ShiftKey)
				{
				this.MouseState.PerformAction(MouseEvents.ShiftDown,(EventArgs)mma);
				}
			}
		
		private void OnInputUp(object sender,KeyEventArgs e)
			{
			MouseMoveArgs mma = new MouseMoveArgs(MousePos,ClickPoint,DragEnd,Offset);
			
			if(e.KeyCode == Keys.ControlKey)
				{
				this.MouseState.PerformAction(MouseEvents.CtrlUp,(EventArgs)mma);
				}
			if(e.KeyCode == Keys.ShiftKey)
				{
				this.MouseState.PerformAction(MouseEvents.ShiftUp,(EventArgs)mma);
				}		
			}
		#endregion
		
		
		#region private methods
		/// <summary>
		/// Handele the event when any function control that the
		/// tabpage own has an update.
		/// </summary>
		/// <param name="sender">The FunctionControl that updated.</param>		
		private void OnFunctionControlUpdate(object sender,EventArgs args)
			{
			bool syncView = false;
			if(!this.refMVCAdapter.Initialized) return;
			if(WorkSpace.Resetting)	return;
			
			IFunctionControl control = (IFunctionControl)sender;
			
					
			//SPECIAL CASES (ie Ugliness):
			switch(control.FunctionName)
				{
				case "SetClimate":
					{
					//make sure if we set the climate to "indoors" that
					//we disable the day/night description boxes
					ComboSelection combo = (ComboSelection)control;
					if(combo.EntryText == "indoors")
						{
						this.EnableDayNight.Checked = false;
						}
					break;
					}
				
				case "SetItems":
					{
					//copies all listbox controls contents to model
					//SetupDummyItemLists();
					break;
					}

				case "SetDoors":
					{
					//SetupDummyItemLists();
					syncView = true;
					break;
					}
				
				case "SetEnters":
					{
					//fall through to 'SetExits'
					//HACK ALERT: this is here because I needed the functionality
					//for up/down exits to still work :p  SyncViewWithRooms()
					//doesn't doo that and I haven't figured out why
					RoomConnectionArgs connect = (RoomConnectionArgs)args;
					if(connect != null)
						{
						if(connect.Type == ConnectionType.Connect)
							{
							this.DomainRenderer.ConnectNodeToAvatar(this.CurrentlySelectedRoom,
																	connect.ExitPath,
																	connect.Direction);
							}
						else
							{
							this.DomainRenderer.ConnectNodeToAvatar(this.CurrentlySelectedRoom,
																	null,
																	connect.Direction);

							this.DomainRenderer.SetNodeExitDoor(this.CurrentlySelectedRoom,
																connect.Direction,
																false);
							//If there was a door based on this exit, it need to be removed too
							this.RemoveDoorEntry(connect.Direction,this.CurrentlySelectedRoom);
							}
						}
					break;
					}
				
				case "SetExits":
					{
					//HACK ALERT: this is here because I needed the functionality
					//for up/down exits to still work :p  SyncViewWithRooms()
					//doesn't doo that and I haven't figured out why
					RoomConnectionArgs connect = (RoomConnectionArgs)args;
					if(connect != null)
						{
						if(connect.Type == ConnectionType.Connect)
							{
							this.DomainRenderer.ConnectNodeToAvatar(this.CurrentlySelectedRoom,
																	connect.ExitPath,
																	connect.Direction);
							}
						else
							{
							this.DomainRenderer.ConnectNodeToAvatar(this.CurrentlySelectedRoom,
																	null,
																	connect.Direction);

							this.DomainRenderer.SetNodeExitDoor(this.CurrentlySelectedRoom,
																connect.Direction,
																false);
							//If there was a door based on this exit, it need to be removed too
							this.RemoveDoorEntry(connect.Direction,this.CurrentlySelectedRoom);
							}
						}
					break;
					}
				
				/*case "inherit LIB_ROOM;":
					{
					Stellarmap.Check box = sender as Stellarmap.Check;
					if(box != null)
						{
                        box.Checked = true;
					    this.InheritLibShopCheck.Checked = false;
						}
                    TriggerModelUpdate(this.CurrentlySelectedRoom);
					return;
					}
				case "inherit LIB_SHOP;":
					{
					Stellarmap.Check box = sender as Stellarmap.Check;
					if(box != null && box.Checked)
						{
                        box.Checked = true;
					    this.InheritLibRoomCheck.Checked = false;
						}
                    TriggerModelUpdate(this.CurrentlySelectedRoom);
					return;
					}*/
				}
			
			//I know it's slow to do this EVERY time a control changes (it is
			//uploading every control in the form to the model). In the future, we
			//should have an update interface for the model that allows us to
			//single out a control.
			TriggerModelUpdate(this.CurrentlySelectedRoom);
			this.refMVCAdapter.TriggerViewUpdate(CurrentlySelectedRoom);
			
			if(syncView)
				{
				//SyncViewWithRooms(this.refMVCAdapter.Rooms);
				SyncDoorsViewWithSingleRoom(this.refMVCAdapter.GetRoom(this.CurrentlySelectedRoom));
				}
			}
		
		/// <summary>
		/// Updates the given room model with the current view's data.
		/// </summary>
		private void TriggerModelUpdate(string roomName)
			{
			WorkSpace.Saved = false; //easy but inaccurate catchall for project updates
			if(roomName != "" && roomName.Length > 0 && !this.DisableModelTriggerFeedback)
				{				
				this.refMVCAdapter.UpdateModel(CurrentlySelectedRoom);
				}
			}
		
		private void ResetWorkspace(string path)
			{
			WorkSpace.ResetBegin();
			
			CurrentlySelectedRoom = "";
			DomainRenderer.Reset(this,null);
			this.RoomTabPanel.Enabled = true;
			this.RenderTarget.Enabled = true;
			//this.domainSettingsToolStripMenuItem.Enabled = true;
			WorkSpace.EnableWorkspaceControls(true);
			this.SetEnters.ValueCollection = new string[] {};
			this.SetExits.ValueCollection = new string[] {};
			refMVCAdapter.ResetModel(path);
			ResetForm(this.RoomTabPanel);
			
			this.Text = "Stellarmap:   " + refMVCAdapter.DomainName;
			
			DisableViewTriggerFeedback = false; //used to stop certain model updates from feeding back to the view
			DisableModelTriggerFeedback = false; //same as above but in reverse
			
			WorkSpace.ResetEnd();
			}
		
		private void DisableWorkspace()
			{
			CurrentlySelectedRoom = "";
			DomainRenderer.Reset(this,null);
			this.RoomTabPanel.Enabled = false;
			this.RenderTarget.Enabled = false;
			//this.domainSettingsToolStripMenuItem.Enabled = false;
			WorkSpace.EnableWorkspaceControls(false);
			this.SetEnters.ValueCollection = new string[] { };
			this.SetExits.ValueCollection = new string[] { };
			refMVCAdapter.DisableModel();
			ResetForm(this.RoomTabPanel);

			this.Text = "Stellarmap";

			WorkSpace.Saved = true;
			WorkSpace.RoomNameCount = 0;
			}						
		
		private void ActivateSelectionRect(object sender,EventArgs e)
			{
			MouseMoveArgs mma = (MouseMoveArgs)e;
			SelectionRect = new Rectangle();
			SelectionRect.X = System.Math.Min(mma.Current.X,mma.Start.X);
			SelectionRect.Y = System.Math.Min(mma.Current.Y,mma.Start.Y);
			SelectionRect.Width = System.Math.Abs(mma.Current.X - mma.Start.X);
			SelectionRect.Height = System.Math.Abs(mma.Current.Y - mma.Start.Y);
			
			SelectionRectActive = true;
			}
		
		private void DeactivateSelectionRect(object sender,EventArgs e)
			{
			SelectionRectActive = false;
			}
		
		private void ActivateDragLine(object sender,EventArgs e)
			{
			MouseMoveArgs mma = (MouseMoveArgs)e;
			DragLineStart = new Point();
			DragLineEnd = new Point();
			DragLineStart.X = mma.Start.X;//System.Math.Min(mma.Current.X,mma.Start.X);
			DragLineStart.Y = mma.Start.Y;//System.Math.Min(mma.Current.Y,mma.Start.Y);
			DragLineEnd.X = mma.Current.X;//System.Math.Abs(mma.Current.X - mma.Start.X);
			DragLineEnd.Y = mma.Current.Y;//System.Math.Abs(mma.Current.Y - mma.Start.Y);

			DragLineActive = true;
			}
		
		private void DeactivateDragLine(object sender,EventArgs e)
			{
			DragLineActive = false;
			}
		
		private void RenderUpdate()
			{
			if(refMVCAdapter != null && refMVCAdapter.Initialized)
				{
				RenderDevice.Clear(Globals.GUI.ActiveBackgroundColor);
				
				RenderOriginPoint();
								
				//BUG ALERT: Syncing the view makes the exits all wonky!!!
				//SyncViewWithCurrentRoom();
				DomainRenderer.OnRenderDomainMap(RenderDevice);
				
				if(SelectionRectActive)
					{
					Pen pen = new Pen(Color.Yellow);
					RenderDevice.Graphics.DrawRectangle(pen,SelectionRect);
					}
				if(DragLineActive)
					{
					Pen pen = new Pen(Color.Yellow);
					RenderDevice.Graphics.DrawLine(pen,DragLineStart,DragLineEnd);
					}
				
				
				}
			else{
				RenderDevice.Clear(Globals.GUI.InactiveBackgroundColor);
				}
			
			RenderDevice.Flip();
			}
		
		private void ResizeView(int xSize,int ySize)
			{
			//let the render target know to resize as well as update it's scollbars
			this.RenderTarget.TriggerResize(xSize - (int)Metrics.RoomControlPanelMarginX - (int)Metrics.RoomLayoutLeftMargin - (int)Metrics.RoomLayoutRightMargin,
											ySize  - (int)Metrics.RoomLayoutBottomMargin - (int)Metrics.RoomLayoutTopMargin);
			
			//resize control panel
			int roomTabHeight = ySize - (int)Metrics.RoomLayoutBottomMargin - this.RoomTabPanel.Top;
			if(roomTabHeight > this.MinRoomTabHeight)
				{this.RoomTabPanel.Height = roomTabHeight;}
			
			
			
			
			//resize the rendering backbuffer
			RenderDevice.ResizeBuffer();
			DomainRenderer.UpdateViewDimensions();
			
			this.VerticalScroll.Value = 0;
			this.HorizontalScroll.Value = 0;
			}
		
		/// <summary>
		/// Adds the very first room to the domain which always uses the default start name.
		/// </summary>
		private void AddDefaultRoom()
			{
			if(this.refMVCAdapter.Rooms.Count > 0)	return;
			
			//add the room model and set its default functions list
			IncludeFile include = this.refMVCAdapter.IncludeFile;
			string generatedName = new StringBuilder(include.GetPathDefinitionIdentifier(ItemSaveType.Room,this.refMVCAdapter.DomainName) + " \"/" + Globals.Model.CustomStartRoomName + Globals.Model.RoomExtension + "\"").ToString();
			this.refMVCAdapter.AddRoom(generatedName,Stellarmap.RoomType.Normal);
			CurrentlySelectedRoom = generatedName;
			
			
			//now create the view (offset the image position by a bit
			Point p = new Point(Globals.ImageBoxProperties.width*2,Globals.ImageBoxProperties.height*2);
			this.DomainRenderer.AddImage(p,Globals.ImageBoxProperties.DefaultImage,generatedName,Globals.ImageBoxProperties.DefaultIconDesc);
			
			
			//add this room to the exits and enters lists
			List<string> temp = new List<string>(this.SetEnters.ValueCollection);
			temp.Add(Stellarmap.Globals.Generator.FunctionParameterLiteral + generatedName);
			this.SetExits.ValueCollection = temp.ToArray();
			this.SetEnters.ValueCollection = temp.ToArray();
			
			
			//reset to default form values
			ResetForm(this.RoomTabPanel);
			
			//save the new room
			this.refMVCAdapter.UpdateModel(generatedName);
			}
		
		private void AddRoom(Point cursor,string name,RoomType type)
			{
			//save the current room
			if(CurrentlySelectedRoom != "") this.refMVCAdapter.UpdateModel(this.CurrentlySelectedRoom);
			
			//add the room model and set its default functions list
			string generatedName = GenerateRoomName(name);
			if(generatedName == null) return;
			
			this.refMVCAdapter.AddRoom(generatedName,type);
			CurrentlySelectedRoom = generatedName;
			
									
			//now create the view
			this.DomainRenderer.AddImage(cursor,Globals.ImageBoxProperties.DefaultImage,generatedName,Globals.ImageBoxProperties.DefaultIconDesc);
						
			
			//add this room to the exits and enters lists
			List<string> temp = new List<string>(this.SetEnters.ValueCollection);
			temp.Add(Globals.Generator.FunctionParameterLiteral + generatedName);
			this.SetExits.ValueCollection = temp.ToArray();
			this.SetEnters.ValueCollection = temp.ToArray();
			
			
			//reset to default form values
			ResetForm(this.RoomTabPanel);
			
			//save the new room
			this.refMVCAdapter.UpdateModel(generatedName);
			}

		private bool ImportRoom(Point cursor,string filePath)
			{
			//save the current room
			if(CurrentlySelectedRoom != "") this.refMVCAdapter.UpdateModel(this.CurrentlySelectedRoom);

			//create filename but ensure that it is not already taken
			string[] t = filePath.Split(System.IO.Path.DirectorySeparatorChar);
			string roomFileName = t[t.Length - 1];
			string generatedName = GenerateRoomName(roomFileName);
			if(generatedName == null) { return false; }

			//now ensure that the workspace takes into account any imported rooms
			//that used the default name with a number
			if(roomFileName.Contains(Globals.Model.BaseRoomName))
				{
				//HACK ALERT: not checking results very well here
				try
					{
					string[] parts = roomFileName.Replace(Globals.Model.BaseRoomName,"").Split('.');
					string numberStr = parts[0];

					//the workspace count only gets changes if this number is higher than the current count
					//as we don't want to start generating numbers we might already have. Remmber, that we
					//already confirmed (I think, anyway) that the room name doesn't exist so having
					//a smaller value shouldn't matter
					int count = Convert.ToInt32(numberStr);
					if(count > WorkSpace.RoomNameCount)
						{
						WorkSpace.RoomNameCount = count + 1;
						}
					}
				catch(ArgumentOutOfRangeException exception)
					{
					//slight hack alert: I'm assuming that if the calculation doesn't come out right
					//then it must not have a name that matters for the name generator's count

					//swallow exception and continue as normal
					}
				catch(FormatException exception)
					{
					//same as above
					}

				}

			if(!this.refMVCAdapter.ImportRoom(roomFileName)) { return false; }
			CurrentlySelectedRoom = generatedName;


			//now create the view
			this.DomainRenderer.AddImage(cursor,Globals.ImageBoxProperties.DefaultImage,generatedName,Globals.ImageBoxProperties.DefaultIconDesc);


			//add this room to the exits and enters lists
			List<string> temp = new List<string>(this.SetEnters.ValueCollection);
			temp.Add(Globals.Generator.FunctionParameterLiteral + generatedName);
			this.SetExits.ValueCollection = temp.ToArray();
			this.SetEnters.ValueCollection = temp.ToArray();

			//save the new room
			this.refMVCAdapter.TriggerViewUpdate(generatedName);
			return true;
			}
				
		private void RemoveRoom()
			{
			List<string> rooms = this.DomainRenderer.RemoveImage();
						
			foreach(string roomName in rooms)
				{
				//remove the room from exits and enters list
				this.SetExits.RemoveValueFromList(Globals.Generator.FunctionParameterLiteral + roomName);
				this.SetEnters.RemoveValueFromList(Globals.Generator.FunctionParameterLiteral + roomName);
				
				//delete room from model
				this.refMVCAdapter.RemoveRoom(roomName);
				}
			
			CurrentlySelectedRoom = "";
			//this.DomainRenderer.OnActivateImage(CurrentlySelectedRoom);
			this.refMVCAdapter.TriggerViewUpdate(CurrentlySelectedRoom);
			this.RoomTabPanel.Enabled = false;
			}
		
		private void OnConnectRoom(object sender,EventArgs args)
			{
			RoomConnectionArgs connect = (RoomConnectionArgs)args;
						
			this.refMVCAdapter.AddExitToRoom(connect.Room,connect.ExitPath,"\"" + connect.Direction + "\"");
			this.refMVCAdapter.TriggerViewUpdate(this.CurrentlySelectedRoom);
			}
		
		private void OnDisconnectRoom(object sender,EventArgs args)
			{
			RoomConnectionArgs connect = (RoomConnectionArgs)args;
			this.DomainRenderer.SetNodeExitDoor(connect.Room,connect.Direction,false);
			this.RemoveDoorEntry(connect.Direction,connect.Room);
			//this.refMVCAdapter.RemoveExitFromRoom(connect.Room,connect.ExitPath);
			this.refMVCAdapter.RemoveExitFromRoom(connect.Room,connect.Direction);
			this.refMVCAdapter.TriggerViewUpdate(this.CurrentlySelectedRoom);
			
			
			}

		private void EditConfigFile()
			{
			using(ProgressModal progress = new ProgressModal(19))
				{
				progress.Show(this);

				//yeah, reloading the config file just to mod it and save again. Derp!
				//System.Windows.Forms.
				//SUPER LAME PROGRESS BAR AHOY!
				try
					{
					Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
					progress.UpdateProgressBar();

					Sluggy.Utility.Tag imageboxes = parser.DepthSeekFirst(null,"imageboxes");
					progress.UpdateProgressBar();

					Sluggy.Utility.Tag nodeboxes = parser.DepthSeekFirst(null,"nodeboxes");
					progress.UpdateProgressBar();

					Sluggy.Utility.Tag baseroom = parser.DepthSeekFirst(null,"baseroom");
					progress.UpdateProgressBar();

					Sluggy.Utility.Tag startroom = parser.DepthSeekFirst(null,"startroom");
					progress.UpdateProgressBar();

					Sluggy.Utility.Tag exitWidth = parser.DepthSeekFirst(null,"exitlinewidth");
					progress.UpdateProgressBar();
					
					Sluggy.Utility.Tag lastDir = parser.DepthSeekFirst(null,"lastdiropened");
					progress.UpdateProgressBar();


					if(imageboxes == null || nodeboxes == null || exitWidth == null)
						{
						MessageBox.Show("The config file has been corrupted and cannot be altered. The problem may be solved by deleting the file 'config.xml' in the Stellarmap assets folder.");
						return;
						}
					progress.UpdateProgressBar();

					imageboxes.Attributes["width"] = System.Convert.ToString(Globals.ImageBoxProperties.width);
					progress.UpdateProgressBar();

					imageboxes.Attributes["height"] = System.Convert.ToString(Globals.ImageBoxProperties.height);
					progress.UpdateProgressBar();

					nodeboxes.Attributes["width"] = System.Convert.ToString(Globals.NodeProperties.width);
					progress.UpdateProgressBar();

					nodeboxes.Attributes["height"] = System.Convert.ToString(Globals.NodeProperties.height);
					progress.UpdateProgressBar();

					baseroom.Attributes["name"] = Globals.Model.BaseRoomName;
					progress.UpdateProgressBar();

					startroom.Attributes["name"] = Globals.Model.CustomStartRoomName;
					progress.UpdateProgressBar();

					exitWidth.Attributes["width"] = System.Convert.ToString(Globals.ImageBoxProperties.ExitLineWidth);
					progress.UpdateProgressBar();
					
					lastDir.Attributes["path"] = Globals.WorkspaceSave.LastDirectory;
					progress.UpdateProgressBar();

					parser.Save(parser.FilePath);

					//TODO: need an option to save Parsed XML data in parser!!!
					}//end try
				catch(Sluggy.Utility.XMLParserException exc)
					{
					MessageBox.Show(exc.Message);
					}
				}//end progress modal
			}
		
		#endregion
		
		
		#region private helpers
		private void SaveForm(Control parentControl)
			{			
			foreach(Control control in parentControl.Controls)
				{
				if(control is CheckBox)
					{
					CheckBox box = (CheckBox)control;
					box.Tag = box.Checked;
					}
				if(control is ComboBox)
					{
					ComboBox box = (ComboBox)control;
					box.Tag = box.Text;
					}
				if(control is TextBox)
					{
					TextBox box = (TextBox)control;
					box.Tag = box.Text;
					}
				if(control is IFunctionControl)
					{
					IFunctionControl func = (IFunctionControl)control;
					func.Tag = func.PullEntry();
					}
				
				//recursion				
				SaveForm(control);
				}			
			}
		
		private void ResetForm(Control targetControl)
			{
			if(targetControl.Controls.Count < 1) return;
			
			//HACK ALERT -
			this.SetMedium.SelectedIndex = -1;
			//END HACK ALERT
					
			foreach(Control control in targetControl.Controls)
				{
				if(control is CheckBox)
					{
					CheckBox box = (CheckBox)control;
					box.Checked = (bool)box.Tag;
					}
				if(control is ComboBox)
					{
					ComboBox box = (ComboBox)control;
					box.Text = (string)box.Tag;
					}
				if(control is TextBox)
					{
					TextBox box = (TextBox)control;
					box.Text = (string)box.Tag;
					}
				if(control is IFunctionControl)
					{
					IFunctionControl func = (IFunctionControl)control;
					func.PushEntry((string)func.Tag);
					}
				
				//recursion
				ResetForm(control);
				}
			}
		
		private void LoadIconsFromConfig()
			{
			this.RoomIcons = Utility.LoadIconsFromConfig(Globals.Files.ConfigFile);
			
			//fill the combox box list with all the different icon names loaded
			this.RoomIconComboBox.Items.Add(Globals.ImageBoxProperties.DefaultIconDesc);
			foreach(string key in this.RoomIcons.Keys)
				{
				this.RoomIconComboBox.Items.Add(key);
				}
			
			}
		
		private string GenerateRoomName(string name)
			{
			StringBuilder baseName;
			IncludeFile include = this.refMVCAdapter.IncludeFile;
			
			if(name != null && name.Length > 0)
				{
				if(name.Contains(Globals.Model.RoomExtension))
					{baseName = new StringBuilder(include.GetPathDefinitionIdentifier(ItemSaveType.Room,this.refMVCAdapter.DomainName) + " \"/" + name + "\"");}
				else{baseName = new StringBuilder(include.GetPathDefinitionIdentifier(ItemSaveType.Room,this.refMVCAdapter.DomainName) + " \"/" + name + Globals.Model.RoomExtension + "\"");}
				}
			else{
				baseName = new StringBuilder(include.GetPathDefinitionIdentifier(ItemSaveType.Room,this.refMVCAdapter.DomainName) + " \"/" + Globals.Model.BaseRoomName + WorkSpace.RoomNameCount.ToString("000") + Globals.Model.RoomExtension + "\"");
				}
				
			if(this.refMVCAdapter.RoomNames.Contains(baseName.ToString()))
				{
				MessageBox.Show("This domain already contains a room file with the name '" + name + "'.");
				return null;
				}
			
			WorkSpace.RoomNameCount ++;
			return baseName.ToString();
			}	
		
		private void GenerateItemCreationMenu()
			{
			
			try {
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ItemsConfigFile);
				List<Sluggy.Utility.Tag> items = parser.FindAll(null,"itemclassdeclaration");
				EventHandler MenuItemCreate = new EventHandler(CreateItemMenuSelection_Clicked);
				
				if(items != null)
					{
					foreach(Sluggy.Utility.Tag tag in items)
						{
						//TODO: add an event handler later that parses which one based on item name	(NOTE: Well, it's now much later and I have no idea what that was supposed to mean)		
						if(tag.Attributes.ContainsKey("id"))
							{							
							this.menuCreateObject.DropDownItems.Add(new ToolStripMenuItem(tag.Attributes["id"],null,MenuItemCreate));
							}
						else{
							MessageBox.Show("Error parsing itemsconfig.xml. The \"Itemclassdeclration\" tag " + tag.Name + " did not contain an id attribute. Cannot create the associated menu item for the Item Generation tool.");
							}
						}
					}
				else{
					MessageBox.Show("Itemsconfig.xml file did not have any \"itemclassdeclaration\" tags with which to build a menu for the Item Creation tool.");
					}
				}
			catch(Sluggy.Utility.XMLParserException e)
				{
				MessageBox.Show(e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the 'itemsconfig.xml' file needed to generate the Item Creation tool.");
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the directory for containing the 'itemsconfig.xml' file needed to generate the Item Creation tool.");
				}
			}

		private void GenerateArmorCreationMenu()
			{
			try
				{
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ArmorsConfigFile);
				List<Sluggy.Utility.Tag> items = parser.FindAll(null,"itemclassdeclaration");
				EventHandler MenuItemCreate = new EventHandler(armorToolStripMenuItem_Click);

				if(items != null)
					{
					foreach(Sluggy.Utility.Tag tag in items)
						{
						//TODO: add an event handler later that parses which one based on item name	(NOTE: Well, it's now much later and I have no idea what that was supposed to mean)		
						if(tag.Attributes.ContainsKey("id"))
							{
							this.armorToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(tag.Attributes["id"],null,MenuItemCreate));
							}
						else
							{
							MessageBox.Show("Error parsing armorsconfig.xml. The \"Itemclassdeclration\" tag " + tag.Name + " did not contain an id attribute. Cannot create the associated menu item for the Armor Generation tool.");
							}
						}
					}
				else
					{
					MessageBox.Show("Armorsconfig.xml file did not have any \"itemclassdeclaration\" tags with which to build a menu for the Armor Creation tool.");
					}
				}
			catch(Sluggy.Utility.XMLParserException e)
				{
				MessageBox.Show(e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the 'armorsconfig.xml' file needed to generate the Armor Creation tool.");
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the directory for containing the 'armorsconfig.xml' file needed to generate the Armor Creation tool.");
				}
			}

		private void GenerateNpcCreationMenu()
			{
			try
				{
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.NpcsConfigFile);
				List<Sluggy.Utility.Tag> items = parser.FindAll(null,"itemclassdeclaration");
				EventHandler MenuItemCreate = new EventHandler(npcToolStripMenuItem_Click);

				if(items != null)
					{
					foreach(Sluggy.Utility.Tag tag in items)
						{
						//TODO: add an event handler later that parses which one based on item name	(NOTE: Well, it's now much later and I have no idea what that was supposed to mean)		
						if(tag.Attributes.ContainsKey("id"))
							{
							this.npcToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(tag.Attributes["id"],null,MenuItemCreate));
							}
						else
							{
							MessageBox.Show("Error parsing npcsconfig.xml. The \"Itemclassdeclration\" tag " + tag.Name + " did not contain an id attribute. Cannot create the associated menu item for the Npc Generation tool.");
							}
						}
					}
				else
					{
					MessageBox.Show("Npcssconfig.xml file did not have any \"itemclassdeclaration\" tags with which to build a menu for the Npc Creation tool.");
					}
				}
			catch(Sluggy.Utility.XMLParserException e)
				{
				MessageBox.Show(e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the 'npcsconfig.xml' file needed to generate the Npc Creation tool.");
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				MessageBox.Show(e.Message + "\n\nCould not locate the directory for containing the 'npcsconfig.xml' file needed to generate the Npc Creation tool.");
				}
			}
		
		/// <summary>
		/// Recursively collects all sub-controls derived from IFunctionControl
		/// that are contained within the parent control.
		/// </summary>
		private List<IFunctionControl> CompileFunctionControlsList(Control parentControl)
			{
			List<IFunctionControl> list = new List<IFunctionControl>();
			
			foreach(Control control in parentControl.Controls)
				{
				if(control is IFunctionControl)
					{
					AFunctionControl func = (AFunctionControl)control;

                    func.SubscribeToUpdate(this.OnFunctionControlUpdate);
					list.Add((IFunctionControl)control);
					}
				
				////recursively add other controls
				if(control.Controls.Count > 0)
				    {
				    list.AddRange(CompileFunctionControlsList(control));
				    }
				//recursively add other controls
				//however, don't do this if either disabled or a FlxieFunction
				//Especially the latter because it will definitely contain more
				//controls inside that should not be exposed
				//if(control.Controls.Count > 0 && control.Enabled)
				//    {
				//    IFunctionControl con = control as IFunctionControl;
				//    if(con == null || con.ParameterType != FuncParamType.MultiParamFunction)
				//        { list.AddRange(CompileFunctionControlsList(control)); }
				//    }
				}
			
			return list;
			}
		
		private FunctionCallsCollection CompileFunctionCallsList(List<IFunctionControl> controls)
			{
			FunctionCallsCollection collection = new FunctionCallsCollection();
			
			foreach(IFunctionControl control in controls)
				{
                //HACK ALERT: because I can simple exclude disabled controls I've
                //hardcoded the ones that should not be saved under certain situations
                //(specifically, the day/night related functions)
                if(control.FunctionName == "SetDayLong" || control.FunctionName == "SetNightLong" ||
                    control.FunctionName == "SetDayLight" || control.FunctionName == "SetNightLight")
                    {
                    if(!this.CurrentRoomEditorState.UseDayNight) {continue;}
                    }
				try {
					//only collect if enabled and it represents an actual function call
					//NOTE: BUG ALERT - 
					//check for enabled controls causes a problem because all controls
					//are disabled when creating a new room, thus causing not data to be
					//saved for the current room and having it's data be reset
					//if(control.Enabled && control.ParameterType != FuncParamType.Inherit)
					//if(control.ParameterType != FuncParamType.Inherit)
						{
						string parameters = control.PullEntry();
						collection.DefineCall(control.FunctionName,parameters);
						if(parameters.Length > 0 && parameters != null && parameters != "({})" && parameters != "([])" && parameters != "\"\"")
							{
							collection.AddHeaderFile(control.RequiredHeader);
							}
						}
					//collection.DefineCall(control.FunctionName,control.PullEntry());
					}
				catch(FunctionControlException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				catch(ParserException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				}
			
			return collection;
			}
		
		/// <summary>
		/// Pulls all of the items from the 'Setitems' function and populates the
		/// comboboxes for enters/exits/smells/sounds controls using that list. Also
		/// does doors.
		/// </summary>
		private void SetupDummyItemLists()
			{
			RoomModel room = this.refMVCAdapter.GetRoom(this.CurrentlySelectedRoom);
			RoomEditorState state = this.CurrentRoomEditorState;
			List<string> list = state.CopyStaticItemsFromControl(this.SetItems);
			
			
			//now copy all lists back to the controls
			this.SetEnters.KeyCollection = list.ToArray();
			this.SetEnters.ValueCollection = this.refMVCAdapter.RoomNames.ToArray();
			this.SetSmell.KeyCollection = state.InteractiveStaticItems.ToArray();
			this.SetListen.KeyCollection = state.InteractiveStaticItems.ToArray();
			this.SetSearch.KeyCollection = state.InteractiveStaticItems.ToArray();
			this.SetRead.KeyCollection = state.InteractiveStaticItems.ToArray();	
			this.SetExits.KeyCollection = state.ExitItems.ToArray();
			this.SetExits.ValueCollection = this.refMVCAdapter.RoomNames.ToArray();
			//this.SetDoors.KeyCollection = ;//state.ExitItems.ToArray();
			
			this.SetDoors.ValueCollection = this.refMVCAdapter.Doors.ToArray();
			this.SetInventory.KeyCollection = this.refMVCAdapter.Inventory.ToArray();
			
			
			//HACK ALERT!!!
			//doors are so special. We only want exits that have been connected or
			//otherwise we might accidentally set a state that shouldn't be and the door
			//wouldn't hilight when the exit *was* connected
			List<string> exitsThatCanBeDoored = new List<string>();
			if(room != null)
				{
				//build list of exits here
				if(room.FunctionCalls != null && room.FunctionCalls.CallList != null && room.FunctionCalls.CallList.ContainsKey("SetExits"))
					{
					Dictionary<string,string> exitParams = ParserTools.StringIntoMap(room.FunctionCalls.CallList["SetExits"],"DomainViewForm.SetupDummyItemsList");
					if(exitParams != null && exitParams.Keys != null)
						{
						foreach(string s in exitParams.Keys)
							{
							string temp = s.Replace("\"","");
							exitsThatCanBeDoored.Add(temp);
							}
						
						}
					}
				}
			exitsThatCanBeDoored.AddRange(list);
			this.SetDoors.KeyCollection = exitsThatCanBeDoored.ToArray();
			
			}
		

		private void SyncViewWithRooms(List<RoomModel> rooms)
			{
			foreach(RoomModel room in rooms)
				{
				SyncViewWithSingleRoom(room);
				SyncDoorsViewWithSingleRoom(room);
				}
			}
		
		private void SyncViewWithCurrentRoom()
			{
			foreach(string exit in this.CurrentRoomEditorState.ExitsList.Keys)
				{
				string strippedExit = exit.TrimStart('"');
				strippedExit = strippedExit.TrimEnd('"');
				this.DomainRenderer.ConnectNodeToAvatar(this.CurrentlySelectedRoom,this.CurrentRoomEditorState.ExitsList[exit],strippedExit);
				}
			}
		
		private void SyncViewWithSingleRoom(RoomModel room)
			{
			foreach(string exit in room.EditorState.ExitsList.Keys)
				{
				string strippedExit = exit.TrimStart('"');
				strippedExit = strippedExit.TrimEnd('"');
				this.DomainRenderer.ConnectNodeToAvatar(room.FileName,room.EditorState.ExitsList[exit],strippedExit);
				
				}

			foreach(string enter in room.EditorState.EntersList.Keys)
				{
				string strippedEnter = enter.TrimStart('"');
				strippedEnter = strippedEnter.TrimEnd('"');
				this.DomainRenderer.ConnectNodeToAvatar(room.FileName,room.EditorState.EntersList[enter],strippedEnter);

				}
			}
		
		private void SyncDoorsViewWithSingleRoom(RoomModel room)
			{
			if(room.FunctionCalls != null && room.FunctionCalls.CallList != null && room.FunctionCalls.CallList.ContainsKey("SetDoors"))
				{
				string doorparams = room.FunctionCalls.CallList["SetDoors"];
				Dictionary<string,string> paramMap = ParserTools.StringIntoMap(doorparams,"DomainViewForm.SyncDoorViewWithSingleRoom");
				
				if(paramMap != null)
					{
					//ok, we have the door parmeters setup, it provides us with a list of keys that
					//represent exit directions. So all we have to do is decide if each
					//exit node should be flagged as having a door or not.
					
					//loop through every possible exit
					foreach(string exit in room.EditorState.ExitsList.Keys)
						{
						//is this exit in the list of doors?
						if(paramMap.ContainsKey(exit))
							{
							this.DomainRenderer.SetNodeExitDoor(room.FileName,exit,true);
							}
						else{
							this.DomainRenderer.SetNodeExitDoor(room.FileName,exit,false);
							}
						}
					}
				}
			}
		
		private string RemoveDoorEntry(string direction,string roomName)
			{
			RoomModel room = this.refMVCAdapter.GetRoom(roomName);
			if(room == null)	return "";
			
			if(room.FunctionCalls != null && room.FunctionCalls.CallList != null && room.FunctionCalls.CallList.ContainsKey("SetDoors"))
				{
				Dictionary<string,string> paramMap = ParserTools.StringIntoMap(room.FunctionCalls.CallList["SetDoors"],"DomainViewForm.SyncDoorViewWithSingleRoom");
				
				if(paramMap != null)
					{
					if(paramMap.ContainsKey("\"" + direction + "\""))
						{
						//remove entry frm room model's data
						string str = ParserTools.MapIntoString(paramMap);
						str = str.Replace("([","");
						str = str.Replace("])","");
						paramMap.Remove("\"" + direction + "\"");
						//room.FunctionCalls.DefineCall("SetDoors","(["+str+"])");
						room.FunctionCalls.DefineCall("SetDoors",ParserTools.MapIntoString(paramMap));
						
						//we also need to remove this from the control. Removing the key
						//will also remove any entries based on that key so we are good to
						//go after this
						this.SetDoors.RemoveKey("\"" + direction + "\"");
						return str;
						}
					}
				}
			
			return "";
			}
		
		private void RenderOriginPoint()
			{
			if(RenderDevice == null || RenderDevice.Graphics == null)	return;
			Pen originPen = new Pen(Color.Yellow);
			//draw circle
			int size = Globals.ImageBoxProperties.width;
			Rectangle rect = new Rectangle(-size,-size,size,size);
			int xoffset = 100;
			int yoffset = 100;
			rect.X -= DomainRenderer.GetCameraLocation().X - xoffset;
			rect.Y -= DomainRenderer.GetCameraLocation().Y - yoffset;
			RenderDevice.Graphics.DrawEllipse(originPen,rect);
			
			//draw cross
			Point h1 = new Point(rect.Left, rect.Y + (rect.Height/2));
			Point h2 = new Point(rect.Right, rect.Y + (rect.Height/2));
			
			Point v1 = new Point(rect.X + (rect.Width/2), rect.Top);
			Point v2 = new Point(rect.X + (rect.Width/2), rect.Bottom);
			RenderDevice.Graphics.DrawLine(originPen,v1,v2);
			RenderDevice.Graphics.DrawLine(originPen,h1,h2);
			}

        //void customRadioButton_CheckedChanged( object sender, EventArgs e )
        //    {
        //    //we care cehcking for the actual radio button within the Stellarmap.Radio control
        //    //since that is the one sending the message
        //    RadioButton radioButton = sender as RadioButton;
        //    if (radioButton != null && radioButton.Checked)
        //        {
        //        radioButton.Checked = true;

        //        List<Radio> buttons = new List<Radio>();
        //        foreach(Control con in this.RoomType.Controls)
        //            {
        //            Radio t = con as Radio;
        //            if(t != null && t.InnerRadio != radioButton)
        //                {
        //                t.Checked = false;
        //                }
        //            }
        //        /*if (radioButton.Checked)
        //            {
        //            radioButton.Checked = false;
        //            //checkedRadioButton = radioButton;
        //            }
        //        else {radioButton.Checked = false;};//if (checkedRadioButton == radioButton)
        //            //{
        //            //checkedRadioButton = null;
        //            //}*/
        //        }
        //    }
		
		#endregion
		}
	}