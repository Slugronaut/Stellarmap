using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Stellarmass.LPC;

namespace Stellarmass.LPC.Scanner
	{
	internal static class ParseData
		{
		readonly public static string Pattern = @"(\/\/  |  \/\*  |  \*\/  |  \+\=  |  \-\=  |  \*\=  |  \/\=  |  \%\=  |  \^\=  |  \+\+  |  \-\-  |  \-\>  |  \&\&  |  \|\|  |  \:\:  |  \:  |  \;  |  \,  |  \{  |  \}  |  \(  |  \)  |  \[  |  \]  |  \=  |  \+  |  \-  |  \*  |  \/  |  \%  |  \^  |  \&  |  \|  |  \!  |  \?  |  \.  |  "" |  \-\>)";
		}
	
	public enum TokenType
		{
		none,
		Word,
		Symbol,
		Number,
		Whitespace,
		NewLine,
		}
	
	public class Token
		{
		public StringBuilder Data = new StringBuilder();
		public TokenType Type;
		}
	
	public class CodeLine
		{
		public int LineNumber = 0;
		public List<Token> Tokens = new List<Token>();
		
		public CodeLine(List<Token> tokens,int lineNumber)
			{
			Tokens = tokens;
			LineNumber = lineNumber;
			}
		
		public CodeLine(LinkedList<Token> tokens,int lineNumber)
			{
			Tokens = new List<Token>(tokens);
			LineNumber = lineNumber;
			}
		}
	
	/// <summary>
	/// Reads in a stream of text representing LPC code and breaks in down into chunks that are either
	/// whitespace, words, numbers, or symbols. Symbols are parsed based on a provided syntaxt
	/// rule in 'ParseData'.
	/// </summary>
	public class Scanner
		{
		#region memebers
		List<CodeLine> CodeLines = new List<CodeLine>();
		
		enum ParseGear
			{
			Word,
			Number,
			Symbol,
			Whitespace,
			NewLine,
			na,
			}
		#endregion
		
		
		#region public interface
		public Scanner(System.IO.StreamReader stream)
			{
			List<string> lines = GetFileLines(stream);
			
			int lineNumber = 0;
			foreach(string str in lines)
				{
				CodeLines.Add(ParseLine(str,lineNumber));
				lineNumber++;
				}
			}
		
		public List<CodeLine> Lines
			{
			get {return CodeLines;}
			}
		#endregion	
		
		
		#region private methods
		/// <summary>
		/// Reads all of the text from a stream line-by-line. These lines
		/// can then be sent to a parser to be identified.
		/// </summary>
		/// <param name="stream">A stream of LPC code.</param>
		/// <returns>A list of strings representing each line of the the LPC code.</returns>
		private List<string> GetFileLines(System.IO.StreamReader reader)
			{
			//System.IO.StreamReader reader = new StreamReader(stream,Globals.LpcCodec,false);
			List<string> lines = new List<string>();
			StringBuilder buffer = new StringBuilder();
			string fileText = reader.ReadToEnd();
			reader.Close();
			
			fileText = fileText.Replace(Environment.NewLine,"\n");
			fileText = fileText.Replace("\r\n","\n");
			fileText = fileText.Replace('\r','\n');
			
			foreach(char c in fileText.ToCharArray())
				{
				buffer.Append(c);
				if(c == '\n')
					{
					lines.Add(buffer.ToString());
					buffer = new StringBuilder();
					}
				}
			
			//flush any remaining text
			if(buffer.Length > 0)	{lines.Add(buffer.ToString());}
			
			return lines;
			}
		
		/// <summary>
		/// Parses a line of text and identifies each element as either whitespace, words, numbers,
		/// or symbols. Symbols require extra attention and use a pattern of rules to determine how
		/// to identify them seperately if they are next to one-another.
		/// </summary>
		/// <param name="line">A single line of LPC code to be parsed.</param>
		/// <returns>A CodeLine object containing the line number and a list of all parsed tokens from the provided line.</returns>
		private CodeLine ParseLine(string line,int lineNumber)
			{
			LinkedList<Token> tokens = new LinkedList<Token>();
			ParseGear gear = ParseGear.na;
			TokenType type = TokenType.none;

			foreach(char c in line.ToCharArray())
				{
				ParseGear tempGear;
				TokenType tempType;
				
				//NOTE: numbers can be a part of a word if they aren't starting it,
				//dashes are symbols, not words
				if(char.IsLetter(c) || (gear == ParseGear.Word && char.IsNumber(c)) )
					{
					if(c == '-') 
						{
						tempGear = ParseGear.Symbol;
						tempType = TokenType.Symbol;
						}
					else{
						tempGear = ParseGear.Word;
						tempType = TokenType.Word;
						}
					}
				else if(char.IsNumber(c))
					{
					tempGear = ParseGear.Number;
					tempType = TokenType.Number;
					}
				else if(char.IsWhiteSpace(c))
					{
					if(c == '\n')
						{
						tempGear = ParseGear.NewLine;
						tempType = TokenType.NewLine;
						}
					else{
						tempGear = ParseGear.Whitespace;
						tempType = TokenType.Whitespace;
						}
					}
				else {
					//if nothing else, we have a symbol... maybe. Underscores are still part of words
					//JC added a few extra cases or characters that are commonly used in string but often choke
					//the parsing due to not being text characters
					//	apostrophes are tricky: for now we will assume they are part of a word
					//	forward slashes are only used for directory string so we are good here
					//	backslashes are normally used for division but for now we only need to treat them

					if(c == '_')
						{
						tempGear = ParseGear.Word;
						tempType = TokenType.Word;
						}
					else{
						tempGear = ParseGear.Symbol;
						tempType = TokenType.Symbol;
						}
					}
				
				if(tempGear != gear)
					{
					//looks like we've switched gears, reeeewwwwww, reeeeeeeeeeeewwww
					gear = tempGear;
					type = tempType;
					tokens.AddLast(new Token());
					}
				else if(tempGear == ParseGear.NewLine)
					{
					type = tempType;
					tokens.AddLast(new Token());
					}
				
				
				tokens.Last.Value.Type = type;
				tokens.Last.Value.Data.Append(c);
				}
			
			//now to make sure all symbols tokens are split properly by going back over the list (yuck)
			LinkedListNode<Token> node = tokens.First;
			LinkedListNode<Token> skip;
			while(node != null)
				{
				if(node.Value.Type == TokenType.Symbol)
					{
					//recursively splits all symbols into largest chunk according to syntax
					LinkedList<Token> temp = ParseSymbol(node.Value.Data.ToString());
					if(temp.Count > 1)
						{
						skip = node.Next;
						//ok, for some dumbass reason there is no way to
						//insert one list into another so I have to do it manually >:(
						foreach(Token nd in temp)
							{
							tokens.AddBefore(node,nd);
							skip = node.Previous;
							}
						//only insert if there was a change
						tokens.Remove(node);
						node = skip.Previous;
						}
					}
				node = node.Next;
			    }
			
			if(tokens == null) return null;
			
			return new CodeLine(tokens,lineNumber);
			}
		
		/// <summary>
		/// Performs sub-parsing for symbols, uses a defined regular expression pattern suitable for LPC code.
		/// </summary>
		/// <param name="symbol">The token of text that represents any number of symbols.</param>
		/// <returns>A linked list of all symbols parsed from the text. It may be the original text passed if no sub-parsing was needed.</returns>
		private LinkedList<Token> ParseSymbol(string symbol)
			{
			LinkedList<Token> list = new LinkedList<Token>();
			
			//hehehe, I got lazy and decided to use some regulare expression magic instead of rolling
			//my own recursive solution. Aren't I a clever little gnome >:D    ... no
			string[] col = Regex.Split(symbol,ParseData.Pattern,RegexOptions.IgnorePatternWhitespace);
			
			string temp;
			foreach(string sym in col)
				{
				temp = sym.Trim();
				if(!string.IsNullOrEmpty(temp))
					{
					Token token = new Token();
					token.Type = TokenType.Symbol;
					token.Data.Append(temp);
					list.AddLast(token);
					}
				}
			return list;
			}
		
		
		#endregion	
		}
	
	}

