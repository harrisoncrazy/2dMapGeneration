using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smeltery : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats smelteryStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultOreReturn = 1.0f;
	public float oreReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public smeltery() {
		tileTitle = "Smeltery";
		tileDescription = "Mass Refinement of Metal from ore.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Mass Refinement of Metal from ore.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {//making this not a return per tick, but an efficency in speed for smelting ore. Tier one blacksmith will be a default of 1 but next tiers will have adjacency which will raise/lower making it faster or slower
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus mountainBonus = new resourceBuildingClass.adjBonus ("None", 0.3f);//TODO add bonus/penalties
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { mountainBonus };

		resourceBuildingClass.adjPenalty testPenalty = new resourceBuildingClass.adjPenalty ("None", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			testPenalty
		};

		smelteryStats = new resourceBuildingClass.resourceBuildingStats ("Ore", defaultOreReturn, tempCosts, tempBonus, tempPenalty); 

		resourceOutTick = 5.0f/smelteryStats.efficiency;

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				if (resourceManager.Instance.requestOre (10.0f)) {
					SpawnResourceDeliveryNode ("Metal", 20.0f);
				}
				readResourceEfficency ();
				resourceOutTick = 5.0f / smelteryStats.efficiency;
			}
		} else if (isHoverMode == true) {
			if (smelteryStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, smelteryStats.adjBonusTiles, smelteryStats.adjPenaltyTiles);
			}
		}
	}

	protected override void OnMouseDown() {
		if (isHoverMode == false) {
			base.OnMouseDown ();
			base.setInfoPanelText (tileTitle, tileDescription);
		}
	}

	void readResourceEfficency() {
		if (isHoverMode == false) {
			this.GetComponent<baseGridPosition> ().setAdjArrayVals ();

			setTileDescription ();

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (smelteryStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			oreReturn = defaultOreReturn + tempEfficency;

			smelteryStats.efficiency = oreReturn;
		}
	}
}
