namespace Stellarmap
	{
	partial class ImportExternalDomainForm
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
			this.buttonOk = new System.Windows.Forms.Button();
			this.checkedListDomains = new System.Windows.Forms.CheckedListBox();
			this.SuspendLayout();
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(267,287);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75,23);
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.button1_Click);
			// 
			// checkedListDomains
			// 
			this.checkedListDomains.CheckOnClick = true;
			this.checkedListDomains.FormattingEnabled = true;
			this.checkedListDomains.Location = new System.Drawing.Point(12,12);
			this.checkedListDomains.Name = "checkedListDomains";
			this.checkedListDomains.Size = new System.Drawing.Size(330,259);
			this.checkedListDomains.TabIndex = 0;
			this.checkedListDomains.SelectedIndexChanged += new System.EventHandler(this.checkedListDomains_SelectedIndexChanged);
			// 
			// ImportExternalDomainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(356,337);
			this.Controls.Add(this.checkedListDomains);
			this.Controls.Add(this.buttonOk);
			this.Name = "ImportExternalDomainForm";
			this.Text = "Import External Domains";
			this.ResumeLayout(false);

			}

		#endregion

		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.CheckedListBox checkedListDomains;
		}
	}