namespace Stellarmap
	{
	partial class CreateItemForm
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
            this.InheritTab = new System.Windows.Forms.TabPage();
            this.WeaponStats = new System.Windows.Forms.TabPage();
            this.groupbox4 = new System.Windows.Forms.GroupBox();
            this.SetWieldLevel = new Stellarmap.NumberEntry();
            this.SetWield = new Stellarmap.TextEntry();
            this.SetProperties = new Stellarmap.MapBuilder();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SetHands = new Stellarmap.NumberEntry();
            this.SetClass = new Stellarmap.NumberEntry();
            this.SetDamagePoints = new Stellarmap.NumberEntry();
            this.SetDamageType = new Stellarmap.CheckList();
            this.SetWeaponType = new Stellarmap.ComboSelection();
            this.SetInventory = new Stellarmap.ComboMapBuilder();
            this.Properties = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SetPreventDrop = new Stellarmap.TextEntry();
            this.SetPreventGet = new Stellarmap.TextEntry();
            this.SetPreventPut = new Stellarmap.TextEntry();
            this.SetDestroyOnSell = new Stellarmap.Check();
            this.CalculateCost = new System.Windows.Forms.Button();
            this.SetMass = new Stellarmap.NumberEntry();
            this.SetBaseCost = new Stellarmap.NumberEntry();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SetOpacity = new Stellarmap.NumberEntry();
            this.SetLocked = new Stellarmap.Check();
            this.SetCanLock = new Stellarmap.Check();
            this.SetClosed = new Stellarmap.Check();
            this.SetCanClose = new Stellarmap.Check();
            this.SetKey = new Stellarmap.TextEntry();
            this.SetMaxCarry = new Stellarmap.NumberEntry();
            this.InheritContainer = new Stellarmap.Check();
            this.SetVendorType = new Stellarmap.CheckList();
            this.MainTabPage = new System.Windows.Forms.TabPage();
            this.SetAdjectives = new Stellarmap.ListBuilder();
            this.SetId = new Stellarmap.ListBuilder();
            this.SetLong = new Stellarmap.TextDump();
            this.SetShort = new Stellarmap.TextEntry();
            this.SetKeyName = new Stellarmap.TextEntry();
            this.TabView = new System.Windows.Forms.TabControl();
            this.WeaponPageToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WeaponStats.SuspendLayout();
            this.groupbox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Properties.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.MainTabPage.SuspendLayout();
            this.TabView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InheritTab
            // 
            this.InheritTab.AutoScroll = true;
            this.InheritTab.BackColor = System.Drawing.Color.White;
            this.InheritTab.Location = new System.Drawing.Point(4, 22);
            this.InheritTab.Name = "InheritTab";
            this.InheritTab.Size = new System.Drawing.Size(496, 392);
            this.InheritTab.TabIndex = 4;
            this.InheritTab.Text = "Inherited Features";
            this.InheritTab.UseVisualStyleBackColor = true;
            // 
            // WeaponStats
            // 
            this.WeaponStats.Controls.Add(this.groupbox4);
            this.WeaponStats.Controls.Add(this.SetProperties);
            this.WeaponStats.Controls.Add(this.groupBox2);
            this.WeaponStats.Controls.Add(this.SetDamageType);
            this.WeaponStats.Controls.Add(this.SetWeaponType);
            this.WeaponStats.Location = new System.Drawing.Point(4, 22);
            this.WeaponStats.Name = "WeaponStats";
            this.WeaponStats.Size = new System.Drawing.Size(496, 392);
            this.WeaponStats.TabIndex = 3;
            this.WeaponStats.Text = "Weapon Stats";
            this.WeaponStats.UseVisualStyleBackColor = true;
            // 
            // groupbox4
            // 
            this.groupbox4.Controls.Add(this.SetWieldLevel);
            this.groupbox4.Controls.Add(this.SetWield);
            this.groupbox4.Location = new System.Drawing.Point(241, 224);
            this.groupbox4.Name = "groupbox4";
            this.groupbox4.Size = new System.Drawing.Size(238, 156);
            this.groupbox4.TabIndex = 20;
            this.groupbox4.TabStop = false;
            this.groupbox4.Text = "Additional Weapon Stats";
            // 
            // SetWieldLevel
            // 
            this.SetWieldLevel.BackColor = System.Drawing.Color.Transparent;
            this.SetWieldLevel.Enabled = false;
            this.SetWieldLevel.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetWieldLevel.EntryMaxmimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SetWieldLevel.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetWieldLevel.EntryValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetWieldLevel.FunctionName = "SetWieldLevel";
            this.SetWieldLevel.KeyType = Stellarmap.EntryType.Mixed;
            this.SetWieldLevel.LabelText = "Wield Level:";
            this.SetWieldLevel.Location = new System.Drawing.Point(6, 66);
            this.SetWieldLevel.Name = "SetWieldLevel";
            this.SetWieldLevel.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetWieldLevel.RequiredHeader = null;
            this.SetWieldLevel.Size = new System.Drawing.Size(174, 26);
            this.SetWieldLevel.TabIndex = 0;
            this.SetWieldLevel.ValueType = Stellarmap.EntryType.Mixed;
            this.SetWieldLevel.Visible = false;
            // 
            // SetWield
            // 
            this.SetWield.BackColor = System.Drawing.Color.Transparent;
            this.SetWield.EntryText = "";
            this.SetWield.FunctionName = "SetWield";
            this.SetWield.KeyType = Stellarmap.EntryType.Mixed;
            this.SetWield.LabelText = "Wield Message:";
            this.SetWield.Location = new System.Drawing.Point(6, 34);
            this.SetWield.Name = "SetWield";
            this.SetWield.ParameterType = Stellarmap.FuncParamType.String;
            this.SetWield.RequiredHeader = null;
            this.SetWield.Size = new System.Drawing.Size(191, 26);
            this.SetWield.TabIndex = 18;
            this.SetWield.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetProperties
            // 
            this.SetProperties.BackColor = System.Drawing.Color.Transparent;
            this.SetProperties.FunctionName = "SetProperties";
            this.SetProperties.KeyType = Stellarmap.EntryType.Mixed;
            this.SetProperties.LabelText = "Custom Properties";
            this.SetProperties.ListTextCollection = new string[0];
            this.SetProperties.Location = new System.Drawing.Point(266, 9);
            this.SetProperties.Name = "SetProperties";
            this.SetProperties.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetProperties.RequiredHeader = null;
            this.SetProperties.Size = new System.Drawing.Size(227, 209);
            this.SetProperties.TabIndex = 13;
            this.SetProperties.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SetHands);
            this.groupBox2.Controls.Add(this.SetClass);
            this.groupBox2.Controls.Add(this.SetDamagePoints);
            this.groupBox2.Location = new System.Drawing.Point(9, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 165);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Melee Weapon Properties";
            // 
            // SetHands
            // 
            this.SetHands.BackColor = System.Drawing.Color.Transparent;
            this.SetHands.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetHands.EntryMaxmimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SetHands.EntryMinimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetHands.EntryValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetHands.FunctionName = "SetHands";
            this.SetHands.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetHands.LabelText = "Hands Required:";
            this.SetHands.Location = new System.Drawing.Point(0, 66);
            this.SetHands.Name = "SetHands";
            this.SetHands.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetHands.RequiredHeader = null;
            this.SetHands.Size = new System.Drawing.Size(195, 26);
            this.SetHands.TabIndex = 17;
            this.SetHands.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetClass
            // 
            this.SetClass.BackColor = System.Drawing.Color.Transparent;
            this.SetClass.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetClass.EntryMaxmimum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.SetClass.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetClass.EntryValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetClass.FunctionName = "SetClass";
            this.SetClass.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetClass.LabelText = "Max Damage (Class):";
            this.SetClass.Location = new System.Drawing.Point(0, 34);
            this.SetClass.Name = "SetClass";
            this.SetClass.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetClass.RequiredHeader = null;
            this.SetClass.Size = new System.Drawing.Size(215, 26);
            this.SetClass.TabIndex = 15;
            this.SetClass.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetDamagePoints
            // 
            this.SetDamagePoints.BackColor = System.Drawing.Color.Transparent;
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
            this.SetDamagePoints.Location = new System.Drawing.Point(0, 98);
            this.SetDamagePoints.Name = "SetDamagePoints";
            this.SetDamagePoints.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetDamagePoints.RequiredHeader = null;
            this.SetDamagePoints.Size = new System.Drawing.Size(222, 26);
            this.SetDamagePoints.TabIndex = 16;
            this.SetDamagePoints.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetDamageType
            // 
            this.SetDamageType.BackColor = System.Drawing.Color.Transparent;
            this.SetDamageType.CheckListStrings = new string[0];
            this.SetDamageType.FunctionName = "SetDamageType";
            this.SetDamageType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetDamageType.LabelText = "Damage Types";
            this.SetDamageType.Location = new System.Drawing.Point(7, 42);
            this.SetDamageType.Name = "SetDamageType";
            this.SetDamageType.ParameterType = Stellarmap.FuncParamType.ORList;
            this.SetDamageType.RequiredHeader = null;
            this.SetDamageType.Size = new System.Drawing.Size(259, 176);
            this.SetDamageType.TabIndex = 14;
            this.SetDamageType.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetWeaponType
            // 
            this.SetWeaponType.BackColor = System.Drawing.Color.Transparent;
            this.SetWeaponType.EntryText = "";
            this.SetWeaponType.FunctionName = "SetWeaponType";
            this.SetWeaponType.KeyType = Stellarmap.EntryType.Mixed;
            this.SetWeaponType.LabelText = "Weapon Type:";
            this.SetWeaponType.ListTextCollection = new string[0];
            this.SetWeaponType.Location = new System.Drawing.Point(7, 10);
            this.SetWeaponType.Name = "SetWeaponType";
            this.SetWeaponType.ParameterType = Stellarmap.FuncParamType.String;
            this.SetWeaponType.RequiredHeader = null;
            this.SetWeaponType.SelectedIndex = -1;
            this.SetWeaponType.Size = new System.Drawing.Size(221, 26);
            this.SetWeaponType.TabIndex = 13;
            this.SetWeaponType.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetInventory
            // 
            this.SetInventory.BackColor = System.Drawing.Color.Transparent;
            this.SetInventory.Enabled = false;
            this.SetInventory.EntryText = "";
            this.SetInventory.FunctionName = "SetInventory";
            this.SetInventory.KeyCollection = new string[0];
            this.SetInventory.KeyType = Stellarmap.EntryType.Mixed;
            this.SetInventory.LabelText = "Inventory";
            this.SetInventory.ListTextCollection = new string[0];
            this.SetInventory.Location = new System.Drawing.Point(6, 172);
            this.SetInventory.Name = "SetInventory";
            this.SetInventory.ParameterType = Stellarmap.FuncParamType.Mapping;
            this.SetInventory.RequiredHeader = null;
            this.SetInventory.Size = new System.Drawing.Size(229, 211);
            this.SetInventory.TabIndex = 21;
            this.SetInventory.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // Properties
            // 
            this.Properties.Controls.Add(this.SetInventory);
            this.Properties.Controls.Add(this.groupBox1);
            this.Properties.Controls.Add(this.groupBox3);
            this.Properties.Controls.Add(this.SetVendorType);
            this.Properties.Location = new System.Drawing.Point(4, 22);
            this.Properties.Name = "Properties";
            this.Properties.Padding = new System.Windows.Forms.Padding(3);
            this.Properties.Size = new System.Drawing.Size(496, 392);
            this.Properties.TabIndex = 1;
            this.Properties.Text = "Properties";
            this.Properties.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SetPreventDrop);
            this.groupBox1.Controls.Add(this.SetPreventGet);
            this.groupBox1.Controls.Add(this.SetPreventPut);
            this.groupBox1.Controls.Add(this.SetDestroyOnSell);
            this.groupBox1.Controls.Add(this.CalculateCost);
            this.groupBox1.Controls.Add(this.SetMass);
            this.groupBox1.Controls.Add(this.SetBaseCost);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 163);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Random Propeties";
            // 
            // SetPreventDrop
            // 
            this.SetPreventDrop.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventDrop.EntryText = "";
            this.SetPreventDrop.FunctionName = "SetPreventDrop";
            this.SetPreventDrop.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventDrop.LabelText = "Disable Dropping:";
            this.SetPreventDrop.Location = new System.Drawing.Point(8, 131);
            this.SetPreventDrop.Name = "SetPreventDrop";
            this.SetPreventDrop.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventDrop.RequiredHeader = null;
            this.SetPreventDrop.Size = new System.Drawing.Size(211, 26);
            this.SetPreventDrop.TabIndex = 22;
            this.SetPreventDrop.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetPreventGet
            // 
            this.SetPreventGet.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventGet.EntryText = "";
            this.SetPreventGet.FunctionName = "SetPreventGet";
            this.SetPreventGet.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventGet.LabelText = "Disable Getting:";
            this.SetPreventGet.Location = new System.Drawing.Point(8, 104);
            this.SetPreventGet.Name = "SetPreventGet";
            this.SetPreventGet.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventGet.RequiredHeader = null;
            this.SetPreventGet.Size = new System.Drawing.Size(202, 26);
            this.SetPreventGet.TabIndex = 22;
            this.SetPreventGet.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetPreventPut
            // 
            this.SetPreventPut.BackColor = System.Drawing.Color.Transparent;
            this.SetPreventPut.EntryText = "";
            this.SetPreventPut.FunctionName = "SetPreventPut";
            this.SetPreventPut.KeyType = Stellarmap.EntryType.Mixed;
            this.SetPreventPut.LabelText = "Disable Putting:";
            this.SetPreventPut.Location = new System.Drawing.Point(8, 78);
            this.SetPreventPut.Name = "SetPreventPut";
            this.SetPreventPut.ParameterType = Stellarmap.FuncParamType.String;
            this.SetPreventPut.RequiredHeader = null;
            this.SetPreventPut.Size = new System.Drawing.Size(201, 26);
            this.SetPreventPut.TabIndex = 22;
            this.SetPreventPut.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetDestroyOnSell
            // 
            this.SetDestroyOnSell.BackColor = System.Drawing.Color.Transparent;
            this.SetDestroyOnSell.Checked = true;
            this.SetDestroyOnSell.FunctionName = "SetDestroyOnSell";
            this.SetDestroyOnSell.KeyType = Stellarmap.EntryType.Mixed;
            this.SetDestroyOnSell.LabelText = "Destroy On Sell";
            this.SetDestroyOnSell.Location = new System.Drawing.Point(157, 13);
            this.SetDestroyOnSell.Name = "SetDestroyOnSell";
            this.SetDestroyOnSell.ParameterType = Stellarmap.FuncParamType.Property;
            this.SetDestroyOnSell.RequiredHeader = null;
            this.SetDestroyOnSell.Size = new System.Drawing.Size(107, 22);
            this.SetDestroyOnSell.TabIndex = 19;
            this.SetDestroyOnSell.ValueType = Stellarmap.EntryType.Mixed;
            this.SetDestroyOnSell.Load += new System.EventHandler(this.SetDestroyOnSell_Load);
            // 
            // CalculateCost
            // 
            this.CalculateCost.Location = new System.Drawing.Point(157, 49);
            this.CalculateCost.Name = "CalculateCost";
            this.CalculateCost.Size = new System.Drawing.Size(115, 23);
            this.CalculateCost.TabIndex = 17;
            this.CalculateCost.Text = "Calculate Cost";
            this.CalculateCost.UseVisualStyleBackColor = true;
            this.CalculateCost.Click += new System.EventHandler(this.CalculateCost_Click);
            // 
            // SetMass
            // 
            this.SetMass.BackColor = System.Drawing.Color.Transparent;
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
            this.SetMass.Location = new System.Drawing.Point(6, 19);
            this.SetMass.Name = "SetMass";
            this.SetMass.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetMass.RequiredHeader = null;
            this.SetMass.Size = new System.Drawing.Size(143, 26);
            this.SetMass.TabIndex = 12;
            this.SetMass.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetBaseCost
            // 
            this.SetBaseCost.BackColor = System.Drawing.Color.Transparent;
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
            this.SetBaseCost.Location = new System.Drawing.Point(6, 45);
            this.SetBaseCost.Name = "SetBaseCost";
            this.SetBaseCost.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetBaseCost.RequiredHeader = null;
            this.SetBaseCost.Size = new System.Drawing.Size(145, 26);
            this.SetBaseCost.TabIndex = 11;
            this.SetBaseCost.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SetOpacity);
            this.groupBox3.Controls.Add(this.SetLocked);
            this.groupBox3.Controls.Add(this.SetCanLock);
            this.groupBox3.Controls.Add(this.SetClosed);
            this.groupBox3.Controls.Add(this.SetCanClose);
            this.groupBox3.Controls.Add(this.SetKey);
            this.groupBox3.Controls.Add(this.SetMaxCarry);
            this.groupBox3.Controls.Add(this.InheritContainer);
            this.groupBox3.Location = new System.Drawing.Point(290, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 198);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Container Properties";
            // 
            // SetOpacity
            // 
            this.SetOpacity.BackColor = System.Drawing.Color.Transparent;
            this.SetOpacity.Enabled = false;
            this.SetOpacity.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetOpacity.EntryMaxmimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetOpacity.EntryMinimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetOpacity.EntryValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SetOpacity.FunctionName = "SetOpacity";
            this.SetOpacity.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetOpacity.LabelText = "Opacity:";
            this.SetOpacity.Location = new System.Drawing.Point(12, 69);
            this.SetOpacity.Name = "SetOpacity";
            this.SetOpacity.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetOpacity.RequiredHeader = null;
            this.SetOpacity.Size = new System.Drawing.Size(154, 26);
            this.SetOpacity.TabIndex = 14;
            this.SetOpacity.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetLocked
            // 
            this.SetLocked.BackColor = System.Drawing.Color.Transparent;
            this.SetLocked.Checked = false;
            this.SetLocked.Enabled = false;
            this.SetLocked.FunctionName = "SetLocked";
            this.SetLocked.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetLocked.LabelText = "Locked";
            this.SetLocked.Location = new System.Drawing.Point(107, 138);
            this.SetLocked.Name = "SetLocked";
            this.SetLocked.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetLocked.RequiredHeader = null;
            this.SetLocked.Size = new System.Drawing.Size(70, 22);
            this.SetLocked.TabIndex = 13;
            this.SetLocked.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetCanLock
            // 
            this.SetCanLock.BackColor = System.Drawing.Color.Transparent;
            this.SetCanLock.Checked = false;
            this.SetCanLock.Enabled = false;
            this.SetCanLock.FunctionName = "SetCanLock";
            this.SetCanLock.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetCanLock.LabelText = "Lockable";
            this.SetCanLock.Location = new System.Drawing.Point(12, 138);
            this.SetCanLock.Name = "SetCanLock";
            this.SetCanLock.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetCanLock.RequiredHeader = null;
            this.SetCanLock.Size = new System.Drawing.Size(78, 22);
            this.SetCanLock.TabIndex = 12;
            this.SetCanLock.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetClosed
            // 
            this.SetClosed.BackColor = System.Drawing.Color.Transparent;
            this.SetClosed.Checked = true;
            this.SetClosed.Enabled = false;
            this.SetClosed.FunctionName = "SetClosed";
            this.SetClosed.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetClosed.LabelText = "Closed";
            this.SetClosed.Location = new System.Drawing.Point(107, 110);
            this.SetClosed.Name = "SetClosed";
            this.SetClosed.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetClosed.RequiredHeader = null;
            this.SetClosed.Size = new System.Drawing.Size(66, 22);
            this.SetClosed.TabIndex = 11;
            this.SetClosed.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetCanClose
            // 
            this.SetCanClose.BackColor = System.Drawing.Color.Transparent;
            this.SetCanClose.Checked = true;
            this.SetCanClose.Enabled = false;
            this.SetCanClose.FunctionName = "SetCanClose";
            this.SetCanClose.KeyType = Stellarmap.EntryType.Mixed;
            this.SetCanClose.LabelText = "Closeable";
            this.SetCanClose.Location = new System.Drawing.Point(12, 110);
            this.SetCanClose.Name = "SetCanClose";
            this.SetCanClose.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetCanClose.RequiredHeader = null;
            this.SetCanClose.Size = new System.Drawing.Size(80, 22);
            this.SetCanClose.TabIndex = 10;
            this.SetCanClose.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetKey
            // 
            this.SetKey.BackColor = System.Drawing.Color.Transparent;
            this.SetKey.Enabled = false;
            this.SetKey.EntryText = "";
            this.SetKey.FunctionName = "SetKey";
            this.SetKey.KeyType = Stellarmap.EntryType.Mixed;
            this.SetKey.LabelText = "Key Unlock Id:";
            this.SetKey.Location = new System.Drawing.Point(3, 166);
            this.SetKey.Name = "SetKey";
            this.SetKey.ParameterType = Stellarmap.FuncParamType.String;
            this.SetKey.RequiredHeader = null;
            this.SetKey.Size = new System.Drawing.Size(197, 26);
            this.SetKey.TabIndex = 9;
            this.SetKey.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetMaxCarry
            // 
            this.SetMaxCarry.BackColor = System.Drawing.Color.Transparent;
            this.SetMaxCarry.Enabled = false;
            this.SetMaxCarry.EntryIncrement = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SetMaxCarry.EntryMaxmimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.SetMaxCarry.EntryMinimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SetMaxCarry.EntryValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SetMaxCarry.FunctionName = "SetMaxCarry";
            this.SetMaxCarry.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetMaxCarry.LabelText = "Max Weight:";
            this.SetMaxCarry.Location = new System.Drawing.Point(7, 37);
            this.SetMaxCarry.Name = "SetMaxCarry";
            this.SetMaxCarry.ParameterType = Stellarmap.FuncParamType.Number;
            this.SetMaxCarry.RequiredHeader = null;
            this.SetMaxCarry.Size = new System.Drawing.Size(175, 26);
            this.SetMaxCarry.TabIndex = 8;
            this.SetMaxCarry.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // InheritContainer
            // 
            this.InheritContainer.BackColor = System.Drawing.Color.Transparent;
            this.InheritContainer.Checked = false;
            this.InheritContainer.FunctionName = "inherit LIB_STORAGE;";
            this.InheritContainer.KeyType = Stellarmap.EntryType.NonStrings;
            this.InheritContainer.LabelText = "Is Container";
            this.InheritContainer.Location = new System.Drawing.Point(12, 15);
            this.InheritContainer.Name = "InheritContainer";
            this.InheritContainer.ParameterType = Stellarmap.FuncParamType.Inherit;
            this.InheritContainer.RequiredHeader = null;
            this.InheritContainer.Size = new System.Drawing.Size(90, 22);
            this.InheritContainer.TabIndex = 7;
            this.WeaponPageToolTip.SetToolTip(this.InheritContainer, "Check if this item can be used to hold other items.");
            this.InheritContainer.ValueType = Stellarmap.EntryType.NonStrings;
            // 
            // SetVendorType
            // 
            this.SetVendorType.BackColor = System.Drawing.Color.Transparent;
            this.SetVendorType.CheckListStrings = new string[0];
            this.SetVendorType.FunctionName = "SetVendorType";
            this.SetVendorType.KeyType = Stellarmap.EntryType.NonStrings;
            this.SetVendorType.LabelText = "Vendor Types";
            this.SetVendorType.Location = new System.Drawing.Point(239, 210);
            this.SetVendorType.Name = "SetVendorType";
            this.SetVendorType.ParameterType = Stellarmap.FuncParamType.ORList;
            this.SetVendorType.RequiredHeader = null;
            this.SetVendorType.Size = new System.Drawing.Size(251, 176);
            this.SetVendorType.TabIndex = 12;
            this.WeaponPageToolTip.SetToolTip(this.SetVendorType, "Check all that apply.");
            this.SetVendorType.ValueType = Stellarmap.EntryType.NonStrings;
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
            this.MainTabPage.Size = new System.Drawing.Size(496, 392);
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
            this.WeaponPageToolTip.SetToolTip(this.SetId, "A collection of single words used py the player to reference the item. They *must" +
                    "* be lowercase.");
            this.SetId.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetLong
            // 
            this.SetLong.BackColor = System.Drawing.Color.Transparent;
            this.SetLong.EntryText = "A generic looking thing.";
            this.SetLong.FunctionName = "SetLong";
            this.SetLong.KeyType = Stellarmap.EntryType.Mixed;
            this.SetLong.LabelText = "Long Description";
            this.SetLong.Location = new System.Drawing.Point(216, 6);
            this.SetLong.Name = "SetLong";
            this.SetLong.ParameterType = Stellarmap.FuncParamType.String;
            this.SetLong.RequiredHeader = null;
            this.SetLong.Size = new System.Drawing.Size(254, 101);
            this.SetLong.TabIndex = 17;
            this.WeaponPageToolTip.SetToolTip(this.SetLong, "A full description of an item in sentence format.");
            this.SetLong.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetShort
            // 
            this.SetShort.BackColor = System.Drawing.Color.Transparent;
            this.SetShort.EntryText = "a thing";
            this.SetShort.FunctionName = "SetShort";
            this.SetShort.KeyType = Stellarmap.EntryType.Mixed;
            this.SetShort.LabelText = "Short Desc:";
            this.SetShort.Location = new System.Drawing.Point(13, 55);
            this.SetShort.Name = "SetShort";
            this.SetShort.ParameterType = Stellarmap.FuncParamType.String;
            this.SetShort.RequiredHeader = null;
            this.SetShort.Size = new System.Drawing.Size(171, 26);
            this.SetShort.TabIndex = 16;
            this.WeaponPageToolTip.SetToolTip(this.SetShort, "The name of the item when displayed in a sentence that requires an article. Shoul" +
                    "d be the same as key name but for the article.");
            this.SetShort.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // SetKeyName
            // 
            this.SetKeyName.BackColor = System.Drawing.Color.Transparent;
            this.SetKeyName.EntryText = "thing";
            this.SetKeyName.FunctionName = "SetKeyName";
            this.SetKeyName.KeyType = Stellarmap.EntryType.Mixed;
            this.SetKeyName.LabelText = "Key Name:";
            this.SetKeyName.Location = new System.Drawing.Point(17, 23);
            this.SetKeyName.Name = "SetKeyName";
            this.SetKeyName.ParameterType = Stellarmap.FuncParamType.String;
            this.SetKeyName.RequiredHeader = null;
            this.SetKeyName.Size = new System.Drawing.Size(167, 26);
            this.SetKeyName.TabIndex = 15;
            this.WeaponPageToolTip.SetToolTip(this.SetKeyName, "Name of the item when displayed in sentences.");
            this.SetKeyName.ValueType = Stellarmap.EntryType.Mixed;
            // 
            // TabView
            // 
            this.TabView.Controls.Add(this.MainTabPage);
            this.TabView.Controls.Add(this.Properties);
            this.TabView.Controls.Add(this.WeaponStats);
            this.TabView.Controls.Add(this.InheritTab);
            this.TabView.Location = new System.Drawing.Point(12, 42);
            this.TabView.Name = "TabView";
            this.TabView.SelectedIndex = 0;
            this.TabView.Size = new System.Drawing.Size(504, 418);
            this.TabView.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 1;
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
            // CreateItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(549, 472);
            this.Controls.Add(this.TabView);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CreateItemForm";
            this.Text = "Create Item";
            this.WeaponStats.ResumeLayout(false);
            this.groupbox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.Properties.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.MainTabPage.ResumeLayout(false);
            this.TabView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TabPage InheritTab;
		private System.Windows.Forms.TabPage WeaponStats;
		private System.Windows.Forms.TabPage Properties;
		private System.Windows.Forms.TabPage MainTabPage;
		private System.Windows.Forms.TabControl TabView;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ToolTip WeaponPageToolTip;
		private TextEntry SetKeyName;
		private TextDump SetLong;
		private TextEntry SetShort;
		private ListBuilder SetAdjectives;
		private ListBuilder SetId;
		private CheckList SetVendorType;
		private NumberEntry SetBaseCost;
		private NumberEntry SetMass;
		private MapBuilder SetProperties;
		private Check InheritContainer;
		private NumberEntry SetMaxCarry;
		private Check SetLocked;
		private Check SetCanLock;
		private Check SetClosed;
		private Check SetCanClose;
		private TextEntry SetKey;
        private System.Windows.Forms.GroupBox groupBox1;
		private ComboSelection SetWeaponType;
		private TextEntry SetWield;
		private NumberEntry SetHands;
		private NumberEntry SetDamagePoints;
		private NumberEntry SetClass;
		private CheckList SetDamageType;
		private System.Windows.Forms.GroupBox groupbox4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button CalculateCost;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private Check SetDestroyOnSell;
        private ComboMapBuilder SetInventory;
        private NumberEntry SetOpacity;
        private TextEntry SetPreventDrop;
        private TextEntry SetPreventGet;
        private TextEntry SetPreventPut;
        private NumberEntry SetWieldLevel;
		
		
		}
	}