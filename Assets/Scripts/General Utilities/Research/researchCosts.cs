using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchCosts : MonoBehaviour {

	public static researchCosts Instance;

	//Bronze ERA
	public researchBuildingClass.technologyInfo tierOneWood;
	public researchBuildingClass.technologyInfo tierOneStone;
	public researchBuildingClass.technologyInfo tierOneFood;
	public researchBuildingClass.technologyInfo tierOneHousing;
	public researchBuildingClass.technologyInfo tierOneResearch;
	public researchBuildingClass.technologyInfo tierOneOre;

	//Medieval Era
	public researchBuildingClass.technologyInfo tierTwoOre;
	public researchBuildingClass.technologyInfo tierTwoWood;
	public researchBuildingClass.technologyInfo tierTwoStone;
	public researchBuildingClass.technologyInfo tierTwoFood;
	public researchBuildingClass.technologyInfo tierTwoHousing;
	public researchBuildingClass.technologyInfo tierTwoResearch;

	//Renaissance Era
	public researchBuildingClass.technologyInfo tierThreeOre;
	public researchBuildingClass.technologyInfo tierOneRefinement;
	public researchBuildingClass.technologyInfo tierThreeWood;
	public researchBuildingClass.technologyInfo tierThreeStone;
	public researchBuildingClass.technologyInfo tierThreeFood;
	public researchBuildingClass.technologyInfo tierThreeHousing;
	public researchBuildingClass.technologyInfo tierThreeResearch;
	public researchBuildingClass.technologyInfo tierOneGatherNode;

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
		tierOneOre = new researchBuildingClass.technologyInfo ("tierOneOre", 100, "Ores!", description, false);


		//MEDIEVAL ERA
		//tier Two Ore
		description = "Deeper Quarrys have revolutionized mining techniques. This has also lead to the discovery of new ores from deeper mines." +
			"\nUnlocks the Advanced Mine, which digs up ore at an increased rate.";
		tierTwoOre = new researchBuildingClass.technologyInfo ("tierTwoOre", 150, "Advanced Mine", description, false);

		//tier Two Wood
		description = "The advent of iron has made better and stronger tools. This advancement is especally seen in the field of forestry, where iron sawblades allows your people to cut down trees more effectivly." +
			"\nUnlocks the Sawmill, which gathers wood at an increased rate.";
		tierTwoWood = new researchBuildingClass.technologyInfo ("tierTwoWood", 125, "Advanced Woodcutting", description, false);

		//tier Two stone
		description = "The advent of iron has made better and stronger tools. This advancement allows quarries to dig even deeper, with iron picks and tools." +
			"\nUnlocks the Advanced Quarry, which gathers stone at an increased rate.";
		tierTwoStone = new researchBuildingClass.technologyInfo ("tierTwoStone", 125, "Advanced Quarries", description, false);

		//tier Two food
		description = "With more mouths to feed your people have started to brainstorm new farming techniques. This has led to a revolutionary new way of transporting water to crops called Irrigation. This allows for bigger farms closer together." +
			"\nUnlocks the Advanced Farm, which brings in huge amounts of food.";
		tierTwoFood = new researchBuildingClass.technologyInfo ("tierTwoFood", 100, "Advanced Farming Techniques", description, false);

		//tier Two Housing
		description = "Better ways of gathering stone has produced even better stone building materials. Using this improved material your people have developed methods of building stronger stone houses." +
			"\nUnlocks stone houses, which improve over wooden houses.";
		tierTwoHousing = new researchBuildingClass.technologyInfo ("tierTwoHousing", 150, "Stone Houses", description, false);

		//tier Two Research
		description = "After building several stone houses your people became ambitious, they build bigger and bigger buildings from stone until the reached the pinnical achivement: Castles." +
			"\nUnlocks the castle, which is a great research building";
		tierTwoResearch = new researchBuildingClass.technologyInfo ("tierTwoResearch", 200, "Castles", description, false);


		//RENAISSANCE ERA
		//Tier Three Ore
		description = "The advent of explosive mining has lead to even better methods of digging into the earth. Shaft mining provides an extremly effective way of digging up usable ore." +
			"\nUnlocks the shaft mine, which brings in large amounts of ore.";
		tierThreeOre = new researchBuildingClass.technologyInfo ("tierThreeOre", 250, "Shaft Mining", description, false);

		//Tier one refinement
		description = "With the advent of improved ore output, the need for better refinement processes lead to the discovery of the smeltery, a building which mass refines ore." +
			"\nUnlocks the Smeltery, which improves from the blacksmith.";
		tierOneRefinement = new researchBuildingClass.technologyInfo ("tierOneRefinement", 225, "Smelting", description, false);

		//Tier Three wood
		description = "With improved forest cutting abilities comes the need to manage the planting and harvesting of trees, your people have begun to call this process 'Forest Managment'" +
			"\nUnlocks the forest manager, which gives adjacency bonuses to adjacent wood gathering buildings.";
		tierThreeWood = new researchBuildingClass.technologyInfo ("tierThreeWood", 300, "Forestry Management", description, false);

		//tier three stone
		description = "As your people have mined more and various ores, they have discovered a volitile new material. It can explode violently and can be used to mine greater quantities of stone." +
			"\nUnlocks the explosive quarry, which mines effectivly with explosives.";
		tierThreeStone = new researchBuildingClass.technologyInfo ("tierThreeStone", 250, "Explosives", description, false);

		//tier three food
		description = "As your people use more and more water for crops, the need for water storage techniques has lead to the invention of a water reservoir to store large amounts of water." +
			"\nUnlocks the water reservoir, which gives adjacency bonuses to adjacent farms.";
		tierThreeFood = new researchBuildingClass.technologyInfo ("tierThreeFood", 300, "Water Reservoirs", description, false);

		//tier three housing
		description = "Advanced building techniques have lead to the ability to make taller, multi story buildings." +
			"\nUnlocks the Multi-Story house, which houses a large amount of people.";
		tierThreeHousing = new researchBuildingClass.technologyInfo ("tierThreeHousing", 225, "Multi-Story Housing", description, false);

		//tier three research
		description = "A bigger population has lead to the formation of what your people call guilds. Gatherings of the smartest people in your population in the same place leads to great advancements." +
			"\nUnlocks Guilds, massive research centers which provide exceptional research.";
		tierThreeResearch = new researchBuildingClass.technologyInfo ("tierThreeResearch", 300, "Guilds", description, false);

		//Tier One Gather Node
		description = "More organized leadership has allowed you to coordinate your citizens from a distance. This technology will allow you to gather resources farther away." +
			"\nUnlocks the Gather Point, which will act as a point for your resource gathering buildings to deposit resources.";
		tierOneGatherNode = new researchBuildingClass.technologyInfo ("tierOneGatherNode", 250, "Gather Point", description, false);

		setArray ();
	}

	void Update() {
		tierOneHousing.isAvailbile = CheckResearchPrerequisites(tierOneHousing);
		tierTwoHousing.isAvailbile = CheckResearchPrerequisites (tierTwoHousing);
		tierThreeHousing.isAvailbile = CheckResearchPrerequisites (tierThreeHousing);

		tierOneResearch.isAvailbile = CheckResearchPrerequisites (tierOneResearch);
		tierTwoResearch.isAvailbile = CheckResearchPrerequisites (tierTwoResearch);
		tierThreeResearch.isAvailbile = CheckResearchPrerequisites (tierThreeResearch);

		tierOneOre.isAvailbile = CheckResearchPrerequisites (tierOneOre);
		tierTwoOre.isAvailbile = CheckResearchPrerequisites (tierTwoOre);
		tierThreeOre.isAvailbile = CheckResearchPrerequisites (tierThreeOre);

		tierTwoWood.isAvailbile = CheckResearchPrerequisites (tierTwoWood);
		tierThreeWood.isAvailbile = CheckResearchPrerequisites (tierThreeWood);

		tierTwoStone.isAvailbile = CheckResearchPrerequisites (tierTwoStone);
		tierThreeStone.isAvailbile = CheckResearchPrerequisites (tierThreeStone);

		tierTwoFood.isAvailbile = CheckResearchPrerequisites (tierTwoFood);
		tierThreeFood.isAvailbile = CheckResearchPrerequisites (tierThreeFood);

		tierOneRefinement.isAvailbile = CheckResearchPrerequisites (tierOneRefinement);

		tierOneGatherNode.isAvailbile = CheckResearchPrerequisites (tierOneGatherNode);

		setArray ();
	}

	bool CheckResearchPrerequisites(researchBuildingClass.technologyInfo techToCheck) {

		int numberOfPreOkay = 0;
		bool returner = false;

		if (techToCheck.hasBeenPurchased == false && techToCheck.isAvailbile == false) {
			for (int i = 0; techToCheck.prerequisites.Count > i; i++) {
				if (techToCheck.prerequisites [i].hasBeenPurchased == true) {
					numberOfPreOkay++;
				}

				if (numberOfPreOkay == techToCheck.prerequisites.Count) {
					techToCheck.isAvailbile = true;
					returner = true;
				}
			}
		} else if (techToCheck.isAvailbile == true) {
			returner = true;
		} else {
			returner = false;
		}
				
		return returner;
	}

	public void tryResearch(string researchName, float cost) {
		switch (researchName) {
		//Bronze Era
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

		//Medieval Era
		case "tierTwoOre":
			if (resourceManager.Instance.purchaseResearch (tierTwoOre.techCost)) {
				researchHandler.Instance.tierTwoOre ();
				tierTwoOre.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoOre.techDescription);

				setArray ();
			}
			break;
		case "tierTwoWood":
			if (resourceManager.Instance.purchaseResearch (tierTwoWood.techCost)) {
				researchHandler.Instance.tierTwoWood ();
				tierTwoWood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoWood.techDescription);

				setArray ();
			}
			break;
		case "tierTwoStone":
			if (resourceManager.Instance.purchaseResearch (tierTwoStone.techCost)) {
				researchHandler.Instance.tierTwoStone ();
				tierTwoStone.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoStone.techDescription);

				setArray ();
			}
			break;
		case "tierTwoFood":
			if (resourceManager.Instance.purchaseResearch (tierTwoFood.techCost)) {
				researchHandler.Instance.tierTwoFood ();
				tierTwoFood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoFood.techDescription);

				setArray ();
			}
			break;
		case "tierTwoHousing":
			if (resourceManager.Instance.purchaseResearch (tierTwoHousing.techCost)) {
				researchHandler.Instance.tierTwoHousing ();
				tierTwoHousing.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoHousing.techDescription);

				setArray ();
			}
			break;
		case "tierTwoResearch":
			if (resourceManager.Instance.purchaseResearch (tierTwoResearch.techCost)) {
				researchHandler.Instance.tierTwoResearch ();
				tierTwoResearch.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierTwoResearch.techDescription);

				setArray ();
			}
			break;

		//Renaissance Era
		case "tierThreeOre":
			if (resourceManager.Instance.purchaseResearch (tierThreeOre.techCost)) {
				researchHandler.Instance.tierThreeOre ();
				tierThreeOre.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeOre.techDescription);

				setArray ();
			}
			break;
		case "tierOneRefinement":
			if (resourceManager.Instance.purchaseResearch (tierOneRefinement.techCost)) {
				researchHandler.Instance.tierOneRefinement ();
				tierOneRefinement.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneRefinement.techDescription);

				setArray ();
			}
			break;
		case "tierThreeWood":
			if (resourceManager.Instance.purchaseResearch (tierThreeWood.techCost)) {
				researchHandler.Instance.tierThreeWood ();
				tierThreeWood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeWood.techDescription);

				setArray ();
			}
			break;
		case "tierThreeStone":
			if (resourceManager.Instance.purchaseResearch (tierThreeStone.techCost)) {
				researchHandler.Instance.tierThreeStone ();
				tierThreeStone.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeStone.techDescription);

				setArray ();
			}
			break;
		case "tierThreeFood":
			if (resourceManager.Instance.purchaseResearch (tierThreeFood.techCost)) {
				researchHandler.Instance.tierThreeFood ();
				tierThreeFood.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeFood.techDescription);

				setArray ();
			}
			break;
		case "tierThreeHousing":
			if (resourceManager.Instance.purchaseResearch (tierThreeHousing.techCost)) {
				researchHandler.Instance.tierThreeHousing ();
				tierThreeHousing.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeHousing.techDescription);

				setArray ();
			}
			break;
		case "tierThreeResearch":
			if (resourceManager.Instance.purchaseResearch (tierThreeResearch.techCost)) {
				researchHandler.Instance.tierThreeResearch ();
				tierThreeResearch.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierThreeResearch.techDescription);

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
		//Bronze Era
		enabledResearch[0] = tierOneWood;
		enabledResearch [1] = tierOneStone;
		enabledResearch [2] = tierOneFood;
		enabledResearch [3] = tierOneHousing;
		enabledResearch [4] = tierOneResearch;
		enabledResearch [5] = tierOneOre;

		//Medieval Era
		enabledResearch [6] = tierTwoOre;
		enabledResearch [7] = tierTwoWood;
		enabledResearch [8] = tierTwoStone;
		enabledResearch [9] = tierTwoFood;
		enabledResearch [10] = tierTwoHousing;
		enabledResearch [11] = tierTwoResearch;

		//Renaissance Era
		enabledResearch [12] = tierThreeOre;
		enabledResearch [13] = tierOneRefinement;
		enabledResearch [14] = tierThreeWood;
		enabledResearch [15] = tierThreeStone;
		enabledResearch [16] = tierThreeFood;
		enabledResearch [17] = tierThreeHousing;
		enabledResearch [18] = tierThreeResearch;
		enabledResearch [19] = tierOneGatherNode;
		setPrereqisites();
	}

	public void setPrereqisites() {
		//setting prerequisites
		//Bronze Era
		List<researchBuildingClass.technologyInfo> tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneWood, tierOneFood };
		tierOneHousing.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneHousing };
		tierOneResearch.prerequisites = tempPre;
	
		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneStone };
		tierOneOre.prerequisites = tempPre;

		//Medieval Era
		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoStone, tierOneOre };
		tierTwoOre.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneWood, tierOneOre };
		tierTwoWood.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneStone, tierOneOre };
		tierTwoStone.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneFood };
		tierTwoFood.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoStone, tierOneHousing };
		tierTwoHousing.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierOneResearch, tierTwoHousing };
		tierTwoResearch.prerequisites = tempPre;

		//Renaissance Era
		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoOre, tierThreeStone };
		tierThreeOre.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoOre };
		tierOneRefinement.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoWood };
		tierThreeWood.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoStone, tierTwoOre };
		tierThreeStone.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoFood, tierTwoHousing };
		tierThreeFood.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoHousing, tierTwoWood };
		tierThreeHousing.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoResearch, tierTwoHousing };
		tierThreeResearch.prerequisites = tempPre;

		tempPre = new List<researchBuildingClass.technologyInfo> () { tierTwoResearch, tierThreeHousing };
		tierOneGatherNode.prerequisites = tempPre;
	}
}
