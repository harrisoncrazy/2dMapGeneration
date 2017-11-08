using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchBuildingClass {

	public struct technologyInfo {
		public string techName;
		public int techCost;
		public string techDisplayName;
		public string techDescription;
		public bool isAvailbile;
		public bool hasBeenPurchased;

		public technologyInfo(string name, int cost, string displayName, string description, bool isOn) {
			techName = name;
			techCost = cost;
			techDisplayName = displayName;
			techDescription = description;
			isAvailbile = isOn;
			hasBeenPurchased = false;
		}
	}
}
