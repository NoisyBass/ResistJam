using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNoteEvent : GameEvent {

	private GameObject _note;

    private bool _hit;

	public GameObject Note
	{
		get { return _note; }
	}

    public bool Hit
    {
        get { return _hit; }
    }

	public EndNoteEvent() : base(GameEventType.END_NOTE) {}

	public EndNoteEvent(GameObject note, bool hit) : base(GameEventType.END_NOTE)
	{
		_note = note;
        _hit = hit;
	}
}
