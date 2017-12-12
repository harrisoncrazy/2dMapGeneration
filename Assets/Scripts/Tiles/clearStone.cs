using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearStone : defaultBuilding {

	public clearStone() {
		tileTitle = "Clear Rocks";
		tileDescription = "";
	}

	void Start() {
		placeableTiles = enabledBuildingList.Instance.stoneClear.placeableTileTypes;

		if (isHoverMode == false) {
			Destroy (this);
		}
	}
}
