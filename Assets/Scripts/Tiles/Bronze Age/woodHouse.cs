using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodHouse : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats woodHouseStats;

	private float defaultManpowerReturn = 1.0f;
	private int valToAddMaxManpower = 4;
	public float manpowerReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public woodHouse() {
		tileTitle = "Wooden House";
		tileDescription = "A better, bigger home for your citizens." + "\nProviding: " + manpowerReturn + " manpower per turn. \nAdding: " + valToAddMaxManpower + " to manpower cache.";
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

		tileDescription = "A better, bigger home for your citizens." + "\nProviding: " + manpowerReturn + " manpower per turn. \nAdding: " + valToAddMaxManpower + " to manpower cache.";

		resourceManager.Instance.addManpowerResource (defaultManpowerReturn);
		resourceManager.Instance.addToManpowerTotal (valToAddMaxManpower);
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodHouse.buildingCosts;

		/*
		resourceBuildingClass.adjBonus forestBonus = new resourceBuildingClass.adjBonus ("Forest", 0.1f);
		resourceBuildingClass.adjBonus baseBonus = new resourceBuildingClass.adjBonus ("Base", 0.5f);
*/

		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { /*forestBonus, baseBonus*/ };

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

		woodHouseStats = new resourceBuildingClass.resourceBuildingStats ("Manpower", defaultManpowerReturn, tempCosts, tempBonus, tempPenalty); 

		float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (woodHouseStats, this.GetComponent<baseGridPosition>().adjacentTiles);

		//Debug.Log ("Temp Efficencey: " + tempEfficency + ". Default: " + defaultWoodReturn + ".");

		manpowerReturn = defaultManpowerReturn + tempEfficency;

		//Debug.Log ("Total Wood return: " + woodReturn);

		/*
		if (woodReturn <= 0) {
			woodReturn = 0;
		}*/

		woodHouseStats.efficiency = manpowerReturn;
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		//tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
