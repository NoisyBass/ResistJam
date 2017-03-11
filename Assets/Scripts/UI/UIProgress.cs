using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour {

	private Slider _slider;

	void Awake()
	{
		_slider = GetComponent<Slider> ();
	}

	void Update () {
		_slider.value = (float)GameManager.Instance.CurrentPoints / (float)GameManager.Instance.MaxPoints;
	}
}
