using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiHouse : defaultBuilding {

	private float manpowerReturn = 6.0f;
	private int valToAddMaxManpower = 12;

	public multiHouse() {
		tileTitle = "Multi-Story House";
		tileDescription = "A 2 floor house for your citizens." + "\nProviding: " + manpowerReturn + " manpower per turn. \nAdding: " + valToAddMaxManpower + " to manpower cache.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (isHoverMode == false) {
			StartCoroutine ("delay");
		}
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);

		tileDescription = "A 2 floor house for your citizens." + "\nProviding: " + manpowerReturn + " manpower per turn. \nAdding: " + valToAddMaxManpower + " to manpower cache.";

		resourceManager.Instance.addManpowerResource (manpowerReturn);
		resourceManager.Instance.addToManpowerTotal (valToAddMaxManpower);
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		//tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
