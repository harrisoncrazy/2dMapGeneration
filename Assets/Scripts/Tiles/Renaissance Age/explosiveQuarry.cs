using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveQuarry : defaultBuilding {


	//TODO make unique adjacency bonuses and improve output

	public resourceBuildingClass.resourceBuildingStats explosiveQuarryStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultStoneReturn = 3.0f;
	public float stoneReturn;

	public explosiveQuarry() {
		tileTitle = "Explosive Quarry";
		tileDescription = "Making quarries with explosives!." + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Making quarries with explosives!." + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Rock", 1.0f);
		resourceBuildingClass.adjBonus mountBonus = new resourceBuildingClass.adjBonus ("Mountain", 2.0f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { forestBonus, mountBonus };

		resourceBuildingClass.adjPenalty testPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);//possibly penalty from adjacent water? only slight if yes
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			testPenalty
		};

		explosiveQuarryStats = new resourceBuildingClass.resourceBuildingStats ("Stone", defaultStoneReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Stone", explosiveQuarryStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (explosiveQuarryStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, explosiveQuarryStats.adjBonusTiles, explosiveQuarryStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (explosiveQuarryStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			stoneReturn = defaultStoneReturn + tempEfficency;

			explosiveQuarryStats.efficiency = stoneReturn;
		}
	}
}
