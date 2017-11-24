using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour {

	public static inputHandler Instance;

	//public bool ifPlacementModeActive = false;

	//public bool isBuildingPanelActive = false;
	public GameObject buildingPanel;
	public GameObject researchPanel;


	void Start() {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.isBuildingSelected == false) {
			if (GameManager.Instance.isResearchPanelActive == false) {
				if (Input.GetKeyDown (KeyCode.B)) {
					toggleBuildingPanel ();
				}
			}
			if (researchHandler.Instance.isResearchEnabled == true) {
				if (GameManager.Instance.isBuildingPanelActive == false) {
					if (Input.GetKeyDown (KeyCode.R)) {
						toggleResearchPanel ();
					}
				}
			}
		}

		if (GameManager.Instance.isPlacementModeActive == false) {
			/*
			if (Input.GetKeyDown (KeyCode.R)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.woodGather.buildingCosts)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingWoodGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			} 

			if (Input.GetKeyDown (KeyCode.T)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.stoneGather.buildingCosts)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingStoneGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			}

			if (Input.GetKeyDown (KeyCode.Y)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.foodGather.buildingCosts)) {
					//set gamemanager building bool to true
					GameManager.Instance.placingFoodGatherer = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log ("Insufficent Resources");
				}
			} 

			if (Input.GetKeyDown (KeyCode.U)) {
				if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts.Instance.leanToHouse.buildingCosts)) {
					GameManager.Instance.placingLeanToHouse = true;
					ifPlacementModeActive = true;
				} else {
					Debug.Log("Insufficent Resources");
				}
			}*/
		}
	}

	void toggleBuildingPanel () {//toggling on or off the building panel, locking or unlocking the camera to the building
		if (GameManager.Instance.isBuildingPanelActive == true) {
			buildingPanel.SetActive (false);
			GameManager.Instance.isBuildingPanelActive = false;
		} else {
			buildingPanel.SetActive (true);
			GameManager.Instance.isBuildingPanelActive = true;
			scrollMenuControl.Instance.ReadActiveBuildings();
		}
	}

	public void toggleResearchPanel() {
		if (GameManager.Instance.isResearchPanelActive == true) {
			researchPanel.SetActive (false);
			GameManager.Instance.isResearchPanelActive = false;
		} else { 
			researchPanel.SetActive (true);
			GameManager.Instance.isResearchPanelActive = true;
			researchScrollMenuControl.Instance.ReadActiveResearchs ();
		}
	}

	public void disablePlacementMode() { //disable placementmode
		GameManager.Instance.isPlacementModeActive = false;
		GameManager.Instance.disablePlacementModes ();
	}
}
