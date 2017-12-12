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
			if (GameManager.Instance.isResearchPanelActive == false) {//opening builing panel
				if (Input.GetKeyDown (KeyCode.B)) {
					toggleBuildingPanel ();
				}
			}
			if (researchHandler.Instance.isResearchEnabled == true) {
				if (GameManager.Instance.isBuildingPanelActive == false) {//opening research panel
					if (Input.GetKeyDown (KeyCode.R)) {
						toggleResearchPanel ();
					}
				}
			}
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

	public void toggleResearchPanel() {//toggling on or off the research panel
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
