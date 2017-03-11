using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent {

	public enum GameEventType
	{
		END_NOTE,
		END_CITIZEN,
		BUTTON_DOWN,
		CITIZEN_HAPPY
	}

	private GameEventType _type;

	public GameEventType Type {
		get { return _type; }
	}

	public GameEvent(GameEventType type)
	{
		_type = type;
	}
}
