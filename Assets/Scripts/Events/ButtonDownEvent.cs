using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDownEvent : GameEvent {

	private NoteButton.ButtonType _buttonType;

	public NoteButton.ButtonType ButtonType
	{
		get { return _buttonType; }
		set { _buttonType = value; }
	}

	public ButtonDownEvent() : base(GameEventType.BUTTON_DOWN) {}

	public ButtonDownEvent(NoteButton.ButtonType type) : base(GameEventType.BUTTON_DOWN) 
	{
		_buttonType = type;
	}
}
