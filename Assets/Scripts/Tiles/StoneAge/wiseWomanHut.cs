using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiseWomanHut : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats wiseWomanHutStats;

	private float defaultResearchReturn = 1.0f;
	private int valToSubtractMaxManpower = -1;
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
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();

		resourceManager.Instance.addManpowerResource (wiseWomanHutStats.efficiency);
		resourceManager.Instance.addToManpowerTotal (valToSubtractMaxManpower);
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.leanToHouse.buildingCosts;


		resourceBuildingClass.adjBonus houseBonus = new resourceBuildingClass.adjBonus ("House", 0.1f);
		//resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.5f);


		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { houseBonus };

		/*
		resourceBuildingClass.adjPenalty stonePenalty = new resourceBuildingClass.adjPenalty ("Rock", 0.1f);
		resourceBuildingClass.adjPenalty buildingPenalty = new resourceBuildingClass.adjPenalty ("Building", 0.2f);
		resourceBuildingClass.adjPenalty mountainPenalty = new resourceBuildingClass.adjPenalty ("Mountain", 0.3f);
		*/

		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			/*stonePenalty,
			buildingPenalty,
			mountainPenalty*/
		};

		wiseWomanHutStats = new resourceBuildingClass.resourceBuildingStats ("Research", defaultResearchReturn, tempCosts, tempBonus, tempPenalty); 

		readResourceEfficency ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
			resourceOutTick -= Time.deltaTime;
			if (resourceOutTick <= 0) {
				readResourceEfficency ();
				//SpawnResourceDeliveryNode ("Wood", woodGathererStats.efficiency);
				resourceOutTick = 5.0f;
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

			Debug.Log ("Total research return: " + researchReturn);

			wiseWomanHutStats.efficiency = researchReturn;
		}
	}
}
