using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chiefsHut : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats chiefsHutStats;

	private float defaultResearchReturn = 1.0f;//TODO make adjacency bonus and better research income
	public float researchReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public chiefsHut() {
		tileTitle = "Chief's Hut";
		tileDescription = "A place for your leaders to live." + "\nProviding: " + researchReturn + " research per turn.";
	}

	void setTileDescription() {
		tileDescription = "A place for your leaders to live." + "\nProviding: " + researchReturn + " research per turn.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if (isHoverMode == false) {
			StartCoroutine ("delay");
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


		resourceBuildingClass.adjBonus houseBonus = new resourceBuildingClass.adjBonus ("House", 0.1f);//bonus from adjacent houses
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { houseBonus };

		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);//no penalty at this tier, blank penalty
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			stonePenalty
		};

		chiefsHutStats = new resourceBuildingClass.resourceBuildingStats ("Research", 0f, tempCosts, tempBonus, tempPenalty); 

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
			if (chiefsHutStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, chiefsHutStats.adjBonusTiles, chiefsHutStats.adjPenaltyTiles);
				//TODO fix arrows not showing up when relevant adjacent tiles are present
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

			float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (chiefsHutStats, this.GetComponent<baseGridPosition> ().adjacentTiles);

			researchReturn = defaultResearchReturn + tempEfficency;

			float oldEfficiency = chiefsHutStats.efficiency;
			//Debug.Log ("Old efficiency: " + oldEfficiency);

			chiefsHutStats.efficiency = researchReturn;
			//Debug.Log ("researchReturn: " + researchReturn);

			float newEfficiency = researchReturn - oldEfficiency;
			//Debug.Log ("temp effic: " + tempEfficency);

			resourceManager.Instance.addResearchResource ( newEfficiency );
		}
	}
}
