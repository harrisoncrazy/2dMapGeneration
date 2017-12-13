using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class defaultBuilding : MonoBehaviour {

	//default buildign script

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

	//Resource deliver node values
	public float resourceOutTick = 5.0f;
	public GameObject resourceDeliveryNodePrefab;
	public List<baseGridPosition> pathToBase;


	public bool isHoverMode = false;

	public string[] placeableTiles = new string[5];

	//upgrade variables
	public bool isUpgradeable = false;
	public GameObject upgradeTile;
	public Button upgradeButton;
	public bool canBeUpgraded;

	private bool isDisabled = false;
	public GameObject ThreeDObjects;

	//default constructor
	public defaultBuilding() {
		tileTitle = "default";
		tileDescription = "default description here";
	}

	// Use this for initialization
	protected virtual void Start () {
		findTileInfoPanelThings ();

		worldPosition = gameObject.transform.position;

		upgradeButton = GameManager.Instance.upgradeButton;

		if (isHoverMode == false)
			this.GetComponent<baseGridPosition> ().fixAdjacentTilesAdjacency ();

		readPlaceTiles ();
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
				} catch (Exception e) {
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
		if (isDisabled == false) {
			resourceDelivery resourceNode = ((GameObject)Instantiate (resourceDeliveryNodePrefab, transform.position, Quaternion.Euler (new Vector3 ()))).GetComponent<resourceDelivery> ();
			resourceNode.sourceBuilding = this.gameObject.GetComponent<baseGridPosition> ();//spawning the delivery node, and inputing its route location
			resourceNode.toLocation = GameObject.Find ("homeBase").GetComponent<baseGridPosition> ();

			resourceBuildingClass.resourceTypeCost temp = new resourceBuildingClass.resourceTypeCost ();
			temp.resourceType = type;
			temp.cost = amount;

			resourceNode.nodeDelivery.Add (temp);

			resourceNode.pathToFollow = pathToBase;
		}
	}

	protected virtual void OnMouseDown() {
		if (UIHoverListener.Instance.isOverUI == false) {
			if (GameManager.Instance.isPlacementModeActive == false) {
				this.GetComponent<baseGridPosition> ().setAdjArrayVals ();
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
		}

	}

	protected virtual void toggleInfoPanel () {//toggling on or off the info panel, locking or unlocking the camera to the building
		upgradeButton.onClick.RemoveAllListeners();
		upgradeButton.interactable = false;

		if (isInfoPanelActive == true && GameManager.Instance.isBuildingSelected == false) {
			tileInfoPanel.SetActive (false);
			CameraController.Instance.setCameraPos (worldPosition.x, worldPosition.y);
			isInfoPanelActive = false;

			//reseting button
			upgradeButton.onClick.RemoveListener (upgradeBuilding);
			upgradeButton.interactable = false;
		} else {
			tileInfoPanel.SetActive (true);
			CameraController.Instance.setCameraPos (worldPosition.x, worldPosition.y);
			isInfoPanelActive = true;
			inputHandler.Instance.buildingPanel.SetActive (false);
			GameManager.Instance.isBuildingPanelActive = false;

			if (isUpgradeable) {
				upgradeButton.interactable = true;
				//setting the onClick of the upgrade button
				upgradeButton.onClick.AddListener (upgradeBuilding);
			}
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

	public void readPlaceTiles() {//reading the building's placement tiles from the building construction script
		for (int i = 0; i < enabledBuildingList.Instance.availableBuildings.Length; i++) {
			if (enabledBuildingList.Instance.availableBuildings [i].buildingName == tileTitle) {
				placeableTiles = enabledBuildingList.Instance.availableBuildings [i].placeableTileTypes;
			}
		}
	}

	void disableTile() {//disabling the building so its invisible, but doesnt dissapear from the world for pathfinding purposes
		this.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
		if (ThreeDObjects != null) {
			ThreeDObjects.SetActive (false);
		}
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 0.1f);
		this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

		this.gameObject.GetComponent<baseGridPosition> ().hexOutline.SetActive (false);
		this.gameObject.GetComponent<baseGridPosition> ().enabled = false;

		//disabling highlight on disabled tile
		selected = false;
		trSelect = null;
		tileOutlineSprite.SetActive (false);
		GameManager.Instance.isBuildingSelected = false;

		isDisabled = true;
	}

	public void upgradeBuilding() {
		defaultBuilding newBuilding = ((GameObject)Instantiate (upgradeTile, this.transform.position, Quaternion.Euler (new Vector3 ()))).GetComponent<defaultBuilding> ();

		resourceBuildingClass.resourceTypeCost[] origCost = new resourceBuildingClass.resourceTypeCost[16];
		resourceBuildingClass.resourceTypeCost[] newCost = new resourceBuildingClass.resourceTypeCost[16];
		for (int i = 0; enabledBuildingList.Instance.availableBuildings.Length > i; i++) {//searching thru the list of buildings in order to grab the cost for the current building and the new building
			if (enabledBuildingList.Instance.availableBuildings [i].buildingName == tileTitle) {//finding original building cost
				origCost = enabledBuildingList.Instance.availableBuildings [i].costTotals;
			}

			if (enabledBuildingList.Instance.availableBuildings [i].buildingName == newBuilding.tileTitle) {//finding upgrade building cost
				newCost = enabledBuildingList.Instance.availableBuildings [i].costTotals;
			}
		}

		resourceBuildingClass.resourceTypeCost[] tempUpgradeCost = resourceBuildingClass.compareUpgradeCost (origCost, newCost); 

		if (resourceBuildingClass.readResourcesForPlacingBuilding (tempUpgradeCost)) {//checking upgrade cost, and seeing if there is enough resources to pay for it
			//if enough resources
			resourceBuildingClass.removeResourcesFromPlacement (tempUpgradeCost);
		} else { //if not enough resources
			Destroy(newBuilding.gameObject);
			Debug.Log ("not enough resources");
			return;
		}



		pathfindingManager.Instance.FindPath (generationManager.Instance.map [this.GetComponent<baseGridPosition> ().mapPosition.X] [this.GetComponent<baseGridPosition> ().mapPosition.Y].GetComponent<baseGridPosition> (), GameObject.Find ("homeBase").GetComponent<baseGridPosition> ());
		newBuilding.pathToBase = pathfindingManager.Instance.GetPath ();
		newBuilding.pathToBase [0] = newBuilding.GetComponent<baseGridPosition> ();
		newBuilding.pathToBase [1].PathFrom = newBuilding.gameObject;

		newBuilding.name = tileTitle;
		newBuilding.GetComponent<baseGridPosition> ().mapPosition = this.GetComponent<baseGridPosition> ().mapPosition;

		newBuilding.GetComponent<baseGridPosition> ().topLeft = this.GetComponent<baseGridPosition> ().topLeft;
		newBuilding.GetComponent<baseGridPosition> ().topRight = this.GetComponent<baseGridPosition> ().topRight;
		newBuilding.GetComponent<baseGridPosition> ().Right = this.GetComponent<baseGridPosition> ().Right;
		newBuilding.GetComponent<baseGridPosition> ().bottomRight = this.GetComponent<baseGridPosition> ().bottomRight;
		newBuilding.GetComponent<baseGridPosition> ().bottomLeft = this.GetComponent<baseGridPosition> ().bottomLeft;
		newBuilding.GetComponent<baseGridPosition> ().Left = this.GetComponent<baseGridPosition> ().Left;

		generationManager.Instance.map [this.GetComponent<baseGridPosition> ().mapPosition.X] [this.GetComponent<baseGridPosition> ().mapPosition.Y] = newBuilding.gameObject;

		//DISABLING INFO PANEL
		tileInfoPanel.SetActive (false);
		CameraController.Instance.setCameraPos (worldPosition.x, worldPosition.y);
		isInfoPanelActive = false;
		//reseting button
		upgradeButton.onClick.RemoveListener (upgradeBuilding);
		upgradeButton.interactable = false;

		disableTile ();
	}
}
