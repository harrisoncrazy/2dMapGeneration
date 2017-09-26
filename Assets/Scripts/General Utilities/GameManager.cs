using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public Transform selectedTile;

	public bool isBuildingSelected = false;

	//building sprites
	public Sprite woodGatherer;

	//building bools
	public bool placingWoodGatherer;/* REPLACE WTIH ARRAY OF BOOLS */
	public bool placingFoodGatherer;
	public bool placingStoneGatherer;

	// Use this for initialization
	void Start () {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void disablePlacementModes() { //run thru the array of bools and disable them all
		placingWoodGatherer = false;
	}
}
