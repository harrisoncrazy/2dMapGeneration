using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMine : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats basicMineStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultOreReturn = 0.5f;
	public float oreReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public basicMine() {
		tileTitle = "Basic Quarry";
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

		resourceBuildingClass.adjBonus mountainBonus = new resourceBuildingClass.adjBonus ("Mountain", 0.3f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { mountainBonus };

		resourceBuildingClass.adjPenalty testPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			testPenalty
		};

		basicMineStats = new resourceBuildingClass.resourceBuildingStats ("Ore", defaultOreReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Ore", basicMineStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (basicMineStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, basicMineStats.adjBonusTiles, basicMineStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (basicMineStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			oreReturn = defaultOreReturn + tempEfficency;

			//Debug.Log ("Total Wood return: " + woodReturn);

			basicMineStats.efficiency = oreReturn;
		}
	}
}
