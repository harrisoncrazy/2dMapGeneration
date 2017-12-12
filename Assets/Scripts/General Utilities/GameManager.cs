using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	//TODO maybe move building construction functions to seperate script?
	//TODO Comment script
	public static GameManager Instance;

	//pathfinding values
	public baseGridPosition previousCell, searchFromCell;

	public Transform selectedTile;


	public bool isPlacementModeActive = false;
	public bool isBuildingSelected = false;

	public bool isBuildingPanelActive = false;
	public bool isResearchPanelActive = false;

	public GameObject currentHoveredTile;
	public GameObject spawnedBuildingPrefab;

	//Default grassland prefab for clearing
	public GameObject defaultClearedForest;
	public GameObject defaultClearedStone;

	//building stuff
	//STONE ERA
	public GameObject woodGatherPrefab;
	public GameObject stoneGatherPrefab;
	public GameObject foodGatherPrefab;
	public GameObject leanToHousePrefab;
	public GameObject wiseWomanHutPrefab;

	//BRONZE ERA
	public GameObject basicLumbererPrefab;
	public GameObject basicQuarryPrefab;
	public GameObject basicFarmPrefab;
	public GameObject woodHousePrefab;
	public GameObject chiefsHutPrefab;
	public GameObject basicMinePrefab;

	//Medieval Era
	public GameObject basicBlacksmithPrefab;

	public GameObject gatherNode;

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
	//TILE CLEARING
	public buildingPlaceMode forestClear;
	public buildingPlaceMode stoneClear;

	//Stone Era
	public buildingPlaceMode woodGather;
	public buildingPlaceMode stoneGather;
	public buildingPlaceMode foodGather;
	public buildingPlaceMode leanToHouse;
	public buildingPlaceMode wiseWomanHut;

	//Bronze Era
	public buildingPlaceMode basicLumberer;
	public buildingPlaceMode basicQuarry;
	public buildingPlaceMode basicFarm;
	public buildingPlaceMode woodHouse;
	public buildingPlaceMode chiefsHut;
	public buildingPlaceMode basicMine;

	//Medieval Era
	public buildingPlaceMode basicBlacksmith;

	public buildingPlaceMode gatherNodeBasic;

	public buildingPlaceMode[] buildingBools;

	//pop up panel values
	public GameObject tileInfoPanel;
	public Text tileText;
	public Text descriptionText;

	private float timerTick = 1.0f;

	private int manpowerTickDown = 5;//delaying manpower add

	public Button upgradeButton;

	// Use this for initialization
	void Start () {
		Instance = this;

		//TILE CLEARING
		forestClear = new buildingPlaceMode("forestClear", defaultClearedForest);
		stoneClear = new buildingPlaceMode("stoneClear", defaultClearedStone);

		//Stone Era
		woodGather = new buildingPlaceMode ("woodGather", woodGatherPrefab);
		stoneGather = new buildingPlaceMode ("stoneGather", stoneGatherPrefab);
		foodGather = new buildingPlaceMode ("foodGather", foodGatherPrefab);
		leanToHouse = new buildingPlaceMode ("leanToHouse", leanToHousePrefab);
		wiseWomanHut = new buildingPlaceMode ("wiseWomanHut", wiseWomanHutPrefab);

		//Bronze Era
		basicLumberer = new buildingPlaceMode ("basicLumberer", basicLumbererPrefab);
		basicQuarry = new buildingPlaceMode ("basicQuarry", basicQuarryPrefab);
		basicFarm = new buildingPlaceMode ("basicFarm", basicFarmPrefab);
		woodHouse = new buildingPlaceMode ("woodHouse", woodHousePrefab);
		chiefsHut = new buildingPlaceMode ("chiefsHut", chiefsHutPrefab);
		basicMine = new buildingPlaceMode ("basicMine", basicMinePrefab);

		//Medieval Era
		basicBlacksmith = new buildingPlaceMode("basicBlacksmtih", basicBlacksmithPrefab);

		gatherNodeBasic = new buildingPlaceMode ("gatherNode", gatherNode);

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
		manpowerTickDown--;
		if (manpowerTickDown <= 0) {
			resourceManager.Instance.manpowerResourceTick ();
			resourceManager.Instance.researchResourceTick ();
			manpowerTickDown = 5;
		}
	}

	public bool clearingForestTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		defaultBuilding forestClear = ((GameObject)Instantiate (defaultClearedForest, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<defaultBuilding> ();//TODO add way of changing to different element default tiles

		for (int i = 0; i < enabledBuildingList.Instance.forestClear.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.forestClear.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.forestClear.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (forestClear.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}
			
		forestClear.GetComponent<tileHandler> ().tileType = "Grassland";

		forestClear.name = "Tile X:" + x + " Y:" + y;

		forestClear.GetComponent<baseGridPosition> ().mapPosition.X = x;
		forestClear.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		forestClear.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		forestClear.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		forestClear.GetComponent<baseGridPosition> ().Right = adjArray [2];
		forestClear.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		forestClear.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		forestClear.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = forestClear.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.forestClear.buildingCosts);

		return true;
	}

	public bool clearingStoneTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		defaultBuilding stoneClear = ((GameObject)Instantiate (defaultClearedStone, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<defaultBuilding> ();//TODO add way of changing to different element default tiles

		for (int i = 0; i < enabledBuildingList.Instance.stoneClear.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.stoneClear.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.stoneClear.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (stoneClear.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		stoneClear.GetComponent<tileHandler> ().tileType = "Grassland";

		stoneClear.name = "Tile X:" + x + " Y:" + y;

		stoneClear.GetComponent<baseGridPosition> ().mapPosition.X = x;
		stoneClear.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		stoneClear.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		stoneClear.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		stoneClear.GetComponent<baseGridPosition> ().Right = adjArray [2];
		stoneClear.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		stoneClear.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		stoneClear.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = stoneClear.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.stoneClear.buildingCosts);

		return true;
	}

	public bool placingWoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		woodGatherer woodGather = ((GameObject)Instantiate (woodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<woodGatherer> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", woodGather.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (woodGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}
			
		for (int i = 0; i < enabledBuildingList.Instance.woodGather.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.woodGather.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.woodGather.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (woodGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}


		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
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

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", stoneGather.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (stoneGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.stoneGather.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.stoneGather.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.stoneGather.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (stoneGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
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

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", foodGather.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (foodGather.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.foodGather.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.foodGather.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.foodGather.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (foodGather.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}


		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
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
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.leanToHouse.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.leanToHouse.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
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
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.wiseWomanHut.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.wiseWomanHut.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
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

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", lumberer.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (lumberer.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.basicLumberer.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.basicLumberer.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.basicLumberer.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (lumberer.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
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

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicLumberer.buildingCosts);

		return true;
	}

	public bool placingBasicQuarryTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		basicQuarry quarry = ((GameObject)Instantiate (basicQuarryPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<basicQuarry> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", quarry.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (quarry.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.basicQuarry.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.basicQuarry.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.basicQuarry.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (quarry.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
		quarry.pathToBase = pathfindingManager.Instance.GetPath ();
		quarry.pathToBase [0] = quarry.GetComponent<baseGridPosition> ();
		quarry.pathToBase [1].PathFrom = quarry.gameObject;

		quarry.name = "basicLumberer";
		quarry.GetComponent<baseGridPosition> ().mapPosition.X = x;
		quarry.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		quarry.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		quarry.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		quarry.GetComponent<baseGridPosition> ().Right = adjArray [2];
		quarry.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		quarry.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		quarry.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = quarry.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicQuarry.buildingCosts);

		return true;
	}

	public bool placingBasicFarmTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		basicFarm farm = ((GameObject)Instantiate (basicFarmPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<basicFarm> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", farm.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (farm.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.basicFarm.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.basicFarm.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.basicFarm.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (farm.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
		farm.pathToBase = pathfindingManager.Instance.GetPath ();
		farm.pathToBase [0] = farm.GetComponent<baseGridPosition> ();
		farm.pathToBase [1].PathFrom = farm.gameObject;

		farm.name = "basicLumberer";
		farm.GetComponent<baseGridPosition> ().mapPosition.X = x;
		farm.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		farm.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		farm.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		farm.GetComponent<baseGridPosition> ().Right = adjArray [2];
		farm.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		farm.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		farm.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = farm.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicFarm.buildingCosts);

		return true;
	}

	public bool placingWoodHouseTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		woodHouse house = ((GameObject)Instantiate (woodHousePrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<woodHouse> ();

		for (int i = 0; i < enabledBuildingList.Instance.woodHouse.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.woodHouse.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.woodHouse.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (house.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		house.name = "woodHouse";
		house.GetComponent<baseGridPosition> ().mapPosition.X = x;
		house.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		house.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		house.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		house.GetComponent<baseGridPosition> ().Right = adjArray [2];
		house.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		house.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		house.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = house.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.woodHouse.buildingCosts);

		return true;
	}

	public bool placingChiefsHutTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		chiefsHut house = ((GameObject)Instantiate (chiefsHutPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<chiefsHut> ();

		for (int i = 0; i < enabledBuildingList.Instance.chiefsHut.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.chiefsHut.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.chiefsHut.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (house.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		house.name = "chiefsHut";
		house.GetComponent<baseGridPosition> ().mapPosition.X = x;
		house.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		house.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		house.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		house.GetComponent<baseGridPosition> ().Right = adjArray [2];
		house.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		house.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		house.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = house.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.chiefsHut.buildingCosts);

		return true;
	}

	public bool placingBasicMineTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		basicMine mine = ((GameObject)Instantiate (basicMinePrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<basicMine> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", mine.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (mine.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.basicMine.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.basicMine.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.basicMine.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (mine.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
		mine.pathToBase = pathfindingManager.Instance.GetPath ();
		mine.pathToBase [0] = mine.GetComponent<baseGridPosition> ();
		mine.pathToBase [1].PathFrom = mine.gameObject;

		mine.name = "basicMine";
		mine.GetComponent<baseGridPosition> ().mapPosition.X = x;
		mine.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		mine.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		mine.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		mine.GetComponent<baseGridPosition> ().Right = adjArray [2];
		mine.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		mine.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		mine.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = mine.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicMine.buildingCosts);

		return true;
	}

	public bool placingBasicBlacksmithTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		basicBlacksmith blacksmith = ((GameObject)Instantiate (basicBlacksmithPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<basicBlacksmith> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", blacksmith.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (blacksmith.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.basicBlacksmith.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.basicBlacksmith.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.basicBlacksmith.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (blacksmith.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
		blacksmith.pathToBase = pathfindingManager.Instance.GetPath ();
		blacksmith.pathToBase [0] = blacksmith.GetComponent<baseGridPosition> ();
		blacksmith.pathToBase [1].PathFrom = blacksmith.gameObject;

		blacksmith.name = "basicBlacksmith";
		blacksmith.GetComponent<baseGridPosition> ().mapPosition.X = x;
		blacksmith.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		blacksmith.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		blacksmith.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		blacksmith.GetComponent<baseGridPosition> ().Right = adjArray [2];
		blacksmith.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		blacksmith.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		blacksmith.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = blacksmith.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicBlacksmith.buildingCosts);

		return true;
	}

	public bool placingGatherNodeBasicTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		gatherNode node = ((GameObject)Instantiate (gatherNode, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<gatherNode> ();

		GameObject pathObject = FindClosest.findClosestGameobjectWithTag ("HomeTile", node.gameObject.transform.position);

		if (!pathfindingManager.Instance.Search(generationManager.Instance.map[x][y].GetComponent<baseGridPosition>(), pathObject.GetComponent<baseGridPosition>())) {
			Destroy (node.gameObject);
			Debug.Log ("No path to home base");
			return false;
		}

		for (int i = 0; i < enabledBuildingList.Instance.gatherNode.placeableTileTypes.Length; i++) {
			bool contains = generationManager.Instance.map [x] [y].GetComponent<tileHandler> ().tileType.Contains (enabledBuildingList.Instance.gatherNode.placeableTileTypes [i]);
			if (contains == true) {
				break;
			} else if (contains == false && i >= enabledBuildingList.Instance.gatherNode.placeableTileTypes.Length) { //only breaking out if at the end of the for loop, after checking the whole array
				Destroy (gatherNode.gameObject);
				Debug.Log ("Invalid Tile Type");
				return false;
			}
		}

		pathfindingManager.Instance.FindPath (generationManager.Instance.map [x] [y].GetComponent<baseGridPosition> (), pathObject.GetComponent<baseGridPosition> ());
		node.pathToBase = pathfindingManager.Instance.GetPath ();
		node.pathToBase [0] = node.GetComponent<baseGridPosition> ();
		node.pathToBase [1].PathFrom = node.gameObject;

		node.name = "gatherNode";
		node.GetComponent<baseGridPosition> ().mapPosition.X = x;
		node.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		node.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		node.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		node.GetComponent<baseGridPosition> ().Right = adjArray [2];
		node.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		node.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		node.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = node.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.basicGatherNode.buildingCosts);

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

		buildingBools [0] = forestClear;
		buildingBools [1] = stoneClear;

		buildingBools[2] = woodGather;
		buildingBools[3] = stoneGather;
		buildingBools[4] = foodGather;
		buildingBools[5] = leanToHouse;
		buildingBools [6] = wiseWomanHut;

		buildingBools [7] = basicLumberer;
		buildingBools [8] = basicQuarry;
		buildingBools [9] = basicFarm;
		buildingBools [10] = woodHouse;
		buildingBools [11] = chiefsHut;
		buildingBools [12] = basicMine;

		buildingBools [13] = basicBlacksmith;

		//buildingBools [8] = gatherNodeBasic;

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

					building.readPlaceTiles ();
					building.isHoverMode = true;
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
