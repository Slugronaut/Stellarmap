using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Sluggy.Utility;

namespace Stellarmap
	{
	public class ItemViewManager
		{
		#region members

		Form refParentForm;
		ItemModel Item;
		EventHandler refFunctionControlUpdateEvent;
		#endregion
		
		public ItemViewManager(Form parentForm,IDomainModelAdapter domain,EventHandler functionUpdate,ItemLoadType loadType)
			{
			if(parentForm == null)
				{throw new NullReferenceException("Invalid parent form passed to ItemViewManager."); }
			if(domain == null)
				{throw new NullReferenceException("Invalid domain passed to ItemViewManager."); }
			if(functionUpdate == null)
				{throw new NullReferenceException("Invalid update event handler passed to ItemViewManager."); }
			
			
			this.refParentForm = parentForm;
			this.Item = new ItemModel(domain,loadType);
			this.WireEvents(parentForm,functionUpdate);
			}
		
		~ItemViewManager()
			{
			if(this.refParentForm != null && this.refFunctionControlUpdateEvent != null)
				{
				this.UnwireEvents(this.refParentForm,this.refFunctionControlUpdateEvent);
				}
			}
		
		
		
		#region public interface
		public void DeleteFunctionsInCode()
			{
			Item.DeleteFunctionsInCode("create");
			}
		
		public void AddLine(string line)
			{
			Item.AddLine(line,"create");
			}
		
		public bool GenerateFile(ItemSaveType itemPath,string fileSaveName)
			{
			bool result = false;
			using(ProgressModal progress = new ProgressModal(5))
				{
				progress.Show();
				if(fileSaveName == null || fileSaveName.Length < 1)
					{return false;}
				
				progress.UpdateProgressBar();
				List<IFunctionControl> controls = this.CompileFunctionControlsList(this.refParentForm);
				progress.UpdateProgressBar();
				FunctionCallsCollection inherits = this.CompileIheritList(controls);
				progress.UpdateProgressBar();
				FunctionCallsCollection functions = this.CompileFunctioCallsList(controls);
				progress.UpdateProgressBar();
				result = this.Item.SaveModelToDisk(functions,inherits,itemPath,fileSaveName);
				progress.UpdateProgressBar();
				}
			return result;
			}
		
		/// <summary>
		/// Collects all controls that represent a function call, and stores their processed
		/// output in a list of FunctionCall objects.
		/// </summary>
		/// <param name="controls"></param>
		/// <returns></returns>
		public FunctionCallsCollection CompileFunctioCallsList(List<IFunctionControl> controls)
			{
			FunctionCallsCollection collection = new FunctionCallsCollection();

			foreach(IFunctionControl control in controls)
				{
				try
					{
					//only collect if enabled and it represents an actual function call
					if(control.Enabled && control.ParameterType != FuncParamType.Inherit)
						{
						string parameters = control.PullEntry();
						collection.DefineCall(control.FunctionName,parameters);
						if(parameters.Length > 0 && parameters != null && parameters != "({})" && parameters != "([])" && parameters != "\"\"")
							{
							collection.AddHeaderFile(control.RequiredHeader);
							}
						}
					}
				catch(FunctionControlException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				catch(ParserException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				}

			return collection;
			}

		/// <summary>
		/// Collects all controls that represent an inherit command, and stores their processed
		/// output in a list of FunctionCall objects. (yeah, I know. It should probably not be
		/// called a 'FunctionCall' object anymore. Sue me.)
		/// </summary>
		/// <param name="controls"></param>
		/// <returns></returns>
		public FunctionCallsCollection CompileIheritList(List<IFunctionControl> controls)
			{
			FunctionCallsCollection collection = new FunctionCallsCollection();

			foreach(IFunctionControl control in controls)
				{
				try
					{
					//only collect if it represents an inherit command
					//control enabled state does not matter
					if(control.ParameterType == FuncParamType.Inherit)
						{
						collection.DefineCall(control.FunctionName,control.PullEntry());
						if(control.PullEntry() == "0" && !collection.RemoveFlag.ContainsKey(control.FunctionName))
							{
							collection.RemoveFlag.Add(control.FunctionName,true);
							}
						if(control.PullEntry() == "1" && collection.RemoveFlag.ContainsKey(control.FunctionName))
							{
							collection.RemoveFlag.Remove(control.FunctionName);
							//collection.AddHeaderFile(control.RequiredHeader);
							}
						}
					}
				catch(FunctionControlException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				catch(ParserException e)
					{
					MessageBox.Show(e.Message,Globals.ErrorStrings.FunctionControlErrorTitle);
					}
				}

			return collection;
			}

		public List<IFunctionControl> CompileFunctionControlsList(Control parentControl)
			{
			List<IFunctionControl> list = new List<IFunctionControl>();

			foreach(Control control in parentControl.Controls)
				{
				if(control is IFunctionControl)
					{
					//AFunctionControl func = (AFunctionControl)control;
					//func.SubscribeToUpdate(this.OnFunctionControlUpdate);
					list.Add((IFunctionControl)control);
					}

				//recursively add other controls
				//however, don't do this if either disabled or a FlxieFunction
				//Especially the latter because it will definitely contain more
				//controls inside that should not be exposed
				if(control.Controls.Count > 0 && control.Enabled)
					{
					IFunctionControl con = control as IFunctionControl;
					if(con == null || con.ParameterType != FuncParamType.MultiParamFunction)
						{ list.AddRange(CompileFunctionControlsList(control)); }
					}
				}

			return list;
			}
		#endregion
		
		
		#region private methods
		private void WireEvents(Control parentControl,EventHandler eventHandler)
			{
			foreach(Control control in parentControl.Controls)
				{
				if(control is IFunctionControl)
					{
					IFunctionControl func = (IFunctionControl)control;
					func.SubscribeToUpdate(eventHandler);
					}
				
				//recursively add other controls
				if(control.Controls.Count > 0)
					{
					WireEvents(control,eventHandler);
					}
				}
			
			return;
			}
		
		private void UnwireEvents(Control parentControl,EventHandler eventHandler)
			{
			foreach(Control control in parentControl.Controls)
				{
				if(control is IFunctionControl)
					{
					AFunctionControl func = (AFunctionControl)control;
					func.UnsubscribeToUpdate(eventHandler);
					}
				
				//recursively add other controls
				if(control.Controls.Count > 0)
					{
					UnwireEvents(control,eventHandler);
					}
				}
			
			return;
			}
		
		#endregion		
		}
	}
