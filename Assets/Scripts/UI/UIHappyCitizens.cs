using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHappyCitizens : MonoBehaviour {

	private Text _label;

	void Awake()
	{
		_label = GetComponent<Text> ();
	}

	void Update()
	{
		_label.text = GameManager.Instance.HappyCitizens.ToString ();
	}
}
