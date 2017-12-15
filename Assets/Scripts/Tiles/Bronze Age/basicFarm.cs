using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicFarm : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats basicFarmStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultFoodReturn = 3.0f;//TODO edit wait value for food returns
	public float foodReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public basicFarm() {
		tileTitle = "Basic Farm";
		tileDescription = "Organized planting of crops." + "\nProviding: " + foodReturn + " food per turn.";

	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Organized planting of crops." + "\nProviding: " + foodReturn + " food per turn.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus noBonus = new resourceBuildingClass.adjBonus ("Water", 2.0f);//no bonus or penalties at this tier
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { noBonus };

		resourceBuildingClass.adjPenalty noPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			noPenalty 
		};

		basicFarmStats = new resourceBuildingClass.resourceBuildingStats ("Food", defaultFoodReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Food", basicFarmStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 15.0f;
			}
		} else if (isHoverMode == true) {
			if (basicFarmStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, basicFarmStats.adjBonusTiles, basicFarmStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (basicFarmStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			foodReturn = defaultFoodReturn + tempEfficency;

			//Debug.Log ("Total Wood return: " + woodReturn);

			basicFarmStats.efficiency = foodReturn;
		}
	}
}
