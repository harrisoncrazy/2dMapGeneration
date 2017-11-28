using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchScrollMenuControl : MonoBehaviour {

	public static researchScrollMenuControl Instance;

	[SerializeField] private GameObject buttonTemplate;
	[SerializeField] private GameObject scrollContent;

	void Start() {
		Instance = this;
	}

	void GenButton(string buttonName, string buttonDescription, int buttonCost, string type) {
		GameObject button = Instantiate (buttonTemplate) as GameObject;
		button.SetActive (true);

		button.GetComponent<researchScrollMenuButton> ().setButtonText (buttonName);
		button.GetComponent<researchScrollMenuButton> ().setInfoText(buttonDescription);
		button.GetComponent<researchScrollMenuButton> ().setCostText (buttonCost);
		button.GetComponent<researchScrollMenuButton> ().setResearchName (type);

		button.transform.SetParent (buttonTemplate.transform.parent, false);

	}

	public void ReadActiveResearchs() {
		clearOldButtons ();

		for (int i = 0; i < researchCosts.Instance.enabledResearch.Length; i++) {
			if (researchCosts.Instance.enabledResearch [i].techName != null) {
				if (researchCosts.Instance.enabledResearch [i].isAvailbile == true && researchCosts.Instance.enabledResearch[i].hasBeenPurchased == false) {
					GenButton (researchCosts.Instance.enabledResearch [i].techDisplayName, researchCosts.Instance.enabledResearch [i].techDescription, researchCosts.Instance.enabledResearch [i].techCost, researchCosts.Instance.enabledResearch [i].techName);
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
