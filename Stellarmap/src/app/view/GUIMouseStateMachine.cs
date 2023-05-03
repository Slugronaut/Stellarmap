using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;




namespace Stellarmap
	{
	class EventCombo
		{
		public MouseStates State;
		public MouseEvents Event;
		
		public EventCombo(MouseEvents mouseEvent,MouseStates mouseState)
			{
			State = mouseState;
			Event = mouseEvent;
			}
		}
	
	enum MouseActions
		{
		//normal actions
		NormalOrHover,
		WheelViewVertically,
		Activate,
		MoveImage,
		EnterHover,
		EnterNodeHover,
		EnterNormal,
		EnterGroupMode,
		Context,
		DragRect,
		DragLine,
		EndDragLine,
		SelectGroup,
		GrabGroup,
		MoveGroup,
		DeselctAndContext,
		AutoDrag,
		
		//alternate actions
		EnterAltMode,
		EnterAltHover,
		EnterAltGroupMode,
		AltModeOrAltHover,
		AltGroupModeOrAltHover,
		AddToImageGroup,
		EnterDraggingView,
        EnterDraggingViewGroup,
		MoveView,
		DraggingViewOrGroup,
		
		//shift actions
		EnterShiftedMode,
		WheelViewHorizontally,
		
		EnterPreviousMode,
		}
	
	public enum MouseStates
		{
		//normal modes
		Normal,		
		Hover,
		NodeHover,
		ContextMenu,
		DraggingImage,
		DraggingLine,
		DraggingSelectionRect,
		DraggingGroup,
		GroupMode,
		AutoDragging,
		
		//alternate modes
		AltMode,
		AltHover,
		AltGroupMode,
		AltGroupHover,
		DraggingView,
        DraggingViewGroup,
		
		//shifted states
		ShiftMode,
		}
	
	public enum MouseEvents
		{
		MouseWheel,
		MoveMouse,		
		LeftClick,
		LeftDrag,
		LeftHold,
		LeftRelease,		
		RightClick,
		RightDrag,
		RightHold,
		RightRelease,
		MiddleClick,
		MiddleDown,
		MiddleRelease,
		CtrlDown,
		CtrlUp,
		ShiftDown,
		ShiftUp,
		}
	
	public class GUIMouseStateMachine
		{
		#region members
		MouseStates State;
		MouseStates PrevState;
		MouseActions PrevStateAction;
		MouseEvents PrevStateEvents;
		Dictionary<EventCombo,MouseActions>	ActionTriggers = new Dictionary<EventCombo,MouseActions>();
		readonly VirtualDomain	refVirtualDomain;
		readonly IViewPort		refViewPort;
		readonly Control refParent;
		
		ImageBox refHoveredImage = null;
		ConnectionNode refHoveredNode = null;
		#endregion
				
		
		#region events
		//public event EventHandler OnIsPointInSelectedGroup;
		//public event EventHandler OnIsPointInImage;
		//public event EventHandler OnActivateImage;
		//public event EventHandler OnMoveImage;
		//public event EventHandler OnMoveImageGroup;
		//public event EventHandler OnSelectGroup;
		public event EventHandler RoomContextMenu;
		public event EventHandler WorkspaceContextMenu;
		public event EventHandler ActivateSelectionRect;
		public event EventHandler DeactivateSelectionRect;
		public event EventHandler SetConnection;
		public event EventHandler RemoveConnection;
		public event EventHandler ActivateDragLine;
		public event EventHandler DeactivateDragLine;
		#endregion
				
		
		#region contructors
		public GUIMouseStateMachine(VirtualDomain domainView,IViewPort viewPort,Control parent)
			{
			System.Diagnostics.Debug.Assert(domainView != null);
			refVirtualDomain = domainView;
			refViewPort = viewPort;
			refParent = parent;
			
			//Note: there are no actions defined for the ContextMenu state. It must be handled externaly
			//by the controlling form. That form must attach event handlers to the appropriate
			//context menu events and then call ContextMenuClose to return to the appropriate state
			//when the menu is closed.
			
			/*
			 * Static States
			 */
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.Normal),MouseActions.NormalOrHover); //also node hover
			ActionTriggers.Add(new EventCombo(MouseEvents.MouseWheel,MouseStates.Normal),MouseActions.WheelViewVertically);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.Normal),MouseActions.DragRect);
			ActionTriggers.Add(new EventCombo(MouseEvents.RightClick,MouseStates.Normal),MouseActions.Context);
			ActionTriggers.Add(new EventCombo(MouseEvents.CtrlDown,MouseStates.Normal),MouseActions.EnterAltMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.ShiftDown,MouseStates.Normal),MouseActions.EnterShiftedMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.MiddleClick,MouseStates.Normal),MouseActions.EnterDraggingView);
			
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.Hover),MouseActions.NormalOrHover);
			ActionTriggers.Add(new EventCombo(MouseEvents.MouseWheel,MouseStates.Hover),MouseActions.WheelViewVertically);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.Hover),MouseActions.Activate);
			ActionTriggers.Add(new EventCombo(MouseEvents.RightClick,MouseStates.Hover),MouseActions.Context);
			ActionTriggers.Add(new EventCombo(MouseEvents.CtrlDown,MouseStates.Hover),MouseActions.EnterAltHover);
			ActionTriggers.Add(new EventCombo(MouseEvents.CtrlDown,MouseStates.Hover),MouseActions.EnterAltMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.ShiftDown,MouseStates.Hover),MouseActions.EnterShiftedMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.MiddleClick,MouseStates.Hover),MouseActions.EnterDraggingView);
			
			
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.NodeHover),MouseActions.NormalOrHover);//@
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.NodeHover),MouseActions.DragLine);//@
			ActionTriggers.Add(new EventCombo(MouseEvents.ShiftDown,MouseStates.NodeHover),MouseActions.EnterShiftedMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.MiddleClick,MouseStates.NodeHover),MouseActions.EnterDraggingView);
			ActionTriggers.Add(new EventCombo(MouseEvents.MouseWheel,MouseStates.NodeHover),MouseActions.WheelViewVertically);
			
			
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.GroupMode),MouseActions.GrabGroup);
			ActionTriggers.Add(new EventCombo(MouseEvents.RightClick,MouseStates.GroupMode),MouseActions.DeselctAndContext);
			ActionTriggers.Add(new EventCombo(MouseEvents.MiddleClick,MouseStates.GroupMode),MouseActions.EnterDraggingViewGroup);
			//ActionTriggers.Add(new EventCombo(MouseEvents.CtrlDown,MouseStates.GroupMode),MouseActions.EnterAltGroupMode);
			
			
			/*
			 * Dynamic States
			 */			
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingImage),MouseActions.MoveImage); //this could enter AutoDraging state as well
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.DraggingImage),MouseActions.EnterHover);
			
			//ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.AutoDragging),MouseActions.AutoDrag); //this could enter DraggingImage state as well
			//ActionTriggers.Add(new EventCombo(MouseEvents.LeftHold,MouseStates.AutoDragging),MouseActions.AutoDrag);
			//ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.AutoDragging),MouseActions.EnterHover);
			
			//selection Rect
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingSelectionRect),MouseActions.DragRect);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.DraggingSelectionRect),MouseActions.SelectGroup);
			ActionTriggers.Add(new EventCombo(MouseEvents.RightClick,MouseStates.DraggingSelectionRect),MouseActions.SelectGroup);
			
			//connection line
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingLine),MouseActions.DragLine);//@
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.DraggingLine),MouseActions.EndDragLine);//@
			
									
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingGroup),MouseActions.MoveGroup);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.DraggingGroup),MouseActions.EnterGroupMode);
			
			
			/*
			 * Alternate Triggers
			 */
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.AltMode),MouseActions.AltModeOrAltHover);
			ActionTriggers.Add(new EventCombo(MouseEvents.CtrlUp,MouseStates.AltMode),MouseActions.EnterNormal);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.AltMode),MouseActions.EnterDraggingView);
			
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingView),MouseActions.MoveView);
			ActionTriggers.Add(new EventCombo(MouseEvents.LeftRelease,MouseStates.DraggingView),MouseActions.EnterAltMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.CtrlUp,MouseStates.DraggingView),MouseActions.EnterNormal);
			ActionTriggers.Add(new EventCombo(MouseEvents.MiddleRelease,MouseStates.DraggingView),MouseActions.EnterPreviousMode);

            ActionTriggers.Add(new EventCombo(MouseEvents.MiddleRelease,MouseStates.DraggingViewGroup),MouseActions.EnterGroupMode);
			ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.DraggingViewGroup),MouseActions.MoveView);
			
			//ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.AltHover),MouseActions.AltModeOrAltHover);
			//ActionTriggers.Add(new EventCombo(MouseEvents.CtrlUp,MouseStates.AltHover),MouseActions.EnterHover);
			//ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.AltHover),MouseActions.AddToImageGroup);
			
			//ActionTriggers.Add(new EventCombo(MouseEvents.LeftClick,MouseStates.AltGroupMode),MouseActions.DraggingViewOrGroup);
			//ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.AltGroupMode),MouseActions.AltModeOrAltHover);
			//ActionTriggers.Add(new EventCombo(MouseEvents.CtrlUp,MouseStates.AltGroupMode),MouseActions.EnterGroupMode);
			//ActionTriggers.Add(new EventCombo(MouseEvents.MoveMouse,MouseStates.AltGroupMode),MouseActions.AltModeOrAltHover);
			
			/*
			 * Shifted Triggers
			 */
			ActionTriggers.Add(new EventCombo(MouseEvents.MouseWheel,MouseStates.ShiftMode),MouseActions.WheelViewHorizontally);
			ActionTriggers.Add(new EventCombo(MouseEvents.ShiftUp,MouseStates.ShiftMode),MouseActions.EnterNormal);
			
			}
		
		#endregion
		
				
		#region methods
		public MouseStates MouseState
			{
			get {return State;}
			}
		
		public void PerformAction(MouseEvents mouseEvent,EventArgs e)
			{
			MouseMoveArgs mma = (MouseMoveArgs)e;			
			
			foreach(EventCombo combo in ActionTriggers.Keys)
				{
				if(combo.Event == mouseEvent && combo.State == this.State)
					{
					DoAction(ActionTriggers[combo],mma,mouseEvent);					
					return;
					}
				}
			}
		
		private void DoAction(MouseActions mouseAction,MouseMoveArgs mma,MouseEvents triggeringEvent)
			{
			switch(mouseAction)
				{
				/*
				 * Normal
				 */
				case MouseActions.NormalOrHover:
					{
					if(NodeHover(mma))
						{
						State = MouseStates.NodeHover;
						PrevState = State;
						PrevStateAction = mouseAction;
						break;
						}
					if(HilightMouseover(mma))
						{State = MouseStates.Hover;}
					else{State = MouseStates.Normal;}
					PrevState = State;
					PrevStateAction = mouseAction;
					break;
					}				
				case MouseActions.Activate:
					{
					if(ActivateMouseover(mma))
						{State = MouseStates.DraggingImage;}
					else{State = MouseStates.Normal;}
					
					break;
					}
				case MouseActions.MoveImage:
					{
					//state stays as MouseState.DraggingImage
					//MoveSelectedImages(mma);
					
					//if(mma.Current.X > this.refViewPort.ViewWidth + Globals.ImageBoxProperties.width)
					//    {
					//    //if the mouse is within within the 'auto scroll margin'
					//    //we stay in that state and calculate any movement
					//    State = MouseStates.AutoDragging;
					//    AutoScrollSelectedImage(mma);
					//    }
					//else{					
					//    //otherwise go back to regular image draging
					//    State = MouseStates.DraggingImage;
						MoveSelectedImages(mma);
					//	}	
					break;
					}
				case MouseActions.EnterHover:
					{
					State = MouseStates.Hover;
					PrevState = State;
					PrevStateAction = mouseAction;
					break;
					}
				case MouseActions.EnterNormal:
					{
					State = MouseStates.Normal;
					PrevState = State;
					PrevStateAction = mouseAction;
					break;
					}
				case MouseActions.EnterPreviousMode:
					{
					State = PrevState;
					
					this.DoAction(PrevStateAction,mma,triggeringEvent);
					break;
					}
				case MouseActions.EnterGroupMode:
					{
					State = MouseStates.GroupMode;
					PrevState = State;
					PrevStateAction = mouseAction;
					break;
					}
				case MouseActions.Context:
					{				
					PrevState = State;
					State = MouseStates.ContextMenu;
					PrevState = State;
					PrevStateAction = mouseAction;
					ShowContextMenu(mma);
					break;
					}
				case MouseActions.DragRect:
					{
					if(ActivateSelectionRect != null)	ActivateSelectionRect(this,mma);
					State = MouseStates.DraggingSelectionRect;
					
					//hilight ones in selection rect
					SelectGroup(mma);
					break;
					}
				case MouseActions.DragLine:
					{
					if(ActivateDragLine != null)	ActivateDragLine(this,mma);
					State = MouseStates.DraggingLine;
					break;
					}
				case MouseActions.EndDragLine:
					{
					ConnectRoom(mma);
					break;
					}
				case MouseActions.SelectGroup:
					{
					if(DeactivateSelectionRect != null) DeactivateSelectionRect(this,null);
					
					if(SelectGroup(mma))
						{State = MouseStates.GroupMode;}
					else{State = MouseStates.Normal;}
					PrevState = State;
					PrevStateAction = mouseAction;
					break;
					}
				case MouseActions.GrabGroup:
					{
					if(PointInGroup(mma))
						{State = MouseStates.DraggingGroup;}
					else{
						//if we didn't select an object in the group, we can still see
						//if anything was at least activated
						if(HilightMouseover(mma))
							{
							State = MouseStates.Hover;
							if(ActivateMouseover(mma))
								{State = MouseStates.DraggingImage;}
							}
						else{State = MouseStates.DraggingSelectionRect;}						
						}
					break;
					}
				case MouseActions.MoveGroup:
					{
					//state stays MouseStates.DraggingGroup
					MoveSelectedImages(mma);
					break;
					}
				case MouseActions.DeselctAndContext:
					{
					DeselectGroup();					
					State = MouseStates.ContextMenu;
					ShowContextMenu(mma);
					break;
					}
				case MouseActions.AutoDrag:
					{
					if(mma.Current.X > this.refViewPort.ViewWidth + Globals.ImageBoxProperties.width)
						{
						//if the mouse is within within the 'auto scroll margin'
						//we stay in that state and calculate any movement
						State = MouseStates.AutoDragging;
						AutoScrollSelectedImage(mma);
						}
					else{					
						//otherwise go back to regular image draging
						State = MouseStates.DraggingImage;
						MoveSelectedImages(mma);
						}
					break;
					}
								
				/*
				 * Alternate Modes
				 */
				case MouseActions.EnterAltMode:
					{
					State = MouseStates.AltMode;
					PrevState = State;
					PrevStateAction = mouseAction;
					//DeselectGroup(); //no hovering in this mode, remove all hilights from imagboxes
					break;
					}				
				case MouseActions.EnterDraggingView:
					{
					State = MouseStates.DraggingView;
					break;
					}
		        case MouseActions.EnterDraggingViewGroup:
                    {
                    State = MouseStates.DraggingViewGroup;
                    break;
                    }
				case MouseActions.MoveView:
					{
					//state stays MouseActions.DraggingView
					
					//Keep in mind that we want the camera to move the opposite direction
					//of the mouse so that the 'background' appears to move in sync with
					//mouse. That's why we need to use negative offsets.
					refVirtualDomain.MoveCamera( (mma.Offset.X - mma.Current.X) / Globals.GUI.ViewDragSpeedModifier, (mma.Offset.Y - mma.Current.Y) / Globals.GUI.ViewDragSpeedModifier);
					break;
					}
				case MouseActions.WheelViewVertically:
					{
					refVirtualDomain.MoveCamera(0,(-mma.Delta) / Globals.GUI.ViewDragSpeedModifier);
					break;
					}
				case MouseActions.EnterShiftedMode:
					{
					State = MouseStates.ShiftMode;
					PrevState = State;
					PrevStateAction = mouseAction;
					//DeselectGroup(); //no hovering in this mode, remove all hilights from imagboxes
					break;
					}	
				case MouseActions.WheelViewHorizontally:
					{
					refVirtualDomain.MoveCamera((-mma.Delta) / Globals.GUI.ViewDragSpeedModifier,0);
					
					break;
					}
				case MouseActions.EnterAltGroupMode:
				    {
					State = MouseStates.AltGroupMode;
					PrevState = State;
					PrevStateAction = mouseAction;
				    break;
				    }
				
				
				default:
					{
					break;
					}		
				}
			
			if(State == MouseStates.DraggingView)
				{
				refParent.Cursor = System.Windows.Forms.Cursors.NoMove2D;
				}
			else{
				refParent.Cursor = System.Windows.Forms.Cursors.Hand;
				}
			return;
			}
		
		private bool HilightMouseover(MouseMoveArgs mma)
			{
			refHoveredImage = refVirtualDomain.OnHilightImage(mma.Current.X,mma.Current.Y);
			
			if(refHoveredImage != null) return true;
			return false;
			}
		
		private bool NodeHover(MouseMoveArgs mma)
			{
			refHoveredNode = refVirtualDomain.OnHoverNode(mma.Current.X,mma.Current.Y);
			
			if(refHoveredNode != null)	{return true;}			
			return false;
			}
		
		private bool ActivateMouseover(MouseMoveArgs mma)
			{
			return refVirtualDomain.OnActivateImage(mma.Current.X,mma.Current.Y);
			}
		
		private bool SelectGroup(MouseMoveArgs mma)
			{
			Rectangle SelectionRect = new Rectangle();
			SelectionRect.X = System.Math.Min(mma.Current.X,mma.Start.X);
			SelectionRect.Y = System.Math.Min(mma.Current.Y,mma.Start.Y);
			SelectionRect.Width = System.Math.Abs(mma.Current.X - mma.Start.X);
			SelectionRect.Height = System.Math.Abs(mma.Current.Y - mma.Start.Y);
			
			//attempt to activate the one clicked on
			//ActivateMouseover(mma);
			
			return refVirtualDomain.OnSelectImageGroup(SelectionRect);
			}
		
		private void DeselectGroup()
			{
			//I need a better way to do this, for now I'm just picking some
			//highly unlikely coordinates (techincally impossible since I don't
			//allow the view to go there, but you never know what can happen
			//when someone starts tinkering ;)
			refVirtualDomain.OnSelectImageGroup(new Rectangle(-10000000,-10000000,1,1));
			}
		
		private bool PointInGroup(MouseMoveArgs mma)
			{
			return refVirtualDomain.IsPointInSelectionGroup(mma.Current.X,mma.Current.Y);
			}
		
		private void ShowContextMenu(MouseMoveArgs mma)
			{
			//bring up the appropriate menu based on where we right-clicked
			if(ActivateMouseover(mma))
				{
				if(RoomContextMenu != null)	RoomContextMenu(this,null);
				}
			else{
				if(WorkspaceContextMenu != null)	WorkspaceContextMenu(this,null);
				}
			}
		
		private void MoveSelectedImages(MouseMoveArgs mma)
			{
			refVirtualDomain.MoveSelectedImages(new Point(mma.Current.X - mma.Offset.X,mma.Current.Y - mma.Offset.Y));
			}
		
		private void AutoScrollSelectedImage(MouseMoveArgs mma)
			{
			//we can safely assume the mouse is within the autoscroll margin
			Point vector = new Point(0,0);
			
			vector.X = mma.Current.X - (refViewPort.ViewWidth - Globals.ImageBoxProperties.width);
			refVirtualDomain.MoveCamera(vector.X,vector.Y);
			refVirtualDomain.MoveSelectedImages(vector);
			}
		
		/// <summary>
		/// This function must be called by the controlling form that has access to the context menu as there 
		/// is no other internally processed way for this statemachine to return to the normal state. It should
		/// be called when the menu collapses to signal a return to the normal state.
		/// </summary>
		public void SignalContextMenuClose()
			{
			if(PrevState == MouseStates.Normal || PrevState == MouseStates.GroupMode)
				{
				State = PrevState;
				}
			else{
				State = MouseStates.Normal;
				}
			}
		
		public void ResetStateMachine()
			{
			//State = MouseStates.Normal;
			//TODO: reset to the closest static state
			}	
		
		public void ConnectRoom(MouseMoveArgs mma)
			{
			if(DeactivateDragLine != null) DeactivateDragLine(this,null);
			
			//if we are over another image, attach the connection to that
			if(HilightMouseover(mma))
				{
				//connect
				State = MouseStates.Hover;
				
				refHoveredNode.SetTarget(refHoveredImage);
				RoomConnectionArgs args = new RoomConnectionArgs(refHoveredNode.Source.RoomName,
																 refHoveredImage.RoomName,
																 refHoveredNode.Direction,
																 ConnectionType.Connect);
				
				if(SetConnection != null)	SetConnection(this,args);
				}
			else{
				//disconnect
				State = MouseStates.Normal;
				RoomConnectionArgs args = new RoomConnectionArgs(refHoveredNode.Source.RoomName,
																 refHoveredNode.Target.RoomName,
																 refHoveredNode.Direction,
																 ConnectionType.Disconnect);
				refHoveredNode.SetTarget(null);
				if(RemoveConnection != null)	RemoveConnection(this,args);
				}
			}	
		#endregion
		}	
	
	}
