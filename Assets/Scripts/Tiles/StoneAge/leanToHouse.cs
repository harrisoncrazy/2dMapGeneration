using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leanToHouse : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats leanToHouseStats;

	private float defaultManpowerReturn = 1.0f;
	public float manpowerReturn; //= Mathf.Clamp(0.0f, 0.0f, 5.0f);

	public leanToHouse() {
		tileTitle = "Lean To";
		tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";
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

		tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";

		resourceManager.Instance.addManpowerResource (defaultManpowerReturn);
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGatherBuidlingCost;

		string[] tempPlaceTiles = new string[] { "Grassland" };

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

		leanToHouseStats = new resourceBuildingClass.resourceBuildingStats ("Manpower", defaultManpowerReturn, tempCosts, tempPlaceTiles, tempBonus, tempPenalty); 

		float tempEfficency = resourceBuildingClass.readResourceBuildingEfficency (leanToHouseStats, this.GetComponent<baseGridPosition>().adjacentTiles);

		//Debug.Log ("Temp Efficencey: " + tempEfficency + ". Default: " + defaultWoodReturn + ".");

		manpowerReturn = defaultManpowerReturn + tempEfficency;

		//Debug.Log ("Total Wood return: " + woodReturn);

		/*
		if (woodReturn <= 0) {
			woodReturn = 0;
		}*/

		leanToHouseStats.efficiency = manpowerReturn;
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
