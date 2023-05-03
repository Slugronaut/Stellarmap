using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Stellarmass.LPC;

using Parser = Stellarmass.LPC.Parser;
using Lexer = Stellarmass.LPC.Lexer;
using Scanner = Stellarmass.LPC.Scanner;

//!!!
//TODO: WE STILL NEED A WILDCARD RULE THAT STOPS PARSING WHEN A GIVEN LEXME OR SET OF LEXMES ARE FOUND
//!!!

namespace ParserTester
	{
	class Program
		{
		static void Main(string[] args)
			{
			//last - 25 - 33
			int UNIT_TEST = 37;
			
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(LPC.RoomUnitTests.GetUnitTest("section 1",UNIT_TEST) )),Stellarmass.LPC.Globals.LpcFileCodec,false);
			Parser.ParseMap map;
			Parser.SyntaxRules lpcRules = new Stellarmass.LPC.Parser.SyntaxRules(out map);
			
			
			Scanner.Scanner scanner;
			Lexer.Lexer lexer;
			Parser.Parser parser;
			
			Console.BufferHeight = 13000;
			Console.WindowWidth = 120;
			Console.WindowHeight = 60;
			
			try{
				scanner = new Scanner.Scanner(stream);
				lexer = new Lexer.Lexer(scanner);
				parser = new Parser.Parser(lexer,map);
             		}
	           catch(Exception e)
	               {
	               Console.WriteLine("Syntax Error:\n\n" + e.Message);
	               End();
	               return;
               	}
			
			List<Parser.Token> TokenCopies = parser.LPCTokens;




			//start over
			stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(LPC.RoomUnitTests.GetUnitTest("section 1",UNIT_TEST))),Stellarmass.LPC.Globals.LpcFileCodec,false);
			
			lpcRules = new Stellarmass.LPC.Parser.SyntaxRules(out map);
			try{
				scanner = new Scanner.Scanner(stream);
				lexer = new Lexer.Lexer(scanner);
				parser = new Parser.Parser(lexer,map);
             		}
          	catch(Exception e)
             	 	{
               	Console.WriteLine("Syntax Error:\n\n" + e.Message);
               	End();
              		return;
               	}
			


			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLine("\n\n\n---- LPC LEXMES FOUND ----\n\n");
			Console.ResetColor();
			PrintLexmes(lexer);
			
			//print all tokens found (process whitespace accordingly)
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLine("\n\n\n---- LPC TOKENS FOUND ----\n\n");
			Console.ResetColor();
			PrintTokens(parser.LPCTokens,"\t");

			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLine("\n\n\n---- ORIGINAL CODE FILE ----\n\n");
			Console.ResetColor();
			PrintOriginalCode(LPC.RoomUnitTests.UnitTests[UNIT_TEST]);
			Console.ResetColor();
			Console.WriteLine("\n\n\n");
			
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.White;
			Console.WriteLine("\n\n\n---- RECOMPOSED CODE FILE ----\n\n");
			Console.ResetColor();
			PrintRecomposedCode(parser.LPCTokens);
			Console.ResetColor();
			Console.WriteLine("\n\n\n");
			LPC.RoomUnitTests.CompareUnitTests(parser.LPCTokens,UNIT_TEST);
			
			
			
			End();
			}
		
		#region DebugOutput
		static void PrintLexmes(Lexer.Lexer lexer)
			{
			//print all found tokens (except whitespace ones, they will be processed for easier reading)
			foreach(Lexer.Line line in lexer.LexedLines)
				{
				foreach(Lexer.Lexme lexme in line.Tokens)
					{
					if(lexme.Type == Stellarmass.LPC.Lexer.LexmeType.Whitespace)
						{
						string tab = "\t\t";
						int remainder = lexme.Data.Length / 5;
						for(int index = 1; index > remainder; index--)
							{
							tab += "\t";
							}

						if(lexme.Data.ToString() == Environment.NewLine)
							{
							Console.WriteLine("-\\n-" + tab + "TYPE: " + lexme.Type.ToString());
							}
						if(lexme.Data.ToString() == "\t")
							{
							Console.WriteLine("-\\t-" + tab + "TYPE: " + lexme.Type.ToString());
							}
						}
					else if(lexme.Type == Stellarmass.LPC.Lexer.LexmeType.NewLine)
						{
						string tab = "\t\t";
						int remainder = lexme.Data.Length / 5;
						for(int index = 1; index > remainder; index--)
							{
							tab += "\t";
							}

						Console.WriteLine("-\\n-" + tab + "TYPE: " + lexme.Type.ToString());
						}
					else{
						string tab = "\t\t";
						int remainder = lexme.Data.Length / 5;
						if(lexme.Data.Length < 15)
							{
							for(int index = 1; index > remainder; index--)
								{
								tab += "\t";
								}
							}
						
						if(lexme.Data.ToString() == Environment.NewLine)
							{
							Console.WriteLine("-\\n-" + tab + "TYPE: " + lexme.Type.ToString());
							}
						else{
							Console.WriteLine("-" + lexme.Data + "-" + tab + "TYPE: " + lexme.Type.ToString());
							}
						}
					
					
					}
				}
			}
		
		static void PrintTokens(List<Parser.Token> tokenList,string intro)
			{
			if(tokenList == null) return;
			foreach(Parser.Token token in tokenList)
				{
				StringBuilder str = new StringBuilder();
				foreach(Lexer.Lexme lex in token.Lexmes)
					{
					string temp = lex.Data.ToString();
					if(temp == Environment.NewLine) temp = "\\n "; 
					if(temp == "\t") temp = "\\t ";
					str.Append(temp);
					}
							
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.Write(intro + "\nTOKEN TYPE: " + token.Type.ToString() + "  DATA:   --");
				Console.ResetColor();
				Console.Write(str);
				Console.ForegroundColor = ConsoleColor.DarkYellow;
				Console.Write("--\n");
				Console.ResetColor();
				
				foreach(List<Parser.Token> list in token.Children)
					{
					PrintTokens(list,intro+intro);
					}
				}
			}
		
		static void PrintOriginalCode(string code)
			{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("-");
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(code);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("-");
			Console.ResetColor();
			}
		
		static void PrintRecomposedCode(List<Parser.Token> tokenList)
			{
			StringBuilder str = new StringBuilder();
			
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("-");
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			foreach(Parser.Token token in tokenList)
				{
				str.Append(token.Code);
				Console.Write(str);
				str = new StringBuilder();
				}

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("-");
			Console.ResetColor();
			}
		
		static void End()
			{
			Console.WriteLine("\n\n\n PRESS ANY KEY TO EXIT\n");
			Console.ReadKey();
			}
		#endregion
		
		}
	}
