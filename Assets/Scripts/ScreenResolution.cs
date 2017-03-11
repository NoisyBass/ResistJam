using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour {

	private Camera cam;

	void Awake () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		cam.orthographicSize = Screen.height / (100.0f * 2.0f);
	}
}
