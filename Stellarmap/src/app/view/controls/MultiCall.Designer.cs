namespace Stellarmap.app.view.controls
	{
	partial class MultiCall
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
			{
			this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonAddNew = new System.Windows.Forms.Button();
			this.buttonRemoveLast = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// flowLayoutPanel
			// 
			this.flowLayoutPanel.AutoSize = true;
			this.flowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel.Location = new System.Drawing.Point(3,51);
			this.flowLayoutPanel.Name = "flowLayoutPanel";
			this.flowLayoutPanel.Size = new System.Drawing.Size(169,155);
			this.flowLayoutPanel.TabIndex = 0;
			// 
			// buttonAddNew
			// 
			this.buttonAddNew.Location = new System.Drawing.Point(3,12);
			this.buttonAddNew.Name = "buttonAddNew";
			this.buttonAddNew.Size = new System.Drawing.Size(75,23);
			this.buttonAddNew.TabIndex = 1;
			this.buttonAddNew.Text = "Add New";
			this.buttonAddNew.UseVisualStyleBackColor = true;
			// 
			// buttonRemoveLast
			// 
			this.buttonRemoveLast.Location = new System.Drawing.Point(97,12);
			this.buttonRemoveLast.Name = "buttonRemoveLast";
			this.buttonRemoveLast.Size = new System.Drawing.Size(75,23);
			this.buttonRemoveLast.TabIndex = 2;
			this.buttonRemoveLast.Text = "Remove last";
			this.buttonRemoveLast.UseVisualStyleBackColor = true;
			// 
			// MultiCall
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.Controls.Add(this.buttonRemoveLast);
			this.Controls.Add(this.buttonAddNew);
			this.Controls.Add(this.flowLayoutPanel);
			this.Name = "MultiCall";
			this.Size = new System.Drawing.Size(175,212);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
		private System.Windows.Forms.Button buttonAddNew;
		private System.Windows.Forms.Button buttonRemoveLast;

		}
	}
