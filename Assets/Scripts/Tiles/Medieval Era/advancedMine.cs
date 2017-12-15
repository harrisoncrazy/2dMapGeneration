using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advancedMine : defaultBuilding {

	//TODO make adjacency bonuses and improve output

	public resourceBuildingClass.resourceBuildingStats advancedMineStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultOreReturn = 1.5f;
	public float oreReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public advancedMine() {
		tileTitle = "Advanced Mine";
		tileDescription = "Digs into the ground for ore." + "\nProviding: " + oreReturn + " ore per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Digs into the ground for ore." + "\nProviding: " + oreReturn + " ore per turn.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus mountainBonus = new resourceBuildingClass.adjBonus ("Mountain", 1.0f);
		resourceBuildingClass.adjBonus quarryBonus = new resourceBuildingClass.adjBonus ("Quarry", 0.5f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { mountainBonus, quarryBonus };

		resourceBuildingClass.adjPenalty testPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty testPenaltyy = new resourceBuildingClass.adjPenalty ("Ass2", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			testPenalty,
			testPenaltyy
		};

		advancedMineStats = new resourceBuildingClass.resourceBuildingStats ("Ore", defaultOreReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Ore", advancedMineStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (advancedMineStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, advancedMineStats.adjBonusTiles, advancedMineStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (advancedMineStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			oreReturn = defaultOreReturn + tempEfficency;

			//Debug.Log ("Total Wood return: " + woodReturn);

			advancedMineStats.efficiency = oreReturn;
		}
	}
}
