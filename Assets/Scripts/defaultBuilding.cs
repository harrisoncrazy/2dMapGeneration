using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class defaultBuilding : MonoBehaviour {

	public tileHandler.gridPosition basePosition = new tileHandler.gridPosition();

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

	public defaultBuilding() {
		tileTitle = "default";
		tileDescription = "default description here";
	}

	// Use this for initialization
	protected virtual void Start () {
		findTileInfoPanelThings ();

		worldPosition = gameObject.transform.position;
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
		} else {
			selected = true;
			GameManager.Instance.selectedTile = this.transform;
			tileOutlineSprite.SetActive (true);
		}

		toggleInfoPanel ();
	}

	protected virtual void toggleInfoPanel () {
		if (isInfoPanelActive == true) {
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
		tileInfoPanel = GameObject.Find ("buildingInfoPanel");
		tileText = GameObject.Find ("titleText").GetComponent<Text> ();
		descriptionText = GameObject.Find ("descriptionText").GetComponent<Text> ();

		tileInfoPanel.SetActive (false);
	}

	protected virtual void setInfoPanelText(string tileName, string tileDescription) {
		tileText.text = tileName;
		descriptionText.text = tileDescription;
	}
}
