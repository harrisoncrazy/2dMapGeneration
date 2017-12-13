using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leanToHouse : defaultBuilding {

	private float manpowerReturn = 0.5f;
	private int valToAddMaxManpower = 2;

	public leanToHouse() {
		tileTitle = "Lean To House";
		tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn. \nAdding: " + valToAddMaxManpower + " to manpower cache.";
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

		tileDescription = "A basic house for your citizens" + "\nProviding: " + manpowerReturn + " manpower per turn.";

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
