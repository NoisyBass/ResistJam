using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGizmo : MonoBehaviour {

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (transform.position, 0.1f);
	}
}
