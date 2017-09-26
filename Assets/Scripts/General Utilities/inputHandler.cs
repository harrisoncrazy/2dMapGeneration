using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputHandler : MonoBehaviour {

	public static inputHandler Instance;

	private bool ifPlacementModeActive;

	void Start() {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (ifPlacementModeActive == false) {
			if (Input.GetKeyDown (KeyCode.R)) {
				//set gamemanager building bool to true
				GameManager.Instance.placingWoodGatherer = true;
				ifPlacementModeActive = true;
			} 

			if (Input.GetKeyDown (KeyCode.T)) {
				//set gamemanager building bool to true
				GameManager.Instance.placingFoodGatherer = true;
				ifPlacementModeActive = true;
			} 

			if (Input.GetKeyDown (KeyCode.Y)) {
				//set gamemanager building bool to true
				GameManager.Instance.placingStoneGatherer = true;
				ifPlacementModeActive = true;
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
