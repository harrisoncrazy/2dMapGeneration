using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodGatherer : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats woodGathererStats;

	private float defaultWoodReturn = 0.5f;
	private float woodReturn = Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public woodGatherer() {
		tileTitle = "Wood Gatherer";
		tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		constructResourceStats ();

		resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 25);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 10);
		resourceBuildingClass.resourceTypeCost[] tempCosts = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost
		}; 

		string[] tempPlaceTiles = new string[] { "Grass", "Forest", "Stone", "Dirt" };

		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Forest", 0.1f);
		resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.5f);

		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { forestBonus, baseBonus };

		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Rock", 0.1f);
		resourceBuildingClass.adjPenalty buildingPenalty = new resourceBuildingClass.adjPenalty ("Building", 0.2f);
		resourceBuildingClass.adjPenalty mountainPenalty = new resourceBuildingClass.adjPenalty ("Mountain", 0.3f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			stonePenalty,
			buildingPenalty,
			mountainPenalty
		};

		woodGathererStats = new resourceBuildingClass.resourceBuildingStats ("Wood", defaultWoodReturn, tempCosts, tempPlaceTiles, tempBonus, tempPenalty); 

		float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (woodGathererStats, adjacentTiles);

		woodReturn = defaultWoodReturn + tempEfficency;

		if (woodReturn < 0)
			woodReturn = 0;

		woodGathererStats.efficiency = woodReturn;
	}

	void calculateWoodReturn() {
		woodReturn = defaultWoodReturn + woodGathererStats.efficiency;
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
