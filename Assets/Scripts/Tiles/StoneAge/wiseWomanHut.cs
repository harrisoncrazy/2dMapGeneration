using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiseWomanHut : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats wiseWomanHutStats;

	private float defaultResearchReturn = 1.0f;
	public float researchReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public wiseWomanHut() {
		tileTitle = "Wise Woman's Hut";
		tileDescription = "A place for the wisest of your population to live." + "\nProviding: " + researchReturn + " research per turn.";
	}

	void setTileDescription() {
		tileDescription = "A place for the wisest of your population to live." + "\nProviding: " + researchReturn + " research per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		StartCoroutine ("delay");

		if (isHoverMode == false) {
			if (researchHandler.Instance.isResearchEnabled == false) {
				researchHandler.Instance.startResearch ();
			}
		}
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.leanToHouse.buildingCosts;


		resourceBuildingClass.adjBonus houseBonus = new resourceBuildingClass.adjBonus ("Lean", 0.3f);
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { houseBonus };

		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			stonePenalty
		};

		wiseWomanHutStats = new resourceBuildingClass.resourceBuildingStats ("Research", 0f, tempCosts, tempBonus, tempPenalty); 

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
			if (wiseWomanHutStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, wiseWomanHutStats.adjBonusTiles, wiseWomanHutStats.adjPenaltyTiles);
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (wiseWomanHutStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			researchReturn = defaultResearchReturn + tempEfficency;

			float oldEfficiency = wiseWomanHutStats.efficiency;
			//Debug.Log ("Old efficiency: " + oldEfficiency);

			wiseWomanHutStats.efficiency = researchReturn;
			//Debug.Log ("researchReturn: " + researchReturn);

			float newEfficiency = researchReturn - oldEfficiency;
			//Debug.Log ("temp effic: " + tempEfficency);

			resourceManager.Instance.addResearchResource ( newEfficiency );
		}
	}
}
