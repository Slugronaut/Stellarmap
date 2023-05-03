using System;
using System.Collections.Generic;
using System.Text;
using Parser = Stellarmass.LPC.Parser;
using Scanner = Stellarmass.LPC.Scanner;
using Lexer = Stellarmass.LPC.Lexer;

namespace Stellarmap
	{
	public enum DeadSoulsObjectType
		{
		Npc,
		Sentient,
		Room,
		InstanceRoom,
        Shop,
        PoliceOffice,
        JailCell,
		Item,
		Armor,
		Food,
		Door,
		}
	
	public class DecomposedCode
		{
		private static Parser.ParseMap map;
		private static Parser.SyntaxRules lpcRules = new Stellarmass.LPC.Parser.SyntaxRules(out map);
		private Dictionary<string,Parser.Token> FunctionBodyMap = new Dictionary<string,Stellarmass.LPC.Parser.Token>();
		public List<Parser.Token> Tokens;
		
		
		
		public bool LoadLPC(string filePath)
			{
			System.IO.StreamReader stream;
			try {
				stream = new System.IO.StreamReader(filePath,Globals.WorkspaceSave.LPCEncoding);
				}
			catch(System.IO.IOException exception)
				{
				string str = "File error:\n\n" + exception.Message;
				System.Windows.Forms.MessageBox.Show(str);
				return false;
				}
			
			Scanner.Scanner scanner;
			Lexer.Lexer lexer;
			Parser.Parser parser;
			
			try
				{
				scanner = new Scanner.Scanner(stream);
				stream.Close();
				lexer = new Lexer.Lexer(scanner);
				parser = new Parser.Parser(lexer,DecomposedCode.map);
				}
			catch(Exception exception)
				{
				string str = "Error parsing LPC file: \n\n" + exception.Message;
				System.Windows.Forms.MessageBox.Show(str);
				return false;
				}
			
			this.Tokens = parser.LPCTokens;
			
			BuildFunctionBodyMap();
			return true;
			}
		
		public bool CreateLPCFromScratch(DeadSoulsObjectType type)
			{
			switch(type)
				{
				case DeadSoulsObjectType.Door:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Door + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				case DeadSoulsObjectType.Room:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.DefaultStartRoomName + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
                case DeadSoulsObjectType.InstanceRoom:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.InstanceRoom + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
                case DeadSoulsObjectType.Shop:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Shop + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
                case DeadSoulsObjectType.PoliceOffice:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.PoliceOffice + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
                case DeadSoulsObjectType.JailCell:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.JailCell + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				case DeadSoulsObjectType.Item:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Item + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				case DeadSoulsObjectType.Armor:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Armor + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				case DeadSoulsObjectType.Npc:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Npc + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				case DeadSoulsObjectType.Food:
					{
					StringBuilder filePath = new StringBuilder(Globals.Dirs.Code + "\\" + Globals.Model.Food + Globals.Model.RoomExtension);
					Utility.ConfirmRequiredFile(filePath.ToString());
					return LoadLPC(filePath.ToString());
					}
				
				default:
					{
					System.Windows.Forms.MessageBox.Show("No support for importing " + type.ToString() + " files.");
					break;
					}
				}
			BuildFunctionBodyMap();
			return true;
			}
		
		public void AddFunctionCall(string where)
			{
			}
		
		public void EditFunctionCall(string where,string funcName,string parameters)
			{
			}
		
		public Parser.Token GetFunctionBody(string name)
			{
			if(!FunctionBodyMap.ContainsKey(name))	return null;
			
			return FunctionBodyMap[name];
			}
		
		public Dictionary<string,Parser.Token> GetAllFunctionCalls(Parser.Token root)
			{
			//List<Parser.Token> calls = new List<Parser.Token>();
			Dictionary<string,Parser.Token> calls = new Dictionary<string,Parser.Token>();
			Dictionary<string,int> FuncInstanceCount = new Dictionary<string,int>();
			
			foreach(Parser.Token token in root.Children[1])
				{
				if(token.Type == Parser.TokenType.FunctionCall)
					{
					string funcName = "";
					//gotta find out the form of the function call with respect to
					//the scope resolution operator
					for(int index = 0; index < token.Lexmes.Count; index++)
						{
						Lexer.Lexme lexme = token.Lexmes[index];
						
						if(lexme.Data.ToString() == "::")
							{
							funcName = token.Lexmes[index+1].Data.ToString();
							break;
							}
						if(lexme.Type == Lexer.LexmeType.Identifier)
							{
							//look ahead
							if(token.Lexmes[index+1].Data.ToString() == "::")
								{
								funcName = token.Lexmes[index+2].Data.ToString();
								break;
								}
							else{
								funcName = lexme.Data.ToString();
								break;
								}
							}
						}
					
					if(funcName.Length < 1 || funcName == "")
						{throw new Exception("Unidentifiable form for function name.");}
					if(calls.ContainsKey(funcName))
						{
						calls.Add(funcName + Globals.Generator.FunctionHashSymbol + FuncInstanceCount[funcName],token);
						FuncInstanceCount[funcName] += 1;
						}
					else{
						calls.Add(funcName,token);
						FuncInstanceCount.Add(funcName,1);
						}
					}
				}
			
			return calls;
			}

		public Dictionary<string,Parser.Token> GetAllInherits()
			{
			//List<Parser.Token> calls = new List<Parser.Token>();
			Dictionary<string,Parser.Token> calls = new Dictionary<string,Parser.Token>();

			foreach(Parser.Token token in this.Tokens)
				{
				if(token.Type == Parser.TokenType.InheritCommand)
					{
					string funcName = "";
					
					//gotta find out the form of the function call with respect to
					//the scope resolution operator
					for(int index = 0; index < token.Lexmes.Count; index++)
						{
						Lexer.Lexme lexme = token.Lexmes[index];

						if(lexme.Type == Lexer.LexmeType.Instruction && lexme.Data.ToString() == "inherit")
							{
							funcName = token.Code;
							break;
							}
						}

					if(funcName.Length < 1 || funcName == "")
						{throw new Exception("Unidentifiable form for inherit.");}
					calls.Add(funcName,token);
					}
				}

			return calls;
			}
		
		public bool AddInherit(string text)
			{
			//HACK CITY!
			//first, we need to find where to put the inherit command. We are assuming
			//the code file already has at least one inherit command (dangerous) or
			//some meta-comment that singlas where they should go (also dangerous) so we'll
			//insert our new one after that
			for(int index = 0; index < this.Tokens.Count; index++)
				{
				Parser.Token insertPoint = this.Tokens[index];
				
				if(insertPoint != null)
					{
					if(insertPoint.Type == Parser.TokenType.CommentLine &&
					   insertPoint.Code.Trim() == Globals.Generator.InheritCode)
						{
						//stick 'er in there!
						Parser.Token newline = new Parser.Token("\n");
						Parser.Token token = new Parser.Token(text);
						this.Tokens.Insert(index,newline);
						this.Tokens.Insert(index,token);
						
						return true;
						}
					}
				
				}
			
			return false;
			}
		
		public bool AddInclude(string fileName)
			{
			//search through every token until the end of the file.
			//if we didn't already file the included file we do it at the top
			foreach(Parser.Token t in Tokens)
				{
				if(t.Type == Stellarmass.LPC.Parser.TokenType.Include)
					{
					if(t.Code.Contains(fileName))	{ return true;}
					}
				}

			Parser.Token newline = new Parser.Token("\n");
			Parser.Token token = new Parser.Token("#include <" + fileName + ">");
			this.Tokens.Insert(0,newline);
			this.Tokens.Insert(0,token);
			
			return true;
			}
		
		private void BuildFunctionBodyMap()
			{
			//first, find main function
			foreach(Parser.Token token in this.Tokens)
				{
				if(token.Type == Stellarmass.LPC.Parser.TokenType.FunctionDefinition)
					{
					//get the name of the damned thing
					foreach(Lexer.Lexme lex in token.Lexmes)
						{
						if(lex.Type == Lexer.LexmeType.Identifier)
							{
							FunctionBodyMap.Add(lex.Data.ToString(),token);
							}
						}
					}
				}
			}
		
		
		}
	}
