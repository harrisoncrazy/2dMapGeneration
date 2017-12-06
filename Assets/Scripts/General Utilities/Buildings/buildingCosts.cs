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
	public buildingInfo wiseWomanHut;

	//Bronze Era
	public buildingInfo basicLumberer;
	public buildingInfo basicQuarry;
	public buildingInfo basicFarm;
	public buildingInfo woodHouse;
	public buildingInfo chiefsHut;
	public buildingInfo basicMine;

	public buildingInfo basicGatherNode;

	void Start () {
		Instance = this;

		//STONE ERA
		//Wood Gatherer
		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		resourceBuildingClass.resourceTypeCost manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] woodGatherBuidlingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 
		woodGather = new buildingInfo (woodGatherBuidlingCost, "Gathers fallen wood and sticks for building material.");

		//Stone Gatherer
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] stoneGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		stoneGather = new buildingInfo (stoneGatherBuildingCost, "Gathers easy to manage rocks to fashion into tools to build buildings.");

		//Food Gatherer
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		resourceBuildingClass.resourceTypeCost[] foodGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		foodGather = new buildingInfo (foodGatherBuildingCost, "Gathers berries and easy to hunt animals.");

		//LeanTo House
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 15);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 10);
		resourceBuildingClass.resourceTypeCost foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 20);
		resourceBuildingClass.resourceTypeCost[] leanToHouseBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			foodCost
		};
		leanToHouse = new buildingInfo (leanToHouseBuildingCost, "A basic house for your citizens.");

		//Wise Woman Hut
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 20);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 5);
		foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 25);
		resourceBuildingClass.resourceTypeCost[] wiseWomanHutBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			foodCost
		};
		wiseWomanHut = new buildingInfo (wiseWomanHutBuildingCost, "A place for the wisest of your population to live.");


		//BRONZE ERA
		//Basic Lumberer
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 10);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 2);
		resourceBuildingClass.resourceTypeCost[] basicLumbererBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 
		basicLumberer = new buildingInfo (basicLumbererBuildCost, "Chops down adjacent trees for building material.");

		//Basic Quarry
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 15);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 10);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 2);
		resourceBuildingClass.resourceTypeCost[] basicQuarryBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 
		basicQuarry = new buildingInfo (basicQuarryBuildCost, "Digs into the ground for stone material.");

		//Basic Farm
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 15);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 5);
		resourceBuildingClass.resourceTypeCost[] basicFarmBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost, 
			stoneCost, 
			manpowerCost
		};
		basicFarm = new buildingInfo (basicFarmBuildCost, "Organized planting of crops.");

		//Wood House
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 30);
		resourceBuildingClass.resourceTypeCost[] woodHouseBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			foodCost
		};
		woodHouse = new buildingInfo (woodHouseBuildCost, "Better housing.");

		//Chief's Hut
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 25);
		foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 40);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 15);
		resourceBuildingClass.resourceTypeCost[] chiefsHutBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			foodCost,
			manpowerCost
		};
		chiefsHut = new buildingInfo (chiefsHutBuildCost, "Better research");

		//Basic Mine
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 25);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 25);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 10);
		resourceBuildingClass.resourceTypeCost[] basicMineBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		basicMine = new buildingInfo (basicMineBuildCost, "First Gathering of Minerals");

		//Basic GatherNode
		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 30);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 20);
		resourceBuildingClass.resourceTypeCost[] basicGatherNodeBuildCost = new resourceBuildingClass.resourceTypeCost[] { 
			woodCost, 
			stoneCost, 
			manpowerCost
		};
		basicGatherNode = new buildingInfo (basicGatherNodeBuildCost, "Localized Gathering Point for resources.");
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
