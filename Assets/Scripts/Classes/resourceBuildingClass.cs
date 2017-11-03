using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceBuildingClass : MonoBehaviour {

	public struct resourceTypeCost {
		public string resourceType;
		public float cost;

		public resourceTypeCost (string t, int c) {
			resourceType = t;
			cost = c;
		}
	}

	public struct adjBonus {
		public string tileType;
		public float bonus;

		public adjBonus(string t, float b) {
			tileType = t;
			bonus = b;
		}
	}

	public struct adjPenalty {
		public string tileType;
		public float penalty;

		public adjPenalty(string t, float p) {
			tileType = t;
			penalty = p;
		}
	}

	public struct resourceBuildingStats {
		public string resourceType;
		public float efficiency;
		public resourceTypeCost[] costs;
		//public string[] placeableTileTypes;
		public adjBonus[] adjBonusTiles;
		public adjPenalty[] adjPenaltyTiles;

		public resourceBuildingStats(string rType, float e, resourceTypeCost[] c, adjBonus[] aBT, adjPenalty[] aPT) {
			resourceType = rType;
			efficiency = e;
			costs = c;
			//placeableTileTypes = pTT;
			adjBonusTiles = aBT;
			adjPenaltyTiles = aPT;
		}
	}

	public static float readResourceBuildingEfficency(resourceBuildingStats stats, GameObject[] adjTiles) {
		float tempBonusTotal = 0;

		for (int i = 0; stats.adjBonusTiles.Length > i; i++) { //going through each value in the bonus array
			if (adjTiles [i].gameObject != null) {
				string tempTileType = stats.adjBonusTiles [i].tileType;
				float tempBonusAdd = stats.adjBonusTiles [i].bonus;


				for (int j = 0; adjTiles.Length > j; j++) {//going through each adjacent tile
					if (adjTiles [j].GetComponent<tileHandler> () != null) { //if a default tile with no building
						if (adjTiles [j].GetComponent<tileHandler> ().tileType.Contains (tempTileType)) {
							tempBonusTotal += tempBonusAdd;
						}
					} else { //if a tile with a building
						if (adjTiles [j].name.Contains (tempTileType)) {
							tempBonusTotal += tempBonusAdd;
						}
					}
				}
			}
		}

		float tempPenaltyTotal = 0;

		for (int i = 0; stats.adjPenaltyTiles.Length > i; i++) { //going through each value in the penalty array
			string tempTileType = stats.adjPenaltyTiles [i].tileType;
			float tempBonusSub = stats.adjPenaltyTiles [i].penalty;

			for (int j = 0; adjTiles.Length > j; j++) {//going through each adjacent tile
				if (adjTiles [j].gameObject != null) {
					if (adjTiles [j].GetComponent<tileHandler> () != null) { //if a default tile with no building
						if (adjTiles [j].GetComponent<tileHandler> ().tileType.Contains (tempTileType)) {
							tempPenaltyTotal -= tempBonusSub;
						}
					} else { //if a tile with a building
						if (adjTiles [j].name.Contains (tempTileType)) {
							tempPenaltyTotal -= tempBonusSub;
						}
					}
				}
			}
		}

		//Debug.Log("Bonus: " + tempBonusTotal + ", Penalty: " + tempPenaltyTotal);

		float totalBonus = tempBonusTotal + tempPenaltyTotal;

		return totalBonus;
	}

	public static bool readResourcesForPlacingBuilding (resourceTypeCost[] costs) {
		int totalOkay = 0;//total for adding up if all resource types are good
		//Debug.Log (costs.Length);

		for (int i = 0; costs.Length > i; i++) { //going through all the resource type requirements
			string tempCostType = costs [i].resourceType;
			float tempCost = costs [i].cost;

			switch (tempCostType) {//checking if there is enough of the resource type
			case "Wood":
				if (tempCost <= resourceManager.Instance.returnTotalWood ()) {
					totalOkay++;
				}
				break;
			case "Stone":
				if (tempCost <= resourceManager.Instance.returnTotalStone ()) {
					totalOkay++;
				}
				break;
			case "Food":
				if (tempCost <= resourceManager.Instance.returnTotalFood ()) {
					totalOkay++;
				}
				break;
			case "Manpower":
				if (tempCost <= resourceManager.Instance.returnTotalManpower ()) {
					totalOkay++;
				}
				break;
			};
		}

		//Debug.Log (totalOkay);

		if (totalOkay >= costs.Length) {
			return true;
		} else {
			return false;
		}
	}

	public static void removeResourcesFromPlacement (resourceTypeCost[] costs) { //removing resource from the total stockpiles
		for (int i = 0; costs.Length > i; i++) {
			string tempCostType = costs [i].resourceType;
			float tempCost = costs [i].cost;

			if (tempCostType == "Wood") {
				resourceManager.Instance.removeWood (tempCost);
			} else if (tempCostType == "Stone") {
				resourceManager.Instance.removeStone (tempCost);
			} else if (tempCostType == "Food") {
				resourceManager.Instance.removeFood (tempCost);
			} else if (tempCostType == "Manpower") {
				resourceManager.Instance.removeManpower (tempCost);
			}
		}
	}
	/*
	public static bool checkPlacementType (resourceBuildingStats stats, string tileType) {
		for (int i = 0; stats.placeableTileTypes.Length > i; i++) {
			if (stats.placeableTileTypes [i].Contains (tileType)) {
				return true;
			}
		}
		return false;
	}*/
}
