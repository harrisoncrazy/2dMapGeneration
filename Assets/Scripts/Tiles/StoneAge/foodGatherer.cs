using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodGatherer : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats foodGathererStats;

	private float defaultFoodReturn = 0.5f;
	public float foodReturn = Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public foodGatherer() {
		tileTitle = "Food Gatherer";
		tileDescription = "Gathers berries and easy to hunt animals." + "\nProviding: " + foodReturn + " food per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		StartCoroutine ("delay");
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		resourceManager.Instance.addFoodResource (foodGathererStats.efficiency);

		tileDescription = "Gathers berries and easy to hunt animals." + "\nProviding: " + foodReturn + " food per turn.";
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.stoneGatherBuildingCost;

		string[] tempPlaceTiles = new string[] { "Grassland" };

		/*
		resourceBuildingClass.adjBonus waterBonus = new resourceBuildingClass.adjBonus ("Ocean", 0.2f);
		resourceBuildingClass.adjBonus plainGrassBonus = new resourceBuildingClass.adjBonus ("Grassland", 0.1f);
		resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.5f);
		*/

		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] {
			/*waterBonus,
			plainGrassBonus,
			baseBonus*/
		};
			
		/*
		resourceBuildingClass.adjPenalty buildingPenalty = new resourceBuildingClass.adjPenalty ("Building", 0.2f);
		resourceBuildingClass.adjPenalty sandPenalty = new resourceBuildingClass.adjPenalty ("Sand", 0.1f);
		resourceBuildingClass.adjPenalty snowPenalty = new resourceBuildingClass.adjPenalty ("Snow", 0.1f);
		*/

		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			/*sandPenalty,
			snowPenalty,
			buildingPenalty*/
		};

		foodGathererStats = new resourceBuildingClass.resourceBuildingStats ("Food", defaultFoodReturn, tempCosts, tempPlaceTiles, tempBonus, tempPenalty); 

		float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (foodGathererStats, this.GetComponent<baseGridPosition>().adjacentTiles);

		//Debug.Log ("Temp Efficencey: " + tempEfficency + ". Default: " + defaultWoodReturn + ".");

		foodReturn = defaultFoodReturn + tempEfficency;

		//Debug.Log ("Total Wood return: " + woodReturn);

		foodGathererStats.efficiency = foodReturn;
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
		resourceOutTick -= Time.deltaTime;
		if (resourceOutTick <= 0) {
			SpawnResourceDeliveryNode ("Food", foodGathererStats.efficiency);
			resourceOutTick = 5.0f;
		}
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		//tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
