using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Stellarmap
	{
	//TODO: First off, there needs to be a safer and easier to follow way of organising global
	//constants into two categories. One should be immutal (truely readonly globals) and ther others
	//are configurable. For the latter group, they should be seen by the rest of the program as
	//readonly globals except for a special interface called 'Configurable' that should be able to
	//modify them
	namespace Globals
		{
		public static class Stellarmap
			{
			readonly public static string ProgName = "Stellarmap";
			readonly public static string Stage = "beta";
			readonly public static string Version = "0.9 - 2011.8.28";
			}
		
		public static class Dirs
			{
			readonly public static string Root = System.Windows.Forms.Application.StartupPath;
			readonly public static string Assets = Root + "\\assets";
			readonly public static string Bitmaps = Assets + "\\bitmaps";	
			readonly public static string Code = Assets + "\\code";
			readonly public static string SpellCheck = Assets + "\\spellcheck";
			readonly public static string DomainReferences = Assets + "\\domainreferences";
			#if STELLARMASS
			readonly public static string Config = Assets + "\\smver_config";
			#else
			readonly public static string Config = Assets + "\\dsver_config";
			#endif
			}
		
		public static class Files
			{
			readonly private static string pConfigFile = Dirs.Config + "\\config.xml";
			readonly private static string pItemsConfigFile = Dirs.Config + "\\inheriteditems.xml";
			readonly private static string pArmorsConfigFile = Dirs.Config + "\\inheritedarmors.xml";
			readonly private static string pNpcsConfigFile = Dirs.Config + "\\inheritednpcs.xml";
			readonly private static string pHelpFile = Dirs.Root + "\\stellarmap_help.chm";
			
			public static string ConfigFile
				{
				get {
					Utility.ConfirmRequiredFile(pConfigFile);
					return pConfigFile;
					}
				}
			
			public static string ItemsConfigFile
				{
				get {
					Utility.ConfirmRequiredFile(pItemsConfigFile);
					return pItemsConfigFile;
					}
				}

			public static string ArmorsConfigFile
				{
				get
					{
					Utility.ConfirmRequiredFile(pArmorsConfigFile);
					return pArmorsConfigFile;
					}
				}

			public static string NpcsConfigFile
				{
				get
					{
					Utility.ConfirmRequiredFile(pNpcsConfigFile);
					return pNpcsConfigFile;
					}
				}
			
			public static string HelpFile
				{
				get{
					//Utility.ConfirmRequiredFile(pItemsConfigFile);
					return pHelpFile;
					}
				}
			}
		
		public static class GUI
			{
			readonly public static int ViewDragSpeedModifier = 1;
			readonly public static int AutoScrollBorderRegion = 64; //the distance from the edge that an image can be dragged before the viewport starts scrolling
			readonly public static int AutoScrollRate = 10;
			
			//TODO: in the future these should be configurable
			readonly public static Color InactiveBackgroundColor = Color.FromArgb(0xff,0x60,0x60,0x60);
			readonly public static Color ActiveBackgroundColor = Color.FromArgb(0xff,0x23,0x30,0x66);
			readonly public static Color NodeColor = Color.FromArgb(0xff,0xcc,0xcc,0xcc);
			readonly public static Color NodeConnectedColor = Color.FromArgb(0xff,0x22,0xff,0x44);
			readonly public static Color NodeFailedConnectedColor = Color.FromArgb(0xff,0xff,0x22,0x22);
			readonly public static Color DoorHilight = Color.Yellow;
			readonly public static Color ConnectionLineCardinal = Color.White;
			readonly public static Color ConnectionLineUp = Color.Tomato;
			readonly public static Color ConnectionLineDown = Color.SteelBlue;
			readonly public static Color ConnectionLineOther = Color.DarkOrange;
			readonly public static Color ConnectionLineOut = Color.LimeGreen;
			}
		
		public static class Model
			{
			public static string BaseRoomName = "room_";
			public static string CustomStartRoomName = "start";
			readonly public static string DefaultStartRoomName = "start"; //user configurable
            readonly public static string InstanceRoom = "instance";
            readonly public static string Shop = "shop";
            readonly public static string PoliceOffice = "policeoffice";
            readonly public static string JailCell = "jailcell";
			readonly public static string Item = "item";
			readonly public static string Armor = "armor";
			readonly public static string Storage = "storage";
			readonly public static string Door = "door";
			readonly public static string Npc = "npc";
			readonly public static string Food = "food";
			readonly public static string RoomExtension = ".c";
			
			static Model()
				{
				//load up config and check out the model properties
				try
					{
					Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
					List<Sluggy.Utility.Tag> items = parser.FindAll(null,"baseroom");
					items.AddRange(parser.FindAll(null,"startroom"));

					//add default

					if(items != null)
						{
						foreach(Sluggy.Utility.Tag tag in items)
							{
							if(tag.Name == "baseroom")
								{
								if(tag.Attributes.ContainsKey("name"))
									{
									BaseRoomName = tag.Attributes["name"];
									}
								}
							if(tag.Name == "startroom")
								{
								if(tag.Attributes.ContainsKey("name"))
									{
									CustomStartRoomName = tag.Attributes["name"];
									}
								}
							}//end if
						}//end foreach
					}//end try
				catch(Sluggy.Utility.XMLParserException e)
					{
					MessageBox.Show(e.Message);
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
			}
		
		public static class Generator
			{
			readonly public static string InheritCode = "///StellarmapCode:Inherit";
			readonly public static string FunctionParameterLiteral = "~";
			readonly public static char FunctionParameterLiteralChar = '~'; 
			readonly public static string FunctionHashSymbol = "#";
			}
		
		public static class WorkspaceSave
			{
			private const string UTF8_NON_SIG_ID = "utf-8 (no signature)";
			//private const string UTF32_NON_SIG_ID = "utf-16 be (no signature)";
			//private const string UTF32_NON_SIG_ID = "utf-16 le (no signature)";
			private static string pAffFile = "en_US.aff";
			private static string pDicFile = "en_US.dic";
			
			
			public static string LastDirectory = "";
			public static Encoding LPCEncoding = new UTF8NoSigEncoding(false);//Encoding.UTF8;
			public static string DefaultLineEndings = "Unix (LF)";
			public static string LineEndings = DefaultLineEndings;
			
			public static Dictionary<string,Encoding> EncodingMap = new Dictionary<string,Encoding>();
			public static Dictionary<string,string> LineEndingMap = new Dictionary<string,string>();
			
			public static string AffFile
				{
				get {
					Utility.ConfirmRequiredFile(Globals.Dirs.SpellCheck + "\\" + pAffFile);
					return Globals.Dirs.SpellCheck + "\\" + pAffFile;
					}
				}
			
			public static string DicFile
				{
				get {
					Utility.ConfirmRequiredFile(Globals.Dirs.SpellCheck + "\\" + pDicFile);
					return Globals.Dirs.SpellCheck + "\\" + pDicFile;
					}
				}
					
			static WorkspaceSave()
				{
				SetupEncodingMap();
				SetupLineEndingMap();
				
				//load up config and check out the model properties
				try
					{
					Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
					Sluggy.Utility.Tag lastDir = parser.DepthSeekFirst(null,"lastdiropened");
					Sluggy.Utility.Tag encoding = parser.DepthSeekFirst(null,"lpcencoding");
					Sluggy.Utility.Tag lineendings = parser.DepthSeekFirst(null,"lpclineending");
					Sluggy.Utility.Tag affFile = parser.DepthSeekFirst(null,"spellcheck_aff");
					Sluggy.Utility.Tag dicFile = parser.DepthSeekFirst(null,"spellcheck_dic");
					
					
					if(lastDir != null)
						{
						if(lastDir.Attributes.ContainsKey("path"))
							{
							WorkspaceSave.LastDirectory = lastDir.Attributes["path"];
							}
						}
					
					if(encoding != null)
						{
						if(encoding.Attributes.ContainsKey("type"))
							{
							string enc = encoding.Attributes["type"];
							//if there is no string or the name is not found in the list,
							//default to UTF-8 encoding
							if(enc != null)
								{
								//if the encoding named in the file exists within the map we assing that value,
								//othwise we stay with default value.
								if(EncodingMap.ContainsKey(enc))	{ LPCEncoding = EncodingMap[enc]; }
								}
							}
						}

					if(lineendings != null)
						{
						if(lineendings.Attributes.ContainsKey("type"))
							{
							string enc = lineendings.Attributes["type"];
							//if there is no string or the name is not found in the list,
							//default to UTF-8 encoding
							if(!LineEndingMap.ContainsKey(enc))
								{ LineEndings = "Windows (CR+LF)"; }
							else { LineEndings = enc; }

							}
						}

					if(affFile != null)
						{
						if(affFile.Attributes.ContainsKey("name"))
							{
							string file = affFile.Attributes["name"];
							if(file.Length > 0)
								{
								pAffFile = file;
								}
							}
						}
					
					if(dicFile != null)
						{
						if(dicFile.Attributes.ContainsKey("name"))
							{
							string file = dicFile.Attributes["name"];
							if(file.Length > 0)
								{
								pDicFile = file;
								}
							}
						}

					}//end try
				catch(Sluggy.Utility.XMLParserException e)
					{
					MessageBox.Show(e.Message);
					}
				catch(System.IO.FileNotFoundException)
					{
					//swallow exception
					}
				catch(System.IO.DirectoryNotFoundException)
					{
					//swallow exception
					}
				
				return;
				}
			
			private static void SetupEncodingMap()
				{
				Dictionary<string,int> encodingnamecount = new Dictionary<string,int>();
				
				foreach(EncodingInfo ei in Encoding.GetEncodings())
					{
					string name = ei.Name;
					if(EncodingMap.ContainsKey(name))
						{
						//check if we already have an encoding with this name.
						//If we do, track the number so we can append indices to each one after the first
						if(encodingnamecount.ContainsKey(name))
							{encodingnamecount[name] ++;}
						else{encodingnamecount.Add(name,2);}
						name = name + " ver " + encodingnamecount[name].ToString();
						}
					
					EncodingMap.Add(name,Encoding.GetEncoding(ei.CodePage));
					}
				
				//add one last encoding manually, a non-signed utf-8 encoding
				UTF8NoSigEncoding nonSigEncoding = new UTF8NoSigEncoding(false);
				EncodingMap.Add(UTF8_NON_SIG_ID,nonSigEncoding);
				}	
			
			private static void SetupLineEndingMap()
				{
				LineEndingMap.Add("Unix (LF)","\n");
				LineEndingMap.Add("Mac (CR)","\r");
				LineEndingMap.Add("Windows (CR+LF)","\r\n");
				}	
			}
		
		#region RenderingConstants
		static public class ImageBoxProperties
			{
			readonly public static int StartingRoomAllocation = 20; //number of elements to be reserved in domain room-array
			readonly public static float ImageAlpha	 = 0.5f;
			public static int width = 64;
			public static int height = 64;
			public static int ExitLineWidth = 1;
			readonly public static string DefaultBitmapFile = "\\default.bmp";
			readonly public static string DefaultIconDesc = "default";
			readonly public static Dictionary<string,Bitmap> RoomIcons = new Dictionary<string,Bitmap>();
			readonly public static Bitmap DefaultImage;
			
			static ImageBoxProperties()
				{
				//load up config and check out the imagebox properties
				try{
					Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
					//List<Sluggy.Utility.Tag> items = parser.FindAll(null,"imageboxes");
					Sluggy.Utility.Tag items = parser.Contents.Children[0];
					//add default
					
					if(items != null && items.Children != null)
						{
						foreach(Sluggy.Utility.Tag tag in items.Children)
							{
							//config global image properties
							if(tag.Name == "imageboxes")
								{
								if(tag.Attributes.ContainsKey("width"))
									{
									width = System.Convert.ToInt32(tag.Attributes["width"]);
									}
								if(tag.Attributes.ContainsKey("height"))
									{
									height = System.Convert.ToInt32(tag.Attributes["height"]);
									}
								}
							if(tag.Name == "exitlinewidth")
								{
								if(tag.Attributes.ContainsKey("width"))
									{
									ExitLineWidth = System.Convert.ToInt32(tag.Attributes["width"]);
									}
								}
							}//end if
						}//end foreach
					}//end try
				catch(Sluggy.Utility.XMLParserException e)
					{
					MessageBox.Show(e.Message);
					}
				catch(System.IO.FileNotFoundException e)
					{
					//swallow exception
					}
				catch(System.IO.DirectoryNotFoundException e)
					{
					//swallow exception
					}
				
				//try to load the default image file
				Utility.ConfirmRequiredFile(Dirs.Bitmaps + DefaultBitmapFile);
					
				try {
					DefaultImage = new Bitmap(Dirs.Bitmaps + DefaultBitmapFile);			
					}
				catch(Exception)
					{
					DefaultImage = new Bitmap(width,height);
					}
				RoomIcons.Add(DefaultBitmapFile,DefaultImage);
				}
			}
		
		public static class NodeProperties
			{	
			public static int width = 8;
			public static int height = 8;
			
			readonly public static SolidBrush DefaultBrush = new SolidBrush(Globals.GUI.NodeColor);
			readonly public static SolidBrush ConnectedBrush = new SolidBrush(Globals.GUI.NodeConnectedColor);
			readonly public static SolidBrush FailedConnectionBrush = new SolidBrush(Globals.GUI.NodeFailedConnectedColor);
			readonly public static Pen DoorPen = new Pen(Globals.GUI.DoorHilight,2.0f);
			readonly public static Pen NoDoorPen = new Pen(Globals.GUI.NodeColor,2.0f);
			public static int numNodes = 12; //8 directions, up, down, and enter "index literal" for "Directions"
			public static Dictionary<string,Rectangle> NodeLocations = new Dictionary<string,Rectangle>(numNodes);
			
			static NodeProperties()
				{
				Sluggy.Utility.XmlParser parser = new Sluggy.Utility.XmlParser(Globals.Files.ConfigFile);
				List<Sluggy.Utility.Tag> items = parser.FindAll(null,"nodeboxes");
				try {
					//add default
					if(items != null)
						{
						foreach(Sluggy.Utility.Tag tag in items)
							{
							//config global image properties
							if(tag.Name == "nodeboxes")
								{
								if(tag.Attributes.ContainsKey("width"))
									{
									Globals.NodeProperties.width = System.Convert.ToInt32(tag.Attributes["width"]);
									}
								if(tag.Attributes.ContainsKey("height"))
									{
									Globals.NodeProperties.height = System.Convert.ToInt32(tag.Attributes["height"]);
									}
								}
							}//end if
						}//end foreach
					}//end try
				catch(Sluggy.Utility.XMLParserException e)
					{
					MessageBox.Show(e.Message);
					}
				catch(System.IO.FileNotFoundException e)
					{
					//swallow exception
					}
				catch(System.IO.DirectoryNotFoundException e)
					{
					//swallow exception
					}

				CreateNodeProperties();
				}

			private static void CreateNodeProperties()
				{
				NodeLocations = new Dictionary<string,Rectangle>(numNodes);

				NodeLocations.Add("north",new Rectangle((ImageBoxProperties.width / 2 - width / 2),-height,width,height));
				NodeLocations.Add("south",new Rectangle((ImageBoxProperties.width / 2 - width / 2),ImageBoxProperties.height,width,height));
				NodeLocations.Add("east",new Rectangle(ImageBoxProperties.width,(ImageBoxProperties.height / 2 - height / 2),width,height));
				NodeLocations.Add("west",new Rectangle(-width,(ImageBoxProperties.height / 2 - height / 2),width,height));

				NodeLocations.Add("up",new Rectangle(1,1,0,0));
				NodeLocations.Add("down",new Rectangle((ImageBoxProperties.width) - (width)-1,(ImageBoxProperties.height) - (height)-1,0,0));
				//NodeLocations.Add("up",new Rectangle((ImageBoxProperties.width/2) - (width/2),height,width,height));
				//NodeLocations.Add("down",new Rectangle((ImageBoxProperties.width/2) - (width/2),(ImageBoxProperties.height) - (height),width,height));
				
				NodeLocations.Add("northwest",new Rectangle(-width,-height,width,height));
				NodeLocations.Add("northeast",new Rectangle(ImageBoxProperties.width,-height,width,height));
				NodeLocations.Add("southwest",new Rectangle(-width,ImageBoxProperties.height,width,height));
				NodeLocations.Add("southeast",new Rectangle(ImageBoxProperties.width,ImageBoxProperties.height,width,height));
				
				//this is a special node in the very center than gets rendered if there is an exit assigned to
				//as static item or an enter. Either way, due to the nature of the node drawing system.
				NodeLocations.Add("other",new Rectangle((ImageBoxProperties.width/2) - (width/2),(ImageBoxProperties.height/2) - (height/2),0,0));
				NodeLocations.Add("out",new Rectangle((ImageBoxProperties.width / 2) - (width / 2),(ImageBoxProperties.height / 2) - (height / 2),0,0));
				}
			
			public static void RefreshNodeProperties()
				{
				NodeLocations["north"] = new Rectangle((ImageBoxProperties.width / 2 - width / 2),-height,width,height);
				NodeLocations["south"] = new Rectangle((ImageBoxProperties.width / 2 - width / 2),ImageBoxProperties.height,width,height);
				NodeLocations["east"] = new Rectangle(ImageBoxProperties.width,(ImageBoxProperties.height / 2 - height / 2),width,height);
				NodeLocations["west"] = new Rectangle(-width,(ImageBoxProperties.height / 2 - height / 2),width,height);

				NodeLocations["up"] = new Rectangle(1,1,0,0);
				NodeLocations["down"] = new Rectangle((ImageBoxProperties.width) - (width)-1,(ImageBoxProperties.height) - (height)-1,0,0);
				
				NodeLocations["northwest"] = new Rectangle(-width,-height,width,height);
				NodeLocations["northeast"] = new Rectangle(ImageBoxProperties.width,-height,width,height);
				NodeLocations["southwest"] = new Rectangle(-width,ImageBoxProperties.height,width,height);
				NodeLocations["southeast"] = new Rectangle(ImageBoxProperties.width,ImageBoxProperties.height,width,height);

				NodeLocations["other"] = new Rectangle((ImageBoxProperties.width / 2) - (width / 2),(ImageBoxProperties.height / 2) - (height / 2),0,0);
				NodeLocations["out"] = new Rectangle((ImageBoxProperties.width / 2) - (width / 2),(ImageBoxProperties.height / 2) - (height / 2),0,0);
				}
			}
		#endregion
		}
	}
