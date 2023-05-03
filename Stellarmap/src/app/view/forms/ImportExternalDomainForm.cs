using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public partial class ImportExternalDomainForm : Form
		{
		private IDomainModelAdapter refDomain;
		
		
		public ImportExternalDomainForm(IDomainModelAdapter domain)
			{
			InitializeComponent();
			
			refDomain = domain;
			Utility.ConfirmRequiredDirectories();
			List<string> files = RemoveIncludedDomainsFromList(BuildListOfAllExternalDomains());
			
			this.checkedListDomains.Items.AddRange(files.ToArray());
			}

		private void button1_Click(object sender,EventArgs e)
			{
			foreach(string dom in checkedListDomains.CheckedItems)
				{
				refDomain.AddDomainReference(dom);
				}
			
			this.Close();
			}
		
		private void checkedListDomains_SelectedIndexChanged(object sender,EventArgs e)
			{
			
			}
		
		private List<string> BuildListOfAllExternalDomains()
			{
			List<string> files = new List<string>(System.IO.Directory.GetFiles(Globals.Dirs.DomainReferences,"*.xml"));
			
			for(int index = 0; index < files.Count; index++)
				{
				files[index] = DomainModel.ConvertDomainFileToDomainName(files[index]);
				}
			
			return files;
			}
		
		private List<string> RemoveIncludedDomainsFromList(List<string> list)
			{
			if(list.Contains(IncludeFile.ConvertToValidDirectoryName(refDomain.DomainName)))
				{
				list.Remove(IncludeFile.ConvertToValidDirectoryName(refDomain.DomainName));
				}
			
			foreach(string s in refDomain.ReferencedDomains)
				{
				if(list.Contains(s)) list.Remove(s);
				}
			
			return list;
			}
		}
	}