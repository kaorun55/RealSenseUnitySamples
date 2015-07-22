/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2012-2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RSUnityToolkit;

/// <summary>
/// Hide action: disables the renderer on trigger
/// </summary>
[EventTrigger.EventTriggerAtt]
public class HideAction : BaseAction {
			
	#region Public Methods
	
	/// <summary>
	/// Returns the actions's description for GUI purposes.
	/// </summary>
	/// <returns>
	/// The action description.
	/// </returns>
	public override string GetActionDescription()
	{  
		return "This Action disables the renderer of the game object whenever the associated triggers are fired.";
	}	
	
	/// <summary>
	/// Sets the default trigger values (for the triggers set in SetDefaultTriggers() )
	/// </summary>
	/// <param name='index'>
	/// Index of the trigger.
	/// </param>
	/// <param name='trigger'>
	/// A pointer to the trigger for which you can set the default rules.
	/// </param>
	public override void SetDefaultTriggerValues(int index, Trigger trigger)
	{				
		switch (index)
		{
		case 0:
			((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<GestureDetectedRule>() };
			((GestureDetectedRule)(trigger.Rules[0])).Gesture = MCTTypes.RSUnityToolkitGestures.Grab;
			break;
		}
	}
	
	#endregion
	
	#region Protected Methods
	
	/// <summary>
	/// Sets the default triggers for that action.
	/// </summary>
	protected override void SetDefaultTriggers()
	{	
		SupportedTriggers = new Trigger[1]{
		AddHiddenComponent<EventTrigger>()};			
	}
	#endregion
	
	#region Private Methods
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{	 		
		//Makes sure we have a renderer
		if (this.GetComponent<Renderer>() == null)
		{
			return;
		}
		
		ProcessAllTriggers();
		
		foreach (Trigger trgr in SupportedTriggers)
		{						
			if ( trgr.Success )
			{
				this.GetComponent<Renderer>().enabled = false;	
				break;
			}
		}
	}
	
	#endregion
	
	#region Menu
	#if UNITY_EDITOR
	
	/// <summary>
	/// Adds the action to the RealSense Unity Toolkit menu.
	/// </summary>
	[UnityEditor.MenuItem ("RealSense Unity Toolkit/Add Action/Hide")]
	static void AddThisAction () 
	{
		AddAction<HideAction>();
	} 
	
	#endif
	#endregion
}
  