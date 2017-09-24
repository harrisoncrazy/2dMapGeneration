using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public Transform selectedTile;

	public bool isBuildingSelected = false;

	// Use this for initialization
	void Start () {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (selectedTile != null) {
			if (selectedTile.gameObject.GetComponent<tileHandler> () == null) {
				isBuildingSelected = true;
			} else {
				isBuildingSelected = false;
			}
		}
	}
}
