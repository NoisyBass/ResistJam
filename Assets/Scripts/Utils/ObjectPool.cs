using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///<summary>
/// 
/// </summary>
public class ObjectPool {

	/**********************************************************************************************/
	/* PRIVATE DATA                                                                               */
	/**********************************************************************************************/

	#region PRIVATE_DATA

	private List<GameObject> _pool;

	#endregion // PRIVATE_DATA



	/**********************************************************************************************/
	/* CONSTRUCTORS                                                                               */
	/**********************************************************************************************/

	#region CONSTRUCTORS

	///<summary>
	/// 
	/// </summary>
	public ObjectPool(int size, GameObject prefab)
	{
		_pool = new List<GameObject> ();
		for (int i = 0; i < size; i++) {
			GameObject obj = GameObject.Instantiate (prefab);
			_pool.Add (obj);
		}
	}

	///<summary>
	/// 
	/// </summary>
	public ObjectPool(int size, GameObject prefab, GameObject parent)
	{
		_pool = new List<GameObject> ();
		for (int i = 0; i < size; i++) {
			GameObject obj = GameObject.Instantiate (prefab);
			obj.transform.SetParent (parent.transform, false);
			_pool.Add (obj);
		}
	}

	#endregion // CONSTRUCTORS



	/**********************************************************************************************/
	/* PUBLIC METHODS                                                                             */
	/**********************************************************************************************/

	#region PUBLIC_METHODS

	///<summary>
	/// 
	/// </summary>
	public GameObject CreateObject()
	{
		if (_pool.Count > 0)
		{
			GameObject obj = _pool [_pool.Count - 1];
			_pool.RemoveAt (_pool.Count - 1);
			return obj;
		}
		return null;
	}

	///<summary>
	/// 
	/// </summary>
	public void DestroyObject(GameObject obj)
	{
		obj.SetActive (false);
		_pool.Add (obj);
	}

	///<summary>
	/// 
	/// </summary>
	public void ClearPool()
	{
		// The GC is supposed to take care of destroying the objects
		_pool.Clear ();
		_pool = null;
	}

	#endregion // PUBLIC_METHODS

}
