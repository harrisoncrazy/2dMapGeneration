using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class defaultBuilding : MonoBehaviour {

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

	private float timer = 2.0f;
	private bool fixedTiles = false;

	public float resourceOutTick = 5.0f;
	public GameObject resourceDeliveryNodePrefab;
	public List<baseGridPosition> pathToBase;

	public defaultBuilding() {
		tileTitle = "default";
		tileDescription = "default description here";
	}

	// Use this for initialization
	protected virtual void Start () {

		findTileInfoPanelThings ();

		worldPosition = gameObject.transform.position;

		this.GetComponent<baseGridPosition>().fixAdjacentTilesAdjacency ();
	}
		
	// Update is called once per frame
	protected virtual void Update () {
		if (fixedTiles == false) {//fixing the adjacent tiles after spawning the building (delay needed for init)
			timer -= Time.deltaTime;
			if (timer <= 0) {
				try {
				this.GetComponent<baseGridPosition> ().findAdjacentTiles ();
				this.GetComponent<baseGridPosition> ().fixAdjacentTilesAdjacency ();
				fixedTiles = true;
				}
				catch (Exception e) {
					Debug.Log (e);
					fixedTiles = true;
				}
			}
		}

		if (GameManager.Instance.selectedTile != null) {
			trSelect = GameManager.Instance.selectedTile;
		}
		//Swapping selected tile
		if (selected == true && this.transform != trSelect) { //If the currently selected tile and the transform do not equal the new selection, deselect this tile
			selected = false;
			tileOutlineSprite.SetActive (false);
		}
	}

	protected virtual void SpawnResourceDeliveryNode(string type, float amount) {
		resourceDelivery resourceNode = ((GameObject)Instantiate (resourceDeliveryNodePrefab, transform.position, Quaternion.Euler (new Vector3 ()))).GetComponent<resourceDelivery> ();
		resourceNode.sourceBuilding = this.gameObject.GetComponent<baseGridPosition> ();
		resourceNode.toLocation = GameObject.Find ("homeBase").GetComponent<baseGridPosition> ();

		resourceNode.nodeDelivery.Type = type;
		resourceNode.nodeDelivery.Amount = amount;

		resourceNode.pathToFollow = pathToBase;
	}

	protected virtual void OnMouseDown() {
		this.GetComponent<baseGridPosition>().setAdjArrayVals ();
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

	protected virtual void toggleInfoPanel () {//toggling on or off the info panel, locking or unlocking the camera to the building
		if (isInfoPanelActive == true && GameManager.Instance.isBuildingSelected == false) {
			tileInfoPanel.SetActive (false);
			CameraController.Instance.setCameraPos (worldPosition.x, worldPosition.y);
			isInfoPanelActive = false;
		} else {
			tileInfoPanel.SetActive (true);
			CameraController.Instance.setCameraPos (worldPosition.x, worldPosition.y);
			isInfoPanelActive = true;
		}
	}

	void findTileInfoPanelThings() {//grabbing the tile info panels
		tileInfoPanel = GameManager.Instance.tileInfoPanel;
		tileText = GameManager.Instance.tileText;
		descriptionText = GameManager.Instance.descriptionText;
	}

	protected virtual void setInfoPanelText(string tileName, string tileDescription) {
		tileText.text = tileName;
		descriptionText.text = tileDescription;
	}

	protected virtual void findPathToBase() {

	}
}
