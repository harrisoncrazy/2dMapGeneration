using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unlockedTechPopup : MonoBehaviour {

	public string techDescription;

	public Text descriptionText;

	public void setText() {
		descriptionText.text = techDescription;
	}
}
