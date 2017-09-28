using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHandler : defaultBuilding {

	public static baseHandler Instance;

	public baseHandler() {
		tileTitle = "Home Base";
		tileDescription = "The main center of your city! Everything starts here!";
	}

	// Use this for initialization
	protected override void Start () {
		Instance = this;
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

	public override void setAdjacentTilesArray ()
	{
		base.setAdjacentTilesArray ();
	}
}
