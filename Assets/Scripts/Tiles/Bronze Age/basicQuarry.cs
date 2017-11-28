using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicQuarry : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats basicQuarryStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultStoneReturn = 0.5f;
	public float stoneReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public basicQuarry() {
		tileTitle = "Basic Quarry";
		tileDescription = "Digs into the ground for stone material." + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Digs into the ground for stone material." + "\nProviding: " + stoneReturn + " stone per turn.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Rock", 0.3f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { forestBonus };

		resourceBuildingClass.adjPenalty testPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			testPenalty
		};

		basicQuarryStats = new resourceBuildingClass.resourceBuildingStats ("Stone", defaultStoneReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Stone", basicQuarryStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (basicQuarryStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, basicQuarryStats.adjBonusTiles, basicQuarryStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (basicQuarryStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			stoneReturn = defaultStoneReturn + tempEfficency;

			//Debug.Log ("Total Wood return: " + woodReturn);

			basicQuarryStats.efficiency = stoneReturn;
		}
	}
}
