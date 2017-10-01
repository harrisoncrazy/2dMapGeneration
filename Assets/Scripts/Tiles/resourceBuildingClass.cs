using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceBuildingClass : MonoBehaviour {

	public struct resourceTypeCost {
		public string resourceType;
		public int cost;

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
		public string[] placeableTileTypes;
		public adjBonus[] adjBonusTiles;
		public adjPenalty[] adjPenaltyTiles;

		public resourceBuildingStats(string rType, float e, resourceTypeCost[] c, string[] pTT, adjBonus[] aBT, adjPenalty[] aPT) {
			resourceType = rType;
			efficiency = e;
			costs = c;
			placeableTileTypes = pTT;
			adjBonusTiles = aBT;
			adjPenaltyTiles = aPT;
		}
	}

	public static float readResourceBuildingEfficency(resourceBuildingStats stats, GameObject[] adjTiles) {
		float tempBonusTotal = 0;

		for (int i = 0; stats.adjBonusTiles.Length > i; i++) { //going through each value in the bonus array
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

		float tempPenaltyTotal = 0;

		for (int i = 0; stats.adjPenaltyTiles.Length > i; i++) { //going through each value in the penalty array
			string tempTileType = stats.adjPenaltyTiles [i].tileType;
			float tempBonusSub = stats.adjPenaltyTiles [i].penalty;

			for (int j = 0; adjTiles.Length > j; j++) {//going through each adjacent tile
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

		Debug.Log("Bonus: " + tempBonusTotal + ", Penalty: " + tempPenaltyTotal);

		float totalBonus = tempBonusTotal + tempPenaltyTotal;

		return totalBonus;
	}
}
