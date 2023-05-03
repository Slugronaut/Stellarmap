using System;
using System.Collections.Generic;
using System.Text;

using Lexer = Stellarmass.LPC.Lexer;
using Scanner = Stellarmass.LPC.Scanner;

namespace Stellarmass.LPC.Parser
	{
	/// <summary>
	/// Compainion class for the SyntaxRules class. This object simply defines
	/// all syntax constructs' rules and rule variations. Each construct is defined
	/// in a single function and the various rules are returned in a list. This
	/// class's methods are invoked indirectly using reflection by the 'SyntaxRules'
	/// class so no method registration is needed. All methods that are to be invoked
	/// must have their signatures begin with "DefineRules_".
	/// </summary>
	public class LPCSyntaxRulesObject
		{
		static public List<Syntax> DefineRules_CreateInheritRule()
			{
			Rule rule1 = new Rule(false,"inherit");
			Rule rule3 = new Rule(false,Lexer.LexmeType.Identifier);
			Rule rule4 = new Rule(false,";");
			
			Syntax InheritCommand = new Syntax(new Rule[] {rule1,rule3,rule4},TokenType.InheritCommand);
			
			List<Syntax> list = new List<Syntax>();
			list.Add(InheritCommand);
			return list;
			}
		
		static public List<Syntax> DefineRules_FunctionDefinitions()
			{
			List<Syntax> list = new List<Syntax>();
			
			//static rules
			Rule IdentifierRule = new Rule(false,Lexer.LexmeType.Identifier);
			Rule ReturnTypeRule = new Rule(false,Lexer.LexmeType.DataType);
			Rule OpenFuncRule = new Rule(false,Lexer.LexmeType.OpenParen);
			Rule CloseFuncRule = new Rule(false,Lexer.LexmeType.CloseParen);
			Rule OpenFuncBlockRule = new Rule(false,Lexer.LexmeType.OpenBrace);
			Rule CloseFuncBlockRule = new Rule(false,Lexer.LexmeType.CloseBrace);
			
			//vary in order
			Rule AccessRule = new Rule(true,new string[] {"public","private","protected"});
			Rule StateRule = new Rule(true,"static");
			Rule ArgsRule = new Rule(true,"varargs");
			Syntax FunctionDecl;
			
			FunctionDecl = new Syntax(new Rule[] {AccessRule,ArgsRule,StateRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			FunctionDecl = new Syntax(new Rule[] {AccessRule,StateRule,ArgsRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			
			FunctionDecl = new Syntax(new Rule[] {StateRule,ArgsRule,AccessRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			FunctionDecl = new Syntax(new Rule[] {StateRule,AccessRule,ArgsRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			
			FunctionDecl = new Syntax(new Rule[] {ArgsRule,StateRule,AccessRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			FunctionDecl = new Syntax(new Rule[] {ArgsRule,AccessRule,StateRule,ReturnTypeRule,IdentifierRule,OpenFuncRule,CloseFuncRule,OpenFuncBlockRule,CloseFuncBlockRule},TokenType.FunctionDefinition);
			list.Add(FunctionDecl);
			
			return list;
			}
		
		static public List<Syntax> DefineRules_FunctionCallsRedux()
			{
			List<Syntax> list = new List<Syntax>();
			
			//varying rules
			Rule ObjectHierarchyRule = new Rule(true,Lexer.LexmeType.Identifier);
			Rule ScopeResolutionRule = new Rule(false,"::");
			
			//static rules
			Rule FunctionNameRule = new Rule(false,Lexer.LexmeType.Identifier);
			Rule OpenParamsRule = new Rule(false,Lexer.LexmeType.OpenParen);
			Rule CloseParamsRule = new Rule(false,Lexer.LexmeType.CloseParen);
			Rule EndStatementRule = new Rule(false,";");

			//list.Add(new Syntax(new Rule[] {ObjectHierarchyRule,ScopeResolutionRule,FunctionNameRule,OpenParamsRule,CloseParamsRule },TokenType.FunctionCall));
			list.Add(new Syntax(new Rule[] {ObjectHierarchyRule,ScopeResolutionRule,FunctionNameRule,OpenParamsRule,CloseParamsRule,EndStatementRule},TokenType.FunctionCall) );
			list.Add(new Syntax(new Rule[] {FunctionNameRule,OpenParamsRule,CloseParamsRule,EndStatementRule},TokenType.FunctionCall) );
			
			return list;
			}
		
		static public List<Syntax> DefineRules_CommentLine()
			{
			List<Syntax> list = new List<Syntax>();
			
			Rule CommentStart = new Rule(false,Lexer.LexmeType.CommentLine);
			
			list.Add(new Syntax(new Rule[] {CommentStart},TokenType.CommentLine));
			return list;
			}
		
		static public List<Syntax> DefineRules_Include()
			{
			Rule preprocess = new Rule(false,Lexer.LexmeType.PreprocessLine);
			Rule includeStart = new Rule(false,"include");
			Rule openChev = new Rule(false,Lexer.LexmeType.OpenChevron);
			Rule closeChev = new Rule(false,Lexer.LexmeType.CloseChevron);
			Rule name = new Rule(false,Lexer.LexmeType.Identifier);
			Rule dot = new Rule(false,".");
			Rule extension = new Rule(false,"h");
			Rule quotedFile = new Rule(false,Lexer.LexmeType.StringLiteral);

			List<Syntax> list = new List<Syntax>();
			list.Add(new Syntax(new Rule[] { preprocess,includeStart,openChev,name,dot,extension,closeChev},TokenType.Include));
			list.Add(new Syntax(new Rule[] { preprocess,includeStart,quotedFile },TokenType.Include));
			return list;
			}	

		static public List<Syntax> REMOVED___DefineRules_LoneNewline()
			{
			List<Syntax> list = new List<Syntax>();

			Rule CommentStart = new Rule(false,Lexer.LexmeType.NewLine);

			list.Add(new Syntax(new Rule[] { CommentStart },TokenType.LoneNewLine));
			return list;
			}
		}
	}
