using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour {

	public static inputHandler Instance;

	private bool ifPlacementModeActive = false;

	void Start() {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (ifPlacementModeActive == false) {
			if (Input.GetKeyDown (KeyCode.R)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.woodGatherBuidlingCost)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingWoodGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			} 

			if (Input.GetKeyDown (KeyCode.Y)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.foodGatherBuildingCost)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingFoodGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			} 

			if (Input.GetKeyDown (KeyCode.T)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.stoneGatherBuildingCost)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingStoneGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			} 
		}
	}

	public bool checkPlacementStatus() { //checks the value of ifPlacementModeActive and returns
		return ifPlacementModeActive;
	}

	public void disablePlacementMode() { //disable placementmode
		ifPlacementModeActive = false;
		GameManager.Instance.disablePlacementModes ();
	}
}
