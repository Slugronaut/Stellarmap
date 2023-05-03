using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stellarmap
	{
	public class FunctionControlParam
		{
		public string ParamName = "";
		public string DisplayName = "";
		public FuncParamType ParamType = FuncParamType.None;
		public ControlType ControlType = ControlType.Unset;
		public EntryType KeyType = EntryType.NA;
		public EntryType ValueType = EntryType.NA;
		public string InitValue = null;
				
		public FunctionControlParam(string name,string display,ControlType conType,FuncParamType paramType,EntryType keyType,EntryType valueType,string initValue)
			{
			ParamName = name;
			DisplayName = display;
			ParamType = paramType;
			ControlType = conType;
			KeyType = keyType;
			ValueType = valueType;
			InitValue = initValue;
			}
		
		public FunctionControlParam(string name,string display,ControlType conType,FuncParamType paramType,EntryType keyType,EntryType valueType)
			: this(name,display,conType,paramType,keyType,valueType,null)
			{
			}
		}
	
	/// <summary>
	/// A highly specialized function control. This one supports multiple parameters for
	/// a function call by dynamically arranging a series of internal function controls.
	/// Each internal control represents a single parameter instead of an entire function.
	/// </summary>
	public partial class FlexiFunction : UserControl,IFunctionControl
		{
		#region members
		readonly private static string DefaultEmptyText = "\"\"";
		private string FunctionDeclaration = "";
		private Object InternalStorage;
		private List<IFunctionControl> ParameterControls = new List<IFunctionControl>();
		
		bool Initialized = false;
		bool NoPostBack = false;
		protected event EventHandler UpdatedFunction;
		protected string IncludeFile;
		#endregion
		
		
		#region constructors
		public FlexiFunction()
			{
			InitializeComponent();
			this.Resize += new EventHandler(OnResizeWindow);
			this.SizeChanged += new EventHandler(OnSizeChanged);
			}
		
		public FlexiFunction(string functionName,string functionDec,List<FunctionControlParam> paramControls) : this()
			{
			Init(functionName,functionDec,paramControls);
			return;
			}

		public FlexiFunction(string functionName,string functionDec,FunctionControlParam[] paramControls) : this()
			{
			Init(functionName,functionDec,paramControls);
			}

		public void Init(string functionName,string functionDec,List<FunctionControlParam> paramControls)
			{
			Init(functionName,functionDec,paramControls.ToArray());
			}

		public void Init(string functionName,string functionDec,FunctionControlParam[] paramControls)
			{
			if(Initialized) return;
			
			this.Label.Text = functionDec;
			if(paramControls != null)
				{
				foreach(FunctionControlParam param in paramControls)
					{
					#region giant switch that creates each control
					switch(param.ControlType)
						{
						case ControlType.CheckBox:
								{
								ParameterControls.Add(new Stellarmap.Check());
								break;
								}
						case ControlType.CheckList:
								{
								ParameterControls.Add(new CheckList());
								break;
								}
						case ControlType.ListBuilder:
								{
								ParameterControls.Add(new ListBuilder());
								break;
								}
						case ControlType.MapBuilder:
								{
								ParameterControls.Add(new MapBuilder());
								break;
								}
						case ControlType.ComboSelection:
								{
								ParameterControls.Add(new ComboSelection());
								break;
								}
						case ControlType.ComboListBuilder:
								{
								ParameterControls.Add(new ComboListBuilder());
								break;
								}
						case ControlType.ComboMapBuilder:
								{
								ParameterControls.Add(new ComboMapBuilder());
								break;
								}
						case ControlType.DoubleComboMap:
								{
								ParameterControls.Add(new DoubleComboMap());
								break;
								}
						case ControlType.NumberEntry:
								{
								ParameterControls.Add(new NumberEntry());
								break;
								}
						case ControlType.TextEntry:
								{
								ParameterControls.Add(new TextEntry());
								break;
								}
						case ControlType.TextDump:
								{
								ParameterControls.Add(new TextDump());
								break;
								}
						}
					#endregion
					
					if(ParameterControls.Count > 0)
						{
						IFunctionControl newestControl = (IFunctionControl)ParameterControls[ParameterControls.Count - 1];
						newestControl.FunctionName = "butts";//param.ParamName;
						newestControl.LabelText = param.DisplayName;
						newestControl.ParameterType = param.ParamType;
						newestControl.KeyType = param.KeyType;
						newestControl.ValueType = param.ValueType;
						
						if(param.InitValue != null && param.InitValue.Length > 0)
							{
							newestControl.PushEntry(param.InitValue);
							}
						}
					
					}//end foreach
				
				ArrangeParamControls();
				}
			
			FunctionName = functionName;
			Initialized = true;
			return;
			}
		#endregion
		
		
		#region public interface
		public void SubscribeToUpdate(EventHandler handler)
			{
			this.UpdatedFunction += new EventHandler(handler);
			}

		public virtual void UnsubscribeToUpdate(EventHandler handler)
			{
			this.UpdatedFunction -= new EventHandler(handler);
			}
		
		public string PullEntry()
			{
			StringBuilder text = new StringBuilder();
			
			foreach(IFunctionControl param in ParameterControls)
				{
				text.Append(param.PullEntry() + ",");
				}
			
			//remove comman from last parameter
			string str = text.ToString();
			return str.TrimEnd(',');
			}
		
		public void PushEntry(string entry)
			{
			}
		
		public ControlType FunctionControlType
			{
			get {return ControlType.MultiParam;}
			}
		#endregion
		
		
		#region properties
		public string LabelText
			{
			set {
				this.Label.Text = value;
				}
			get {
				return this.Label.Text;
				}
			}
		
		public Control ParamControlsContainer
			{
			get{return this.Label;}
			}
		
		public FuncParamType ParameterType
			{
			get{return FuncParamType.MultiParamFunction;}
			set{ }
			}
		
		public new bool Enabled
			{
			get
				{
				UserControl con = this as UserControl;
				return con.Enabled;
				}
			set
				{
				UserControl con = this as UserControl;
				con.Enabled = value;
				}
			}
		
		public string FunctionName
			{
			get
				{
				return this.FunctionDeclaration;
				}
			set
				{
				this.FunctionDeclaration = value;
				
				}
			}
		
		public EntryType ValueType
			{
			get { return EntryType.NA; }
			set { }
			}
		
		public EntryType KeyType
			{
			get { return EntryType.NA; }
			set { }
			}
		
		public new Object Tag
			{
			set { InternalStorage = value; }
			get { return InternalStorage; }
			}
		
		public List<IFunctionControl> Parameters
			{
			get {return this.ParameterControls;}
			}

		public string RequiredHeader
			{
			get { return this.IncludeFile; }
			set { this.IncludeFile = value; }
			}	
		#endregion
		
		
		#region private methods
		private void OnResizeWindow(object sender,EventArgs e)
			{
			int border = (int)Metrics.FlexiFunctionGroupBorder;
			Label.Location = new Point(border,border);
			Label.Size =  new Size(this.Size.Width - (border*2),this.Size.Height - (border*2));
			}

		private void OnSizeChanged(object sender,EventArgs args)
			{
			OnResizeWindow(sender,args);
			}
		
		private void PostUpdate(object sender,EventArgs args)
			{
			if(NoPostBack)	return;
			if(UpdatedFunction != null) UpdatedFunction(this,args);
			}
		
		private void ArrangeParamControls()
			{
			if(this.ParameterControls != null && this.ParameterControls.Count > 0)
				{
				Positioner post = new Positioner();
				Point offset = new Point((int)Metrics.FlexiFunctionGroupBorder/2,(int)Metrics.FlexiFunctionGroupBorder);
				int height = (int)Metrics.FlexiFunctionGroupBorder*2;
				
				Control controlRef = null;
				foreach(Control control in this.ParameterControls)
					{
					post.AddControlBelow(this.Label,controlRef,control,offset);
					height += control.Height + (int)Metrics.VerticalMargin;
					controlRef = control;
					
					//reset offset otherwise we'll keep doing it
					offset = new Point(0,0);
					}
				
				//resize control to match height of all sub-controls
				height += (int)Metrics.FlexiFunctionGroupBorder*2;
				this.Height = height;
				//controlRef = null;
				}
			}
		#endregion
		}
	}
