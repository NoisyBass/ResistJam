using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 
/// </summary>
public class EventManager : Singleton<EventManager> {

	/**********************************************************************************************/
	/* PUBLIC DATA                                                                                */
	/**********************************************************************************************/

	#region PUBLIC_DATA

	public delegate void EventHandler(object sender, GameEvent e);

	#endregion // PUBLIC_DATA



	/**********************************************************************************************/
	/* PRIVATE DATA                                                                               */
	/**********************************************************************************************/

	#region PRIVATE_DATA

	private Dictionary<GameEvent.GameEventType, List<EventHandler>> _eventHandlers;

	#endregion // PRIVATE_DATA



	/**********************************************************************************************/
	/* MONOBEHAVIOUR METHODS                                                                      */
	/**********************************************************************************************/

	#region MONOBEHAVIOUR_METHODS

	///<summary>
	/// 
	/// </summary>
	protected override void Awake () {
		base.Awake ();

		_eventHandlers = new Dictionary<GameEvent.GameEventType, List<EventHandler>> ();
	}

	#endregion // MONOBEHAVIOUR_METHODS



	/**********************************************************************************************/
	/* PUBLIC METHODS                                                                             */
	/**********************************************************************************************/

	#region PUBLIC_METHODS

	///<summary>
	/// 
	/// </summary>
	public void AddHandler(GameEvent.GameEventType type, EventHandler handler)
	{
		if (!_eventHandlers.ContainsKey (type)) {
			_eventHandlers.Add(type, new List<EventHandler>());
		} 
		_eventHandlers [type].Add (handler);
	}

	///<summary>
	/// 
	/// </summary>
	public void DeleteHandler(GameEvent.GameEventType type, EventHandler handler)
	{
		List<EventHandler> handlers = _eventHandlers [type];

		if (handlers != null) {
			handlers.Remove (handler);
		}
	}

	///<summary>
	/// 
	/// </summary>
	public void OnEvent(object sender, GameEvent e)
	{
		List<EventHandler> handlers = _eventHandlers [e.Type];
		for (int i = 0; i < handlers.Count; i++) {
			handlers [i] (sender, e);
		}
	}

	#endregion // PUBLIC_METHODS
}
