namespace Stellarmap
	{
	partial class DomainViewForm
		{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
			{
			if (disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateRoomQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateRoomComplete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuCreateDoor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateObject = new System.Windows.Forms.ToolStripMenuItem();
            this.armorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.npcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importExitingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.domainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.domainSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToOriginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpTips = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.WorkAreaContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddRoomContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRoomCompleteContextMeruItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ImportRoomContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoomContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesTab = new System.Windows.Forms.TabPage();
            this.SetBlockNPCs = new Stellarmap.Check();
            this.SetRespirationType = new Stellarmap.CheckList();
            this.SetTerrainType = new Stellarmap.CheckList();
            this.SetMedium = new Stellarmap.ComboSelection();
            this.SetObviousExits = new Stellarmap.TextEntry();
            this.checkList1 = new Stellarmap.CheckList();
            this.SetTown = new Stellarmap.TextEntry();
            this.SetGravity = new Stellarmap.NumberEntry();
            this.SetTracksAllowed = new Stellarmap.Check();
            this.ExitsTab = new System.Windows.Forms.TabPage();
            this.SetDoors = new Stellarmap.DoubleComboMap();
            this.SetExits = new Stellarmap.DoubleComboMap();
            this.SetEnters = new Stellarmap.DoubleComboMap();
            this.DetailsTab = new System.Windows.Forms.TabPage();
            this.SetListen = new Stellarmap.ComboMapBuilder();
            this.SetSmell = new Stellarmap.ComboMapBuilder();
            this.SetSearch = new Stellarmap.ComboMapBuilder();
            this.SetRead = new Stellarmap.ComboMapBuilder();
            this.ContentTab = new System.Windows.Forms.TabPage();
            this.SetInventory = new Stellarmap.ComboMapBuilder();
            this.SetItems = new Stellarmap.MapBuilder();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.RoomIconComboBox = new System.Windows.Forms.ComboBox();
            this.icon = new System.Windows.Forms.Label();
            this.EnableDayNight = new System.Windows.Forms.CheckBox();
            this.SetNightLong = new Stellarmap.TextDump();
            this.SetDayLong = new Stellarmap.TextDump();
            this.SetLong = new Stellarmap.TextDump();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SetNightLight = new Stellarmap.NumberEntry();
            this.SetDayLight = new Stellarmap.NumberEntry();
            this.SetClimate = new Stellarmap.ComboSelection();
            this.SetShort = new Stellarmap.TextEntry();
            this.SetLight = new Stellarmap.NumberEntry();
            this.FileName = new System.Windows.Forms.TextBox();
            this.lable_filename = new System.Windows.Forms.Label();
            this.RoomTabPanel = new System.Windows.Forms.TabControl();
            this.textboxDomainName = new System.Windows.Forms.TextBox();
            this.RenderTarget = new Stellarmap.RenderTarget();
            this.MainMenu.SuspendLayout();
            this.WorkAreaContextMenu.SuspendLayout();
            this.RoomContextMenu.SuspendLayout();
            this.PropertiesTab.SuspendLayout();
            this.ExitsTab.SuspendLayout();
            this.DetailsTab.SuspendLayout();
            this.ContentTab.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.RoomTabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuOptions,
            this.viewToolStripMenuItem,
            this.menuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(818, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "Main Menu";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileQuit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            // 
            // menuFileNew
            // 
            this.menuFileNew.Name = "menuFileNew";
            this.menuFileNew.Size = new System.Drawing.Size(103, 22);
            this.menuFileNew.Text = "&New";
            this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size(103, 22);
            this.menuFileOpen.Text = "&Open";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Size = new System.Drawing.Size(103, 22);
            this.menuFileSave.Text = "&Save";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileQuit
            // 
            this.menuFileQuit.Name = "menuFileQuit";
            this.menuFileQuit.Size = new System.Drawing.Size(103, 22);
            this.menuFileQuit.Text = "&Quit";
            this.menuFileQuit.Click += new System.EventHandler(this.menuFileQuit_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditCreate,
            this.importExitingToolStripMenuItem});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "&Edit";
            // 
            // menuEditCreate
            // 
            this.menuEditCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreateRoomQuick,
            this.menuCreateRoomComplete,
            this.toolStripSeparator1,
            this.menuCreateDoor,
            this.menuCreateObject,
            this.armorToolStripMenuItem,
            this.npcToolStripMenuItem});
            this.menuEditCreate.Name = "menuEditCreate";
            this.menuEditCreate.Size = new System.Drawing.Size(148, 22);
            this.menuEditCreate.Text = "Create &New";
            // 
            // menuCreateRoomQuick
            // 
            this.menuCreateRoomQuick.Name = "menuCreateRoomQuick";
            this.menuCreateRoomQuick.Size = new System.Drawing.Size(146, 22);
            this.menuCreateRoomQuick.Text = "&Room (quick)";
            this.menuCreateRoomQuick.Click += new System.EventHandler(this.menuCreateRoomQuick_Click);
            // 
            // menuCreateRoomComplete
            // 
            this.menuCreateRoomComplete.Name = "menuCreateRoomComplete";
            this.menuCreateRoomComplete.Size = new System.Drawing.Size(146, 22);
            this.menuCreateRoomComplete.Text = "&Room";
            this.menuCreateRoomComplete.Click += new System.EventHandler(this.menuCreateRoomComplete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // menuCreateDoor
            // 
            this.menuCreateDoor.Name = "menuCreateDoor";
            this.menuCreateDoor.Size = new System.Drawing.Size(146, 22);
            this.menuCreateDoor.Text = "&Door";
            this.menuCreateDoor.Click += new System.EventHandler(this.menuCreateDoor_Click);
            // 
            // menuCreateObject
            // 
            this.menuCreateObject.Name = "menuCreateObject";
            this.menuCreateObject.Size = new System.Drawing.Size(146, 22);
            this.menuCreateObject.Text = "&Item";
            // 
            // armorToolStripMenuItem
            // 
            this.armorToolStripMenuItem.Name = "armorToolStripMenuItem";
            this.armorToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.armorToolStripMenuItem.Text = "&Armor";
            // 
            // npcToolStripMenuItem
            // 
            this.npcToolStripMenuItem.Name = "npcToolStripMenuItem";
            this.npcToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.npcToolStripMenuItem.Text = "&Npc";
            // 
            // importExitingToolStripMenuItem
            // 
            this.importExitingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomToolStripMenuItem,
            this.itemToolStripMenuItem,
            this.toolStripSeparator3,
            this.domainToolStripMenuItem});
            this.importExitingToolStripMenuItem.Name = "importExitingToolStripMenuItem";
            this.importExitingToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.importExitingToolStripMenuItem.Text = "&Import Exiting";
            // 
            // roomToolStripMenuItem
            // 
            this.roomToolStripMenuItem.Name = "roomToolStripMenuItem";
            this.roomToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.roomToolStripMenuItem.Text = "&Room";
            this.roomToolStripMenuItem.Click += new System.EventHandler(this.roomToolStripMenuItem_Click);
            // 
            // itemToolStripMenuItem
            // 
            this.itemToolStripMenuItem.Name = "itemToolStripMenuItem";
            this.itemToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.itemToolStripMenuItem.Text = "&Item";
            this.itemToolStripMenuItem.Click += new System.EventHandler(this.itemToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // domainToolStripMenuItem
            // 
            this.domainToolStripMenuItem.Name = "domainToolStripMenuItem";
            this.domainToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.domainToolStripMenuItem.Text = "&External Domain Reference";
            this.domainToolStripMenuItem.Click += new System.EventHandler(this.domainToolStripMenuItem_Click);
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.domainSettingsToolStripMenuItem});
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(61, 20);
            this.menuOptions.Text = "&Options";
            // 
            // domainSettingsToolStripMenuItem
            // 
            this.domainSettingsToolStripMenuItem.Name = "domainSettingsToolStripMenuItem";
            this.domainSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.domainSettingsToolStripMenuItem.Text = "Domain Settings";
            this.domainSettingsToolStripMenuItem.Click += new System.EventHandler(this.domainSettingsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToOriginToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // moveToOriginToolStripMenuItem
            // 
            this.moveToOriginToolStripMenuItem.Name = "moveToOriginToolStripMenuItem";
            this.moveToOriginToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.moveToOriginToolStripMenuItem.Text = "Move To Origin";
            this.moveToOriginToolStripMenuItem.Click += new System.EventHandler(this.moveToOriginToolStripMenuItem_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpTips,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpTips
            // 
            this.menuHelpTips.Name = "menuHelpTips";
            this.menuHelpTips.Size = new System.Drawing.Size(107, 22);
            this.menuHelpTips.Text = "Help";
            this.menuHelpTips.Click += new System.EventHandler(this.menuHelpTips_Click);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.menuHelpAbout.Text = "About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // WorkAreaContextMenu
            // 
            this.WorkAreaContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddRoomContextMenuItem,
            this.AddRoomCompleteContextMeruItem,
            this.toolStripSeparator2,
            this.ImportRoomContextMenuItem});
            this.WorkAreaContextMenu.Name = "WorkAreaContextMenu";
            this.WorkAreaContextMenu.Size = new System.Drawing.Size(174, 76);
            // 
            // AddRoomContextMenuItem
            // 
            this.AddRoomContextMenuItem.Name = "AddRoomContextMenuItem";
            this.AddRoomContextMenuItem.Size = new System.Drawing.Size(173, 22);
            this.AddRoomContextMenuItem.Text = "Add Room (&Quick)";
            this.AddRoomContextMenuItem.Click += new System.EventHandler(this.AddRoomContextMenuItem_Click);
            // 
            // AddRoomCompleteContextMeruItem
            // 
            this.AddRoomCompleteContextMeruItem.Name = "AddRoomCompleteContextMeruItem";
            this.AddRoomCompleteContextMeruItem.Size = new System.Drawing.Size(173, 22);
            this.AddRoomCompleteContextMeruItem.Text = "Add &Room";
            this.AddRoomCompleteContextMeruItem.Click += new System.EventHandler(this.AddRoomCompleteContextMeruItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // ImportRoomContextMenuItem
            // 
            this.ImportRoomContextMenuItem.Name = "ImportRoomContextMenuItem";
            this.ImportRoomContextMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ImportRoomContextMenuItem.Text = "&Import Room";
            this.ImportRoomContextMenuItem.Click += new System.EventHandler(this.ImportRoomContextMenuItem_Click);
            // 
            // RoomContextMenu
            // 
            this.RoomContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeRoomToolStripMenuItem});
            this.RoomContextMenu.Name = "RoomContextMenu";
            this.RoomContextMenu.Size = new System.Drawing.Size(143, 26);
            this.RoomContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.RoomContextMenu_Opening);
            // 
            // removeRoomToolStripMenuItem
            // 
            this.removeRoomToolStripMenuItem.Name = "removeRoomToolStripMenuItem";
            this.removeRoomToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.removeRoomToolStripMenuItem.Text = "Delete Room";
            this.removeRoomToolStripMenuItem.Click += new System.EventHandler(this.removeRoomToolStripMenuItem_Click);
            // 
            // PropertiesTab
            // 
            this.PropertiesTab.AutoScroll = true;
            this.PropertiesTab.Controls.Add(this.SetRespirationType);
            this.PropertiesTab.Controls.Add(this.SetTerrainType);
            this.PropertiesTab.Controls.Add(this.SetMedium);
            this.PropertiesTab.Controls.Add(this.SetObviousExits);
            this.PropertiesTab.Controls.Add(this.checkList1);
            this.PropertiesTab.Controls.Add(this.SetTown);
            this.PropertiesTab.Controls.Add(this.SetGravity);
            this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
            this.PropertiesTab.Name = "PropertiesTab";
            this.PropertiesTab.Size = new System.Drawing.Size(278, 510);
            this.PropertiesTab.TabIndex = 2;
            this.PropertiesTab.Text = "Properties";
            this.PropertiesTab.UseVisualStyleBackColor = true;
            // 
            // SetBlockNPCs
            // 
            this.SetBlockNPCs.BackColor = System.Drawing.Color.Transparent;
            this.SetBlockNPCs.Checked = false;
            this.SetBlockNPCs.FunctionName = "SetBlockNPCs";
            this.SetBlockNPCs.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetBlockNPCs.LabelText = "Block NPCs From Entering";
            this.SetBlockNPCs.Location = new System.Drawing.Point(3, 83);
            this.SetBlockNPCs.Name = "SetBlockNPCs";
            this.SetBlockNPCs.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetBlockNPCs.RequiredHeader = null;
            this.SetBlockNPCs.Size = new System.Drawing.Size(156, 22);
            this.SetBlockNPCs.TabIndex = 24;
            this.SetBlockNPCs.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetRespirationType
            // 
            this.SetRespirationType.BackColor = System.Drawing.Color.Transparent;
            this.SetRespirationType.CheckListStrings = new string[0];
            this.SetRespirationType.FunctionName = "SetRespirationType";
            this.SetRespirationType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetRespirationType.LabelText = "Respiration Types:";
            this.SetRespirationType.Location = new System.Drawing.Point(3, 500);
            this.SetRespirationType.Name = "SetRespirationType";
            this.SetRespirationType.ParameterType = Stellarmap.FuncParamType.ORList;
            this.SetRespirationType.RequiredHeader = "respiration_types.h";
            this.SetRespirationType.Size = new System.Drawing.Size(250, 176);
            this.SetRespirationType.TabIndex = 23;
            this.SetRespirationType.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetTerrainType
            // 
            this.SetTerrainType.BackColor = System.Drawing.Color.Transparent;
            this.SetTerrainType.CheckListStrings = new string[0];
            this.SetTerrainType.FunctionName = "SetTerrainType";
            this.SetTerrainType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetTerrainType.LabelText = "Terrain Types";
            this.SetTerrainType.Location = new System.Drawing.Point(3, 287);
            this.SetTerrainType.Name = "SetTerrainType";
            this.SetTerrainType.ParameterType = Stellarmap.FuncParamType.ORList;
            this.SetTerrainType.RequiredHeader = "terrain_types.h";
            this.SetTerrainType.Size = new System.Drawing.Size(266, 176);
            this.SetTerrainType.TabIndex = 22;
            this.SetTerrainType.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetMedium
            // 
            this.SetMedium.BackColor = System.Drawing.Color.Transparent;
            this.SetMedium.EntryText = "";
            this.SetMedium.FunctionName = "SetMedium";
            this.SetMedium.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetMedium.LabelText = "Medium:";
            this.SetMedium.ListTextCollection = new string[0];
            this.SetMedium.Location = new System.Drawing.Point(3, 468);
            this.SetMedium.Name = "SetMedium";
            this.SetMedium.ParameterType = Stellarmap.FuncParamType.Raw;
            this.SetMedium.RequiredHeader = "medium.h";
            this.SetMedium.SelectedIndex = -1;
            this.SetMedium.Size = new System.Drawing.Size(190, 26);
            this.SetMedium.TabIndex = 21;
            this.SetMedium.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetObviousExits
            // 
            this.SetObviousExits.BackColor = System.Drawing.Color.Transparent;
            this.SetObviousExits.EntryText = "";
            this.SetObviousExits.FunctionName = "SetObviousExits";
            this.SetObviousExits.KeyType = Stellarmap.EntryType.Mixed;
            this.SetObviousExits.LabelText = "Obvious Exits:";
            this.SetObviousExits.Location = new System.Drawing.Point(9, 57);
            this.SetObviousExits.Name = "SetObviousExits";
            this.SetObviousExits.ParameterType = Stellarmap.FuncParamType.String;
            this.SetObviousExits.RequiredHeader = null;
            this.SetObviousExits.Size = new System.Drawing.Size(194, 26);
            this.SetObviousExits.TabIndex = 19;
            this.SetObviousExits.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // checkList1
            // 
            this.checkList1.BackColor = System.Drawing.Color.Transparent;
            this.checkList1.CheckListStrings = new string[] {
        "\"no attack\"",
        "\"no steal\"",
        "\"no magic\"",
        "\"no bump\"",
        "\"no paralyze\"",
        "\"no teleport\"",
        "\"no clear\""};
            this.checkList1.FunctionName = "SetProperties";
            this.checkList1.KeyType = Stellarmap.EntryType.Strings;
            this.checkList1.LabelText = "Room Properties:";
            this.checkList1.Location = new System.Drawing.Point(3, 111);
            this.checkList1.Name = "checkList1";
            this.checkList1.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.checkList1.RequiredHeader = null;
            this.checkList1.Size = new System.Drawing.Size(266, 176);
            this.checkList1.TabIndex = 18;
            this.checkList1.Tag = "\"\"";
            this.checkList1.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetTown
            // 
            this.SetTown.BackColor = System.Drawing.Color.Transparent;
            this.SetTown.EntryText = "";
            this.SetTown.FunctionName = "SetTown";
            this.SetTown.KeyType = Stellarmap.EntryType.Mixed;
            this.SetTown.LabelText = "Town:";
            this.SetTown.Location = new System.Drawing.Point(9, 30);
            this.SetTown.Name = "SetTown";
            this.SetTown.ParameterType = Stellarmap.FuncParamType.String;
            this.SetTown.RequiredHeader = null;
            this.SetTown.Size = new System.Drawing.Size(146, 26);
            this.SetTown.TabIndex = 17;
            this.SetTown.Tag = "\"none\"";
            this.SetTown.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetGravity
            // 
            this.SetGravity.BackColor = System.Drawing.Color.Transparent;
            this.SetGravity.DecimalPlaces = 4;
            this.SetGravity.EntryIncrement = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.SetGravity.EntryMaxmimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetGravity.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetGravity.EntryValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetGravity.FunctionName = "SetGravity";
            this.SetGravity.KeyType = Stellarmap.EntryType.Mixed;
            this.SetGravity.LabelText = "Gravity:";
            this.SetGravity.Location = new System.Drawing.Point(9, 3);
            this.SetGravity.Name = "SetGravity";
            this.SetGravity.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetGravity.RequiredHeader = null;
            this.SetGravity.Size = new System.Drawing.Size(152, 26);
            this.SetGravity.TabIndex = 16;
            this.SetGravity.Tag = "1";
            this.SetGravity.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetTracksAllowed
            // 
            this.SetTracksAllowed.BackColor = System.Drawing.Color.Transparent;
            this.SetTracksAllowed.Checked = true;
            this.SetTracksAllowed.FunctionName = "SetTracksAllowed";
            this.SetTracksAllowed.KeyType = Stellarmap.EntryType.Mixed;
            this.SetTracksAllowed.LabelText = "Leave Tracks";
            this.SetTracksAllowed.Location = new System.Drawing.Point(165, 83);
            this.SetTracksAllowed.Name = "SetTracksAllowed";
            this.SetTracksAllowed.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetTracksAllowed.RequiredHeader = null;
            this.SetTracksAllowed.Size = new System.Drawing.Size(100, 22);
            this.SetTracksAllowed.TabIndex = 23;
            this.SetTracksAllowed.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // ExitsTab
            // 
            this.ExitsTab.AutoScroll = true;
            this.ExitsTab.Controls.Add(this.SetDoors);
            this.ExitsTab.Controls.Add(this.SetExits);
            this.ExitsTab.Controls.Add(this.SetEnters);
            this.ExitsTab.Location = new System.Drawing.Point(4, 22);
            this.ExitsTab.Name = "ExitsTab";
            this.ExitsTab.Size = new System.Drawing.Size(278, 510);
            this.ExitsTab.TabIndex = 3;
            this.ExitsTab.Text = "Exits";
            this.ExitsTab.UseVisualStyleBackColor = true;
            // 
            // SetDoors
            // 
            this.SetDoors.BackColor = System.Drawing.Color.Transparent;
            this.SetDoors.FunctionName = "SetDoors";
            this.SetDoors.KeyCollection = new string[0];
            this.SetDoors.KeyEntry = "";
            this.SetDoors.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDoors.LabelText = "Doors";
            this.SetDoors.ListTextCollection = new string[0];
            this.SetDoors.Location = new System.Drawing.Point(3, 437);
            this.SetDoors.Name = "SetDoors";
            this.SetDoors.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetDoors.RequiredHeader = null;
            this.SetDoors.Size = new System.Drawing.Size(229, 211);
            this.SetDoors.TabIndex = 16;
            this.SetDoors.ValueCollection = new string[0];
            this.SetDoors.ValueEntry = "";
            this.SetDoors.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetExits
            // 
            this.SetExits.BackColor = System.Drawing.Color.Transparent;
            this.SetExits.FunctionName = "SetExits";
            this.SetExits.KeyCollection = new string[0];
            this.SetExits.KeyEntry = "";
            this.SetExits.KeyType = Stellarmap.EntryType.Mixed;
            this.SetExits.LabelText = "Exits";
            this.SetExits.ListTextCollection = new string[0];
            this.SetExits.Location = new System.Drawing.Point(3, 220);
            this.SetExits.Name = "SetExits";
            this.SetExits.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetExits.RequiredHeader = null;
            this.SetExits.Size = new System.Drawing.Size(229, 211);
            this.SetExits.TabIndex = 15;
            this.SetExits.Tag = "\"\"";
            this.SetExits.ValueCollection = new string[0];
            this.SetExits.ValueEntry = "";
            this.SetExits.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetEnters
            // 
            this.SetEnters.BackColor = System.Drawing.Color.Transparent;
            this.SetEnters.FunctionName = "SetEnters";
            this.SetEnters.KeyCollection = new string[0];
            this.SetEnters.KeyEntry = "";
            this.SetEnters.KeyType = Stellarmap.EntryType.Mixed;
            this.SetEnters.LabelText = "Enterables";
            this.SetEnters.ListTextCollection = new string[0];
            this.SetEnters.Location = new System.Drawing.Point(3, 3);
            this.SetEnters.Name = "SetEnters";
            this.SetEnters.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetEnters.RequiredHeader = null;
            this.SetEnters.Size = new System.Drawing.Size(229, 211);
            this.SetEnters.TabIndex = 14;
            this.SetEnters.Tag = "\"\"";
            this.SetEnters.ValueCollection = new string[0];
            this.SetEnters.ValueEntry = "";
            this.SetEnters.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // DetailsTab
            // 
            this.DetailsTab.AutoScroll = true;
            this.DetailsTab.Controls.Add(this.SetListen);
            this.DetailsTab.Controls.Add(this.SetSmell);
            this.DetailsTab.Controls.Add(this.SetSearch);
            this.DetailsTab.Controls.Add(this.SetRead);
            this.DetailsTab.Location = new System.Drawing.Point(4, 22);
            this.DetailsTab.Name = "DetailsTab";
            this.DetailsTab.Size = new System.Drawing.Size(278, 510);
            this.DetailsTab.TabIndex = 4;
            this.DetailsTab.Text = "Room Details";
            this.DetailsTab.UseVisualStyleBackColor = true;
            // 
            // SetListen
            // 
            this.SetListen.BackColor = System.Drawing.Color.Transparent;
            this.SetListen.EntryText = "";
            this.SetListen.FunctionName = "SetListen";
            this.SetListen.KeyCollection = new string[] {
        "default"};
            this.SetListen.KeyType = Stellarmap.EntryType.Mixed;
            this.SetListen.LabelText = "Sounds";
            this.SetListen.ListTextCollection = new string[0];
            this.SetListen.Location = new System.Drawing.Point(3, 651);
            this.SetListen.Name = "SetListen";
            this.SetListen.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetListen.RequiredHeader = null;
            this.SetListen.Size = new System.Drawing.Size(229, 211);
            this.SetListen.TabIndex = 13;
            this.SetListen.Tag = "\"\"";
            this.SetListen.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetSmell
            // 
            this.SetSmell.BackColor = System.Drawing.Color.Transparent;
            this.SetSmell.EntryText = "";
            this.SetSmell.FunctionName = "SetSmell";
            this.SetSmell.KeyCollection = new string[] {
        "default"};
            this.SetSmell.KeyType = Stellarmap.EntryType.Mixed;
            this.SetSmell.LabelText = "Smells";
            this.SetSmell.ListTextCollection = new string[0];
            this.SetSmell.Location = new System.Drawing.Point(3, 437);
            this.SetSmell.Name = "SetSmell";
            this.SetSmell.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetSmell.RequiredHeader = null;
            this.SetSmell.Size = new System.Drawing.Size(229, 211);
            this.SetSmell.TabIndex = 12;
            this.SetSmell.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetSearch
            // 
            this.SetSearch.BackColor = System.Drawing.Color.Transparent;
            this.SetSearch.EntryText = "";
            this.SetSearch.FunctionName = "SetSearch";
            this.SetSearch.KeyCollection = new string[0];
            this.SetSearch.KeyType = Stellarmap.EntryType.Mixed;
            this.SetSearch.LabelText = "Searchables";
            this.SetSearch.ListTextCollection = new string[0];
            this.SetSearch.Location = new System.Drawing.Point(3, 3);
            this.SetSearch.Name = "SetSearch";
            this.SetSearch.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetSearch.RequiredHeader = null;
            this.SetSearch.Size = new System.Drawing.Size(229, 211);
            this.SetSearch.TabIndex = 11;
            this.SetSearch.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetRead
            // 
            this.SetRead.BackColor = System.Drawing.Color.Transparent;
            this.SetRead.EntryText = "";
            this.SetRead.FunctionName = "SetRead";
            this.SetRead.KeyCollection = new string[0];
            this.SetRead.KeyType = Stellarmap.EntryType.Mixed;
            this.SetRead.LabelText = "Readables";
            this.SetRead.ListTextCollection = new string[0];
            this.SetRead.Location = new System.Drawing.Point(3, 220);
            this.SetRead.Name = "SetRead";
            this.SetRead.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetRead.RequiredHeader = null;
            this.SetRead.Size = new System.Drawing.Size(229, 211);
            this.SetRead.TabIndex = 11;
            this.SetRead.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // ContentTab
            // 
            this.ContentTab.AutoScroll = true;
            this.ContentTab.Controls.Add(this.SetInventory);
            this.ContentTab.Controls.Add(this.SetItems);
            this.ContentTab.Location = new System.Drawing.Point(4, 22);
            this.ContentTab.Name = "ContentTab";
            this.ContentTab.Padding = new System.Windows.Forms.Padding(3);
            this.ContentTab.Size = new System.Drawing.Size(278, 510);
            this.ContentTab.TabIndex = 1;
            this.ContentTab.Text = "Content";
            this.ContentTab.UseVisualStyleBackColor = true;
            // 
            // SetInventory
            // 
            this.SetInventory.BackColor = System.Drawing.Color.Transparent;
            this.SetInventory.EntryText = "";
            this.SetInventory.FunctionName = "SetInventory";
            this.SetInventory.KeyCollection = new string[0];
            this.SetInventory.KeyType = Stellarmap.EntryType.Mixed;
            this.SetInventory.LabelText = "Inventory (interactive)";
            this.SetInventory.ListTextCollection = new string[0];
            this.SetInventory.Location = new System.Drawing.Point(3, 3);
            this.SetInventory.Name = "SetInventory";
            this.SetInventory.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetInventory.RequiredHeader = null;
            this.SetInventory.Size = new System.Drawing.Size(229, 211);
            this.SetInventory.TabIndex = 10;
            this.SetInventory.Tag = "\"\"";
            this.SetInventory.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetItems
            // 
            this.SetItems.BackColor = System.Drawing.Color.Transparent;
            this.SetItems.FunctionName = "SetItems";
            this.SetItems.KeyType = Stellarmap.EntryType.Mixed;
            this.SetItems.LabelText = "Static Items (non-interactive)";
            this.SetItems.ListTextCollection = new string[0];
            this.SetItems.Location = new System.Drawing.Point(3, 220);
            this.SetItems.Name = "SetItems";
            this.SetItems.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetItems.RequiredHeader = null;
            this.SetItems.Size = new System.Drawing.Size(227, 209);
            this.SetItems.TabIndex = 9;
            this.SetItems.Tag = "\"\"";
            this.SetItems.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // GeneralTab
            // 
            this.GeneralTab.AutoScroll = true;
            this.GeneralTab.Controls.Add(this.RoomIconComboBox);
            this.GeneralTab.Controls.Add(this.icon);
            this.GeneralTab.Controls.Add(this.EnableDayNight);
            this.GeneralTab.Controls.Add(this.SetNightLong);
            this.GeneralTab.Controls.Add(this.SetDayLong);
            this.GeneralTab.Controls.Add(this.SetLong);
            this.GeneralTab.Controls.Add(this.groupBox1);
            this.GeneralTab.Location = new System.Drawing.Point(4, 22);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTab.Size = new System.Drawing.Size(278, 510);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "General";
            this.GeneralTab.UseVisualStyleBackColor = true;
            // 
            // RoomIconComboBox
            // 
            this.RoomIconComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RoomIconComboBox.FormattingEnabled = true;
            this.RoomIconComboBox.Location = new System.Drawing.Point(46, 480);
            this.RoomIconComboBox.Name = "RoomIconComboBox";
            this.RoomIconComboBox.Size = new System.Drawing.Size(169, 21);
            this.RoomIconComboBox.TabIndex = 7;
            this.RoomIconComboBox.Tag = "";
            this.RoomIconComboBox.SelectedIndexChanged += new System.EventHandler(this.RoomIcon_SelectedIndexChanged);
            // 
            // icon
            // 
            this.icon.AutoSize = true;
            this.icon.Location = new System.Drawing.Point(9, 483);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(31, 13);
            this.icon.TabIndex = 15;
            this.icon.Text = "Icon:";
            // 
            // EnableDayNight
            // 
            this.EnableDayNight.AutoSize = true;
            this.EnableDayNight.Location = new System.Drawing.Point(140, 283);
            this.EnableDayNight.Name = "EnableDayNight";
            this.EnableDayNight.Size = new System.Drawing.Size(117, 17);
            this.EnableDayNight.TabIndex = 4;
            this.EnableDayNight.Tag = "false";
            this.EnableDayNight.Text = "Enable Day / Night";
            this.EnableDayNight.UseVisualStyleBackColor = true;
            this.EnableDayNight.CheckedChanged += new System.EventHandler(this.EnableDayNight_CheckedChanged);
            // 
            // SetNightLong
            // 
            this.SetNightLong.BackColor = System.Drawing.Color.Transparent;
            this.SetNightLong.Enabled = false;
            this.SetNightLong.EntryText = "";
            this.SetNightLong.FunctionName = "SetNightLong";
            this.SetNightLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetNightLong.LabelText = "Night Description (optional)";
            this.SetNightLong.Location = new System.Drawing.Point(3, 386);
            this.SetNightLong.Name = "SetNightLong";
            this.SetNightLong.ParameterType = Stellarmap.FuncParamType.String;
            this.SetNightLong.RequiredHeader = null;
            this.SetNightLong.Size = new System.Drawing.Size(254, 101);
            this.SetNightLong.TabIndex = 6;
            this.SetNightLong.Tag = "\"\"";
            this.SetNightLong.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetDayLong
            // 
            this.SetDayLong.BackColor = System.Drawing.Color.Transparent;
            this.SetDayLong.Enabled = false;
            this.SetDayLong.EntryText = "";
            this.SetDayLong.FunctionName = "SetDayLong";
            this.SetDayLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDayLong.LabelText = "Day Description (optional)";
            this.SetDayLong.Location = new System.Drawing.Point(3, 298);
            this.SetDayLong.Name = "SetDayLong";
            this.SetDayLong.ParameterType = Stellarmap.FuncParamType.String;
            this.SetDayLong.RequiredHeader = null;
            this.SetDayLong.Size = new System.Drawing.Size(254, 101);
            this.SetDayLong.TabIndex = 5;
            this.SetDayLong.Tag = "\"\"";
            this.SetDayLong.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetLong
            // 
            this.SetLong.BackColor = System.Drawing.Color.Transparent;
            this.SetLong.EntryText = "An empty room.";
            this.SetLong.FunctionName = "SetLong";
            this.SetLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetLong.LabelText = "Long Description";
            this.SetLong.Location = new System.Drawing.Point(6, 191);
            this.SetLong.Name = "SetLong";
            this.SetLong.ParameterType = Stellarmap.FuncParamType.String;
            this.SetLong.RequiredHeader = null;
            this.SetLong.Size = new System.Drawing.Size(254, 99);
            this.SetLong.TabIndex = 3;
            this.SetLong.Tag = "\"An empty room.\"";
            this.SetLong.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SetNightLight);
            this.groupBox1.Controls.Add(this.SetDayLight);
            this.groupBox1.Controls.Add(this.SetClimate);
            this.groupBox1.Controls.Add(this.SetShort);
            this.groupBox1.Controls.Add(this.SetLight);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // SetNightLight
            // 
            this.SetNightLight.BackColor = System.Drawing.Color.Transparent;
            this.SetNightLight.DecimalPlaces = 0;
            this.SetNightLight.Enabled = false;
            this.SetNightLight.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetNightLight.EntryMaxmimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetNightLight.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetNightLight.EntryValue = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.SetNightLight.FunctionName = "SetNightLight";
            this.SetNightLight.KeyType = Stellarmap.EntryType.Mixed;
            this.SetNightLight.LabelText = "Night Light:";
            this.SetNightLight.Location = new System.Drawing.Point(6, 115);
            this.SetNightLight.Name = "SetNightLight";
            this.SetNightLight.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetNightLight.RequiredHeader = null;
            this.SetNightLight.Size = new System.Drawing.Size(169, 26);
            this.SetNightLight.TabIndex = 10;
            this.SetNightLight.Tag = "26";
            this.SetNightLight.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetDayLight
            // 
            this.SetDayLight.BackColor = System.Drawing.Color.Transparent;
            this.SetDayLight.DecimalPlaces = 0;
            this.SetDayLight.Enabled = false;
            this.SetDayLight.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetDayLight.EntryMaxmimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetDayLight.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetDayLight.EntryValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.SetDayLight.FunctionName = "SetDayLight";
            this.SetDayLight.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDayLight.LabelText = "Day Light:";
            this.SetDayLight.Location = new System.Drawing.Point(6, 83);
            this.SetDayLight.Name = "SetDayLight";
            this.SetDayLight.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetDayLight.RequiredHeader = null;
            this.SetDayLight.Size = new System.Drawing.Size(163, 26);
            this.SetDayLight.TabIndex = 9;
            this.SetDayLight.Tag = "26";
            this.SetDayLight.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetClimate
            // 
            this.SetClimate.BackColor = System.Drawing.Color.Transparent;
            this.SetClimate.EntryText = "indoors";
            this.SetClimate.FunctionName = "SetClimate";
            this.SetClimate.KeyType = Stellarmap.EntryType.Mixed;
            this.SetClimate.LabelText = "Climate:";
            this.SetClimate.ListTextCollection = new string[] {
        "indoors",
        "temperate",
        "arid",
        "arctic",
        "tropical",
        "sub-tropical"};
            this.SetClimate.Location = new System.Drawing.Point(6, 147);
            this.SetClimate.Name = "SetClimate";
            this.SetClimate.ParameterType = Stellarmap.FuncParamType.String;
            this.SetClimate.RequiredHeader = null;
            this.SetClimate.SelectedIndex = 0;
            this.SetClimate.Size = new System.Drawing.Size(188, 26);
            this.SetClimate.TabIndex = 8;
            this.SetClimate.Tag = "\"indoors\"";
            this.SetClimate.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetShort
            // 
            this.SetShort.BackColor = System.Drawing.Color.Transparent;
            this.SetShort.EntryText = "Empty Room";
            this.SetShort.FunctionName = "SetShort";
            this.SetShort.KeyType = Stellarmap.EntryType.Mixed;
            this.SetShort.LabelText = "Room Name:";
            this.SetShort.Location = new System.Drawing.Point(6, 19);
            this.SetShort.Name = "SetShort";
            this.SetShort.ParameterType = Stellarmap.FuncParamType.String;
            this.SetShort.RequiredHeader = null;
            this.SetShort.Size = new System.Drawing.Size(177, 26);
            this.SetShort.TabIndex = 1;
            this.SetShort.Tag = "\"Empty Room\"";
            this.SetShort.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetLight
            // 
            this.SetLight.BackColor = System.Drawing.Color.Transparent;
            this.SetLight.DecimalPlaces = 0;
            this.SetLight.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetLight.EntryMaxmimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetLight.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetLight.EntryValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.SetLight.FunctionName = "SetAmbientLight";
            this.SetLight.KeyType = Stellarmap.EntryType.Mixed;
            this.SetLight.LabelText = "Light:";
            this.SetLight.Location = new System.Drawing.Point(6, 51);
            this.SetLight.Name = "SetLight";
            this.SetLight.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetLight.RequiredHeader = null;
            this.SetLight.Size = new System.Drawing.Size(141, 26);
            this.SetLight.TabIndex = 2;
            this.SetLight.Tag = "26";
            this.SetLight.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // FileName
            // 
            this.FileName.BackColor = System.Drawing.Color.White;
            this.FileName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FileName.Location = new System.Drawing.Point(315, 27);
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Size = new System.Drawing.Size(191, 20);
            this.FileName.TabIndex = 1000;
            this.FileName.Tag = "\"\"";
            // 
            // lable_filename
            // 
            this.lable_filename.AutoSize = true;
            this.lable_filename.Location = new System.Drawing.Point(103, 30);
            this.lable_filename.Name = "lable_filename";
            this.lable_filename.Size = new System.Drawing.Size(60, 13);
            this.lable_filename.TabIndex = 3;
            this.lable_filename.Text = "File Name: ";
            // 
            // RoomTabPanel
            // 
            this.RoomTabPanel.Controls.Add(this.GeneralTab);
            this.RoomTabPanel.Controls.Add(this.ContentTab);
            this.RoomTabPanel.Controls.Add(this.DetailsTab);
            this.RoomTabPanel.Controls.Add(this.ExitsTab);
            this.RoomTabPanel.Controls.Add(this.PropertiesTab);
            this.RoomTabPanel.Enabled = false;
            this.RoomTabPanel.Location = new System.Drawing.Point(508, 27);
            this.RoomTabPanel.Name = "RoomTabPanel";
            this.RoomTabPanel.SelectedIndex = 0;
            this.RoomTabPanel.ShowToolTips = true;
            this.RoomTabPanel.Size = new System.Drawing.Size(286, 536);
            this.RoomTabPanel.TabIndex = 2;
            this.RoomTabPanel.Tag = "\"\"";
            // 
            // textboxDomainName
            // 
            this.textboxDomainName.BackColor = System.Drawing.Color.White;
            this.textboxDomainName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.textboxDomainName.Location = new System.Drawing.Point(158, 27);
            this.textboxDomainName.Name = "textboxDomainName";
            this.textboxDomainName.ReadOnly = true;
            this.textboxDomainName.Size = new System.Drawing.Size(151, 20);
            this.textboxDomainName.TabIndex = 1001;
            // 
            // RenderTarget
            // 
            this.RenderTarget.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.RenderTarget.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RenderTarget.Location = new System.Drawing.Point(12, 49);
            this.RenderTarget.Name = "RenderTarget";
            this.RenderTarget.Size = new System.Drawing.Size(206, 221);
            this.RenderTarget.TabIndex = 3;
            this.RenderTarget.ViewHeight = 221;
            this.RenderTarget.ViewWidth = 206;
            this.RenderTarget.ViewX = 0;
            this.RenderTarget.ViewY = 0;
            // 
            // DomainViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(818, 588);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.RoomTabPanel);
            this.Controls.Add(this.RenderTarget);
            this.Controls.Add(this.textboxDomainName);
            this.Controls.Add(this.lable_filename);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "DomainViewForm";
            this.Text = "Stellarmap";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.WorkAreaContextMenu.ResumeLayout(false);
            this.RoomContextMenu.ResumeLayout(false);
            this.PropertiesTab.ResumeLayout(false);
            this.ExitsTab.ResumeLayout(false);
            this.DetailsTab.ResumeLayout(false);
            this.ContentTab.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.GeneralTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.RoomTabPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuFileNew;
		private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem menuFileSave;
		private System.Windows.Forms.ToolStripMenuItem menuFileQuit;
		private System.Windows.Forms.ToolStripMenuItem menuEdit;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem menuOptions;
		private System.Windows.Forms.ToolStripMenuItem menuHelpTips;
		private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
		private System.Windows.Forms.ToolStripMenuItem menuEditCreate;
		private System.Windows.Forms.ToolStripMenuItem menuCreateDoor;
		private System.Windows.Forms.ToolStripMenuItem menuCreateObject;
		private System.Windows.Forms.ContextMenuStrip WorkAreaContextMenu;
		private System.Windows.Forms.ToolStripMenuItem AddRoomContextMenuItem;
		private System.Windows.Forms.ContextMenuStrip RoomContextMenu;
		private System.Windows.Forms.ToolStripMenuItem removeRoomToolStripMenuItem;
		private RenderTarget RenderTarget;
		private System.Windows.Forms.TabPage PropertiesTab;
		private CheckList checkList1;
		private TextEntry SetTown;
		private NumberEntry SetGravity;
		private System.Windows.Forms.TabPage ExitsTab;
		private System.Windows.Forms.TabPage DetailsTab;
		private ComboMapBuilder SetListen;
		private ComboMapBuilder SetSmell;
		private System.Windows.Forms.TabPage ContentTab;
		private MapBuilder SetItems;
		private ComboMapBuilder SetInventory;
		private System.Windows.Forms.TabPage GeneralTab;
		private System.Windows.Forms.CheckBox EnableDayNight;
		private TextDump SetNightLong;
		private TextDump SetDayLong;
		private TextDump SetLong;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox FileName;
		private System.Windows.Forms.Label lable_filename;
		private ComboSelection SetClimate;
		private TextEntry SetShort;
		private NumberEntry SetLight;
		private System.Windows.Forms.TabControl RoomTabPanel;
		private System.Windows.Forms.ComboBox RoomIconComboBox;
		private System.Windows.Forms.Label icon;
		private Stellarmap.DoubleComboMap SetEnters;
		private Stellarmap.DoubleComboMap SetExits;
		private System.Windows.Forms.ToolStripMenuItem domainSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem moveToOriginToolStripMenuItem;
		private ComboMapBuilder SetSearch;
		private DoubleComboMap SetDoors;
		private ComboMapBuilder SetRead;
		private TextEntry SetObviousExits;
		private System.Windows.Forms.ToolStripMenuItem armorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem npcToolStripMenuItem;
		private System.Windows.Forms.TextBox textboxDomainName;
		private System.Windows.Forms.ToolStripMenuItem importExitingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem roomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem itemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuCreateRoomQuick;
		private System.Windows.Forms.ToolStripMenuItem menuCreateRoomComplete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem AddRoomCompleteContextMeruItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem ImportRoomContextMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem domainToolStripMenuItem;
		private ComboSelection SetMedium;
		private CheckList SetTerrainType;
        private Check SetTracksAllowed;
        private CheckList SetRespirationType;
        private NumberEntry SetNightLight;
        private NumberEntry SetDayLight;
        private Check SetBlockNPCs;			
		}
	}