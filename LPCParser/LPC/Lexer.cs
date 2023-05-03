using System;
using System.Collections.Generic;
using System.Text;

using Stellarmass.LPC;
using Scanner = Stellarmass.LPC.Scanner;

//NOTE: CombineTokens needs to be adapted to handle multiple versions of lineends

namespace Stellarmass.LPC.Lexer
	{
	public class LexerException : Exception
		{
		readonly public static string UnknownTokenParsed = "Unknown token ' {0} '.";
		readonly public static string UnknownPreprocessor = "Unknown preprocess command ' {0} '.";
		
		
		readonly public static string UnclosedCommentBlock = "Unclosed comment block.";
		readonly public static string UnclosedQuote = "Unclosed string quote.";
		
		readonly public static string MismatchedCommentBlock = "Mismatched comment block.";
		readonly public static string MismatchedQuote = "Mismatched string quote.";
		
		readonly public static string InvalidPreprocessor = "Invalid preprocess command.";
		
		public LexerException() : base() {}
		public LexerException(string message):base(message) {}
		public LexerException(string message,int line) : base(string.Format("Lexer error on line {0}:  {1}",line,message)) {}
		public LexerException(string message,Exception inner):base(message,inner) {}
		}
	
	public enum LexmeType
		{
		none,
		Unknown,
		
		PreprocessLine,
		Quote,
		SingleQuote,
		QuoteText,
		Grouped,
		CommentLine,
		OpenComment,
		CloseComment,
			
		//final category for all tokens
		Keyword,
		DataType,
		OpenParen,
		CloseParen,
		OpenBrace,
		CloseBrace,
		OpenBracket,
		CloseBracket,
		OpenChevron,
		CloseChevron,
		Operator,
		Whitespace,
		NewLine,
		NumberLiteral,
		StringLiteral,
		Instruction,
		Preprocess,
		Identifier,
		Comment,
		Symbol,
		EscapeCharacter,
		}
	
	public enum LexmeFlag
		{
		Set,
		Unset,
		Skip,
		BelongsToUnidentifiedToken,
		BeginKnownToken,
		}
	
	public class Lexme
		{
		public int LineNumber;
		public int Index;
		public bool Open = false; //flags when the token is waiting for a matching closing type
		public LexmeFlag Flag = LexmeFlag.Unset; //flags for token matching state
		public StringBuilder Data = new StringBuilder();
		public LexmeType Type = LexmeType.none;
		}
	
	public class Line
		{
		public int LineNumber;
		public List<Lexme> Tokens;
		
		public Line(List<Lexme> tokens,int lineNumber)
			{
			LineNumber = lineNumber;
			
			if(tokens != null)	Tokens = tokens;
			else{ throw new System.ArgumentNullException("Cannot pass tokens as null.");}
			}
		}
	
	public static class PairStateMachine
		{
		static Dictionary<LexmeType,LexmeType[]> PairedLexmes = new Dictionary<LexmeType,LexmeType[]>();
		
		static PairStateMachine()
			{
			PairedLexmes.Add(LexmeType.Quote,		new LexmeType[]	{LexmeType.Quote});
			//PairedLexmes.Add(LexmeType.OpenBrace,	new LexmeType[]	{LexmeType.CloseBrace});
			//PairedLexmes.Add(LexmeType.OpenBracket,	new LexmeType[]	{LexmeType.CloseBracket});
			//PairedLexmes.Add(LexmeType.OpenParen,	new LexmeType[]	{LexmeType.CloseParen});
			//PairedLexmes.Add(LexmeType.OpenChevron,	new LexmeType[]	{LexmeType.CloseChevron});
			PairedLexmes.Add(LexmeType.OpenComment,	new LexmeType[]	{LexmeType.CloseComment});
			PairedLexmes.Add(LexmeType.CommentLine, new LexmeType[]	{LexmeType.NewLine});
			PairedLexmes.Add(LexmeType.Preprocess,	new LexmeType[]	{LexmeType.Whitespace});
			}
		
		public static bool RequiresClosing(LexmeType type)
			{
			return PairedLexmes.ContainsKey(type);
			}
		
		public static LexmeType MatchingCloseLexme(LexmeType type)
			{
			//for now we are just assuming one ending type for each starting type, ignore the array
			if(PairedLexmes.ContainsKey(type))	return PairedLexmes[type][0];
			
			return LexmeType.none;
			}
		
		public static bool ValidMatchingCloseLexme(LexmeType start,LexmeType end)
			{
			if(!RequiresClosing(start))	return true;
			
			if(end == MatchingCloseLexme(start))	return true;
			
			return false;
			}
			
		}
	
	
	/// <summary>
	/// Used for processing lexmes when they are contained withing greedy pairs such as quotes.
	/// </summary>
	public class GreedyLexmePair
		{
		int StartLine; //the line where the pairing started (in case there is a mismatch error and we need to print the line it started on)
		Lexme StartLexme;// = LexmeType.none;
		bool ConsumingFlag = false;
		
		public bool Active
			{
			get {return ConsumingFlag;}
			}
		
		public GreedyLexmePair()
			{
			StartLexme = new Lexme();
			StartLexme.Type = LexmeType.none;
			}
		
		private void Begin(Lexme start,int startLine)
			{
			StartLine = startLine;
			StartLexme = start;
			}
		
		public List<Lexme> FlushGroup()
			{
			if(StartLexme != null)
				{
				List<Lexme> tokenList = new List<Lexme>();
				tokenList.Add(StartLexme);
				return tokenList;
				}
			return null;
			}
		
		public bool CombineGroupedTokens(Lexme lexme,List<Lexme> tokenList,int startLine)
			{
			if(ConsumingFlag)
				{
				//we previously started consuming, now we need to know if we can stop
				if(PairStateMachine.ValidMatchingCloseLexme(StartLexme.Type,lexme.Type))
					{
					ConsumingFlag = false;
					
					//HACK CITY!
					if(StartLexme.Type == LexmeType.CommentLine)
						{
						tokenList.Add(StartLexme);
						tokenList.Add(lexme);
						}
					else{
						if(StartLexme.Type == LexmeType.OpenComment)
							{StartLexme.Type = LexmeType.Comment;}
						else if(StartLexme.Type == LexmeType.Quote)
							{StartLexme.Type = LexmeType.StringLiteral;}
						StartLexme.Data.Append(lexme.Data);
						tokenList.Add(StartLexme);
						}
					StartLexme = null;
					}
				else{
					StartLexme.Data.Append(lexme.Data);
					}
				}
			else{
				//we have not opened consumption yet. Do we start?
				if(PairStateMachine.RequiresClosing(lexme.Type))
					{
					//yes
					ConsumingFlag = true;
					Begin(lexme,startLine);
					}
				else{
					tokenList.Add(lexme);
					}
				}

			
			return ConsumingFlag;
			}
		}
	
	
	/// <summary>
	/// Parses data scanned from a file and then identifies each
	/// scanner token with a language-specific classification.
	/// </summary>
	public class Lexer
		{
		List<Line> Lines = new List<Line>();
		
		
		#region public interface
		public Lexer(Stellarmass.LPC.Scanner.Scanner scannedData)
			{
			int lineCount = 0;
			int tokenCount = 0;
			GreedyLexmePair greedy = new GreedyLexmePair();
			List<Lexme> tempTokensList = new List<Lexme>();
			
				
			
			foreach(Scanner.CodeLine line in scannedData.Lines)
				{
				
				foreach(Scanner.Token scanned in line.Tokens)
					{
					try{
						Lexme lexme = new Lexme();
						lexme.Data = scanned.Data;
						lexme.Type = IdentifyType(scanned,lineCount+1,greedy);
						//this will automatically combine lexme with any previous lexmes if they are supposed to
						//be grouped together as a string or comment
						greedy.CombineGroupedTokens(lexme,tempTokensList,lineCount+1);
						}
					catch(LexerException e)
						{
						throw new LexerException(e.Message,lineCount);
						}
					
					
					tokenCount++;
					}//end foreach
				
				//OLD POSITION
				
				
				lineCount++;
				tokenCount = 0;
				}
			//NEW POSITION
			//just in case the line ended before we could finish the token list
			if(greedy.FlushGroup() != null)
				{
				tempTokensList.AddRange(greedy.FlushGroup());
				}
			Lines.Add(new Line(tempTokensList,Lines.Count));
			}
		
		public List<Line> LexedLines
			{
			get {return Lines;}
			}
		#endregion
		
		
		#region private methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sToken"></param>
		/// <param name="lineNumber"></param>
		/// <param name="pairState">A mini state machine used to see how to process lexmes when
		/// we are waiting for closing pairs of lexmes</param>
		/// <returns></returns>
		private LexmeType IdentifyType(Scanner.Token sToken,int lineNumber,GreedyLexmePair greedy)//LexmeType pairState)
			{
			LexmeType type = LexmeType.none;
			string data = sToken.Data.ToString();
			
			
			
			//switch statements... ruining good code since 1967. (good thing this isn't good code ;)
			switch(sToken.Type)
				{
				case Stellarmass.LPC.Scanner.TokenType.Whitespace:
					{
					type = LexmeType.Whitespace;
					break;
					}
				
				case Stellarmass.LPC.Scanner.TokenType.NewLine:
					{
					type = LexmeType.NewLine;
					//converting from '\n' back to a standard type for this environment
					//sToken.Data = new StringBuilder(Environment.NewLine);
					break;
					}
				
				case Stellarmass.LPC.Scanner.TokenType.Number:
					{
					type = LexmeType.NumberLiteral;
					break;
					}
				
				case Stellarmass.LPC.Scanner.TokenType.Word:
					{
					if(LPC.LPCKeywords.IsInstruction(data))
						{
						type = LexmeType.Instruction;
						}
					else if(LPC.LPCKeywords.IsDataType(data))
						{
						type = LexmeType.DataType;
						}
					else if(LPC.LPCKeywords.IsKeyword(data))
						{
						type = LexmeType.Keyword;
						}
					else{
						type = LexmeType.Identifier;
						}
					
					break;
					}
				
				case Stellarmass.LPC.Scanner.TokenType.Symbol:
					{
					if(LPC.LPCKeywords.IsOperator(data))
						{
						type = LexmeType.Operator;
						if(data == "(")	type = LexmeType.OpenParen;
						if(data == ")")	type = LexmeType.CloseParen;
						if(data == "{") type = LexmeType.OpenBrace;
						if(data == "}") type = LexmeType.CloseBrace;
						if(data == "[")	type = LexmeType.OpenBracket;
						if(data == "]")	type = LexmeType.CloseBracket;
						if(data == "<")	type = LexmeType.OpenChevron;
						if(data == ">")	type = LexmeType.CloseChevron;
						}
					else if(LPC.LPCKeywords.IsSymbol(data))
						{
						switch(data)
							{
							//special cases for apostrophes, slashes, and backslashes
							case "\'":	{type = LexmeType.SingleQuote; break;}
							case "\\'":	{type = LexmeType.SingleQuote; break;}
							case "\\":	{type = LexmeType.EscapeCharacter; break;}
							
							

							case "\"":	{type = LexmeType.Quote; break;}
							case "#":	{type = LexmeType.PreprocessLine; break;}
							default:	{type = LexmeType.Symbol; break;}
							}
						}
					else if(LPC.LPCKeywords.IsCommentMark(data))
						{
						switch(data)
							{
							case "/*": {type = LexmeType.OpenComment; break;}
							case "*/": {type = LexmeType.CloseComment; break;}
							case "//": {type = LexmeType.CommentLine; break;}
							default:{
									if(greedy.Active)
										{type = LexmeType.Grouped;}
									else{throw new LexerException(string.Format(LexerException.UnknownTokenParsed,(string)data));}
									break;
									}
							}
						}
					else{
						switch(data)
							{
							//special cases for apostrophes, slashes, and backslashes
							case "\'":	{type = LexmeType.SingleQuote; break;}
							case "\\'":	{type = LexmeType.SingleQuote; break;}
							case "\\":	{type = LexmeType.EscapeCharacter; break;}
							default:
									{
									if(greedy.Active)
										{type = LexmeType.Grouped;}
									else { throw new LexerException(string.Format(LexerException.UnknownTokenParsed,(string)data)); }
									break;
									}
							}
						}
					break;
					}
				
				default:
					{
					if(greedy.Active)
						{type = LexmeType.Grouped;}
					else{throw new LexerException(string.Format(LexerException.UnknownTokenParsed,(string)data));}
					break;
					}
				}
			
			return type;
			}
		
		#endregion	
		}
	}
