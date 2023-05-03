namespace Stellarmap
	{
	partial class CreateNpcForm
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
			if(disposing && (components != null))
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
            this.TabView = new System.Windows.Forms.TabControl();
            this.MainTabPage = new System.Windows.Forms.TabPage();
            this.SetAdjectives = new Stellarmap.ListBuilder();
            this.SetId = new Stellarmap.ListBuilder();
            this.SetLong = new Stellarmap.TextDump();
            this.SetShort = new Stellarmap.TextEntry();
            this.SetKeyName = new Stellarmap.TextEntry();
            this.Properties = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SetDefaultLanguage = new Stellarmap.ComboSelection();
            this.SetNativeLanguage = new Stellarmap.ComboSelection();
            this.SetRace = new Stellarmap.ComboSelection();
            this.SetLevel = new Stellarmap.NumberEntry();
            this.SetClass = new Stellarmap.ComboSelection();
            this.SetGender = new Stellarmap.ComboSelection();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveLastAction = new System.Windows.Forms.Button();
            this.buttonAddNewAction = new System.Windows.Forms.Button();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.SetInventory = new Stellarmap.ComboMapBuilder();
            this.SetCurrency = new Stellarmap.ComboMapBuilder();
            this.CombatSettings = new System.Windows.Forms.TabPage();
            this.groupInventoryDrops = new System.Windows.Forms.GroupBox();
            this.SetDropInventoryOnDeath = new Stellarmap.Check();
            this.SetItemDropRates = new Stellarmap.ComboMapBuilder();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonAddNewCombatAction = new System.Windows.Forms.Button();
            this.buttonRemoveLastCombatAction = new System.Windows.Forms.Button();
            this.flowCombatActions = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetEncounter = new Stellarmap.TextEntry();
            this.SetUnique = new Stellarmap.Check();
            this.SetDie = new Stellarmap.TextEntry();
            this.SetMorality = new Stellarmap.NumberEntry();
            this.StatsTab = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboLanguageAdd = new System.Windows.Forms.ComboBox();
            this.comboLanguageRemove = new System.Windows.Forms.ComboBox();
            this.flowLanguages = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRemoveLanguage = new System.Windows.Forms.Button();
            this.buttonAddLanguage = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboSkillAdd = new System.Windows.Forms.ComboBox();
            this.comboSkillRemove = new System.Windows.Forms.ComboBox();
            this.flowSkills = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRemoveSkill = new System.Windows.Forms.Button();
            this.buttonAddNewSkill = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboStatAdd = new System.Windows.Forms.ComboBox();
            this.comboStatRemove = new System.Windows.Forms.ComboBox();
            this.flowStats = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRemoveStat = new System.Windows.Forms.Button();
            this.buttonAddNewStat = new System.Windows.Forms.Button();
            this.InheritTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TabView.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.Properties.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.CombatSettings.SuspendLayout();
            this.groupInventoryDrops.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.StatsTab.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabView
            // 
            this.TabView.Controls.Add(this.MainTabPage);
            this.TabView.Controls.Add(this.Properties);
            this.TabView.Controls.Add(this.CombatSettings);
            this.TabView.Controls.Add(this.StatsTab);
            this.TabView.Controls.Add(this.InheritTab);
            this.TabView.Location = new System.Drawing.Point(12, 42);
            this.TabView.Name = "TabView";
            this.TabView.SelectedIndex = 0;
            this.TabView.Size = new System.Drawing.Size(488, 518);
            this.TabView.TabIndex = 1;
            // 
            // MainTabPage
            // 
            this.MainTabPage.Controls.Add(this.SetAdjectives);
            this.MainTabPage.Controls.Add(this.SetId);
            this.MainTabPage.Controls.Add(this.SetLong);
            this.MainTabPage.Controls.Add(this.SetShort);
            this.MainTabPage.Controls.Add(this.SetKeyName);
            this.MainTabPage.Location = new System.Drawing.Point(4, 22);
            this.MainTabPage.Name = "MainTabPage";
            this.MainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainTabPage.Size = new System.Drawing.Size(480, 492);
            this.MainTabPage.TabIndex = 0;
            this.MainTabPage.Text = "General";
            this.MainTabPage.UseVisualStyleBackColor = true;
            // 
            // SetAdjectives
            // 
            this.SetAdjectives.BackColor = System.Drawing.Color.Transparent;
            this.SetAdjectives.FunctionName = "SetAdjectives";
            this.SetAdjectives.KeyType = Stellarmap.EntryType.Mixed;
            this.SetAdjectives.LabelText = "Adjectives";
            this.SetAdjectives.ListTextCollection = new string[0];
            this.SetAdjectives.Location = new System.Drawing.Point(244, 98);
            this.SetAdjectives.Name = "SetAdjectives";
            this.SetAdjectives.ParameterType = Stellarmap.FuncParamType.Array;
            this.SetAdjectives.RequiredHeader = null;
            this.SetAdjectives.Size = new System.Drawing.Size(200, 200);
            this.SetAdjectives.TabIndex = 19;
            this.SetAdjectives.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetId
            // 
            this.SetId.BackColor = System.Drawing.Color.Transparent;
            this.SetId.FunctionName = "SetId";
            this.SetId.KeyType = Stellarmap.EntryType.Mixed;
            this.SetId.LabelText = "IDs";
            this.SetId.ListTextCollection = new string[0];
            this.SetId.Location = new System.Drawing.Point(10, 98);
            this.SetId.Name = "SetId";
            this.SetId.ParameterType = Stellarmap.FuncParamType.Array;
            this.SetId.RequiredHeader = null;
            this.SetId.Size = new System.Drawing.Size(200, 200);
            this.SetId.TabIndex = 18;
            this.SetId.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetLong
            // 
            this.SetLong.BackColor = System.Drawing.Color.Transparent;
            this.SetLong.EntryText = "A generic npc.";
            this.SetLong.FunctionName = "SetLong";
            this.SetLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetLong.LabelText = "Long Description";
            this.SetLong.Location = new System.Drawing.Point(190, 6);
            this.SetLong.Name = "SetLong";
            this.SetLong.ParameterType = Stellarmap.FuncParamType.String;
            this.SetLong.RequiredHeader = null;
            this.SetLong.Size = new System.Drawing.Size(254, 101);
            this.SetLong.TabIndex = 17;
            this.SetLong.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetShort
            // 
            this.SetShort.BackColor = System.Drawing.Color.Transparent;
            this.SetShort.EntryText = "a generic npc";
            this.SetShort.FunctionName = "SetShort";
            this.SetShort.KeyType = Stellarmap.EntryType.Mixed;
            this.SetShort.LabelText = "Short Desc:";
            this.SetShort.Location = new System.Drawing.Point(13, 55);
            this.SetShort.Name = "SetShort";
            this.SetShort.ParameterType = Stellarmap.FuncParamType.String;
            this.SetShort.RequiredHeader = null;
            this.SetShort.Size = new System.Drawing.Size(171, 26);
            this.SetShort.TabIndex = 16;
            this.SetShort.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetKeyName
            // 
            this.SetKeyName.BackColor = System.Drawing.Color.Transparent;
            this.SetKeyName.EntryText = "generic npc";
            this.SetKeyName.FunctionName = "SetKeyName";
            this.SetKeyName.KeyType = Stellarmap.EntryType.Mixed;
            this.SetKeyName.LabelText = "Key Name:";
            this.SetKeyName.Location = new System.Drawing.Point(17, 23);
            this.SetKeyName.Name = "SetKeyName";
            this.SetKeyName.ParameterType = Stellarmap.FuncParamType.String;
            this.SetKeyName.RequiredHeader = null;
            this.SetKeyName.Size = new System.Drawing.Size(167, 26);
            this.SetKeyName.TabIndex = 15;
            this.SetKeyName.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // Properties
            // 
            this.Properties.AutoScroll = true;
            this.Properties.Controls.Add(this.groupBox1);
            this.Properties.Controls.Add(this.groupBox5);
            this.Properties.Controls.Add(this.SetInventory);
            this.Properties.Controls.Add(this.SetCurrency);
            this.Properties.Location = new System.Drawing.Point(4, 22);
            this.Properties.Name = "Properties";
            this.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.Properties.Size = new System.Drawing.Size(480, 492);
            this.Properties.TabIndex = 1;
            this.Properties.Text = "Properties";
            this.Properties.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SetDefaultLanguage);
            this.groupBox1.Controls.Add(this.SetNativeLanguage);
            this.groupBox1.Controls.Add(this.SetRace);
            this.groupBox1.Controls.Add(this.SetLevel);
            this.groupBox1.Controls.Add(this.SetClass);
            this.groupBox1.Controls.Add(this.SetGender);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(433, 162);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Properties";
            // 
            // SetDefaultLanguage
            // 
            this.SetDefaultLanguage.BackColor = System.Drawing.Color.Transparent;
            this.SetDefaultLanguage.EntryText = "";
            this.SetDefaultLanguage.FunctionName = "SetDefaultLanguage";
            this.SetDefaultLanguage.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDefaultLanguage.LabelText = "Default Language:";
            this.SetDefaultLanguage.ListTextCollection = new string[0];
            this.SetDefaultLanguage.Location = new System.Drawing.Point(3, 126);
            this.SetDefaultLanguage.Name = "SetDefaultLanguage";
            this.SetDefaultLanguage.ParameterType = Stellarmap.FuncParamType.String;
            this.SetDefaultLanguage.RequiredHeader = null;
            this.SetDefaultLanguage.SelectedIndex = -1;
            this.SetDefaultLanguage.Size = new System.Drawing.Size(238, 26);
            this.SetDefaultLanguage.TabIndex = 25;
            this.SetDefaultLanguage.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetNativeLanguage
            // 
            this.SetNativeLanguage.BackColor = System.Drawing.Color.Transparent;
            this.SetNativeLanguage.EntryText = "";
            this.SetNativeLanguage.FunctionName = "SetNativeLanguage";
            this.SetNativeLanguage.KeyType = Stellarmap.EntryType.Mixed;
            this.SetNativeLanguage.LabelText = "Native Language:";
            this.SetNativeLanguage.ListTextCollection = new string[0];
            this.SetNativeLanguage.Location = new System.Drawing.Point(6, 94);
            this.SetNativeLanguage.Name = "SetNativeLanguage";
            this.SetNativeLanguage.ParameterType = Stellarmap.FuncParamType.String;
            this.SetNativeLanguage.RequiredHeader = null;
            this.SetNativeLanguage.SelectedIndex = -1;
            this.SetNativeLanguage.Size = new System.Drawing.Size(235, 26);
            this.SetNativeLanguage.TabIndex = 24;
            this.SetNativeLanguage.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetRace
            // 
            this.SetRace.BackColor = System.Drawing.Color.Transparent;
            this.SetRace.EntryText = "";
            this.SetRace.FunctionName = "SetRace";
            this.SetRace.KeyType = Stellarmap.EntryType.Strings;
            this.SetRace.LabelText = "Race:";
            this.SetRace.ListTextCollection = new string[0];
            this.SetRace.Location = new System.Drawing.Point(26, 17);
            this.SetRace.Name = "SetRace";
            this.SetRace.ParameterType = Stellarmap.FuncParamType.String;
            this.SetRace.RequiredHeader = null;
            this.SetRace.SelectedIndex = -1;
            this.SetRace.Size = new System.Drawing.Size(179, 26);
            this.SetRace.TabIndex = 20;
            this.SetRace.ValueType = Stellarmap.EntryType.Strings;
            // 
            // SetLevel
            // 
            this.SetLevel.BackColor = System.Drawing.Color.Transparent;
            this.SetLevel.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetLevel.EntryMaxmimum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.SetLevel.EntryMinimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetLevel.EntryValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetLevel.FunctionName = "SetLevel";
            this.SetLevel.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetLevel.LabelText = "Class Level:";
            this.SetLevel.Location = new System.Drawing.Point(233, 49);
            this.SetLevel.Name = "SetLevel";
            this.SetLevel.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetLevel.RequiredHeader = null;
            this.SetLevel.Size = new System.Drawing.Size(172, 26);
            this.SetLevel.TabIndex = 23;
            this.SetLevel.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetClass
            // 
            this.SetClass.BackColor = System.Drawing.Color.Transparent;
            this.SetClass.EntryText = "";
            this.SetClass.FunctionName = "SetClass";
            this.SetClass.KeyType = Stellarmap.EntryType.Strings;
            this.SetClass.LabelText = "Class:";
            this.SetClass.ListTextCollection = new string[0];
            this.SetClass.Location = new System.Drawing.Point(26, 49);
            this.SetClass.Name = "SetClass";
            this.SetClass.ParameterType = Stellarmap.FuncParamType.String;
            this.SetClass.RequiredHeader = null;
            this.SetClass.SelectedIndex = -1;
            this.SetClass.Size = new System.Drawing.Size(178, 26);
            this.SetClass.TabIndex = 21;
            this.SetClass.ValueType = Stellarmap.EntryType.Strings;
            // 
            // SetGender
            // 
            this.SetGender.BackColor = System.Drawing.Color.Transparent;
            this.SetGender.EntryText = "";
            this.SetGender.FunctionName = "SetGender";
            this.SetGender.KeyType = Stellarmap.EntryType.Strings;
            this.SetGender.LabelText = "Gender:";
            this.SetGender.ListTextCollection = new string[0];
            this.SetGender.Location = new System.Drawing.Point(233, 17);
            this.SetGender.Name = "SetGender";
            this.SetGender.ParameterType = Stellarmap.FuncParamType.String;
            this.SetGender.RequiredHeader = null;
            this.SetGender.SelectedIndex = -1;
            this.SetGender.Size = new System.Drawing.Size(188, 26);
            this.SetGender.TabIndex = 22;
            this.SetGender.ValueType = Stellarmap.EntryType.Strings;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonRemoveLastAction);
            this.groupBox5.Controls.Add(this.buttonAddNewAction);
            this.groupBox5.Controls.Add(this.flowActions);
            this.groupBox5.Location = new System.Drawing.Point(6, 384);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(432, 315);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Casual Actions";
            // 
            // buttonRemoveLastAction
            // 
            this.buttonRemoveLastAction.Enabled = false;
            this.buttonRemoveLastAction.Location = new System.Drawing.Point(10, 86);
            this.buttonRemoveLastAction.Name = "buttonRemoveLastAction";
            this.buttonRemoveLastAction.Size = new System.Drawing.Size(90, 23);
            this.buttonRemoveLastAction.TabIndex = 9;
            this.buttonRemoveLastAction.Text = "Remove Last";
            this.buttonRemoveLastAction.UseVisualStyleBackColor = true;
            this.buttonRemoveLastAction.Visible = false;
            this.buttonRemoveLastAction.Click += new System.EventHandler(this.buttonRemoveLastAction_Click);
            // 
            // buttonAddNewAction
            // 
            this.buttonAddNewAction.Enabled = false;
            this.buttonAddNewAction.Location = new System.Drawing.Point(10, 36);
            this.buttonAddNewAction.Name = "buttonAddNewAction";
            this.buttonAddNewAction.Size = new System.Drawing.Size(90, 23);
            this.buttonAddNewAction.TabIndex = 8;
            this.buttonAddNewAction.Text = "Add New";
            this.buttonAddNewAction.UseVisualStyleBackColor = true;
            this.buttonAddNewAction.Visible = false;
            this.buttonAddNewAction.Click += new System.EventHandler(this.buttonAddNewAction_Click);
            // 
            // flowActions
            // 
            this.flowActions.AutoScroll = true;
            this.flowActions.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowActions.Location = new System.Drawing.Point(106, 19);
            this.flowActions.Name = "flowActions";
            this.flowActions.Size = new System.Drawing.Size(315, 290);
            this.flowActions.TabIndex = 19;
            // 
            // SetInventory
            // 
            this.SetInventory.BackColor = System.Drawing.Color.Transparent;
            this.SetInventory.EntryText = "";
            this.SetInventory.FunctionName = "SetInventory";
            this.SetInventory.KeyCollection = new string[0];
            this.SetInventory.KeyType = Stellarmap.EntryType.Mixed;
            this.SetInventory.LabelText = "Inventory";
            this.SetInventory.ListTextCollection = new string[0];
            this.SetInventory.Location = new System.Drawing.Point(1, 174);
            this.SetInventory.Name = "SetInventory";
            this.SetInventory.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetInventory.RequiredHeader = null;
            this.SetInventory.Size = new System.Drawing.Size(229, 211);
            this.SetInventory.TabIndex = 5;
            this.SetInventory.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetCurrency
            // 
            this.SetCurrency.BackColor = System.Drawing.Color.Transparent;
            this.SetCurrency.EntryText = "";
            this.SetCurrency.FunctionName = "SetCurrency";
            this.SetCurrency.KeyCollection = new string[0];
            this.SetCurrency.KeyType = Stellarmap.EntryType.Strings;
            this.SetCurrency.LabelText = "Set Currency";
            this.SetCurrency.ListTextCollection = new string[0];
            this.SetCurrency.Location = new System.Drawing.Point(228, 174);
            this.SetCurrency.Name = "SetCurrency";
            this.SetCurrency.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetCurrency.RequiredHeader = null;
            this.SetCurrency.Size = new System.Drawing.Size(229, 211);
            this.SetCurrency.TabIndex = 2;
            this.SetCurrency.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // CombatSettings
            // 
            this.CombatSettings.AutoScroll = true;
            this.CombatSettings.Controls.Add(this.groupInventoryDrops);
            this.CombatSettings.Controls.Add(this.groupBox6);
            this.CombatSettings.Controls.Add(this.groupBox2);
            this.CombatSettings.Location = new System.Drawing.Point(4, 22);
            this.CombatSettings.Name = "CombatSettings";
            this.CombatSettings.Size = new System.Drawing.Size(480, 492);
            this.CombatSettings.TabIndex = 3;
            this.CombatSettings.Text = "Combat Settings";
            this.CombatSettings.UseVisualStyleBackColor = true;
            // 
            // groupInventoryDrops
            // 
            this.groupInventoryDrops.Controls.Add(this.SetDropInventoryOnDeath);
            this.groupInventoryDrops.Controls.Add(this.SetItemDropRates);
            this.groupInventoryDrops.Location = new System.Drawing.Point(5, 427);
            this.groupInventoryDrops.Name = "groupInventoryDrops";
            this.groupInventoryDrops.Size = new System.Drawing.Size(427, 276);
            this.groupInventoryDrops.TabIndex = 23;
            this.groupInventoryDrops.TabStop = false;
            this.groupInventoryDrops.Text = "Inventory Drops";
            this.groupInventoryDrops.Visible = false;
            // 
            // SetDropInventoryOnDeath
            // 
            this.SetDropInventoryOnDeath.BackColor = System.Drawing.Color.Transparent;
            this.SetDropInventoryOnDeath.Checked = false;
            this.SetDropInventoryOnDeath.Enabled = false;
            this.SetDropInventoryOnDeath.FunctionName = "SetDropInventoryOnDeath";
            this.SetDropInventoryOnDeath.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDropInventoryOnDeath.LabelText = "Drop Carried Inventory on Death";
            this.SetDropInventoryOnDeath.Location = new System.Drawing.Point(10, 31);
            this.SetDropInventoryOnDeath.Name = "SetDropInventoryOnDeath";
            this.SetDropInventoryOnDeath.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetDropInventoryOnDeath.RequiredHeader = null;
            this.SetDropInventoryOnDeath.Size = new System.Drawing.Size(187, 22);
            this.SetDropInventoryOnDeath.TabIndex = 9;
            this.SetDropInventoryOnDeath.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetItemDropRates
            // 
            this.SetItemDropRates.BackColor = System.Drawing.Color.Transparent;
            this.SetItemDropRates.Enabled = false;
            this.SetItemDropRates.EntryText = "";
            this.SetItemDropRates.FunctionName = "SetItemDropRates";
            this.SetItemDropRates.KeyCollection = new string[0];
            this.SetItemDropRates.KeyType = Stellarmap.EntryType.Mixed;
            this.SetItemDropRates.LabelText = "Other Item Drop Rates";
            this.SetItemDropRates.ListTextCollection = new string[0];
            this.SetItemDropRates.Location = new System.Drawing.Point(10, 59);
            this.SetItemDropRates.Name = "SetItemDropRates";
            this.SetItemDropRates.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetItemDropRates.RequiredHeader = null;
            this.SetItemDropRates.Size = new System.Drawing.Size(229, 211);
            this.SetItemDropRates.TabIndex = 8;
            this.SetItemDropRates.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonAddNewCombatAction);
            this.groupBox6.Controls.Add(this.buttonRemoveLastCombatAction);
            this.groupBox6.Controls.Add(this.flowCombatActions);
            this.groupBox6.Location = new System.Drawing.Point(5, 103);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(427, 318);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Combat Actions";
            // 
            // buttonAddNewCombatAction
            // 
            this.buttonAddNewCombatAction.Enabled = false;
            this.buttonAddNewCombatAction.Location = new System.Drawing.Point(10, 36);
            this.buttonAddNewCombatAction.Name = "buttonAddNewCombatAction";
            this.buttonAddNewCombatAction.Size = new System.Drawing.Size(90, 23);
            this.buttonAddNewCombatAction.TabIndex = 10;
            this.buttonAddNewCombatAction.Text = "Add New";
            this.buttonAddNewCombatAction.UseVisualStyleBackColor = true;
            this.buttonAddNewCombatAction.Visible = false;
            this.buttonAddNewCombatAction.Click += new System.EventHandler(this.buttonAddNewCombatAction_Click);
            // 
            // buttonRemoveLastCombatAction
            // 
            this.buttonRemoveLastCombatAction.Enabled = false;
            this.buttonRemoveLastCombatAction.Location = new System.Drawing.Point(10, 86);
            this.buttonRemoveLastCombatAction.Name = "buttonRemoveLastCombatAction";
            this.buttonRemoveLastCombatAction.Size = new System.Drawing.Size(90, 23);
            this.buttonRemoveLastCombatAction.TabIndex = 11;
            this.buttonRemoveLastCombatAction.Text = "Remove Last ";
            this.buttonRemoveLastCombatAction.UseVisualStyleBackColor = true;
            this.buttonRemoveLastCombatAction.Visible = false;
            this.buttonRemoveLastCombatAction.Click += new System.EventHandler(this.buttonRemoveLastCombatAction_Click);
            // 
            // flowCombatActions
            // 
            this.flowCombatActions.AutoScroll = true;
            this.flowCombatActions.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowCombatActions.Location = new System.Drawing.Point(106, 19);
            this.flowCombatActions.Name = "flowCombatActions";
            this.flowCombatActions.Size = new System.Drawing.Size(315, 293);
            this.flowCombatActions.TabIndex = 20;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SetEncounter);
            this.groupBox2.Controls.Add(this.SetUnique);
            this.groupBox2.Controls.Add(this.SetDie);
            this.groupBox2.Controls.Add(this.SetMorality);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 94);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // SetEncounter
            // 
            this.SetEncounter.BackColor = System.Drawing.Color.Transparent;
            this.SetEncounter.EntryText = "~0";
            this.SetEncounter.FunctionName = "SetEncounter";
            this.SetEncounter.KeyType = Stellarmap.EntryType.Mixed;
            this.SetEncounter.LabelText = "Encounter Setting:";
            this.SetEncounter.Location = new System.Drawing.Point(12, 51);
            this.SetEncounter.Name = "SetEncounter";
            this.SetEncounter.ParameterType = Stellarmap.FuncParamType.String;
            this.SetEncounter.RequiredHeader = null;
            this.SetEncounter.Size = new System.Drawing.Size(215, 26);
            this.SetEncounter.TabIndex = 4;
            this.SetEncounter.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetUnique
            // 
            this.SetUnique.BackColor = System.Drawing.Color.Transparent;
            this.SetUnique.Checked = false;
            this.SetUnique.FunctionName = "SetUnique";
            this.SetUnique.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetUnique.LabelText = "Unique Npc";
            this.SetUnique.Location = new System.Drawing.Point(282, 19);
            this.SetUnique.Name = "SetUnique";
            this.SetUnique.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetUnique.RequiredHeader = null;
            this.SetUnique.Size = new System.Drawing.Size(91, 22);
            this.SetUnique.TabIndex = 1;
            this.SetUnique.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetDie
            // 
            this.SetDie.BackColor = System.Drawing.Color.Transparent;
            this.SetDie.EntryText = "";
            this.SetDie.FunctionName = "SetDie";
            this.SetDie.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDie.LabelText = "Death Message:";
            this.SetDie.Location = new System.Drawing.Point(12, 19);
            this.SetDie.Name = "SetDie";
            this.SetDie.ParameterType = Stellarmap.FuncParamType.String;
            this.SetDie.RequiredHeader = null;
            this.SetDie.Size = new System.Drawing.Size(205, 26);
            this.SetDie.TabIndex = 7;
            this.SetDie.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetMorality
            // 
            this.SetMorality.BackColor = System.Drawing.Color.Transparent;
            this.SetMorality.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetMorality.EntryMaxmimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.SetMorality.EntryMinimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
            this.SetMorality.EntryValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetMorality.FunctionName = "SetMorality";
            this.SetMorality.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetMorality.LabelText = "Morality:";
            this.SetMorality.Location = new System.Drawing.Point(233, 51);
            this.SetMorality.Name = "SetMorality";
            this.SetMorality.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetMorality.RequiredHeader = null;
            this.SetMorality.Size = new System.Drawing.Size(154, 26);
            this.SetMorality.TabIndex = 0;
            this.SetMorality.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // StatsTab
            // 
            this.StatsTab.AutoScroll = true;
            this.StatsTab.Controls.Add(this.groupBox8);
            this.StatsTab.Controls.Add(this.groupBox4);
            this.StatsTab.Controls.Add(this.groupBox3);
            this.StatsTab.Location = new System.Drawing.Point(4, 22);
            this.StatsTab.Name = "StatsTab";
            this.StatsTab.Size = new System.Drawing.Size(480, 492);
            this.StatsTab.TabIndex = 5;
            this.StatsTab.Text = "Stats / Skills";
            this.StatsTab.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.comboLanguageAdd);
            this.groupBox8.Controls.Add(this.comboLanguageRemove);
            this.groupBox8.Controls.Add(this.flowLanguages);
            this.groupBox8.Controls.Add(this.buttonRemoveLanguage);
            this.groupBox8.Controls.Add(this.buttonAddLanguage);
            this.groupBox8.Location = new System.Drawing.Point(12, 583);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(430, 282);
            this.groupBox8.TabIndex = 22;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Language Skills";
            // 
            // comboLanguageAdd
            // 
            this.comboLanguageAdd.FormattingEnabled = true;
            this.comboLanguageAdd.Location = new System.Drawing.Point(24, 48);
            this.comboLanguageAdd.MaxDropDownItems = 30;
            this.comboLanguageAdd.Name = "comboLanguageAdd";
            this.comboLanguageAdd.Size = new System.Drawing.Size(104, 21);
            this.comboLanguageAdd.TabIndex = 20;
            // 
            // comboLanguageRemove
            // 
            this.comboLanguageRemove.FormattingEnabled = true;
            this.comboLanguageRemove.Location = new System.Drawing.Point(24, 125);
            this.comboLanguageRemove.MaxDropDownItems = 30;
            this.comboLanguageRemove.Name = "comboLanguageRemove";
            this.comboLanguageRemove.Size = new System.Drawing.Size(104, 21);
            this.comboLanguageRemove.TabIndex = 16;
            // 
            // flowLanguages
            // 
            this.flowLanguages.AutoScroll = true;
            this.flowLanguages.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLanguages.Location = new System.Drawing.Point(146, 11);
            this.flowLanguages.Name = "flowLanguages";
            this.flowLanguages.Size = new System.Drawing.Size(278, 265);
            this.flowLanguages.TabIndex = 19;
            // 
            // buttonRemoveLanguage
            // 
            this.buttonRemoveLanguage.Location = new System.Drawing.Point(24, 96);
            this.buttonRemoveLanguage.Name = "buttonRemoveLanguage";
            this.buttonRemoveLanguage.Size = new System.Drawing.Size(104, 23);
            this.buttonRemoveLanguage.TabIndex = 13;
            this.buttonRemoveLanguage.Text = "Remove Language";
            this.buttonRemoveLanguage.UseVisualStyleBackColor = true;
            this.buttonRemoveLanguage.Click += new System.EventHandler(this.buttonRemoveLanguage_Click);
            // 
            // buttonAddLanguage
            // 
            this.buttonAddLanguage.Location = new System.Drawing.Point(24, 19);
            this.buttonAddLanguage.Name = "buttonAddLanguage";
            this.buttonAddLanguage.Size = new System.Drawing.Size(104, 23);
            this.buttonAddLanguage.TabIndex = 12;
            this.buttonAddLanguage.Text = "Add Language";
            this.buttonAddLanguage.UseVisualStyleBackColor = true;
            this.buttonAddLanguage.Click += new System.EventHandler(this.buttonAddLanguage_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboSkillAdd);
            this.groupBox4.Controls.Add(this.comboSkillRemove);
            this.groupBox4.Controls.Add(this.flowSkills);
            this.groupBox4.Controls.Add(this.buttonRemoveSkill);
            this.groupBox4.Controls.Add(this.buttonAddNewSkill);
            this.groupBox4.Location = new System.Drawing.Point(12, 295);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(430, 282);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Npc Skills";
            // 
            // comboSkillAdd
            // 
            this.comboSkillAdd.FormattingEnabled = true;
            this.comboSkillAdd.Location = new System.Drawing.Point(24, 48);
            this.comboSkillAdd.MaxDropDownItems = 30;
            this.comboSkillAdd.Name = "comboSkillAdd";
            this.comboSkillAdd.Size = new System.Drawing.Size(104, 21);
            this.comboSkillAdd.TabIndex = 20;
            // 
            // comboSkillRemove
            // 
            this.comboSkillRemove.FormattingEnabled = true;
            this.comboSkillRemove.Location = new System.Drawing.Point(24, 125);
            this.comboSkillRemove.MaxDropDownItems = 30;
            this.comboSkillRemove.Name = "comboSkillRemove";
            this.comboSkillRemove.Size = new System.Drawing.Size(104, 21);
            this.comboSkillRemove.TabIndex = 16;
            // 
            // flowSkills
            // 
            this.flowSkills.AutoScroll = true;
            this.flowSkills.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowSkills.Location = new System.Drawing.Point(146, 11);
            this.flowSkills.Name = "flowSkills";
            this.flowSkills.Size = new System.Drawing.Size(278, 265);
            this.flowSkills.TabIndex = 19;
            // 
            // buttonRemoveSkill
            // 
            this.buttonRemoveSkill.Location = new System.Drawing.Point(24, 96);
            this.buttonRemoveSkill.Name = "buttonRemoveSkill";
            this.buttonRemoveSkill.Size = new System.Drawing.Size(104, 23);
            this.buttonRemoveSkill.TabIndex = 13;
            this.buttonRemoveSkill.Text = "Remove Skill";
            this.buttonRemoveSkill.UseVisualStyleBackColor = true;
            this.buttonRemoveSkill.Click += new System.EventHandler(this.buttonRemoveSkill_Click);
            // 
            // buttonAddNewSkill
            // 
            this.buttonAddNewSkill.Location = new System.Drawing.Point(24, 19);
            this.buttonAddNewSkill.Name = "buttonAddNewSkill";
            this.buttonAddNewSkill.Size = new System.Drawing.Size(104, 23);
            this.buttonAddNewSkill.TabIndex = 12;
            this.buttonAddNewSkill.Text = "Add Skill";
            this.buttonAddNewSkill.UseVisualStyleBackColor = true;
            this.buttonAddNewSkill.Click += new System.EventHandler(this.buttonAddNewSkill_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboStatAdd);
            this.groupBox3.Controls.Add(this.comboStatRemove);
            this.groupBox3.Controls.Add(this.flowStats);
            this.groupBox3.Controls.Add(this.buttonRemoveStat);
            this.groupBox3.Controls.Add(this.buttonAddNewStat);
            this.groupBox3.Location = new System.Drawing.Point(12, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 286);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Npc Stats";
            // 
            // comboStatAdd
            // 
            this.comboStatAdd.FormattingEnabled = true;
            this.comboStatAdd.Location = new System.Drawing.Point(24, 48);
            this.comboStatAdd.MaxDropDownItems = 16;
            this.comboStatAdd.Name = "comboStatAdd";
            this.comboStatAdd.Size = new System.Drawing.Size(104, 21);
            this.comboStatAdd.TabIndex = 19;
            // 
            // comboStatRemove
            // 
            this.comboStatRemove.FormattingEnabled = true;
            this.comboStatRemove.Location = new System.Drawing.Point(24, 127);
            this.comboStatRemove.MaxDropDownItems = 16;
            this.comboStatRemove.Name = "comboStatRemove";
            this.comboStatRemove.Size = new System.Drawing.Size(104, 21);
            this.comboStatRemove.TabIndex = 14;
            // 
            // flowStats
            // 
            this.flowStats.AutoScroll = true;
            this.flowStats.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowStats.Location = new System.Drawing.Point(144, 12);
            this.flowStats.Name = "flowStats";
            this.flowStats.Size = new System.Drawing.Size(278, 265);
            this.flowStats.TabIndex = 18;
            // 
            // buttonRemoveStat
            // 
            this.buttonRemoveStat.Location = new System.Drawing.Point(24, 98);
            this.buttonRemoveStat.Name = "buttonRemoveStat";
            this.buttonRemoveStat.Size = new System.Drawing.Size(104, 23);
            this.buttonRemoveStat.TabIndex = 11;
            this.buttonRemoveStat.Text = "Remove Stat";
            this.buttonRemoveStat.UseVisualStyleBackColor = true;
            this.buttonRemoveStat.Click += new System.EventHandler(this.buttonRemoveStat_Click);
            // 
            // buttonAddNewStat
            // 
            this.buttonAddNewStat.Location = new System.Drawing.Point(24, 19);
            this.buttonAddNewStat.Name = "buttonAddNewStat";
            this.buttonAddNewStat.Size = new System.Drawing.Size(104, 23);
            this.buttonAddNewStat.TabIndex = 10;
            this.buttonAddNewStat.Text = "Add Stat";
            this.buttonAddNewStat.UseVisualStyleBackColor = true;
            this.buttonAddNewStat.Click += new System.EventHandler(this.buttonAddNewStat_Click);
            // 
            // InheritTab
            // 
            this.InheritTab.AutoScroll = true;
            this.InheritTab.BackColor = System.Drawing.Color.White;
            this.InheritTab.Location = new System.Drawing.Point(4, 22);
            this.InheritTab.Name = "InheritTab";
            this.InheritTab.Size = new System.Drawing.Size(480, 492);
            this.InheritTab.TabIndex = 4;
            this.InheritTab.Text = "Inherited Features";
            this.InheritTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Enabled = false;
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(107, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(24, 48);
            this.comboBox1.MaxDropDownItems = 30;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(104, 21);
            this.comboBox1.TabIndex = 20;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(24, 125);
            this.comboBox2.MaxDropDownItems = 30;
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(104, 21);
            this.comboBox2.TabIndex = 16;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(146, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(278, 265);
            this.flowLayoutPanel1.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Remove Skill";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Add Skill";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CreateNpcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 572);
            this.Controls.Add(this.TabView);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CreateNpcForm";
            this.Text = "Create Npc";
            this.TabView.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.Properties.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.CombatSettings.ResumeLayout(false);
            this.groupInventoryDrops.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.StatsTab.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TabControl TabView;
		private System.Windows.Forms.TabPage MainTabPage;
		private ListBuilder SetAdjectives;
		private ListBuilder SetId;
		private TextDump SetLong;
		private TextEntry SetShort;
		private TextEntry SetKeyName;
		private System.Windows.Forms.TabPage Properties;
		private System.Windows.Forms.TabPage CombatSettings;
		private System.Windows.Forms.TabPage InheritTab;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private ComboSelection SetClass;
		private ComboSelection SetRace;
		private ComboSelection SetGender;
		private NumberEntry SetLevel;
		private NumberEntry SetMorality;
		private Check SetUnique;
		private ComboMapBuilder SetCurrency;
		private TextEntry SetEncounter;
		private ComboMapBuilder SetInventory;
		private TextEntry SetDie;
		private System.Windows.Forms.Button buttonRemoveLastAction;
		private System.Windows.Forms.Button buttonAddNewAction;
		private System.Windows.Forms.Button buttonRemoveLastCombatAction;
		private System.Windows.Forms.Button buttonAddNewCombatAction;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TabPage StatsTab;
		private System.Windows.Forms.ComboBox comboStatRemove;
		private System.Windows.Forms.Button buttonRemoveSkill;
		private System.Windows.Forms.Button buttonAddNewSkill;
		private System.Windows.Forms.Button buttonRemoveStat;
		private System.Windows.Forms.Button buttonAddNewStat;
		private System.Windows.Forms.ComboBox comboSkillRemove;
		private System.Windows.Forms.FlowLayoutPanel flowSkills;
		private System.Windows.Forms.FlowLayoutPanel flowStats;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.FlowLayoutPanel flowCombatActions;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.FlowLayoutPanel flowActions;
		private System.Windows.Forms.ComboBox comboSkillAdd;
		private System.Windows.Forms.ComboBox comboStatAdd;
		private ComboSelection SetDefaultLanguage;
		private ComboSelection SetNativeLanguage;
		private System.Windows.Forms.GroupBox groupInventoryDrops;
		private ComboMapBuilder SetItemDropRates;
		private Check SetDropInventoryOnDeath;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.ComboBox comboLanguageAdd;
		private System.Windows.Forms.ComboBox comboLanguageRemove;
		private System.Windows.Forms.FlowLayoutPanel flowLanguages;
		private System.Windows.Forms.Button buttonRemoveLanguage;
		private System.Windows.Forms.Button buttonAddLanguage;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		}
	}