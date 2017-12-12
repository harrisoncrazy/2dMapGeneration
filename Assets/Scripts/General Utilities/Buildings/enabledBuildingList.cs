using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabledBuildingList : MonoBehaviour {

	//TODO comment extensive list of what to do to add a building

	public static enabledBuildingList Instance;

	public struct buildingData {
		public bool isEnabled;
		public string buildingName;//display name for the building
		public string builidingDescription;//description for display
		public resourceBuildingClass.resourceTypeCost[] costTotals;//total cost for description
		public string buildingType;//code name of the building
		public string[] placeableTileTypes;

		public buildingData(bool isOn, string name, buildingCosts.buildingInfo info, string type, string[] ptt) {
			isEnabled = isOn;
			buildingName = name;
			builidingDescription = info.buildingDescription;
			costTotals = info.buildingCosts;
			buildingType = type;
			placeableTileTypes = ptt;
		}

		public string returnCostsAsString() {
			return buildingCosts.Instance.ReadResourceTotals (costTotals);
		}
	}

	//Tile Clearing
	public buildingData forestClear;
	public buildingData stoneClear;

	//Stone Era
	public buildingData woodGather;
	public buildingData stoneGather;
	public buildingData foodGather;
	public buildingData leanToHouse;
	public buildingData wiseWomanHut;

	//Bronze Era
	public buildingData basicLumberer;
	public buildingData basicQuarry;
	public buildingData basicFarm;
	public buildingData woodHouse;
	public buildingData chiefsHut;
	public buildingData basicMine;


	public buildingData gatherNode;

	public buildingData[] availableBuildings = new buildingData[125];

	// Use this for initialization
	void Start () {
		Instance = this;

		StartCoroutine ("delayStart");//delaying start so building costs inits first
	}

	IEnumerator delayStart() {
		yield return new WaitForSeconds (1.0f);

		//Tile Clearing
		string[] tempPlaceTiles = new string[] { "Forest" };
		forestClear = new buildingData(false, "Clear Forest", buildingCosts.Instance.forestClear, "forestClear", tempPlaceTiles);

		tempPlaceTiles = new string[] { "Rocks" };
		stoneClear = new buildingData(false, "Clear Rocks", buildingCosts.Instance.stoneClear, "stoneClear", tempPlaceTiles);

		//Stone Era
		tempPlaceTiles = new string[] { "Grassland", "Default" };
		woodGather = new buildingData (true, "Wood Gatherer", buildingCosts.Instance.woodGather, "woodGather", tempPlaceTiles);
		stoneGather = new buildingData (true, "Stone Gatherer", buildingCosts.Instance.stoneGather, "stoneGather", tempPlaceTiles);
		foodGather = new buildingData (true, "Food Gatherer", buildingCosts.Instance.foodGather, "foodGather", tempPlaceTiles);
		leanToHouse = new buildingData (true, "Lean To", buildingCosts.Instance.leanToHouse, "leanToHouse", tempPlaceTiles);
		wiseWomanHut = new buildingData (true, "Wise Woman's Hut", buildingCosts.Instance.wiseWomanHut, "wiseWomanHut", tempPlaceTiles);

		//Bronze Era
		basicLumberer = new buildingData(false, "Basic Lumberer", buildingCosts.Instance.basicLumberer, "basicLumberer", tempPlaceTiles);
		basicQuarry = new buildingData (false, "Basic Quarry", buildingCosts.Instance.basicQuarry, "basicQuarry", tempPlaceTiles);
		basicFarm = new buildingData (false, "Basic Farm", buildingCosts.Instance.basicFarm, "basicFarm", tempPlaceTiles);
		woodHouse = new buildingData (false, "Wooden House", buildingCosts.Instance.woodHouse, "woodHouse", tempPlaceTiles);
		chiefsHut = new buildingData (false, "Chief's Hut", buildingCosts.Instance.chiefsHut, "chiefsHut", tempPlaceTiles);
		basicMine = new buildingData (false, "Basic Mine", buildingCosts.Instance.basicMine, "basicMine", tempPlaceTiles);

		gatherNode = new buildingData (false, "Basic Gather Point", buildingCosts.Instance.basicGatherNode, "gatherNode", tempPlaceTiles);

		setArray ();

		//scrollMenuControl.Instance.ReadActiveBuildings ();
	}

	public void setArray() {
		availableBuildings [0] = forestClear;
		availableBuildings [1] = stoneClear;

		availableBuildings [2] = woodGather;
		availableBuildings [3] = stoneGather;
		availableBuildings [4] = foodGather;
		availableBuildings [5] = leanToHouse;
		availableBuildings [6] = wiseWomanHut;
		availableBuildings [7] = basicLumberer;
		availableBuildings [8] = basicQuarry;
		availableBuildings [9] = basicFarm;
		availableBuildings [10] = woodHouse;
		availableBuildings [11] = chiefsHut;
		availableBuildings [12] = basicMine;

		//availableBuildings [8] = gatherNode;
	}
}
