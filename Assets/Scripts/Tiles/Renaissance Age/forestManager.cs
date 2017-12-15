using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forestManager : defaultBuilding {

	public resourceBuildingClass.resourceBuildingStats forestMangerStats = new resourceBuildingClass.resourceBuildingStats();

	public forestManager() {
		tileTitle = "Forest Manager";
		tileDescription = "Organizes the cutting and replanting of nearby wood gathering buildings.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Organizes the cutting and replanting of nearby wood gathering buildings.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		constructResourceStats ();
		//resourceManager.Instance.addWoodResource (woodGathererStats.efficiency);

		setTileDescription ();
	}

	void constructResourceStats() {
		resourceBuildingClass.resourceTypeCost[] tempCosts = buildingCosts.Instance.woodGather.buildingCosts;

		resourceBuildingClass.adjBonus noBonus = new resourceBuildingClass.adjBonus ("Farm", 0.1f);//TODO add bonus effect for wood cutters
		resourceBuildingClass.adjBonus[] tempBonus = new resourceBuildingClass.adjBonus[] { noBonus };

		resourceBuildingClass.adjPenalty noPenalty = new resourceBuildingClass.adjPenalty ("Ass", 0.1f);
		resourceBuildingClass.adjPenalty[] tempPenalty = new resourceBuildingClass.adjPenalty[] {
			noPenalty 
		};

		forestMangerStats = new resourceBuildingClass.resourceBuildingStats ("Food", 0.0f, tempCosts, tempBonus, tempPenalty); 
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();
		} else if (isHoverMode == true) {
			if (forestMangerStats.adjBonusTiles != null) {
				this.GetComponent<baseGridPosition> ().enableArrows (GameManager.Instance.currentHoveredTile.GetComponent<baseGridPosition> ().adjacentTiles, forestMangerStats.adjBonusTiles, forestMangerStats.adjPenaltyTiles);
			}
		}
	}

	protected override void OnMouseDown() {
		if (isHoverMode == false) {
			base.OnMouseDown ();
			base.setInfoPanelText (tileTitle, tileDescription);
		}
	}
}
