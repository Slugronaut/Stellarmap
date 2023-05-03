using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;


namespace Stellarmap
	{
	class App
		{			
		[STAThread]
		static int Main()
			{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			
			//create instances
			DomainModel Domain = new DomainModel();
			DomainViewForm Form = new DomainViewForm();
			DomainController Controller = new DomainController(Domain,Form);
						
			//a-a-a-a-and begin!
			Application.Run(Form);
			return 1;
			}
		
		}
	}
