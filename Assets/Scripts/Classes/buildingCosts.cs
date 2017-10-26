using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingCosts : MonoBehaviour {

	public static buildingCosts Instance;

	public struct buildingInfo {
		public resourceBuildingClass.resourceTypeCost[] buildingCosts;
		public string buildingDescription;

		public buildingInfo (resourceBuildingClass.resourceTypeCost[] costs, string description) {
			buildingCosts = costs;
			buildingDescription = description;
		}
	}

	//Stone Era
	public buildingInfo woodGather;
	public buildingInfo stoneGather;
	public buildingInfo foodGather;
	public buildingInfo leanToHouse;

	//Bronze Era
	public buildingInfo basicLumberer;

	void Start () {
		Instance = this;

		//STONE ERA
		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		resourceBuildingClass.resourceTypeCost manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] woodGatherBuidlingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 
		woodGather = new buildingInfo (woodGatherBuidlingCost, "Gathers fallen wood and sticks for building material.");


		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] stoneGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		stoneGather = new buildingInfo (stoneGatherBuildingCost, "Gathers easy to manage rocks to fashion into tools to build buildings.");


		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] foodGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		foodGather = new buildingInfo (foodGatherBuildingCost, "Gathers berries and easy to hunt animals.");


		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 15);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 10);
		resourceBuildingClass.resourceTypeCost foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 20);
		resourceBuildingClass.resourceTypeCost[] leanToHouseBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			foodCost
		};
		leanToHouse = new buildingInfo (leanToHouseBuildingCost, "A basic house for your citizens.");


		//BRONZE ERA
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 10);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 2);
		resourceBuildingClass.resourceTypeCost[] basicLumbererBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 
		basicLumberer = new buildingInfo (basicLumbererBuildCost, "Chops down adjacent trees for building material.");
	} 

	public string ReadResourceTotals(resourceBuildingClass.resourceTypeCost[] costs) {
		string costDecription = "Cost: ";

		for (int i = 0; i < costs.Length; i++) {
			if (i != costs.Length-1) { 
				costDecription += costs[i].cost + " " + costs [i].resourceType + ", ";
			} else {
				costDecription += costs [i].cost + " " + costs [i].resourceType + ".";
			}
		}

		return costDecription;
	}
}
