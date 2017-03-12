using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultLabel : MonoBehaviour {

	void OnEnable()
    {
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
