using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class advancedFarm : defaultBuilding {

	//TODO make some sort of unique feature

	public resourceBuildingClass.resourceBuildingStats advancedFarmStats = new resourceBuildingClass.resourceBuildingStats();

	private float defaultFoodReturn = 5.0f;//TODO edit wait value for food returns
	public float foodReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public advancedFarm() {
		tileTitle = "Advanced Farm";
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

		resourceBuildingClass.adjBonus resBonus = new resourceBuildingClass.adjBonus ("Water", 3.0f);//no bonus or penalties at this tier
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { resBonus };

		resourceBuildingClass.adjPenalty noPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			noPenalty 
		};

		advancedFarmStats = new resourceBuildingClass.resourceBuildingStats ("Food", defaultFoodReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				SpawnResourceDeliveryNode ("Food", advancedFarmStats.efficiency);
				readResourceEfficency ();
				resourceOutTick = 10.0f;
			}
		} else if (isHoverMode == true) {
			if (advancedFarmStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, advancedFarmStats.adjBonusTiles, advancedFarmStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (advancedFarmStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			foodReturn = defaultFoodReturn + tempEfficency;

			//Debug.Log ("Total Wood return: " + woodReturn);

			advancedFarmStats.efficiency = foodReturn;
		}
	}
}
