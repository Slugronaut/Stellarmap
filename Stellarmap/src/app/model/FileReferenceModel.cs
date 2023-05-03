using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Stellarmap
	{
	/// <summary>
	/// Quick reference to an item loaded into the domain's list.
	/// It is stored along with it's directory information
	/// and object type.
	/// </summary>
	public class FileReferenceModel
		{
		public string FullDSPath;
		public string RawName;
		public ItemSaveType Type;
		public IncludeFile refIncludeFile;

		public Dictionary<string,ItemSaveType> TypeNameMapByName = new Dictionary<string,ItemSaveType>();
		public Dictionary<ItemSaveType,string> TypeNameMapByType = new Dictionary<ItemSaveType,string>();

		private FileReferenceModel()
			{
			Type enumType = typeof(ItemSaveType);
			System.Reflection.FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
			foreach(System.Reflection.FieldInfo field in fields)
				{
				object en = field.GetValue(null);
				string name = field.Name.ToLower();
				TypeNameMapByName.Add(name, (ItemSaveType)en);
				TypeNameMapByType.Add((ItemSaveType)en, name);
				}
			}

		public FileReferenceModel(string fullDsPath,IncludeFile includeFile)
			: this()
			{
			//we should be able to figure out what it is based on it's save path
			refIncludeFile = includeFile;
			Type = includeFile.DetermineSaveType(fullDsPath);
			RawName = ConvertFullPathToName(fullDsPath);
			FullDSPath = fullDsPath;
			}

		public FileReferenceModel(string itemName,ItemSaveType type,IncludeFile includeFile,string domain)
			: this()
			{
			refIncludeFile = includeFile;
			Type = type;
			RawName = itemName;
			FullDSPath = ConvertNameToFullPath(itemName,domain);
			}

		public string ConvertNameToFullPath(string itemName,string domain)
			{
			RawName = itemName;
			StringBuilder itemPath = new StringBuilder(Stellarmap.Globals.Generator.FunctionParameterLiteral);
			itemPath.Append(refIncludeFile.GetPathDefinitionIdentifier(Type,domain));
			itemPath.Append(" \"/" + itemName);

			if(!itemPath.ToString().EndsWith(Globals.Model.RoomExtension))
				{
				itemPath.Append(Globals.Model.RoomExtension);
				}
			itemPath.Append("\"");
			return itemPath.ToString();
			}

		public string ConvertFullPathToName(string fileName)
			{
			fileName = fileName.TrimStart(Globals.Generator.FunctionParameterLiteralChar);
			return refIncludeFile.ConvertFullPathToName(fileName);
			}

		}
	}
