using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabledBuildingList : MonoBehaviour {

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

	//Stone Era
	public buildingData woodGather;
	public buildingData stoneGather;
	public buildingData foodGather;
	public buildingData leanToHouse;

	//Bronze Era
	public buildingData basicLumberer;

	public buildingData[] availableBuildings = new buildingData[125];

	// Use this for initialization
	void Start () {
		Instance = this;

		StartCoroutine ("delayStart");//delaying start so building costs inits first
	}

	IEnumerator delayStart() {
		yield return new WaitForSeconds (1.0f);

		//Stone Era
		string[] tempPlaceTiles = new string[] { "Grassland" };
		woodGather = new buildingData (true, "Wood Gatherer", buildingCosts.Instance.woodGather, "woodGather", tempPlaceTiles);
		stoneGather = new buildingData (true, "Stone Gatherer", buildingCosts.Instance.stoneGather, "stoneGather", tempPlaceTiles);
		foodGather = new buildingData (true, "Food Gatherer", buildingCosts.Instance.foodGather, "foodGather", tempPlaceTiles);
		leanToHouse = new buildingData (true, "Lean To", buildingCosts.Instance.leanToHouse, "leanToHouse", tempPlaceTiles);

		//Bronze Era
		basicLumberer = new buildingData(true, "Basic Lumberer", buildingCosts.Instance.basicLumberer, "basicLumberer", tempPlaceTiles);

		availableBuildings [0] = woodGather;
		availableBuildings [1] = stoneGather;
		availableBuildings [2] = foodGather;
		availableBuildings [3] = leanToHouse;
		availableBuildings [4] = basicLumberer;

		//scrollMenuControl.Instance.ReadActiveBuildings ();
	}
}
