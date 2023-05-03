using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class About : Form
		{
		public About()
			{
			InitializeComponent();
			
			//CreateLink(this.linkDeadSouls,"http://www.dead-souls.net");
			//CreateLink(this.linkKeetah,"http://maniac-online.livejournal.com/");
			//CreateLink(this.linkStellarmass,"http://space-madness.net/stellarmass/");
            this.StellarmapVersion.Text += Stellarmap.Globals.Stellarmap.Version + "  " + Stellarmap.Globals.Stellarmap.Stage;
			}
		
		private void About_Load(object sender,EventArgs e)
			{
			this.CenterToParent();
			}



		private void linkDeadSouls_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
			{
			System.Diagnostics.Process.Start("http://www.dead-souls.net");
			//System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
			}

		private void linkKeetah_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
			{
			System.Diagnostics.Process.Start("http://astrocat.space-madness.net/");
			//System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
			}
		
		private void linkStellarmass_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
			{
			System.Diagnostics.Process.Start("http://stellarmass.space-madness.net/");
			//System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
			}

		

		private void CreateLink(LinkLabel label,string address)
			{
			LinkLabel.Link link = new LinkLabel.Link();
			link.LinkData = address;
			//link.Length = 4;//label.Text.Length;
			link.Start = 0;
			link.Enabled = true;

			label.Links.Add(link);
			}

		private void linkHunspell_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
			{
			System.Diagnostics.Process.Start("http://nhunspell.sourceforge.net/");
			}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
            System.Diagnostics.Process.Start("http://slugronaut.space-madness.net/");
            }

		

		

		}
	}