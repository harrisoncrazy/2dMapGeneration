﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guildHouse : defaultBuilding {

	//TODO make unique adjacency bonuses and improve output

	public resourceBuildingClass.resourceBuildingStats guildHouseStats;

	private float defaultResearchReturn = 5.0f;
	public float researchReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public guildHouse() {
		tileTitle = "Guild House";
		tileDescription = "An organized institution for your smartest citzens to work." + "\nProviding: " + researchReturn + " research per turn.";
	}

	void setTileDescription() {
		tileDescription = "An organized institution for your smartest citzens to work." + "\nProviding: " + researchReturn + " research per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		StartCoroutine ("delay");

		if (isHoverMode == false) {

		}
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.leanToHouse.buildingCosts;


		resourceBuildingClass.adjBonus houseBonus = new resourceBuildingClass.adjBonus ("House", 2.0f);//bonus from adjacent houses
		resourceBuildingClass.adjBonus managerBonus = new resourceBuildingClass.adjBonus("Manager", 3.0f);
		resourceBuildingClass.adjBonus resBonus = new resourceBuildingClass.adjBonus ("Water", 3.0f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { houseBonus, managerBonus, resBonus };

		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);//no penalty at this tier, blank penalty
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			stonePenalty
		};

		guildHouseStats = new resourceBuildingClass.resourceBuildingStats ("Research", 0f, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				readResourceEfficency ();
				setTileDescription ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (guildHouseStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, guildHouseStats.adjBonusTiles, guildHouseStats.adjPenaltyTiles);
			}
		}
	}

	protected override void OnMouseDown() {
		if (isHoverMode == false) {
			base.OnMouseDown ();
			//tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";
			base.setInfoPanelText (tileTitle, tileDescription);
		}
	}

	void readResourceEfficency() {
		if (isHoverMode == false) {
			this.GetComponent<baseGridPosition> ().setAdjArrayVals ();

			setTileDescription ();

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (guildHouseStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			researchReturn = defaultResearchReturn + tempEfficency;

			float oldEfficiency = guildHouseStats.efficiency;

			guildHouseStats.efficiency = researchReturn;

			float newEfficiency = researchReturn - oldEfficiency;

			resourceManager.Instance.addResearchResource ( newEfficiency );
		}
	}
}
