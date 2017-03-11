using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCitizenEvent : GameEvent {

	private GameObject _citizen;

	public GameObject Citizen
	{
		get { return _citizen; }
		set { _citizen = value; }
	}

	public EndCitizenEvent() : base(GameEventType.END_CITIZEN) {}

	public EndCitizenEvent(GameObject citizen) : base(GameEventType.END_CITIZEN)
	{
		_citizen = citizen;
	}
}
