using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Stellarmap.DeadSouls.Globals
	{
	/// <summary>
	/// Global system directories for Dead Souls.
	/// </summary>
	public static class DeadSoulsGlobals
		{
		//directories
		readonly public static string DeadSoulsRoot = "";//"C:/ds/ds"; TURNS OUT, DEAD SOULS DOESN'T NEED THIS INFO. DEEERRRRRR
		readonly public static string Domains = DeadSoulsRoot + "/domains";
		readonly public static string GlobalDomain = Domains + "/global";
		
		//files
		readonly public static string LocalMasterHeaderName = "/include.h";
		}
	
	/// <summary>
	/// The subdirectories to be created within the domain folder upon domain creation.
	/// 
	/// IMPORTANT: The names of the variables must match the names of the variables in the
	///            enumeration Stellarmap.DomainWorkspaceDirectory due to the use of reflection
	///			   when generating the domain's global include header.
	/// </summary>
	public static class DomainDirectories
		{
		//public static string ThisDomain = "/"; //this will need to be set by the application
		readonly public static string Admin = "/adm";
        readonly public static string Armor = "/armor";
        readonly public static string Door = "/doors";
        readonly public static string Etc = "/etc";
        readonly public static string Meal = "/meals";
        readonly public static string Mob = "/npc";
        readonly public static string Npc = "/npc";
        readonly public static string Object = "/obj";
        readonly public static string Room = "/room";
        readonly public static string Root = "";
        readonly public static string Save = "/save";
        readonly public static string Text = "/txt";
		readonly public static string Virtual = "/virtual";
		readonly public static string Weapon = "/weap";
        readonly public static string Uknown = "/unknown"; //not used
		
		
		static Type MyType = typeof(DomainDirectories);
		
		static public void ValidateDirectoriesExist(string root)
			{
			//ensure root directory exists
			try{Directory.SetCurrentDirectory(root);}
			catch(DirectoryNotFoundException)
				{
				Directory.CreateDirectory(root);
				}
			
			Directory.SetCurrentDirectory(root);
			System.Reflection.FieldInfo[] fields = MyType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			foreach(System.Reflection.FieldInfo field in fields)
			    {
			    //Console.WriteLine(member.Name);
			    //System.Reflection.PropertyInfo pi = MyType.GetProperty(member.Name);

                //skip 'Unknown' directory. Its just a place holder for a return type
                string dir = (string)field.GetValue(null);
			    if(!Directory.Exists(root + dir) && dir != DomainDirectories.Uknown)
					{
					Directory.CreateDirectory(root + dir);
					}
                    
			    }
			}

		}
	
	/// <summary>
	/// Files to be created by default for new domains.
	/// </summary>
	public static class DomainFiles
		{
		public static class Rooms
			{
			readonly public static string Death = "death.c";
			readonly public static string Freezer = "freezer.c";
			readonly public static string Furnace = "furnace.c";
			
			readonly public static string Pod = "pod.c";
			readonly public static string Start = "start.c";
			
			readonly public static string Void = "void.c";
			readonly public static string WizHall = "wiz_hall.c";

			static public void GenerateFiles(string root)
				{
				//ensure root directory exists
				if(!Directory.Exists(root))
					{
					Directory.CreateDirectory(root);
					}

				Type MyType = typeof(Rooms);
				System.Reflection.FieldInfo[] fields = MyType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
				foreach(System.Reflection.FieldInfo field in fields)
					{
					//confirms asset exists (and generates it if it doesn't) then copies
					//it to the path supplied (usually the default room dir of a domain)
					string file = (string)field.GetValue(null);
					Utility.ConfirmRequiredFile(Stellarmap.Globals.Dirs.Code + "\\" + file);

					try
						{
						string subDir = DomainDirectories.Room.Replace("/","");
						File.Copy(Stellarmap.Globals.Dirs.Code + "\\" + file,root + "\\" + subDir + "\\" + file,true);
						}
					catch(IOException exc)
						{
						System.Windows.Forms.MessageBox.Show("Could not copy default room file '" + file + "' to domain.");
						}
					}
				}
			}
		
		public static class Npcs
			{
			readonly public static string Npc = "npc.c";
			readonly public static string Barkeep = "barkeep.c";
			readonly public static string Vendor = "vendor.c";
			readonly public static string Trainer = "trainer.c";
			
			static public void GenerateFiles(string root)
				{
				//ensure root directory exists
				if(!Directory.Exists(root))
					{
					Directory.CreateDirectory(root);
					}

				Type MyType = typeof(Npcs);
				System.Reflection.FieldInfo[] fields = MyType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
				foreach(System.Reflection.FieldInfo field in fields)
					{
					//confirms asset exists (and generates it if it doesn't) then copies
					//it to the path supplied (usually the default room dir of a domain)
					string file = (string)field.GetValue(null);
					Utility.ConfirmRequiredFile(Stellarmap.Globals.Dirs.Code + "\\" + file);

					try
						{
						string subDir = DomainDirectories.Npc.Replace("/","");
						File.Copy(Stellarmap.Globals.Dirs.Code + "\\" + file,root + "\\" + subDir + "\\" + file,true);
						}
					catch(IOException exc)
						{
						System.Windows.Forms.MessageBox.Show("Could not copy default room file '" + file + "' to domain.");
						}
					}
				}
			}
		
		
		
		/// <summary>
		/// Used to copy these files from the app's assets folder. Usually used to
		/// generate default room files for a newly created domain. Asset files are
		/// first copied from the embedded data of the application if the asset files
		/// can't be found.
		/// </summary>
		/// <param name="root"></param>
		static public void GenerateFiles(string root)
			{
			//ensure root directory exists
			Rooms.GenerateFiles(root);
			//Npcs.GenerateFiles(root);
			
			}
		}
	
	public static class AdditionalIncludes
		{
		readonly public static string StellarmassInclude = "#include <stellarmass.h>\n";
		
		public static string GetAllLines()
			{
			//todo: this should use reflection to compose a list but this is the only thing
			//I need for now so I didn't bother
			#if STELLARMASS
			return StellarmassInclude;
			#else
			return "";
			#endif
			}
		}	
	
	public static class DefinedLists
		{
		#region private members
		private readonly static Dictionary<string,List<string>> typeLists = new Dictionary<string,List<string>>();
		/*
		static List<string> vendortypes;
		static List<string> damagetypes;
		static List<string> armortypes;
		static List<string> weapontypes;
		static List<string> limbs;
		static List<string> races;
		static List<string> classes;
		static List<string> currencies;
		static List<string> skills;
		static List<string> stats;
		static List<string> languages;
		static List<string> genders;
		
		static List<string> bodytypes;
		static List<string> buildtypes;
		static List<string> sizetypes;
		static List<string> mealtypes;
		static List<string> static List<string>
		*/
		#endregion
		
		
		#region public common names
		public readonly static string vendortypes = "vendortypes";
		public readonly static string damagetypes = "damagetypes";
		public readonly static string armortypes = "armortypes";
		public readonly static string weapontypes = "weapontypes";
		public readonly static string limbs = "limbs";
		public readonly static string races = "races";
		public readonly static string classes = "classes";
		public readonly static string currencies = "currencies";
		public readonly static string skills = "skills";
		public readonly static string stats = "stats";
		public readonly static string languages = "languages";
		public readonly static string genders = "genders";
		#endregion
		
		
		#region propeties
		public static List<string> GetList(string param)
			{
			param = param.ToLower();
			
			if(typeLists.ContainsKey(param))
				{
				return typeLists[param];
				}
			return null;
			}
		#endregion	
		
			
		#region constructor
		static DefinedLists()
			{
			try{
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Stellarmap.Globals.Files.ConfigFile);
				
				foreach(Sluggy.Utility.Tag tag in parser.Contents.Children[0].Children)
					{
					if(tag.Name == "list")
						{
						if(tag.Attributes.ContainsKey("type"))
							{
							List<string> entries = new List<string>();
							foreach(Sluggy.Utility.Tag child in tag.Children)
								{
								if(child.Name == "entry") {entries.Add(child.Value);}
								}
							
							typeLists.Add(tag.Attributes["type"],entries);
							}
						}
					}
				}//en try
			catch(Sluggy.Utility.XMLParserException e)
				{
				System.Windows.Forms.MessageBox.Show(e.Message);
				}
			catch(System.IO.FileNotFoundException e)
				{
				//swallow exception
				}
			catch(System.IO.DirectoryNotFoundException e)
				{
				//swallow exception
				}
			
			return;
			}
		#endregion
		}
	}
