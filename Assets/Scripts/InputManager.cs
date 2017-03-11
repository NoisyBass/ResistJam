using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager> {

	private bool _controller = false;

	private ButtonDownEvent _buttonDownEvent;

	public bool ControllerConnected
	{
		get { return _controller; }
	}

	protected override void Awake()
	{
		base.Awake ();

		_buttonDownEvent = new ButtonDownEvent ();
	}

	void Update () {

		_controller = Input.GetJoystickNames().Length != 0 && Input.GetJoystickNames ()[0] != string.Empty;
		if (Input.GetButtonDown ("A")) {
			if (_controller) {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_A;
				//Debug.Log ("A pressed");
			} else {
				//Debug.Log ("Down pressed");
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_DOWN;
			}
			EventManager.Instance.OnEvent (this, _buttonDownEvent);
		}	

		if (Input.GetButtonDown ("B")) {
			if (_controller) {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_B;
				//Debug.Log ("B pressed");
			} else {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_RIGHT;
				//Debug.Log ("Right pressed");
			}
			EventManager.Instance.OnEvent (this, _buttonDownEvent);
		}

		if (Input.GetButtonDown ("X")) {
			if (_controller) {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_X;
				//Debug.Log ("X pressed");
			} else {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_LEFT;
				//Debug.Log ("Left pressed");
			}
			EventManager.Instance.OnEvent (this, _buttonDownEvent);
		}

		if (Input.GetButtonDown ("Y")) {
			if (_controller) {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_Y;
				//Debug.Log ("Y pressed");
			} else {
				_buttonDownEvent.ButtonType = NoteButton.ButtonType.BUTTON_UP;
				//Debug.Log ("Up pressed");
			}
			EventManager.Instance.OnEvent (this, _buttonDownEvent);
		}

		//Debug.Log(Input.GetAxisRaw ("Horizontal"));
	}
}
