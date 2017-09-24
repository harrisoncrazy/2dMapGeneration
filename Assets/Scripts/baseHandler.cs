using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseHandler : MonoBehaviour {

	public tileHandler.gridPosition basePosition = new tileHandler.gridPosition();

	public GameObject tileOutlineSprite;

	//Tile selection values
	private Transform trSelect = null;
	private bool selected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.selectedTile != null) {
			trSelect = GameManager.Instance.selectedTile;
		}
		//Swapping selected tile
		if (selected == true && this.transform != trSelect) { //If the currently selected tile and the transform do not equal the new selection, deselect this tile
			selected = false;
			tileOutlineSprite.SetActive (false);
		}
	}

	public void OnMouseDown() {
		if (selected && transform == trSelect) {
			selected = false;
			trSelect = null;
			tileOutlineSprite.SetActive (false);
		} else {
			selected = true;
			GameManager.Instance.selectedTile = this.transform;
			tileOutlineSprite.SetActive (true);
		}
	}
}
