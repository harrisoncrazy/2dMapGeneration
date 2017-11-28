using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchCosts : MonoBehaviour {

	public static researchCosts Instance;

	//Bronze ERA
	public researchBuildingClass.technologyInfo tierOneLumber;
	public researchBuildingClass.technologyInfo tierOneStone;
	public researchBuildingClass.technologyInfo tierOneFood;
	public researchBuildingClass.technologyInfo tierOneGatherNode;


	public researchBuildingClass.technologyInfo[] enabledResearch = new researchBuildingClass.technologyInfo[125];
	// Use this for initialization
	void Start () {
		Instance = this;

		//BRONZE ERA
		//Tier One Lumber
		string description =  "Advanced strategies in wood cutting will allow you to cut down larger trees. This technology will allow you to clear forests to place down buildings. " +
			"\nUnlocks the Basic Lumberer, which gains additional wood gathering capabilites when placed adjacent to forest.";
		tierOneLumber = new researchBuildingClass.technologyInfo("tierOneLumber", 50, "Wood Cutting", description, true);

		//Tier One Stone
		description = "Advanced strategies in mining will allow you to break and collect larger stones. This technology will allow you to clear smaller rock tiles to place down buildings. " +
			"\nUnlocks the Basic Quarry, which gains additional stone gathering capabilities when placed adjacent to rocks.";
		tierOneStone = new researchBuildingClass.technologyInfo ("tierOneStone", 50, "Mining", description, true);

		//Tier One Farm
		description = "Farm sample text. " +
		"\nUnlocks the Basic Farm, which brings in large amounts of food slowly.";
		tierOneFood = new researchBuildingClass.technologyInfo ("tierOneFood", 50, "Farms", description, true);

		//Tier One Gather Node
		description = "A more organized government has allowed you to coordinate your citizens from a distance. This technology will allow you to gather resources farther away." +
			"\nUnlocks the Gather Point, which will act as a point for your resource gathering buildings to deposit resources.";
		tierOneGatherNode = new researchBuildingClass.technologyInfo ("tierOneGatherNode", 75, "Gather Point", description, true);

		setArray ();
	}

	public void tryResearch(string researchName, float cost) {
		switch (researchName) {
		case "tierOneLumber":
			if (resourceManager.Instance.purchaseResearch (tierOneLumber.techCost)) {
				researchHandler.Instance.tierOneLumber ();
				tierOneLumber.hasBeenPurchased = true;
				inputHandler.Instance.toggleResearchPanel ();
				researchHandler.Instance.researchUnlockPopup (tierOneLumber.techDescription);

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
		enabledResearch[0] = tierOneLumber;
		enabledResearch [1] = tierOneStone;
		enabledResearch [2] = tierOneFood;
		enabledResearch [3] = tierOneGatherNode;
	}
}
