using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyButton : MonoBehaviour {

	public GameObject objectToDestroy;

	public void OnClickDestroy() {
		Destroy (objectToDestroy.gameObject);
	}
}
