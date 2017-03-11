using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
/// 
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

	/**********************************************************************************************/
	/* PUBLIC DATA                                                                                */
	/**********************************************************************************************/

	#region PUBLIC_DATA

	///<summary>
	/// 
	/// </summary>
	public static T Instance
	{
		get { return _instance; }
	}
	#endregion // PUBLIC_DATA



	/**********************************************************************************************/
	/* PRIVATE DATA                                                                               */
	/**********************************************************************************************/

	#region PRIVATE_DATA

	private static T _instance;

	#endregion // PRIVATE_DATA



	/**********************************************************************************************/
	/* MONOBEHAVIOUR METHODS                                                                      */
	/**********************************************************************************************/

	#region MONOBEHAVIOUR_METHODS

	///<summary>
	/// 
	///</summary>
	protected virtual void Awake()
	{
		if (_instance == null) {
			_instance = this as T;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
	}

	#endregion // MONOBEHAVIOUR_METHODS

}
