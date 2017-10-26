﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	//pathfinding values
	public baseGridPosition previousCell, searchFromCell;

	public Transform selectedTile;


	public bool isPlacementModeActive = false;
	public bool isBuildingSelected = false;

	public bool isBuildingPanelActive = false;

	//building stuff
	public GameObject woodGatherPrefab;
	public GameObject stoneGatherPrefab;
	public GameObject foodGatherPrefab;
	public GameObject leanToHousePrefab;

	//building bools
	public struct buildingPlaceMode
	{
		public string buildingName;
		public bool isPlacing;

		public buildingPlaceMode(string name) {
			buildingName = name;
			isPlacing = false;
		}
	}

	//placement bools
	public buildingPlaceMode woodGather = new buildingPlaceMode ("woodGather");
	public buildingPlaceMode stoneGather= new buildingPlaceMode ("stoneGather");
	public buildingPlaceMode foodGather = new buildingPlaceMode ("foodGather");
	public buildingPlaceMode leanToHouse = new buildingPlaceMode ("leanToHouse");

	public buildingPlaceMode[] buildingBools;

	//pop up panel values
	public GameObject tileInfoPanel;
	public Text tileText;
	public Text descriptionText;

	private float timerTick = 1.0f;

	private int manpowerTickDown = 5;//delaying manpower add

	// Use this for initialization
	void Start () {
		Instance = this;
		GrabBuildingInfoPanel ();
		addBuildingBools ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		timerTick -= Time.deltaTime;
		if (timerTick < 0) {
			updateResourceTotals ();
			timerTick = 1.0f;
		}
	}

	private void updateResourceTotals() {
		//resourceManager.Instance.woodResourceTick ();
		//resourceManager.Instance.stoneResourceTick ();
		//resourceManager.Instance.foodResourceTick ();
		manpowerTickDown--;
		if (manpowerTickDown <= 0) {
			resourceManager.Instance.manpowerResourceTick ();
			manpowerTickDown = 5;
		}
	}

	public bool placingWoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		woodGatherer woodGather = ((GameObject)Instantiate (woodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<woodGatherer> ();

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), GameObject.Find("homeBase").GetComponent<baseGridPosition>())) {
			Destroy (woodGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), GameObject.Find ("homeBase").GetComponent<baseGridPosition> ());
		woodGather.pathToBase = pathfindingManager.Instance.GetPath ();
		woodGather.pathToBase [0] = woodGather.GetComponent<baseGridPosition> ();
		woodGather.pathToBase [1].PathFrom = woodGather.gameObject;

		woodGather.name = "woodGathererBuilding";
		woodGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		woodGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		woodGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		woodGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		woodGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		woodGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		woodGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		woodGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = woodGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.woodGather.buildingCosts);

		return true;
	}

	public bool placingStoneGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		stoneGatherer stoneGather = ((GameObject)Instantiate (stoneGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<stoneGatherer> ();

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), GameObject.Find("homeBase").GetComponent<baseGridPosition>())) {
			Destroy (stoneGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), GameObject.Find ("homeBase").GetComponent<baseGridPosition> ());
		stoneGather.pathToBase = pathfindingManager.Instance.GetPath ();
		stoneGather.pathToBase [0] = stoneGather.GetComponent<baseGridPosition> ();
		stoneGather.pathToBase [1].PathFrom = stoneGather.gameObject;

		stoneGather.name = "stoneGathererBuilding";
		stoneGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		stoneGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		stoneGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		stoneGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		stoneGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		stoneGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		stoneGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		stoneGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = stoneGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.stoneGather.buildingCosts);

		return true;
	}

	public bool placingFoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		foodGatherer foodGather = ((GameObject)Instantiate (foodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<foodGatherer> ();

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), GameObject.Find("homeBase").GetComponent<baseGridPosition>())) {
			Destroy (foodGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), GameObject.Find ("homeBase").GetComponent<baseGridPosition> ());
		foodGather.pathToBase = pathfindingManager.Instance.GetPath ();
		foodGather.pathToBase [0] = foodGather.GetComponent<baseGridPosition> ();
		foodGather.pathToBase [1].PathFrom = foodGather.gameObject;

		foodGather.name = "foodGathererBuilding";
		foodGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		foodGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		foodGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		foodGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		foodGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		foodGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		foodGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		foodGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = foodGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.foodGather.buildingCosts);

		return true;
	}

	public bool placingLeanToHouseTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		leanToHouse house = ((GameObject)Instantiate (leanToHousePrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<leanToHouse> ();

		house.name = "leanToHouse";
		house.GetComponent<baseGridPosition> ().mapPosition.X = x;
		house.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		house.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		house.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		house.GetComponent<baseGridPosition> ().Right = adjArray [2];
		house.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		house.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		house.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = house.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.leanToHouse.buildingCosts);

		return true;
	}

	private void GrabBuildingInfoPanel() {
		tileInfoPanel = GameObject.Find ("buildingInfoPanel");
		tileText = GameObject.Find ("titleText").GetComponent<Text> ();
		descriptionText = GameObject.Find ("descriptionText").GetComponent<Text> ();

		tileInfoPanel.SetActive (false);
	}

	private void addBuildingBools() {
		buildingBools = new buildingPlaceMode[125];

		buildingBools[0] = woodGather;
		buildingBools[1] = stoneGather;
		buildingBools[2] = foodGather;
		buildingBools[3] = leanToHouse;

		disablePlacementModes ();
	}

	public void disablePlacementModes() {
		for (int i = 0; i < buildingBools.Length; i++) {
			buildingBools [i].isPlacing = false;
		}
	}

	public bool checkPlacementAt (string buildingName) {
		for (int i = 0; i <= buildingBools.Length; i++) {
			if (buildingBools [i].buildingName.Contains (buildingName)) {
				return buildingBools [i].isPlacing;
			}
		}
		return false;
	}

	public void enablePlacementMode (string buildingName) {
		for (int i = 0; i < buildingBools.Length; i++) {
			if (buildingBools [i].buildingName != null) {
				if (buildingBools [i].buildingName == buildingName) {
					buildingBools [i].isPlacing = true;
					Debug.Log (buildingBools [i].buildingName + "Is Active");
					return;
				}
			}
		}
	}
}
