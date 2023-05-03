using System;
using System.Collections.Generic;
using System.Text;

namespace Stellarmass.LPC
	{
	public static class LPCKeywords
		{
		readonly public static string NewLine = Environment.NewLine;
		readonly public static List<string> Comments = new List<string> (new string[] {	"//",
																						"/*",
																						"*/" });
		
		readonly public static List<string> Operators = new List<string>(new string[] {	"+=",
																						"-=",
																						"*=",
																						"/=",
																						"%=",
																						"^=",
																						"++",
																						"--",
																						"->",
																						"&&",
																						"||",
																						"==",
																						"!=",
																						"<=",
																						">=",
																						"::",
																						":",
																						",",
																						"{",
																						"}",
																						"(",
																						")",
																						"[",
																						"]",
																						"=",
																						"<",
																						">",
																						"+",
																						"-",
																						"*",
																						"/",
																						"%",
																						"^",
																						"&",
																						"|",
																						"!",
																						"?",
																						".",
																						"$", });
		
		readonly public static List<string> LogicalOperators = new List<string> (new string[] {	"&&",
																								"||",
																								"==",
																								"!=",
																								"<=",
																								">=",
																								"<",
																								">"});
		readonly public static List<string> MathOperators = new List<string> (new string[] {"++",
																							"--",
																							"+",
																							"-",
																							"*",
																							"/",
																							"%",
																							"^"});
																							
		
		readonly public static List<string> BooleanOperators = new List<string> (new string[] {	"&",
																								"|",
																								"!"});
		
		readonly public static List<string> LanguageOperators = new List<string> (new string[] {"+=",
																								"-=",
																								"*=",
																								"/=",
																								"%=",
																								"^=",
																								"->",
																								"::",
																								":",
																								",",
																								"{",
																								"}",
																								"(",
																								")",
																								"[",
																								"]",
																								"=",
																								".",
																								"?"});
		
		readonly public static List<string> DataTypes = new List<string>(new string[] {	"object",
																						"int",
																						"float",
																						"string",
																						"class",
																						"void",
																						"mapping",
																						"mixed",
																						"function",
																						"closure",
																						"enum",
																						"struct",
																						"char" });
		
		readonly public static List<string> Modifiers = new List<string>(new string[] {	"::",
																						"*",
																						"array",
																						"varargs",
																						"static",
																						"public",
																						"private",
																						"protected",
																						"nosave",
																						"nomask",
																						"virtual" });
		
		readonly public static List<string> FunctionModifiers = new List<string>(new string[] {	"::",
																								"varargs",
																								"static",
																								"public",
																								"private",
																								"protected",
																								"nomask",
																								"virtual" });
		
		readonly public static List<string> VariableModifiers = new List<string>(new string[] {	"*",
																								"array",
																								"static",
																								"public",
																								"private",
																								"protected",
																								"nosave" });
		
		readonly public static List<string> AccessModifiers = new List<string>(new string[] {	"public",
																								"private",
																								"protected" });
		
		readonly public static List<string> FunctionStateModifiers = new List<string>(new string[] {"static"});
		
		readonly public static List<string> FunctionArgumentsModifiers = new List<string>(new string[] {"varargs"});
		
		readonly public static List<string> Instructions = new List<string> (new string[] {	"return",
																							"inherit",
																							"break",
																							";" });
		
		readonly public static List<string> Logic = new List<string> (new string[] {	"if",
																						"else",
																						"for",
																						"do",
																						"while",
																						"switch",
																						"case",
																						"default" });
		
		readonly public static List<string> Values = new List<string> (new string[]	{	"null" });
		
		readonly public static List<string> Symbols = new List<string> (new string[] {	"<",
																						">",
																						";",
																						"\"",
																						"#" });
				
		readonly public static List<string> Preprocess = new List<string> (new string [] {	"#define ",
																							"#undefine ",
																							"#include ",
																							"#ifdef ",
																							"#ifndef ",
																							"#if ",
																							"#elseif ",
																							"#else ",
																							"#endif ",
																							"#pragma " });
		
		readonly public static List<string> OpenningPaired = new List<string> (new string[] {"{",
																							"(",
																							"["});
		
		readonly public static List<string> ClosingPaired = new List<string> (new string[] {"}",
																							")",
																							"]"});
		
		public static List<string> Keywords
			{
			get {
				List<string> list = new List<string>(37);
				list.AddRange(DataTypes);
				list.AddRange(Modifiers);
				list.AddRange(Instructions);
				list.AddRange(Logic);
				list.AddRange(Values);
				return list;
				}
			}
		
		public static bool IsKeyword(string str)
			{
			foreach(string s in Keywords)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsOperator(string str)
			{
			foreach(string s in Operators)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsMathOperator(string str)
			{
			foreach(string s in MathOperators)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsBooleanOperator(string str)
			{
			foreach(string s in BooleanOperators)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsLogicalOperator(string str)
			{
			foreach(string s in LogicalOperators)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsLanguageOperator(string str)
			{
			foreach(string s in LanguageOperators)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsCommentMark(string str)
			{
			foreach(string s in Comments)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsInstruction(string str)
			{
			foreach(string s in Instructions)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsSymbol(string str)
			{
			foreach(string s in Symbols)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsDataType(string str)
			{
			foreach(string s in DataTypes)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsLogicKeyword(string str)
			{
			foreach(string s in Logic)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsModifier(string str)
			{
			foreach(string s in Modifiers)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsFunctionModifier(string str)
			{
			foreach(string s in FunctionModifiers)
				{
				if(str == s) return true;
				}
			return false;
			}	
		
		public static bool IsVariableModifier(string str)
			{
			foreach(string s in VariableModifiers)
				{
				if(str == s) return true;
				}
			return false;
			}	
		
		public static bool IsAccessModifier(string str)
			{
			foreach(string s in AccessModifiers)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsFunctionStateModifier(string str)
			{
			foreach(string s in FunctionStateModifiers)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsFunctionArgumentsModifier(string str)
			{
			foreach(string s in FunctionArgumentsModifiers)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsPreprocess(string str)
			{
			foreach(string s in Preprocess)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsOpenningPair(string str)
			{
			foreach(string s in OpenningPaired)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static bool IsClosingPair(string str)
			{
			foreach(string s in ClosingPaired)
				{
				if(str == s) return true;
				}
			return false;
			}
		
		public static string GetMatchingEndPair(string str)
			{
			for(int index = 0; index < OpenningPaired.Count; index++)
				{
				if(str == OpenningPaired[index])
					{
					return ClosingPaired[index];
					}
				}
			throw new System.IndexOutOfRangeException();
			}
		
		public static string GetMatchingBeginPair(string str)
			{
			for(int index = 0; index < ClosingPaired.Count; index++)
				{
				if(str == ClosingPaired[index])
					{
					return OpenningPaired[index];
					}
				}
			throw new System.IndexOutOfRangeException();
			}
		
		}
	}
