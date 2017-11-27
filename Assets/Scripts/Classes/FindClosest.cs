using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest {

	public static GameObject findClosestGameobjectWithTag(string tagName, Vector3 pos) {
		GameObject[] objArray;
		objArray = GameObject.FindGameObjectsWithTag (tagName);
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = pos;

		foreach (GameObject go in objArray) {
			if (go.transform.position == pos) {
				continue;
			}
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		if (closest != null) {
			return closest;
		} else {
			return null;
		}
	}
}
