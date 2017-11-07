using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchBuildingClass {

	public struct technologyInfo {
		public string techName;
		public int techCost;
		public string techDisplayName;
		public string techDescription;
		public bool hasBeenPurchased = false;

		public technologyInfo(string name, int cost, string displayName, string description) {
			techName = name;
			techCost = cost;
			techDisplayName = displayName;
			techDescription = description;
		}
	}
}
