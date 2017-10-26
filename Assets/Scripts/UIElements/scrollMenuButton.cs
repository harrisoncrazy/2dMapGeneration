using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollMenuButton : MonoBehaviour {

	[SerializeField] private Button button;
	[SerializeField] private Text infoText;
	[SerializeField] private Text costText;
	[SerializeField] private string buildingType;

	public resourceBuildingClass.resourceTypeCost[] buildingCosts;

	public void setButtonText(string inputText) {
		button.GetComponentInChildren<Text> ().text = inputText;
	}

	public void setInfoText(string inputText) {
		infoText.text = inputText;
	}

	public void setCostText(string inputText) {
		costText.text = inputText;
	}

	public void setBuildingType(string type) {
		buildingType = type;
	}

	public void OnClick() {
		if (GameManager.Instance.isPlacementModeActive == true) {
			inputHandler.Instance.disablePlacementMode ();
		} else {
			if (resourceBuildingClass.readResourcesForPlacingBuilding (buildingCosts)) {
				GameManager.Instance.isPlacementModeActive = true;
				GameManager.Instance.enablePlacementMode (buildingType);
			} else {
				Debug.Log ("Insufficent Resources");
			}
		}
	}

	void readBuildingType() {

	}
}
