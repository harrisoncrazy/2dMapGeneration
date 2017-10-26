using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollMenuControl : MonoBehaviour {

	public static scrollMenuControl Instance;

	[SerializeField] private GameObject buttonTemplate;
	[SerializeField] private GameObject scrollContent;

	void Start() {
		Instance = this;
	}

	void GenButton(string buttonName, string buttonDescription, string buttonCost, string type, resourceBuildingClass.resourceTypeCost[] costs) {
		GameObject button = Instantiate (buttonTemplate) as GameObject;
		button.SetActive (true);

		button.GetComponent<scrollMenuButton> ().setButtonText (buttonName);
		button.GetComponent<scrollMenuButton> ().setInfoText(buttonDescription);
		button.GetComponent<scrollMenuButton> ().setCostText (buttonCost);
		button.GetComponent<scrollMenuButton> ().setBuildingType (type);

		button.GetComponent<scrollMenuButton> ().buildingCosts = costs;

		button.transform.SetParent (buttonTemplate.transform.parent, false);

	}

	public void ReadActiveBuildings() {
		clearOldButtons ();

		for (int i = 0; i < enabledBuildingList.Instance.availableBuildings.Length; i++) {
			if (enabledBuildingList.Instance.availableBuildings [i].buildingName != null) {
				if (enabledBuildingList.Instance.availableBuildings [i].isEnabled) {
					GenButton (enabledBuildingList.Instance.availableBuildings [i].buildingName, enabledBuildingList.Instance.availableBuildings[i].builidingDescription,
						enabledBuildingList.Instance.availableBuildings[i].returnCostsAsString(), enabledBuildingList.Instance.availableBuildings[i].buildingType, enabledBuildingList.Instance.availableBuildings[i].costTotals);
				}
			}
		}
	}

	public void clearOldButtons() {
		for (int i = scrollContent.transform.childCount - 1; i >= 0; i--) {
			GameObject childButton = scrollContent.transform.GetChild (i).gameObject;
			if (childButton.name != "ButtonTemplate") {
				Destroy (childButton);
			}
		}
	}
}
