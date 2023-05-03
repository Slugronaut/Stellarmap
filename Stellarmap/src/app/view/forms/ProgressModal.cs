using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class ProgressModal : Form
		{
		public ProgressModal(int numSteps) : this()
			{
			this.progressBar1.Maximum = numSteps;
			}
		
		public ProgressModal()
			{
			InitializeComponent();
			CenterToParent();
			}
		
		public void UpdateProgressBar()
			{
			this.progressBar1.Value++;
			}
		}
	}