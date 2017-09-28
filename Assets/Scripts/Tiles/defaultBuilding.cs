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
		findTileInfoPanelThings ();

		worldPosition = gameObject.transform.position;

		//setting tiles to numbers
		adjacentTiles = new GameObject[6];

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

	public virtual void setAdjacentTilesArray() {
		adjacentTiles [0] = topLeft;
		adjacentTiles [1] = topRight;
		adjacentTiles [2] = Right;
		adjacentTiles [3] = bottomRight;
		adjacentTiles [4] = bottomLeft;
		adjacentTiles [5] = Left;
	}
}
