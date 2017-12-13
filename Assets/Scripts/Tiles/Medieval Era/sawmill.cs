using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawmill : defaultBuilding {

	//TODO make unique adjacency bonuses and improve output

	public resourceBuildingClass.resourceBuildingStats sawmillStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultWoodReturn = 0.5f;
	public float woodReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public sawmill() {
		tileTitle = "Sawmill";
		tileDescription = "Uses metal saws to chop and refine wood." + "\nProviding: " + woodReturn + " wood per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Uses metal saws to chop and refine wood." + "\nProviding: " + woodReturn + " wood per turn.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Forest", 0.5f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { forestBonus };

		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			stonePenalty /*
			buildingPenalty,
			mountainPenalty*/
		};

		sawmillStats = new resourceBuildingClass.resourceBuildingStats ("Wood", defaultWoodReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Wood", sawmillStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 5.0f;
			}
		} else if (isHoverMode == true) {
			if (sawmillStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, sawmillStats.adjBonusTiles, sawmillStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (sawmillStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			woodReturn = defaultWoodReturn + tempEfficency;

			sawmillStats.efficiency = woodReturn;
		}
	}
}
