using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


/*
 * NOTE: Pay attention to the encodings for inserting new tokens into an already parsed file. It uses unicode
 *		rather than the defined global encoding because it is working entire in memory (using Unicode strings)
 *		and is not writing or reading files.
 * */

namespace Stellarmass.LPC.Parser
	{
	public class ParserException : Exception
		{
		public ParserException() : base() {}
		public ParserException(string message):base(message) {}
		public ParserException(string message,int line) : base(string.Format("Parser error on line {0}:  {1}",line,message)) {}
		public ParserException(string message,Exception inner):base(message,inner) {}
		}
	
	public enum TokenType
		{
		none,
		Unknown,
		
		NameSpace,
		FunctionDefinition,
		FunctionCall,
		InheritCommand,
		Expression, //may contain function calls
		
		CommentLine,
		CommentBlock,
		LoneNewLine,
		
		Include,
		}
	
	public class Token
		{
		public List<Lexer.Lexme> Lexmes;
		public TokenType Type;
		public List<List<Token>> Children = new List<List<Token>>();
		
		
		public string Code
			{
			get {
				int childIndex = 0;
				StringBuilder str = new StringBuilder();
				foreach(Lexer.Lexme lex in Lexmes)
					{
					/*
					if(lex.Type == Stellarmass.LPC.Lexer.LexmeType.NewLine)
						{
						str.Append(Environment.NewLine);
						}
					else{
						str.Append(lex.Data);
						}*/
					str.Append(lex.Data);
					if(LPC.LPCKeywords.IsOpenningPair(lex.Data.ToString()))
						{
						if(Children.Count > childIndex)
							{
							if(Children[childIndex] != null)
								{
								foreach(Token child in Children[childIndex])
									{
									str.Append(child.Code);
									}
								}
							
							childIndex++;
							}
						}
					}
				
				return str.ToString();
				}
			}
		
		public string GetCode(string newlineSymbol)
			{
			int childIndex = 0;
			StringBuilder str = new StringBuilder();
			foreach(Lexer.Lexme lex in Lexmes)
				{
				
				if(lex.Type == Stellarmass.LPC.Lexer.LexmeType.NewLine)
					{
					str.Append(newlineSymbol);
					}
				else{
					str.Append(lex.Data);
					}
				
				//recurse
				if(LPC.LPCKeywords.IsOpenningPair(lex.Data.ToString()))
					{
					if(Children.Count > childIndex)
						{
						if(Children[childIndex] != null)
							{
							foreach(Token child in Children[childIndex])
								{str.Append(child.GetCode(newlineSymbol));}
							}
						childIndex++;
						}
					}
				
				}

			return str.ToString();
			}
		
		public Token(Token copy)
			{
			Type = copy.Type;
			
			object tempLexmes = copy.Lexmes.ToArray().Clone();
			this.Lexmes = new List<Stellarmass.LPC.Lexer.Lexme>((Lexer.Lexme[])tempLexmes);
			
			for(int outter = 0; outter < copy.Children.Count; outter++)
				{
				this.Children.Add(new List<Token>());
				foreach(Token t in copy.Children[outter])
					{
					this.Children[outter].Add(new Token(t));
					}
				}
			}
		
		public Token(List<Lexer.Lexme> lexmes,TokenType type)
			{
			Lexmes = lexmes;
			Type = type;
			}
		
		public Token(Lexer.Lexme[] lexmes,TokenType type)
			{
			Lexmes = new List<Stellarmass.LPC.Lexer.Lexme>(lexmes);
			Type = type;
			}
		
		public Token(string rawText)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);
			
			
			
			foreach(Token token in parser.LPCTokens)
				{
				if(token.Lexmes.Count > 0)
					{
					Token copy = token;
					Type = copy.Type;
					object tempLexmes = copy.Lexmes.ToArray().Clone();
					this.Lexmes = new List<Stellarmass.LPC.Lexer.Lexme>((Lexer.Lexme[])tempLexmes);

					for(int outter = 0; outter < copy.Children.Count; outter++)
						{
						this.Children.Add(new List<Token>());
						foreach(Token t in copy.Children[outter])
							{
							this.Children[outter].Add(new Token(t));
							}
						}
					return;
					}
				}
			return;
			}
		
		public static List<Token> CreateTokenListFromText(string rawText)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);
			
			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);
			
			return parser.LPCTokens;
			}
		
		public void SetChildren(List<List<Token>> children)
			{
			Children = children;
			}	
		
		public void SetTextAsThisToken(string rawText)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);
			
			foreach(Token token in parser.LPCTokens)
				{
				if(token.Lexmes.Count > 0)
					{
					Token copy = token;
					Type = copy.Type;
					object tempLexmes = copy.Lexmes.ToArray().Clone();
					this.Lexmes = new List<Stellarmass.LPC.Lexer.Lexme>((Lexer.Lexme[])tempLexmes);

					for(int outter = 0; outter < copy.Children.Count; outter++)
						{
						this.Children.Add(new List<Token>());
						foreach(Token t in copy.Children[outter])
							{
							this.Children[outter].Add(new Token(t));
							}
						}
					return;
					}
				}
			return;
			}
		
		public void SetTextAsTokens(string rawText,int childIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);
			
			this.Children[childIndex] = parser.LPCTokens;
			}
		
		public void InsertTextAsTokensEnd(string rawText,int childIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);

			this.Children[childIndex].AddRange(parser.LPCTokens);
			}
		
		public void InsertTextAsTokensBeginning(string rawText,int childIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);

			this.Children[childIndex].InsertRange(0,parser.LPCTokens);
			}

		public void InsertTextAsTokens(string rawText,int childIndex,int tokenIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);

			this.Children[childIndex].InsertRange(tokenIndex,parser.LPCTokens);
			}
		}
	
	/// <summary>
	/// A high-level parser for constructing parse-trees based on language-specific tokens
	/// found in an LPC code file.
	/// 
	/// For our purposes here, only a few basic constructs are needed. A namespace (file),
	/// function definitions (as opposed to function declaration), function calls, and the
	/// 'inherit' command. All other lines are considered general 'statements'.
	/// Legal syntax is enforced to a small degree during function name parsing;
	/// </summary>
	public class Parser
		{
		private List<Token> Tokens;
		
		public List<Token> LPCTokens
			{
			get {return Tokens;}
			}
		
		public Parser(Lexer.Lexer lexer,ParseMap map)
			{
			List<Lexer.Lexme> list = new List<Stellarmass.LPC.Lexer.Lexme>(100);
			
			foreach(Lexer.Line line in lexer.LexedLines)
				{
				foreach(Lexer.Lexme lexme in line.Tokens)
					{
					list.Add(lexme);
					}
				}
			Tokens = map.Parse(list);
			}

		public void InsertTextAsTokensEnd(string rawText,int childIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);
			
			this.Tokens.AddRange(parser.LPCTokens);
			}

		public void InsertTextAsTokensBeginning(string rawText,int childIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);

			this.Tokens.InsertRange(0,parser.LPCTokens);
			}

		public void InsertTextAsTokens(string rawText,int childIndex,int tokenIndex)
			{
			//Stream stream = new MemoryStream(UnicodeEncoding.UTF8.GetBytes(rawText));
			StreamReader stream = new StreamReader(new MemoryStream(Globals.LpcInternalCodec.GetBytes(rawText)),Globals.LpcFileCodec,false);
			ParseMap map;
			SyntaxRules lpcRules = new SyntaxRules(out map);

			Scanner.Scanner scanner = new Scanner.Scanner(stream);
			Lexer.Lexer lexer = new Lexer.Lexer(scanner);
			Parser parser = new Parser(lexer,map);

			this.Tokens.InsertRange(tokenIndex,parser.LPCTokens);
			}
		}
	}
