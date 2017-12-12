using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearForest : defaultBuilding {

	//empty shell for clearing forest

	public clearForest() {
		tileTitle = "Clear Forest";
		tileDescription = "";
	}

	void Start() {
		placeableTiles = enabledBuildingList.Instance.forestClear.placeableTileTypes;

		if (isHoverMode == false) {
			Destroy (this);
		}
	}
}
