using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;

public class ResetPlayerPrefs : MonoBehaviour {

	[MenuItem("Edit/Reset PlayerPrefs")] 
	public static void DeletePlayerPrefs() 
	{ 
		PlayerPrefs.DeleteAll(); 
	}
}
#endif