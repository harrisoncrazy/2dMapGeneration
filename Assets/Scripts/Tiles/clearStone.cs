using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearStone : defaultBuilding {

	//Basic empty building script for clearing stone tiles

	public clearStone() {
		tileTitle = "Clear Rocks";
		tileDescription = "";
	}

	new void Start() {
		placeableTiles = enabledBuildingList.Instance.stoneClear.placeableTileTypes;

		if (isHoverMode == false) {
			Destroy (this);
		}
	}
}
