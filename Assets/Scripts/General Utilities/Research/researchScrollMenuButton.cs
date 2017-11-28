﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class researchScrollMenuButton : MonoBehaviour {

	[SerializeField] private Button button;
	[SerializeField] private Text infoText;
	[SerializeField] private Text costText;
	[SerializeField] private string researchName;
	[SerializeField] private int techCost;

	public void setButtonText(string inputText) {
		button.GetComponentInChildren<Text> ().text = inputText;
	}

	public void setInfoText(string inputText) {
		infoText.text = inputText;
	}

	public void setCostText(int cost) {
		costText.text = "Cost: " + cost;
		techCost = cost;
	}

	public void setResearchName(string type) {
		researchName = type;
	}

	public void OnClick() {
		researchCosts.Instance.tryResearch (researchName, techCost);
	}
}
