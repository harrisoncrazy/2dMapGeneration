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

	public GameObject currentHoveredTile;
	public GameObject spawnedBuildingPrefab;

	//building stuff
	//STONE AGE
	public GameObject woodGatherPrefab;
	public GameObject stoneGatherPrefab;
	public GameObject foodGatherPrefab;
	public GameObject leanToHousePrefab;
	public GameObject wiseWomanHutPrefab;

	//BRONZE AGE
	public GameObject basicLumbererPrefab;

	//building bools
	public struct buildingPlaceMode
	{
		public string buildingName;
		public GameObject buildingPrefab;
		public bool isPlacing;

		public buildingPlaceMode(string name, GameObject prefab) {
			buildingName = name;
			buildingPrefab = prefab;
			isPlacing = false;
		}
	}

	//placement bools
	//STONE AGE
	public buildingPlaceMode woodGather;
	public buildingPlaceMode stoneGather;
	public buildingPlaceMode foodGather;
	public buildingPlaceMode leanToHouse;
	public buildingPlaceMode wiseWomanHut;

	//BRONZE AGE
	public buildingPlaceMode basicLumberer;

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

		//STONE AGE
		woodGather = new buildingPlaceMode ("woodGather", woodGatherPrefab);
		stoneGather= new buildingPlaceMode ("stoneGather", stoneGatherPrefab);
		foodGather = new buildingPlaceMode ("foodGather", foodGatherPrefab);
		leanToHouse = new buildingPlaceMode ("leanToHouse", leanToHousePrefab);
		wiseWomanHut = new buildingPlaceMode ("wiseWomanHut", wiseWomanHutPrefab);

		//BRONZE AGE
		basicLumberer = new buildingPlaceMode ("basicLumberer", basicLumbererPrefab);

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

		if (isPlacementModeActive == true) {
			updateHoverBuildingPosition ();
		}
	}

	private void updateResourceTotals() {
		//resourceManager.Instance.woodResourceTick ();
		//resourceManager.Instance.stoneResourceTick ();
		//resourceManager.Instance.foodResourceTick ();
		manpowerTickDown--;
		if (manpowerTickDown <= 0) {
			resourceManager.Instance.manpowerResourceTick ();
			resourceManager.Instance.researchResourceTick ();
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
			
		for (int i = 0; i < enabledBuildingList.Instance.woodGather.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.woodGather.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (woodGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
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

		for (int i = 0; i < enabledBuildingList.Instance.stoneGather.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.stoneGather.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (stoneGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
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

		for (int i = 0; i < enabledBuildingList.Instance.foodGather.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.foodGather.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (foodGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
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

		for (int i = 0; i < enabledBuildingList.Instance.leanToHouse.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.leanToHouse.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (house.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

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

	public bool placingWiseWomanHutTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		wiseWomanHut hut = ((GameObject)Instantiate (wiseWomanHutPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<wiseWomanHut> ();

		for (int i = 0; i < enabledBuildingList.Instance.wiseWomanHut.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.wiseWomanHut.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (hut.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		hut.name = "wiseWomanHut";
		hut.GetComponent<baseGridPosition> ().mapPosition.X = x;
		hut.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		hut.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		hut.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		hut.GetComponent<baseGridPosition> ().Right = adjArray [2];
		hut.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		hut.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		hut.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = hut.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.wiseWomanHut.buildingCosts);

		return true;
	}

	public bool placingBasicLumbererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		basicLumberer lumberer = ((GameObject)Instantiate (basicLumbererPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<basicLumberer> ();

		for (int i = 0; i < enabledBuildingList.Instance.basicLumberer.placeableTileTypes.Length; i++) {
			if (enabledBuildingList.Instance.basicLumberer.placeableTileTypes [i] != generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType) {
				Destroy (lumberer.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), GameObject.Find ("homeBase").GetComponent<baseGridPosition> ());
		lumberer.pathToBase = pathfindingManager.Instance.GetPath ();
		lumberer.pathToBase [0] = lumberer.GetComponent<baseGridPosition> ();
		lumberer.pathToBase [1].PathFrom = lumberer.gameObject;

		lumberer.name = "basicLumberer";
		lumberer.GetComponent<baseGridPosition> ().mapPosition.X = x;
		lumberer.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		lumberer.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		lumberer.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		lumberer.GetComponent<baseGridPosition> ().Right = adjArray [2];
		lumberer.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		lumberer.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		lumberer.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = lumberer.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.wiseWomanHut.buildingCosts);

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
		buildingBools [4] = wiseWomanHut;

		buildingBools [5] = basicLumberer;

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
		deleteSpawnedBuildingPrefab ();
		for (int i = 0; i < buildingBools.Length; i++) {
			if (buildingBools [i].buildingName != null) {
				if (buildingBools [i].buildingName == buildingName) {
					buildingBools [i].isPlacing = true;

					defaultBuilding building = ((GameObject)Instantiate (buildingBools[i].buildingPrefab, new Vector3 (), Quaternion.Euler (new Vector3 ()))).GetComponent<defaultBuilding> ();

					building.GetComponent<defaultBuilding> ().readPlaceTiles ();
					building.GetComponent<defaultBuilding> ().isHoverMode = true;
					building.GetComponent<baseGridPosition> ().isHoverMode = true;

					spawnedBuildingPrefab = building.gameObject;

					Debug.Log (buildingBools [i].buildingName + "Is Active");
					return;
				}
			}
		}
	}

	void updateHoverBuildingPosition() {
		if (spawnedBuildingPrefab != null) {
			spawnedBuildingPrefab.transform.position = currentHoveredTile.transform.position;
		}
	}

	public void deleteSpawnedBuildingPrefab() {
		Destroy (spawnedBuildingPrefab);
	}
}
