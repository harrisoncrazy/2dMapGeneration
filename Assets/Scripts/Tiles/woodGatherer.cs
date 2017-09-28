using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodGatherer : defaultBuilding {

	public woodGatherer() {
		tileTitle = "Wood Gatherer";
		tileDescription = "Brings wood into your resources!";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		resourceManager.Instance.addWoodResource (0.5f);
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update ();
	}

	protected override void OnMouseDown() {
		base.OnMouseDown ();
		base.setInfoPanelText (tileTitle, tileDescription);
	}
}
