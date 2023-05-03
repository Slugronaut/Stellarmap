using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Stellarmap
	{
	
	/// <summary>
	///  IMPORTANT: The names of the variables must match the names of the variables in the
	///				static class Stellarmap.DeadSouls.Globals.DomainDirectories due to the us
	///				of reflection when generating the domain's global include header.
	/// 
	/// Also note that these variable names will be used directly in the generation of the #define
	/// identifiers after being capitalized and appended with the domain name.
	/// </summary>
	public enum DomainWorkspaceDirectory
		{
		Admin,
		Armor,
		Door,
		Etc,
		Meal,
		Mob,
		Npc,
		Object,
		Room,
		Root,
		Save,
		Text,
		Virtual,
		Weapon,
		Unknown,
		}
	
	
	/// <summary>
	/// A single DS directory as represented by a defined constant in a header file.
	/// </summary>
	internal class DefinedDirectory
		{
		public readonly string IdentifiersTitle;
		public readonly string DomainConstantTitle;
		public readonly string DefinedIndentifier = "";
		public readonly string DirectoryLiteral;
		public readonly DomainWorkspaceDirectory DirectoryType;
		
		/// <summary>
		/// Constructs the identifier and value for a single #define statment for use in a domain's global
		/// header file. The identifier name is an amalgamation of the passed domain name and
		/// the name of the variable of the enumerated type. The string literal for the value
		/// is obtained through reflection from the Stellarmap.DeadSouls.Globals.DomainDirectories
		/// class using reflection by matching the enumed variable name to the aforementioned class's
		/// variable of the same name. (yeh, read that again then go look up the files in question so
		/// you get it straight. I realize that was pretty obtuse ;)
		/// </summary>
		/// <param name="domainName"></param>
		/// <param name="identifier"></param>
		public DefinedDirectory(string domainName,string identifiersTitle,DomainWorkspaceDirectory identifier)
			{
			domainName = IncludeFile.ConvertToValidDirectoryName(domainName);
			DomainConstantTitle = domainName;
			IdentifiersTitle = identifiersTitle;
			
			//HACK ALERT: We are retreiving the name of the enumeration's identifier in DomainWorkspaceDirectory
			//            and then using that name to find the variable of the same name in DeadSouls.GLobals.DomainDirecotries
			//			  so that we can retreive the value from it. Using this method we can generate both the identifer and the value
			//            for a single #define statement.
			Type dirGlobal = typeof(DeadSouls.Globals.DomainDirectories);
            //don't bother with any of this if the define is being generated for 'Unknown'. It's just
            //a paceholder return type.
            if(dirGlobal.Name == "Unknown")
                {
                this.DirectoryType = DomainWorkspaceDirectory.Unknown;
                this.DirectoryLiteral = "";
                this.DefinedIndentifier = "";
                this.DomainConstantTitle = "";
                this.IdentifiersTitle = "";
                return;
                }
            FieldInfo globalInfo = dirGlobal.GetField(identifier.ToString(),BindingFlags.Static | BindingFlags.Public);
			if(globalInfo != null)
				{
				DefinedIndentifier = IdentifiersTitle.ToUpper() + "_" + identifier.ToString().ToUpper();
				string lastDir = (string)globalInfo.GetValue(null);
				if(lastDir.Length > 0)
					{
					DirectoryLiteral = DeadSouls.Globals.DeadSoulsGlobals.Domains + "/" + DomainConstantTitle + lastDir;
					}
				else{
					DirectoryLiteral = DeadSouls.Globals.DeadSoulsGlobals.Domains;
					}
				}
			
			DirectoryType = identifier;

            //if it's the root directory we need to add the domain directory name to the end of it
			//if(dirGlobal.Name == "Root")
            if(globalInfo.Name == "Root")
                {
                DirectoryLiteral += "/" + domainName;
                }
			return;
			}
		}
	
	
	/// <summary>
	/// 
	/// </summary>
	internal class DomainDirectoryDefines
		{
		readonly public Dictionary<DomainWorkspaceDirectory,DefinedDirectory> Defines = new Dictionary<DomainWorkspaceDirectory,DefinedDirectory>();
		readonly public string DomainName;
		
		public DomainDirectoryDefines(string domainName,Dictionary<DomainWorkspaceDirectory,DefinedDirectory> defines)
			{
			if(domainName.Length < 1)	throw new ArgumentException("Domain name invalid");
			if(defines == null)			throw new ArgumentNullException();
			
			DomainName = domainName;
			Defines = defines;
			}
		}
	
	
	/// <summary>
	/// This object represents all of the values that will be defined
	/// in the domain's header file, "include.h".
	/// 
	/// This header allows us to change directories later by simply changing the
	/// defined constants rather than having to change the reference strings in
	/// every file found in the domain.
	/// </summary>
	public sealed class IncludeFile
		{
		#region private members
		public readonly string LocalDomain; //for referencing the main domain
		readonly string DomainName;
		readonly string DomainRoot;
		Dictionary<string,DomainDirectoryDefines>	ReferenceDomains = new Dictionary<string,DomainDirectoryDefines>();
		#endregion
		
		
		#region public constants
		readonly public Dictionary<string,ItemSaveType> DirectoryToTypeMap = new Dictionary<string,ItemSaveType>();
		#endregion
		
		
		#region constructor
		public IncludeFile(string domainName)
			{
			DomainName = IncludeFile.ConvertToValidDirectoryName(domainName);
			DomainRoot = DeadSouls.Globals.DeadSoulsGlobals.Domains + "/" + domainName;
			LocalDomain = domainName;
			
			AddDomainReference(domainName,"domain");
			
			}
		#endregion
		
		
		#region public methods
		/// <summary>
		/// Removes the leading Domain path so that only the remaining text is returned
		/// which should hopefully give a raw and singlular folder name.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string StripDeadSoulsPath(string path)
			{
			//check every defined value in order to determine
			foreach(string dom in this.ReferenceDomains.Keys)
				{
				if(path.StartsWith(DeadSouls.Globals.DeadSoulsGlobals.Domains + "/" + dom + "/"))
					{
					return path.Replace(DeadSouls.Globals.DeadSoulsGlobals.Domains + "/" + dom + "/","");
					}
				else if(path.StartsWith("/"))
					{
					return path.Replace("/","");
					}
				}
			return path;
			}
		
		public string ConvertNameToFullPath(string objectPath,string roomName)
			{
			return objectPath + " \"/" + roomName + "\"";
			}

		public string ConvertFullPathToName(string fileName)
			{
			//foreach(string key in Defines.Keys)
			//    {
			//    if(fileName.Contains(key))
			//        {
			//        fileName = fileName.Replace(key + " \"/","");
			//        fileName = fileName.Replace("\"","");
			//        }
			//    }
			
			//foreach(DefinedDirectory definedDir in DefinedValues.Values)
			//    {
			//    string id = definedDir.DefinedIndentifier;
			//    if(fileName.Contains(id))
			//        {
			//        fileName = fileName.Replace(id + "\"/","");
			//        fileName = fileName.Replace("\"","");
			//        return fileName;
			//        }
			//    }
			foreach(DomainDirectoryDefines definesList in ReferenceDomains.Values)
				{
				foreach(DefinedDirectory definedDir in definesList.Defines.Values)
					{
					string id = definedDir.DefinedIndentifier;
				    if(fileName.Contains(id))
				        {
				        //we do it this way to ensure we don't miss any spaces between names (ie: DOMAIN_ROOM"/file.c"  vs. DOMAIN_ROOM "/file.c" )
				        fileName = fileName.Replace(id,"");
				        fileName = fileName.Replace("\"/","");
				        fileName = fileName.Replace("\"","");
				        fileName.Trim();
				        return fileName;
				        }
					}
				}
			return fileName;
			}

		/// <summary>
		/// Determines the item type (and thus the directory is should be saved too) based
		/// on it's full path, regardless of it's domain.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns></returns>
		public DomainWorkspaceDirectory GetDirectoryType(string fullPath)
			{
			fullPath = IncludeFile.ConvertToValidDirectoryName(fullPath);
			
			foreach(DomainDirectoryDefines definesList in ReferenceDomains.Values)
				{
				foreach(DefinedDirectory definedDir in definesList.Defines.Values)
					{
					//METHOD 1
					//string basePath = DeadSouls.Globals.DeadSoulsGlobals.Domains + "/" + definedDir.DomainConstantTitle;
					//string remainingPath = definedDir.DirectoryLiteral.Replace(basePath + "/","");
					
					//METHOD 2
					//string[] temp = definedDir.DirectoryLiteral.Split('/');
					//string remainingPath = temp[temp.Length-1];
					
					////de-pluralize
					//remainingPath = remainingPath.TrimEnd('s');
					
					//if(fullPath.Contains(remainingPath))
					//    {
					//    return definedDir.DirectoryType;
					//    }
					
					//METHOD 3
					if(definedDir.DefinedIndentifier.ToLower().Contains(fullPath.ToLower()))
						{
						return definedDir.DirectoryType;
						}
					}
				}
			
			return DomainWorkspaceDirectory.Unknown;
			}

		public void CreateFile(string dir)
			{
			string filePath = dir + Stellarmap.DeadSouls.Globals.DeadSoulsGlobals.LocalMasterHeaderName;
			if(System.IO.File.Exists(filePath))
				{
				}
			using(System.IO.StreamWriter stream = new System.IO.StreamWriter(filePath))
				{
				stream.Write(this.Composed());
				stream.Flush();
				}

			}
		
		public string GetPathDefinitionValue(ItemSaveType saveType)
			{
			//HACK ALERT: this is just hardcoded values. ItemSaveType needs to be phased out by DomainWorkspaceDirectories
			//Also note that this only works for the default domain (the one this workspace is for, because
			//that's the context under which we would ever need to know where to save files
			switch(saveType)
				{
				case ItemSaveType.Armor: 
					{return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Armor].DirectoryLiteral;}
				case ItemSaveType.Door:
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Door].DirectoryLiteral; }
				case ItemSaveType.Meal:
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Meal].DirectoryLiteral; }
				case ItemSaveType.NPC:
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Npc].DirectoryLiteral; }
				case ItemSaveType.Object:
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Object].DirectoryLiteral; }
				case ItemSaveType.Weapon:
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Weapon].DirectoryLiteral; }
				default: 
					{ return this.ReferenceDomains[this.DomainName].Defines[DomainWorkspaceDirectory.Object].DirectoryLiteral; }
				}
			}	
		
		public string GetPathDefinitionIdentifier(ItemSaveType saveType,string domain)
			{
			domain = IncludeFile.ConvertToValidDirectoryName(domain);
			//HACK ALERT: this is just hardcoded values. ItemSaveType needs to be phased out by DomainWorkspaceDirectories
			//Also note that this only works for the default domain (the one this workspace if for, because
			//that's the context under which we would ever need to know where to save files
			switch(saveType)
				{
				case ItemSaveType.Armor:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Armor].DefinedIndentifier; }
				case ItemSaveType.Door:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Door].DefinedIndentifier; }
				case ItemSaveType.Meal:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Meal].DefinedIndentifier; }
				case ItemSaveType.NPC:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Npc].DefinedIndentifier; }
				case ItemSaveType.Object:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Object].DefinedIndentifier; }
				case ItemSaveType.Room:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Room].DefinedIndentifier; }
				case ItemSaveType.Weapon:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Weapon].DefinedIndentifier; }
				default:
					{ return this.ReferenceDomains[domain].Defines[DomainWorkspaceDirectory.Object].DefinedIndentifier; }
				}
			}
		
		public void AddDomainReference(string domainName,string domainTitle)
			{
			domainName = IncludeFile.ConvertToValidDirectoryName(domainName);
			if(ReferenceDomains.ContainsKey(domainName))	return;
			
			//to maintain backwards compatibility we refer to the started domain (the one we are building
			//with this tool) with the title 'DOMAIN' rather than simply the domain name
			DomainDirectoryDefines defines = new DomainDirectoryDefines(domainName,DefineAllValuesForDomain(domainName,domainTitle));
			this.ReferenceDomains.Add(domainName,defines);
			}

		public ItemSaveType DetermineSaveType(string fullPath)
			{
			//string path = fullPath.ToLower();
			
			//HACK ALERT: this should probably use reflection or maybe even just some iteration
			//if(Defines[refIncludeFile.Object].ToLower().Contains(path) ) return ItemSaveType.Object;
			//if(Defines[refIncludeFile.Armor].ToLower().Contains(path) ) return ItemSaveType.Armor;
			//if(Defines[refIncludeFile.Weapon].ToLower().Contains(path) ) return ItemSaveType.Weapon;
			//if(Defines[refIncludeFile.NPC].ToLower().Contains(path) ) return ItemSaveType.NPC;
			//if(Defines[refIncludeFile.Meal].ToLower().Contains(path) ) return ItemSaveType.Meal;
			//if(Defines[refIncludeFile.Door].ToLower().Contains(path)) return ItemSaveType.Door;
			//if(Defines[refIncludeFile.Room].ToLower().Contains(path)) return ItemSaveType.Room;

			//foreach(string definedPath in Defines.Values)
			//    {
			//    if(definedPath.ToLower().Contains(path))
			//        {
			//        if(DirectoryToTypeMap.ContainsKey(definedPath))
			//        { return DirectoryToTypeMap[definedPath]; }
			//        else { return ItemSaveType.Object; }
			//        }
			//    }
			
			switch(GetDirectoryType(fullPath))
				{
				case DomainWorkspaceDirectory.Armor:	{return ItemSaveType.Armor;}
				case DomainWorkspaceDirectory.Door:		{return ItemSaveType.Door;}
				case DomainWorkspaceDirectory.Meal:		{return ItemSaveType.Meal;}
				case DomainWorkspaceDirectory.Npc:		{return ItemSaveType.NPC;}
				case DomainWorkspaceDirectory.Object:	{return ItemSaveType.Object;}
				case DomainWorkspaceDirectory.Room:		{return ItemSaveType.Room;}
				case DomainWorkspaceDirectory.Weapon:	{return ItemSaveType.Weapon;}
				default:	{return ItemSaveType.Unknown;}
				}
			}		
		#endregion
		
		
		#region private methods
		public static string ConvertToValidDirectoryName(string dir)
			{
			dir = dir.Replace(" ","");
			dir = dir.Replace("\t","");
			dir = dir.Replace("\n","");
			dir = dir.Replace("\r","");
			dir = dir.ToLower();
			return dir;
			}
		
		private Dictionary<DomainWorkspaceDirectory,DefinedDirectory> DefineAllValuesForDomain(string domainReferenceName,string domainReferenceTitle)
			{
			if(domainReferenceName.Length < 1) throw new ArgumentException("Domain reference name was not specified for the include file directories");
			
			Dictionary<DomainWorkspaceDirectory,DefinedDirectory> list = new Dictionary<DomainWorkspaceDirectory,DefinedDirectory>();
			Type dirEnumType = typeof(DomainWorkspaceDirectory);
			foreach(FieldInfo info in dirEnumType.GetFields(BindingFlags.Public | BindingFlags.Static))
				{
                //don't count 'Unknown' directory it's just a placeholder value for a return type
                if(info.Name != "Unknown")
                    {
                    list.Add((DomainWorkspaceDirectory)info.GetValue(null),new DefinedDirectory(domainReferenceName,domainReferenceTitle,(DomainWorkspaceDirectory)info.GetValue(null)));
				    }
                
                }
			
			return list;
			}
		
		/// <summary>
		/// Creates a string that contains the entire contents of a default header file.
		/// TODO: Add parameter for upcoming 'rference domain' list. That way it can have
		/// #defines for each of the referenced domains stuff too.
		/// </summary>
		/// <returns></returns>
		private string Composed()
			{
			StringBuilder str = new StringBuilder();

			str.Append("//Domain header generated by " + Stellarmap.Globals.Stellarmap.ProgName +
					   " " + Stellarmap.Globals.Stellarmap.Stage +
					   " " + Stellarmap.Globals.Stellarmap.Version +
					   "\n//Warning, this file will be deleted and re-created frequenty.\n//Do not modify manually. It will be largely a waste of your time.\n\n");
			str.Append(DeadSouls.Globals.AdditionalIncludes.GetAllLines());
			str.Append("#ifndef __" + DomainName.Replace(" ","").ToUpper() + "_H\n");
			str.Append("#define __" + DomainName.Replace(" ","").ToUpper() + "_H\n\n");

			foreach(DomainDirectoryDefines defines in ReferenceDomains.Values)
				{
				foreach(DefinedDirectory definedDir in defines.Defines.Values)
					{
					str.Append("#define " + definedDir.DefinedIndentifier + "\t\t" + "\"" + definedDir.DirectoryLiteral + "\"\n");
					}
				str.Append("\n");
				}
			str.Append("\n#endif\n\n");

			return str.ToString();
			}
		#endregion
		}
	
	}
