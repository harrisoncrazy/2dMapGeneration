using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatherNode : defaultBuilding {

	public gatherNode() {
		tileTitle = "Basic Gather Point";
		tileDescription = "Acts as a central location for your resources to gather.";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		StartCoroutine ("delay");
	}

	void setTileDescription() {
		tileDescription = "Acts as a central location for your resources to gather.";
	}

	IEnumerator delay() {
		yield return new WaitForSeconds (0.15f);
		setTileDescription ();
	}

	// Update is called once per frame
	protected override void Update() {
		if (isHoverMode == false) {
			base.Update ();

		} else if (isHoverMode == true) {

		}
	}

	protected override void OnMouseDown() {
		if (isHoverMode == false) {
			base.OnMouseDown ();
			//tileDescription = "Brings wood into your resources!" + "\nProviding: " + woodReturn + " wood per turn.";
			base.setInfoPanelText (tileTitle, tileDescription);
		}
	}
}
