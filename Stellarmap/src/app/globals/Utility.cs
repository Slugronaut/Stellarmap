using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Stellarmap
	{
	public class UTF8NoSigEncoding : UTF8Encoding
		{
		private const string NameAppend = " (no signature)";
		public UTF8NoSigEncoding()	: base()
			{
			}
		
		public UTF8NoSigEncoding(bool encoderShouldEmitUTF8Identifier)	: base(encoderShouldEmitUTF8Identifier)
			{
			}
		
		public UTF8NoSigEncoding(bool encoderShouldEmitUTF8Identifier,bool throwOnInvalidBytes) : base(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes)
			{
			}

		public override string EncodingName
			{
			get
				{
				return base.EncodingName + NameAppend;
				}
			}
		
		public override string WebName
			{
			get
				{
				return base.WebName + NameAppend;
				}
			}
		}
	
	public static class Utility
		{
		//[DllImport("kernel32.dll")]
		//public static extern bool Beep(int Frequency, int Duration);
		
		//static public bool Beep()
		//    {
		//    Beep(250, 250);
		//    return true;
		//    }

		// xcopy "$(ProjectDir)assets" "$(ProjectDir)$(OutDir)assets" /v /e /d  /y
		
		#region Win32
		#if MONOCOMPAT
		public static void MessageBeep(uint uType)
			{
			}
		#else
		[DllImport("user32.dll")]
		public static extern void MessageBeep(uint uType);
		#endif

		public const uint MB_OK                = 0x00000000;
	    public const uint MB_ICONHAND          = 0x00000010;
		public const uint MB_ICONQUESTION      = 0x00000020;
		public const uint MB_ICONEXCLAMATION   = 0x00000030;
		public const uint MB_ICONASTERISK      = 0x00000040;   
		#endregion

		//public static void MessageBeep(uint type)
		//{
		//	MessageBeep(type);
		//}
		
		/// <summary>
		/// Confirms that a required config directory is available. If not,
		/// it is generated.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static bool ConfirmRequiredDirectories()
			{
			Type globalType = typeof(Globals.Dirs);
			System.Reflection.FieldInfo[] fields = globalType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			foreach(System.Reflection.FieldInfo field in fields)
				{
				string directoryName = (string)field.GetValue(null);
				if(!System.IO.Directory.Exists(directoryName))
					{
					System.IO.Directory.CreateDirectory(directoryName);
					}
				}
			
			return true;
			}
		
		/// <summary>
		/// Confirms that a file exists in the specified directory, if not it generates it from
		/// a backup embedded in the program.
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static bool ConfirmRequiredFile(string filePath)
			{
			if(filePath == null || filePath.Length < 1) return false;
			string[] temp = filePath.Split('\\');
			if(temp.Length < 1) return false;
			string fileName = temp[temp.Length - 1];
			
			ConfirmRequiredDirectories();

			StringBuilder resourceName = new StringBuilder("Stellarmap.assets.");
			string[] resPath = null;
			//HACK ALERT: hardcoded values here
			if(fileName.EndsWith(".bmp"))	{ resPath = Globals.Dirs.Bitmaps.Split('\\'); }
			if(fileName.EndsWith(".png"))	{ resPath = Globals.Dirs.Bitmaps.Split('\\'); }
			if(fileName.EndsWith(".c"))		{ resPath = Globals.Dirs.Code.Split('\\'); }
			if(fileName.EndsWith(".h"))		{ resPath = Globals.Dirs.Code.Split('\\'); }
			if(fileName.EndsWith(".xml"))	{ resPath = Globals.Dirs.Config.Split('\\'); }
			if(fileName.EndsWith(".aff"))	{ resPath = Globals.Dirs.SpellCheck.Split('\\'); }
			if(fileName.EndsWith(".dic"))   { resPath = Globals.Dirs.SpellCheck.Split('\\'); }
			if(resPath != null && resPath.Length > 0)
				{
				string s = resPath[resPath.Length-1];
				if(s != null && s.Length > 0)
					{resourceName.Append(s + ".");}
				}
			resourceName.Append(fileName);
			
			

			if(!System.IO.File.Exists(filePath))
				{
				try
					{
					System.Reflection.Assembly assem = System.Reflection.Assembly.GetExecutingAssembly();
					System.IO.Stream stream = assem.GetManifestResourceStream(resourceName.ToString());
					if(stream == null)
						{
						throw new Exception("Could not locate embedded resource '" + fileName + ".");
						}

					using(System.IO.FileStream file = new System.IO.FileStream(filePath,System.IO.FileMode.OpenOrCreate))
						{
						//copy backup resource to disk
						//TODO: This only copies a byte at a time - kinda slow
						for(int chunk = 0; chunk < stream.Length; chunk++)
							{
							file.WriteByte((byte)stream.ReadByte());
							}
						}
					}
				catch(Exception e)
					{
					System.Windows.Forms.MessageBox.Show("Required file not obtained: " + filePath + "\n\n" + e.Message);
					}


				}

			return true;
			}

		static public Dictionary<string,System.Drawing.Bitmap> LoadIconsFromConfig(string configFile)
			{
			Dictionary<string,Bitmap> roomIcons = new Dictionary<string,Bitmap>();
			try
				{
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(configFile);
				List<Sluggy.Utility.Tag> items = parser.FindAll(null,"icon");

				
				if(items != null)
					{
					foreach(Sluggy.Utility.Tag tag in items)
						{
						//config global bitmaps
						if(tag.Attributes.ContainsKey("file") && tag.Attributes.ContainsKey("desc"))
							{
							Utility.ConfirmRequiredFile(Globals.Dirs.Bitmaps + tag.Attributes["file"]);
							try{
								Bitmap image = new Bitmap(Globals.Dirs.Bitmaps + tag.Attributes["file"]);
								roomIcons.Add(tag.Attributes["desc"],image);
								}
							catch(System.IO.FileNotFoundException e)
								{
								//swallow exception
								}
							catch(System.IO.DirectoryNotFoundException e)
								{
								//swallow exception
								}
							}


						}//end if
					}//end foreach
				}//end try
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
			
			return roomIcons;
			}
		}
	}
