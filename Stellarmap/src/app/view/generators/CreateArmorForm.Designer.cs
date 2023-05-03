namespace Stellarmap
	{
	partial class CreateArmorForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetPreventDrop = new Stellarmap.TextEntry();
            this.SetPreventPut = new Stellarmap.TextEntry();
            this.SetPreventGet = new Stellarmap.TextEntry();
            this.SetMass = new Stellarmap.NumberEntry();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CalculateCost = new System.Windows.Forms.Button();
            this.SetBaseCost = new Stellarmap.NumberEntry();
            this.SetDamagePoints = new Stellarmap.NumberEntry();
            this.SetDestroyOnSell = new Stellarmap.Check();
            this.SetProperties = new Stellarmap.MapBuilder();
            this.SetVendorType = new Stellarmap.CheckList();
            this.ArmorStats = new System.Windows.Forms.TabPage();
            this.SetWear = new Stellarmap.TextEntry();
            this.SetRestrictLimbs = new Stellarmap.ComboListBuilder();
            this.SetProtections = new Stellarmap.ComboMapBuilder();
            this.SetArmorType = new Stellarmap.ComboSelection();
            this.InheritTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetWearLevel = new Stellarmap.NumberEntry();
            this.TabView.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.Properties.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ArmorStats.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabView
            // 
            this.TabView.Controls.Add(this.MainTabPage);
            this.TabView.Controls.Add(this.Properties);
            this.TabView.Controls.Add(this.ArmorStats);
            this.TabView.Controls.Add(this.InheritTab);
            this.TabView.Location = new System.Drawing.Point(12, 42);
            this.TabView.Name = "TabView";
            this.TabView.SelectedIndex = 0;
            this.TabView.Size = new System.Drawing.Size(525, 418);
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
            this.MainTabPage.Size = new System.Drawing.Size(517, 392);
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
            this.SetAdjectives.Location = new System.Drawing.Point(270, 98);
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
            this.SetId.Location = new System.Drawing.Point(17, 98);
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
            this.SetLong.EntryText = "A generic looking armor.";
            this.SetLong.FunctionName = "SetLong";
            this.SetLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetLong.LabelText = "Long Description";
            this.SetLong.Location = new System.Drawing.Point(216, 6);
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
            this.SetShort.EntryText = "an armor";
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
            this.SetKeyName.EntryText = "armor";
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
            this.Properties.Controls.Add(this.groupBox2);
            this.Properties.Controls.Add(this.groupBox1);
            this.Properties.Controls.Add(this.SetProperties);
            this.Properties.Controls.Add(this.SetVendorType);
            this.Properties.Location = new System.Drawing.Point(4, 22);
            this.Properties.Name = "Properties";
            this.Properties.Size = new System.Drawing.Size(517, 392);
            this.Properties.TabIndex = 3;
            this.Properties.Text = "Properties";
            this.Properties.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SetPreventDrop);
            this.groupBox2.Controls.Add(this.SetPreventPut);
            this.groupBox2.Controls.Add(this.SetPreventGet);
            this.groupBox2.Controls.Add(this.SetMass);
            this.groupBox2.Location = new System.Drawing.Point(3, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 204);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Interactive Properties";
            // 
            // SetPreventDrop
            // 
            this.SetPreventDrop.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventDrop.EntryText = "";
            this.SetPreventDrop.FunctionName = "SetPreventDrop";
            this.SetPreventDrop.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventDrop.LabelText = "Disable Dropping:";
            this.SetPreventDrop.Location = new System.Drawing.Point(0, 142);
            this.SetPreventDrop.Name = "SetPreventDrop";
            this.SetPreventDrop.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventDrop.RequiredHeader = null;
            this.SetPreventDrop.Size = new System.Drawing.Size(211, 26);
            this.SetPreventDrop.TabIndex = 18;
            this.SetPreventDrop.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetPreventPut
            // 
            this.SetPreventPut.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventPut.EntryText = "";
            this.SetPreventPut.FunctionName = "SetPreventPut";
            this.SetPreventPut.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventPut.LabelText = "Disable Putting:";
            this.SetPreventPut.Location = new System.Drawing.Point(0, 110);
            this.SetPreventPut.Name = "SetPreventPut";
            this.SetPreventPut.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventPut.RequiredHeader = null;
            this.SetPreventPut.Size = new System.Drawing.Size(201, 26);
            this.SetPreventPut.TabIndex = 18;
            this.SetPreventPut.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetPreventGet
            // 
            this.SetPreventGet.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventGet.EntryText = "";
            this.SetPreventGet.FunctionName = "SetPreventGet";
            this.SetPreventGet.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventGet.LabelText = "Disable Getting:";
            this.SetPreventGet.Location = new System.Drawing.Point(0, 78);
            this.SetPreventGet.Name = "SetPreventGet";
            this.SetPreventGet.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventGet.RequiredHeader = null;
            this.SetPreventGet.Size = new System.Drawing.Size(202, 26);
            this.SetPreventGet.TabIndex = 17;
            this.SetPreventGet.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetMass
            // 
            this.SetMass.BackColor = System.Drawing.Color.Transparent;
            this.SetMass.DecimalPlaces = 0;
            this.SetMass.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetMass.EntryMaxmimum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SetMass.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetMass.EntryValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetMass.FunctionName = "SetMass";
            this.SetMass.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetMass.LabelText = "Mass:";
            this.SetMass.Location = new System.Drawing.Point(6, 31);
            this.SetMass.Name = "SetMass";
            this.SetMass.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetMass.RequiredHeader = null;
            this.SetMass.Size = new System.Drawing.Size(143, 26);
            this.SetMass.TabIndex = 12;
            this.SetMass.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CalculateCost);
            this.groupBox1.Controls.Add(this.SetBaseCost);
            this.groupBox1.Controls.Add(this.SetDamagePoints);
            this.groupBox1.Controls.Add(this.SetDestroyOnSell);
            this.groupBox1.Location = new System.Drawing.Point(267, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 168);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vendor Properties";
            // 
            // CalculateCost
            // 
            this.CalculateCost.Location = new System.Drawing.Point(8, 22);
            this.CalculateCost.Name = "CalculateCost";
            this.CalculateCost.Size = new System.Drawing.Size(82, 23);
            this.CalculateCost.TabIndex = 17;
            this.CalculateCost.Text = "Calculate Cost";
            this.CalculateCost.UseVisualStyleBackColor = true;
            this.CalculateCost.Click += new System.EventHandler(this.CalculateCost_Click);
            // 
            // SetBaseCost
            // 
            this.SetBaseCost.BackColor = System.Drawing.Color.Transparent;
            this.SetBaseCost.DecimalPlaces = 0;
            this.SetBaseCost.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetBaseCost.EntryMaxmimum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.SetBaseCost.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetBaseCost.EntryValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetBaseCost.FunctionName = "SetBaseCost";
            this.SetBaseCost.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetBaseCost.LabelText = "Value:";
            this.SetBaseCost.Location = new System.Drawing.Point(96, 19);
            this.SetBaseCost.Name = "SetBaseCost";
            this.SetBaseCost.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetBaseCost.RequiredHeader = null;
            this.SetBaseCost.Size = new System.Drawing.Size(145, 26);
            this.SetBaseCost.TabIndex = 11;
            this.SetBaseCost.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetDamagePoints
            // 
            this.SetDamagePoints.BackColor = System.Drawing.Color.Transparent;
            this.SetDamagePoints.DecimalPlaces = 0;
            this.SetDamagePoints.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetDamagePoints.EntryMaxmimum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SetDamagePoints.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetDamagePoints.EntryValue = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.SetDamagePoints.FunctionName = "SetDamagePoints";
            this.SetDamagePoints.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetDamagePoints.LabelText = "Value Half-Life (in HP):";
            this.SetDamagePoints.Location = new System.Drawing.Point(19, 104);
            this.SetDamagePoints.Name = "SetDamagePoints";
            this.SetDamagePoints.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetDamagePoints.RequiredHeader = null;
            this.SetDamagePoints.Size = new System.Drawing.Size(222, 26);
            this.SetDamagePoints.TabIndex = 16;
            this.SetDamagePoints.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetDestroyOnSell
            // 
            this.SetDestroyOnSell.BackColor = System.Drawing.Color.Transparent;
            this.SetDestroyOnSell.Checked = true;
            this.SetDestroyOnSell.FunctionName = "SetDestroyOnSell";
            this.SetDestroyOnSell.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDestroyOnSell.LabelText = "Destroy On Sell";
            this.SetDestroyOnSell.Location = new System.Drawing.Point(134, 64);
            this.SetDestroyOnSell.Name = "SetDestroyOnSell";
            this.SetDestroyOnSell.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetDestroyOnSell.RequiredHeader = null;
            this.SetDestroyOnSell.Size = new System.Drawing.Size(107, 22);
            this.SetDestroyOnSell.TabIndex = 17;
            this.SetDestroyOnSell.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetProperties
            // 
            this.SetProperties.BackColor = System.Drawing.Color.Transparent;
            this.SetProperties.FunctionName = "SetProperties";
            this.SetProperties.KeyType = Stellarmap.EntryType.Mixed;
            this.SetProperties.LabelText = "Custom Properties";
            this.SetProperties.ListTextCollection = new string[0];
            this.SetProperties.Location = new System.Drawing.Point(267, 185);
            this.SetProperties.Name = "SetProperties";
            this.SetProperties.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetProperties.RequiredHeader = null;
            this.SetProperties.Size = new System.Drawing.Size(227, 209);
            this.SetProperties.TabIndex = 13;
            this.SetProperties.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetVendorType
            // 
            this.SetVendorType.BackColor = System.Drawing.Color.Transparent;
            this.SetVendorType.CheckListStrings = new string[0];
            this.SetVendorType.FunctionName = "SetVendorType";
            this.SetVendorType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetVendorType.LabelText = "Vendor Types";
            this.SetVendorType.Location = new System.Drawing.Point(3, 3);
            this.SetVendorType.Name = "SetVendorType";
            this.SetVendorType.ParameterType = Stellarmap.FuncParamType.ORList;
            this.SetVendorType.RequiredHeader = null;
            this.SetVendorType.Size = new System.Drawing.Size(258, 176);
            this.SetVendorType.TabIndex = 12;
            this.SetVendorType.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // ArmorStats
            // 
            this.ArmorStats.Controls.Add(this.SetWearLevel);
            this.ArmorStats.Controls.Add(this.SetWear);
            this.ArmorStats.Controls.Add(this.SetRestrictLimbs);
            this.ArmorStats.Controls.Add(this.SetProtections);
            this.ArmorStats.Controls.Add(this.SetArmorType);
            this.ArmorStats.Location = new System.Drawing.Point(4, 22);
            this.ArmorStats.Name = "ArmorStats";
            this.ArmorStats.Size = new System.Drawing.Size(517, 392);
            this.ArmorStats.TabIndex = 5;
            this.ArmorStats.Text = "Armor Stats";
            this.ArmorStats.UseVisualStyleBackColor = true;
            // 
            // SetWear
            // 
            this.SetWear.BackColor = System.Drawing.Color.Transparent;
            this.SetWear.EntryText = "";
            this.SetWear.FunctionName = "SetWear";
            this.SetWear.KeyType = Stellarmap.EntryType.Mixed;
            this.SetWear.LabelText = "Equip Message:";
            this.SetWear.Location = new System.Drawing.Point(231, 220);
            this.SetWear.Name = "SetWear";
            this.SetWear.ParameterType = Stellarmap.FuncParamType.String;
            this.SetWear.RequiredHeader = null;
            this.SetWear.Size = new System.Drawing.Size(203, 26);
            this.SetWear.TabIndex = 3;
            this.SetWear.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetRestrictLimbs
            // 
            this.SetRestrictLimbs.BackColor = System.Drawing.Color.Transparent;
            this.SetRestrictLimbs.EntryCollection = new string[0];
            this.SetRestrictLimbs.EntryText = "";
            this.SetRestrictLimbs.FunctionName = "SetRestrictLimbs";
            this.SetRestrictLimbs.KeyType = Stellarmap.EntryType.Strings;
            this.SetRestrictLimbs.LabelText = "Required Limbs:";
            this.SetRestrictLimbs.Location = new System.Drawing.Point(231, 6);
            this.SetRestrictLimbs.Name = "SetRestrictLimbs";
            this.SetRestrictLimbs.ParameterType = Stellarmap.FuncParamType.Array;
            this.SetRestrictLimbs.RequiredHeader = null;
            this.SetRestrictLimbs.Size = new System.Drawing.Size(225, 208);
            this.SetRestrictLimbs.TabIndex = 2;
            this.SetRestrictLimbs.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetProtections
            // 
            this.SetProtections.BackColor = System.Drawing.Color.Transparent;
            this.SetProtections.EntryText = "";
            this.SetProtections.FunctionName = "SetProtections";
            this.SetProtections.KeyCollection = new string[0];
            this.SetProtections.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetProtections.LabelText = "Protections:";
            this.SetProtections.ListTextCollection = new string[0];
            this.SetProtections.Location = new System.Drawing.Point(3, 3);
            this.SetProtections.Name = "SetProtections";
            this.SetProtections.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetProtections.RequiredHeader = null;
            this.SetProtections.Size = new System.Drawing.Size(229, 211);
            this.SetProtections.TabIndex = 1;
            this.SetProtections.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetArmorType
            // 
            this.SetArmorType.BackColor = System.Drawing.Color.Transparent;
            this.SetArmorType.EntryText = "";
            this.SetArmorType.FunctionName = "SetArmorType";
            this.SetArmorType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetArmorType.LabelText = "Armor Type:";
            this.SetArmorType.ListTextCollection = new string[0];
            this.SetArmorType.Location = new System.Drawing.Point(3, 220);
            this.SetArmorType.Name = "SetArmorType";
            this.SetArmorType.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetArmorType.RequiredHeader = null;
            this.SetArmorType.SelectedIndex = -1;
            this.SetArmorType.Size = new System.Drawing.Size(207, 26);
            this.SetArmorType.TabIndex = 0;
            this.SetArmorType.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // InheritTab
            // 
            this.InheritTab.AutoScroll = true;
            this.InheritTab.BackColor = System.Drawing.Color.White;
            this.InheritTab.Location = new System.Drawing.Point(4, 22);
            this.InheritTab.Name = "InheritTab";
            this.InheritTab.Size = new System.Drawing.Size(517, 392);
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
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
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
            // SetWearLevel
            // 
            this.SetWearLevel.BackColor = System.Drawing.Color.Transparent;
            this.SetWearLevel.DecimalPlaces = 0;
            this.SetWearLevel.Enabled = false;
            this.SetWearLevel.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetWearLevel.EntryMaxmimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SetWearLevel.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetWearLevel.EntryValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetWearLevel.FunctionName = "SetWearLevel";
            this.SetWearLevel.KeyType = Stellarmap.EntryType.Mixed;
            this.SetWearLevel.LabelText = "Wear Level:";
            this.SetWearLevel.Location = new System.Drawing.Point(3, 267);
            this.SetWearLevel.Name = "SetWearLevel";
            this.SetWearLevel.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetWearLevel.RequiredHeader = null;
            this.SetWearLevel.Size = new System.Drawing.Size(173, 26);
            this.SetWearLevel.TabIndex = 4;
            this.SetWearLevel.ValueType = Stellarmap.EntryType.Mixed;
            this.SetWearLevel.Visible = false;
            // 
            // CreateArmorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 472);
            this.Controls.Add(this.TabView);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreateArmorForm";
            this.Text = "Create Armor";
            this.TabView.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.Properties.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ArmorStats.ResumeLayout(false);
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
        private System.Windows.Forms.Button CalculateCost;
		private NumberEntry SetMass;
		private NumberEntry SetBaseCost;
		private MapBuilder SetProperties;
		private CheckList SetVendorType;
		private System.Windows.Forms.TabPage Properties;
		private NumberEntry SetDamagePoints;
		private System.Windows.Forms.TabPage InheritTab;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private Check SetDestroyOnSell;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TabPage ArmorStats;
		private ComboListBuilder SetRestrictLimbs;
		private ComboMapBuilder SetProtections;
		private ComboSelection SetArmorType;
		private TextEntry SetWear;
        private TextEntry SetPreventDrop;
        private TextEntry SetPreventPut;
        private TextEntry SetPreventGet;
        private NumberEntry SetWearLevel;
		}
	}