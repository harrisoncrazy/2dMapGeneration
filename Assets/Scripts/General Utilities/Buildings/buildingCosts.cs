using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingCosts : MonoBehaviour {

	//TODO FORMATTING

	public static buildingCosts Instance;

	public struct buildingInfo {
		public resourceBuildingClass.resourceTypeCost[] buildingCosts;
		public string buildingDescription;

		public buildingInfo (resourceBuildingClass.resourceTypeCost[] costs, string description) {
			buildingCosts = costs;
			buildingDescription = description;
		}
	}

	//Tile Clearing
	public buildingInfo forestClear;
	public buildingInfo stoneClear;

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
	public buildingInfo basicBlacksmith;

	//Medieval Era
	public buildingInfo sawmill;
	public buildingInfo advancedQuarry;
	public buildingInfo advancedFarm;
	public buildingInfo stoneHouse;
	public buildingInfo castle;
	public buildingInfo advancedMine;

	//Renaissance Era
	public buildingInfo shaftMine;
	public buildingInfo smeltery;
	public buildingInfo forestManager;//bonus wood building that provides large bonuses to adjacent wood cutting buildings
	public buildingInfo explosiveQuarry;
	public buildingInfo waterReservoir;//bonus food building that provides large bonuses to adjacent farms
	public buildingInfo multiHouse;
	public buildingInfo guildHouse;

	public buildingInfo basicGatherNode;

	void Start () {
		Instance = this;

		//TILE CLEARING
		//Forest Clearing
		resourceBuildingClass.resourceTypeCost manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 5);
		resourceBuildingClass.resourceTypeCost foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 10);
		resourceBuildingClass.resourceTypeCost[] forestClearUseCost = new resourceBuildingClass.resourceTypeCost[] {
			manpowerCost,
			foodCost
		};
		forestClear = new buildingInfo (forestClearUseCost, "Clears out the forest on a tile.");

		//Stone Clearing
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 10);
		foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 15);
		resourceBuildingClass.resourceTypeCost[] stoneClearUseCost = new resourceBuildingClass.resourceTypeCost[] {
			manpowerCost,
			foodCost
		};
		stoneClear = new buildingInfo (stoneClearUseCost, "Clears out the rocks on a tile.");


		//STONE ERA
		//Wood Gatherer
		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
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
		foodCost = new resourceBuildingClass.resourceTypeCost ("Food", 20);
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
			foodCost,
			manpowerCost
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
		basicMine = new buildingInfo (basicMineBuildCost, "First Gatherer of Ore");

		//Basic Blacksmith
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 25);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 25);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 5);
		resourceBuildingClass.resourceTypeCost[] basicBlacksmithBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		basicBlacksmith = new buildingInfo (basicBlacksmithBuildCost, "Refinement of Ore");

		//Medieval Era
		//Sawmill
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 25);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 25);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 10);
		resourceBuildingClass.resourceTypeCost metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 20);
		resourceBuildingClass.resourceTypeCost[] sawmillBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		sawmill = new buildingInfo (sawmillBuildCost, "Fast handling of wood");

		//AdvancedQuarry
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 20);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 10);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 15);
		resourceBuildingClass.resourceTypeCost[] advancedQuarryBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		advancedQuarry = new buildingInfo (advancedQuarryBuildCost, "Expanded Quarries");

		//AdvancedFarm
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 20);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 20);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 15);
		resourceBuildingClass.resourceTypeCost[] advancedFarmBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		advancedFarm = new buildingInfo (advancedFarmBuildCost, "Better Farming techniques");

		//StoneHouse
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 20);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 40);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 20);
		resourceBuildingClass.resourceTypeCost[] stoneHouseBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		stoneHouse = new buildingInfo (stoneHouseBuildCost, "Stone Houses");

		//Castle
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 40);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 50);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 25);
		resourceBuildingClass.resourceTypeCost[] castleBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		castle = new buildingInfo (castleBuildCost, "Grand housing for leaders");

		//AdvancedMine
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 35);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 15);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 15);
		resourceBuildingClass.resourceTypeCost[] advancedMineBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		advancedMine = new buildingInfo (advancedMineBuildCost, "Advanced Ores");

		//Renaissance Era
		//ShaftMine
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 50);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 40);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 20);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 35);
		resourceBuildingClass.resourceTypeCost[] shaftMineBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		shaftMine = new buildingInfo (shaftMineBuildCost, "Deep Mines");

		//Smeltery
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 50);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 35);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 15);
		resourceBuildingClass.resourceTypeCost[] smelteryBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
		smeltery = new buildingInfo (smelteryBuildCost, "Mass Refinement of Ore");

		//ForestManager
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 75);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 75);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 40);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 40);
		resourceBuildingClass.resourceTypeCost[] forestManagerBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		forestManager = new buildingInfo (forestManagerBuildCost, "Managing of wood lots, bonus to adjacent wood Gatherers");

		//explosiveQuarry
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 40);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 40);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 20);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 30);
		resourceBuildingClass.resourceTypeCost[] explosiveQuarryBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		explosiveQuarry = new buildingInfo (explosiveQuarryBuildCost, "Making Quarries with explosives!");
		
		//waterReservoir
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 75);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 75);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 40);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 40);
		resourceBuildingClass.resourceTypeCost[] waterReservoirBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		waterReservoir = new buildingInfo (waterReservoirBuildCost, "A source of water for farms, bonus to adjacent farms.");

		//multiHouse
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 40);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 40);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 30);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 30);
		resourceBuildingClass.resourceTypeCost[] multiHouseBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		multiHouse = new buildingInfo (multiHouseBuildCost, "Multi-Story Housing");

		//guildHouse
		woodCost = new resourceBuildingClass.resourceTypeCost("Wood", 75);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 75);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 40);
		metalCost = new resourceBuildingClass.resourceTypeCost ("Metal", 50);
		resourceBuildingClass.resourceTypeCost[] guildHouseBuildCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost,
			metalCost
		};
		guildHouse = new buildingInfo (guildHouseBuildCost, "Organized institutions for your smartest");

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
