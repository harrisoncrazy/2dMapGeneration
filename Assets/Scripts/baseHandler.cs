using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHandler : defaultBuilding {

	public baseHandler() {
		tileTitle = "Home Base";
		tileDescription = "The main center of your city! Everything starts here!";
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
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
