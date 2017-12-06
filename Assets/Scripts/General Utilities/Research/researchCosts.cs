using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchCosts : MonoBehaviour {

	public static researchCosts Instance;

	//Bronze ERA
	public researchBuildingClass.technologyInfo tierOneWood;
	public researchBuildingClass.technologyInfo tierOneStone;
	public researchBuildingClass.technologyInfo tierOneFood;
	public researchBuildingClass.technologyInfo tierOneGatherNode;
	public researchBuildingClass.technologyInfo tierOneHousing;
	public researchBuildingClass.technologyInfo tierOneResearch;
	public researchBuildingClass.technologyInfo tierOneOre;


	public researchBuildingClass.technologyInfo[] enabledResearch = new researchBuildingClass.technologyInfo[125];
	// Use this for initialization
	void Start () {
		Instance = this;

		//BRONZE ERA
		//Tier One Lumber
		string description =  "Advanced strategies in wood cutting will allow you to cut down larger trees. This technology will allow you to clear forests to place down buildings. " +
			"\nUnlocks the Basic Lumberer, which gains additional wood gathering capabilites when placed adjacent to forest.";
		tierOneWood = new researchBuildingClass.technologyInfo("tierOneWood", 50, "Wood Cutting", description, true);

		//Tier One Stone
		description = "Advanced strategies in mining will allow you to break and collect larger stones. This technology will allow you to clear smaller rock tiles to place down buildings. " +
			"\nUnlocks the Basic Quarry, which gains additional stone gathering capabilities when placed adjacent to rocks.";
		tierOneStone = new researchBuildingClass.technologyInfo ("tierOneStone", 50, "Basic Quarries", description, true);

		//Tier One Farm
		description = "With more mouths to feed your people have started to brainstorm ways of bringing in more food reliably. The discovery of several crops that are easy to plant and cultivate has lead to a solution." +
		"\nUnlocks the Basic Farm, which brings in large amounts of food slowly.";
		tierOneFood = new researchBuildingClass.technologyInfo ("tierOneFood", 50, "Farms", description, true);

		//Tier One Gather Node
		description = "More organized leadership has allowed you to coordinate your citizens from a distance. This technology will allow you to gather resources farther away." +
			"\nUnlocks the Gather Point, which will act as a point for your resource gathering buildings to deposit resources.";
		tierOneGatherNode = new researchBuildingClass.technologyInfo ("tierOneGatherNode", 75, "Gather Point", description, false);

		//TierOneHousing
		description = "With the introduction of better wood building materials and the explosion in population due to better food intake your people have figured out a way to build bigger and better houses." +
		"\nUnlocks Wooden Houses, which allows more manpower to be stored, and will bring in additional manpower.";
		tierOneHousing = new researchBuildingClass.technologyInfo ("tierOneHousing", 75, "Wooden Houses", description, false);

		//Tier One Research
		description = "As your civilization becomes more complex it becomes apparent that you need dedicated leaders to run things. Obviously you will need a place for them to live." +
		"\nUnlocks the Chief's Hut, which acts as an improved research building.";
		tierOneResearch = new researchBuildingClass.technologyInfo ("tierOneResearch", 150, "Chief's Hut", description, false);

		//Tier One Ore
		description = "As you dug further into the ground with your new quarries your citizens found a new type of rock. It has become apparent that this rock can be used in some way as an even stronger building material, when refined... somehow." +
		"\nUnlocks the basic mine, which will allow you to dig up ore that will need to be refined.";
		tierOneOre = new researchBuildingClass.technologyInfo ("tierOneOre", 100, "Basic Mine", description, false);

		setArray ();
	}

	void Update() {
		//checking TierOneHousing prerequisites
		if (tierOneHousing.hasBeenPurchased == false && tierOneHousing.isAvailbile == false) {
			if (tierOneWood.hasBeenPurchased == true && tierOneFood.hasBeenPurchased == true) {
				tierOneHousing.isAvailbile = true;
				setArray ();
			}
		}

		//checking tierOneResearch prerequisites
		if (tierOneResearch.hasBeenPurchased == false && tierOneResearch.isAvailbile == false) {
			if (tierOneHousing.hasBeenPurchased == true) {
				tierOneResearch.isAvailbile = true;
				setArray ();
			}
		}

		//checking tierOneMining prerequisites
		if (tierOneOre.hasBeenPurchased == false && tierOneOre.isAvailbile == false) {
			if (tierOneStone.hasBeenPurchased == true) {
				tierOneOre.isAvailbile = true;
				setArray ();
			}
		}
	}

	public void tryResearch(string researchName, float cost) {
		switch (researchName) {
		case "tierOneWood":
			if (resourceManager.Instance.purchaseResearch (tierOneWood.techCost)) {
				researchHandler.Instance.tierOneLumber ();
				tierOneWood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneWood.techDescription);

				setArray ();
			}
			break;
		case "tierOneStone":
			if (resourceManager.Instance.purchaseResearch (tierOneStone.techCost)) {
				researchHandler.Instance.tierOneStone ();
				tierOneStone.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneStone.techDescription);

				setArray ();
			}
			break;
		case "tierOneFood":
			if (resourceManager.Instance.purchaseResearch (tierOneFood.techCost)) {
				researchHandler.Instance.tierOneFood ();
				tierOneFood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneFood.techDescription);

				setArray ();   
			}
			break;
		case "tierOneHousing":
			if (resourceManager.Instance.purchaseResearch (tierOneHousing.techCost)) {
				researchHandler.Instance.tierOneHousing ();
				tierOneHousing.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneHousing.techDescription);

				setArray ();
			}
			break;
		case "tierOneResearch":
			if (resourceManager.Instance.purchaseResearch (tierOneResearch.techCost)) {
				researchHandler.Instance.tierOneResearch ();
				tierOneResearch.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneResearch.techDescription);

				setArray ();
			}
			break;
		case "tierOneOre":
			if (resourceManager.Instance.purchaseResearch (tierOneOre.techCost)) {
				researchHandler.Instance.tierOneOre ();
				tierOneOre.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneOre.techDescription);

				setArray ();
			}
			break;
		case "tierOneGatherNode":
			if (resourceManager.Instance.purchaseResearch (tierOneGatherNode.techCost)) {
				researchHandler.Instance.tierOneGatherNode ();
				tierOneGatherNode.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneGatherNode.techDescription);

				setArray ();
			}
			break;
		}
	}

	public void setArray() {
		enabledResearch[0] = tierOneWood;
		enabledResearch [1] = tierOneStone;
		enabledResearch [2] = tierOneFood;
		enabledResearch [3] = tierOneHousing;
		enabledResearch [4] = tierOneResearch;
		enabledResearch [5] = tierOneOre;

		//enabledResearch [] = tierOneGatherNode;
	}
}
