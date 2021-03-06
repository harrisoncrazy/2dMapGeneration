﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodGatherer : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats woodGathererStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultWoodReturn = 0.5f;
	public float woodReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public woodGatherer() {
		tileTitle = "Wood Gatherer";
		tileDescription = "Gathers fallen wood and sticks for building material" + "\nProviding: " + woodReturn + " wood per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void setTileDescription() {
		tileDescription = "Gathers fallen wood and sticks for building material" + "\nProviding: " + woodReturn + " wood per turn.";
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		/*
		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Forest", 0.1f);
		resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.5f);
*/

		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { /*forestBonus, baseBonus*/ };

		/*
		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Rock", 0.1f);
		resourceBuildingClass.adjPenalty buildingPenalty = new resourceBuildingClass.adjPenalty ("Building", 0.2f);
		resourceBuildingClass.adjPenalty mountainPenalty = new resourceBuildingClass.adjPenalty ("Mountain", 0.3f);
		*/

		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			/*stonePenalty,
			buildingPenalty,
			mountainPenalty*/
		};

		woodGathererStats = new resourceBuildingClass.resourceBuildingStats ("Wood", defaultWoodReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				readResourceEfficency ();
				SpawnResourceDeliveryNode ("Wood", woodGathererStats.efficiency);
				resourceOutTick = 5.0f;
			}
		}
	}

	protected override void OnMouseDown() {
		if (isHoverMode == false) {
			base.OnMouseDown ();
			//tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
			base.setInfoPanelText (tileTitle, tileDescription);
		}
	}

	void readResourceEfficency() {
		if (isHoverMode == false) {
			this.GetComponent<baseGridPosition> ().setAdjArrayVals ();

			setTileDescription ();

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (woodGathererStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			woodReturn = defaultWoodReturn + tempEfficency;

			woodGathererStats.efficiency = woodReturn;


		}
	}
}
