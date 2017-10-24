using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneGatherer : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats stoneGathererStats;

	private float defaultStoneReturn = 0.5f;
	public float stoneReturn = Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public stoneGatherer() {
		tileTitle = "Stone Gatherer";
		tileDescription = "Gathers easy to manage rocks to fashion into tools to build buildings." +
			"" + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		StartCoroutine ("delay");
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		resourceManager.Instance.addStoneResource (stoneGathererStats.efficiency);

		tileDescription = "Gathers easy to manage rocks to fashion into tools to build buildings." +
			"" + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.stoneGatherBuildingCost;

		string[] tempPlaceTiles = new string[] { "Grassland" };

		/*
		resourceBuildingClass.adjBonus heavyRockBonus = new resourceBuildingClass.adjBonus ("Heavy Rock", 0.2f);
		resourceBuildingClass.adjBonus lightRockBonus = new resourceBuildingClass.adjBonus ("Light Rock", 0.1f);
		resourceBuildingClass.adjBonus mountainBonus = new resourceBuildingClass.adjBonus ("Mountain", 0.3f);
		resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.3f);
		*/

		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] {
			/*heavyRockBonus,
			lightRockBonus,
			mountainBonus,
			baseBonus*/
		};

		/*
		resourceBuildingClass.adjPenalty waterPenalty = new resourceBuildingClass.adjPenalty ("Ocean", 0.2f);
		resourceBuildingClass.adjPenalty buildingPenalty = new resourceBuildingClass.adjPenalty ("Building", 0.2f);
		resourceBuildingClass.adjPenalty sandPenalty = new resourceBuildingClass.adjPenalty ("Sand", 0.1f);
		resourceBuildingClass.adjPenalty snowPenalty = new resourceBuildingClass.adjPenalty ("Snow", 0.1f);
		*/

		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			/*waterPenalty,
			sandPenalty,
			snowPenalty,
			buildingPenalty*/
		};

		stoneGathererStats = new resourceBuildingClass.resourceBuildingStats ("Stone", defaultStoneReturn, tempCosts, tempPlaceTiles, tempBonus, tempPenalty); 

		float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (stoneGathererStats, this.GetComponent<baseGridPosition>().adjacentTiles);

		//Debug.Log ("Temp Efficencey: " + tempEfficency + ". Default: " + defaultWoodReturn + ".");

		stoneReturn = defaultStoneReturn + tempEfficency;

		//Debug.Log ("Total Wood return: " + woodReturn);

		/*
		if (woodReturn <= 0) {
			woodReturn = 0;
		}*/

		stoneGathererStats.efficiency = stoneReturn;
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
		resourceOutTick -= Time.deltaTime;
		if (resourceOutTick <= 0) {
			SpawnResourceDeliveryNode ("Stone", stoneGathererStats.efficiency);
			resourceOutTick = 5.0f;
		}
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		//tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
