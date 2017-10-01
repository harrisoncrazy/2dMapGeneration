using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class defaultBuilding : MonoBehaviour {

	public tileHandler.gridPosition tilePosition = new tileHandler.gridPosition();

	public GameObject tileOutlineSprite;

	public Vector3 worldPosition;

	//pop up panel values
	private GameObject tileInfoPanel;
	private Text tileText;
	private Text descriptionText;

	//title and description
	public string tileTitle;
	public string tileDescription;

	//Tile selection values
	private Transform trSelect = null;
	private bool selected = false;

	private bool isInfoPanelActive = false;

	//adjacent tiles
	public GameObject topLeft;
	public GameObject topRight;
	public GameObject Right;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject Left;
	public GameObject[] adjacentTiles;

	public defaultBuilding() {
		tileTitle = "default";
		tileDescription = "default description here";
	}

	// Use this for initialization
	protected virtual void Start () {
		adjacentTiles = new GameObject[6];

		findTileInfoPanelThings ();

		worldPosition = gameObject.transform.position;

		fixAdjacentTilesAdjacency ();

		setAdjArrayVals ();
	}

	protected virtual void setAdjArrayVals() {
		adjacentTiles [0] = topLeft;
		adjacentTiles [1] = topRight;
		adjacentTiles [2] = Right;
		adjacentTiles [3] = bottomRight;
		adjacentTiles [4] = bottomLeft;
		adjacentTiles [5] = Left;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (GameManager.Instance.selectedTile != null) {
			trSelect = GameManager.Instance.selectedTile;
		}
		//Swapping selected tile
		if (selected == true && this.transform != trSelect) { //If the currently selected tile and the transform do not equal the new selection, deselect this tile
			selected = false;
			tileOutlineSprite.SetActive (false);
		}
	}

	protected virtual void OnMouseDown() {
		setAdjArrayVals ();
		if (selected && transform == trSelect) {
			selected = false;
			trSelect = null;
			tileOutlineSprite.SetActive (false);
			GameManager.Instance.isBuildingSelected = false;
		} else {
			selected = true;
			GameManager.Instance.selectedTile = this.transform;
			GameManager.Instance.isBuildingSelected = true;
			tileOutlineSprite.SetActive (true);
		}

		toggleInfoPanel ();
	}

	protected virtual void toggleInfoPanel () {
		if (isInfoPanelActive == true && GameManager.Instance.isBuildingSelected == false) {
			tileInfoPanel.SetActive (false);
			CameraMovement.Instance.centerCam (worldPosition);
			isInfoPanelActive = false;
		} else {
			tileInfoPanel.SetActive (true);
			CameraMovement.Instance.centerCam (worldPosition);
			isInfoPanelActive = true;
		}
	}

	void findTileInfoPanelThings() {
		tileInfoPanel = GameManager.Instance.tileInfoPanel;
		tileText = GameManager.Instance.tileText;
		descriptionText = GameManager.Instance.descriptionText;
	}

	protected virtual void setInfoPanelText(string tileName, string tileDescription) {
		tileText.text = tileName;
		descriptionText.text = tileDescription;
	}

	protected virtual void fixAdjacentTilesAdjacency() { //when placing a new building, add it to the adjacent tiles' adjacency
		if (checkIfBuildingOrTile (topLeft)) { //fixing top left tile
			topLeft.GetComponent<tileHandler> ().bottomRight = this.gameObject;
		} else {
			topLeft.GetComponent<defaultBuilding> ().bottomRight = this.gameObject;
		}

		if (checkIfBuildingOrTile (topRight)) { //fixing top right tile
			topRight.GetComponent<tileHandler> ().bottomLeft = this.gameObject;
		} else {
			topRight.GetComponent<defaultBuilding> ().bottomLeft = this.gameObject;
		}

		if (checkIfBuildingOrTile (Right)) { //fixing top left tile
			Right.GetComponent<tileHandler> ().Left = this.gameObject;
		} else {
			Right.GetComponent<defaultBuilding> ().Left = this.gameObject;
		}

		if (checkIfBuildingOrTile (bottomRight)) { //fixing top left tile
			bottomRight.GetComponent<tileHandler> ().topLeft = this.gameObject;
		} else {
			bottomRight.GetComponent<defaultBuilding> ().topLeft = this.gameObject;
		}

		if (checkIfBuildingOrTile (bottomLeft)) { //fixing top left tile
			bottomLeft.GetComponent<tileHandler> ().topRight = this.gameObject;
		} else {
			bottomLeft.GetComponent<defaultBuilding> ().topRight = this.gameObject;
		}

		if (checkIfBuildingOrTile (Left)) { //fixing top left tile
			Left.GetComponent<tileHandler> ().Right = this.gameObject;
		} else {
			Left.GetComponent<defaultBuilding> ().Right = this.gameObject;
		}
	}

	bool checkIfBuildingOrTile(GameObject obj) {
		if (obj.GetComponent<tileHandler> () != null) {
			return true;
		} else if (obj.GetComponent<defaultBuilding> () != null) {
			return false;
		} else {
			return false;
		}
	}
}
