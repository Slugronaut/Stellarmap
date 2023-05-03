using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	//This was originally an abstract class but the VS designer seems to hate them so I had to make it
	//non-abstract with virtual functions instead
	public class AFunctionControl : UserControl, IFunctionControl
		{
		readonly private static string DefaultEmptyText = "\"\"";
		protected EntryType KeyEntryType = EntryType.Mixed;
		protected EntryType ValueEntryType = EntryType.Mixed;
		private string FunctionDeclaration = "";
		private Object InternalStorage;
		protected bool NoPostback = false;
		protected string IncludeFile;
		
		protected event EventHandler UpdatedFunction;	
		
		#region abstract
		//These were originally abstract elements but due to VS control desgner's hatred of them
		//I had to change the class type to something instantible
		public virtual string PullEntry()
			{
			throw new Exception("The method or operation is not implemented.");
			}
		public virtual void PushEntry(string entry)
			{
			throw new Exception("The method or operation is not implemented.");
			}
		
		public virtual FuncParamType ParameterType
			{
			get {throw new Exception("The method or operation is not implemented.");}
			set	{throw new Exception("The method or operation is not implemented.");}
			}	
		#endregion
		
		
				
		public void SubscribeToUpdate(EventHandler handler)
			{
			this.UpdatedFunction += new EventHandler(handler);
			}
		
		public virtual void UnsubscribeToUpdate(EventHandler handler)
			{
			this.UpdatedFunction -= new EventHandler(handler);
			}
					
		
		#region properties
		public virtual ControlType FunctionControlType
			{
			get {return ControlType.Unset;}
			}
		
		public new bool Enabled
			{
			get {
				UserControl con = this as UserControl;
				return con.Enabled;
				}
			set {
				UserControl con = this as UserControl;
				con.Enabled = value;
				}
			}
		
		public string FunctionName
			{
			get {
				return this.FunctionDeclaration;
				}
			set {
				this.FunctionDeclaration = value;
				}
			}
			
		public EntryType ValueType
			{
			get {return this.ValueEntryType;}
			set {this.ValueEntryType = value;}
			}
		
		public EntryType KeyType
			{
			get {return this.KeyEntryType;}
			set {this.KeyEntryType = value;}
			}
		
		public new Object Tag
			{
			set {InternalStorage = value;}
			get {return InternalStorage;}
			}
		
		public virtual string LabelText
			{
			get{return "";}
			set{}
			}
		
		public string RequiredHeader
			{
			get {return this.IncludeFile;}
			set {this.IncludeFile = value;}
			}
		#endregion	
		
		
		#region private methods
		public void PostUpdate(object sender,EventArgs args)
			{
			if(NoPostback)	return;
			if(UpdatedFunction != null) UpdatedFunction(this,args);
			}
		
		/// <summary>
		/// 
		/// </summary>
		protected string ProcessInputKey(string str)
			{
			return ProcessInputText(str,this.KeyEntryType);
			}
		
		/// <summary>
		/// 
		/// </summary>
		protected string ProcessInputValue(string str)
			{
			return ProcessInputText(str,this.ValueEntryType);
			}
		
		private string ProcessInputText(string str,EntryType type)
			{
			str = str.Trim();
			if(str.Length < 1) return str;
			
			switch(type)
				{
				case EntryType.Mixed:
					{
					//first check if it is a variable or a string literal
					if(str[0] == '\"')
						{
						if(str[str.Length-1] != '\"')
							{
							throw new FunctionControlException("Invalid string literal passed to " + this.FunctionName);
							}
						//literal
						str = str.TrimStart('\"');
						str = str.TrimEnd('\"');
						}
					else{
						//identifier
						if(str[0] != Stellarmap.Globals.Generator.FunctionParameterLiteralChar)	str = Stellarmap.Globals.Generator.FunctionParameterLiteral + str;
						}
					break;
					}
				
				case EntryType.Strings:
					{
					if(str[0] == '\"')
						{
						if(str[str.Length-1] != '\"')
							{
							throw new FunctionControlException("Invalid string literal passed to " + this.FunctionName);
							}
						//literal
						str = str.TrimStart('\"');
						str = str.TrimEnd('\"');
						}
					break;
					}
				}
			
			return str;
			}
				
		/// <summary>
		/// Processes a key stored in a FunctionControl so
		/// that it is LPC compatible.
		/// </summary>
		protected string ProcessOutputKey(string str)
			{
			return ProcessOutputText(str,this.KeyEntryType);
			}
		
		/// <summary>
		/// Processes a value stored in a FunctionControl so
		/// that it is LPC compatible.
		/// </summary>
		protected string ProcessOutputValue(string str)
			{
			return ProcessOutputText(str,this.ValueEntryType);
			}
		
		private string ProcessOutputText(string str,EntryType type)
			{
			str = str.Trim();
			
			switch(type)
				{
				case EntryType.Mixed:
					{
					if(str == null || str == "")
						{
						return DefaultEmptyText;
						}
					
					//check to see if it was a string literal or an identifier name
					if(str[0] == Stellarmap.Globals.Generator.FunctionParameterLiteralChar)
						{
						//identifier
						return str.TrimStart(Stellarmap.Globals.Generator.FunctionParameterLiteralChar);
						}
					else{
						//literal
						if(str[0] != ('\"'))			str = '\"' + str;
						if(str[str.Length-1] !=('\"'))	str += '\"';
						}
					break;
					}
								
				case EntryType.Strings:
					{
					if(str == null || str == "")
						{
						return DefaultEmptyText;
						}
					
					if(str[0] != '\"')				str = "\"" + str;
					if(str[str.Length-1] != '\"')	str += "\"";
					
					break;
					}
				}
			
			return str;
			}
		
		#endregion	
		}
	}
