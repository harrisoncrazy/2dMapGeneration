using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabledBuildingList : MonoBehaviour {

	//TODO comment extensive list of what to do to add a building
	//Make Building cost: buildingCosts.cs
	//Add reference here: add building data varable, make constructor, and add it to availableBuildings array
	//Make building specific script
	//Make placement function in gamemanager
	//Make placement call in tile handler onMouseDown

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
	public buildingData basicBlacksmith;

	//Medieval Era
	public buildingData sawmill;
	public buildingData advancedQuarry;
	public buildingData advancedFarm;
	public buildingData stoneHouse;
	public buildingData castle;
	public buildingData advancedMine;

	//Renaissance Era
	public buildingData shaftMine;
	public buildingData smeltery;
	public buildingData forestManager;//bonus wood building that provides large bonuses to adjacent wood cutting buildings
	public buildingData explosiveQuarry;
	public buildingData waterReservoir;//bonus food building that provides large bonuses to adjacent farms
	public buildingData multiHouse;
	public buildingData guildHouse;
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
		basicBlacksmith = new buildingData (false, "Basic Blacksmith", buildingCosts.Instance.basicBlacksmith, "basicBlacksmith", tempPlaceTiles);

		//Medieval Era
		sawmill = new buildingData(false, "Sawmill", buildingCosts.Instance.sawmill, "sawmill", tempPlaceTiles);
		advancedQuarry = new buildingData (false, "Advanced Quarry", buildingCosts.Instance.advancedQuarry, "advancedQuarry", tempPlaceTiles);
		advancedFarm = new buildingData (false, "Advanced Farm", buildingCosts.Instance.advancedFarm, "advancedFarm", tempPlaceTiles);
		stoneHouse = new buildingData (false, "Stone House", buildingCosts.Instance.stoneHouse, "stoneHouse", tempPlaceTiles);
		castle = new buildingData (false, "Castle", buildingCosts.Instance.castle, "castle", tempPlaceTiles);
		advancedMine = new buildingData (false, "Advanced Mine", buildingCosts.Instance.advancedMine, "advancedMine", tempPlaceTiles);

		//Renaissance Era
		shaftMine = new buildingData(false, "Shaft Mine", buildingCosts.Instance.shaftMine, "shaftMine", tempPlaceTiles);
		smeltery = new buildingData (false, "Smeltery", buildingCosts.Instance.smeltery, "smeltery", tempPlaceTiles);
		forestManager = new buildingData (false, "Forest Manager", buildingCosts.Instance.forestManager, "forestManager", tempPlaceTiles);
		explosiveQuarry = new buildingData (false, "Explosive Quarry", buildingCosts.Instance.explosiveQuarry, "explosiveQuarry", tempPlaceTiles);
		waterReservoir = new buildingData (false, "Water Reservoir", buildingCosts.Instance.waterReservoir, "waterReservoir", tempPlaceTiles);
		multiHouse = new buildingData (false, "Multi-Story House", buildingCosts.Instance.multiHouse, "multiHouse", tempPlaceTiles);
		guildHouse = new buildingData (false, "Guild House", buildingCosts.Instance.guildHouse, "guildHouse", tempPlaceTiles);
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
		availableBuildings [13] = basicBlacksmith;

		availableBuildings [14] = sawmill;
		availableBuildings [15] = advancedQuarry;
		availableBuildings [16] = advancedFarm;
		availableBuildings [17] = stoneHouse;
		availableBuildings [18] = castle;
		availableBuildings [19] = advancedMine;

		availableBuildings [20] = shaftMine;
		availableBuildings [21] = smeltery;
		availableBuildings [22] = forestManager;
		availableBuildings [23] = explosiveQuarry;
		availableBuildings [24] = waterReservoir;
		availableBuildings [25] = multiHouse;
		availableBuildings [26] = guildHouse;
		availableBuildings [27] = gatherNode;
	}
}
