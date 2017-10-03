using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	//pathfinding values
	public baseGridPosition previousCell, searchFromCell;

	public Transform selectedTile;

	public bool isBuildingSelected = false;

	//building stuff
	public GameObject woodGatherPrefab;

	//building bools
	public bool placingWoodGatherer;/* REPLACE WTIH ARRAY OF BOOLS */
	public bool placingFoodGatherer;
	public bool placingStoneGatherer;

	//pop up panel values
	public GameObject tileInfoPanel;
	public Text tileText;
	public Text descriptionText;

	private float timerTick = 1.0f;

	// Use this for initialization
	void Start () {
		Instance = this;
		GrabBuildingInfoPanel ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		timerTick -= Time.deltaTime;
		if (timerTick < 0) {
			updateResourceTotals ();
			timerTick = 1.0f;
		}
	}

	private void updateResourceTotals() {
		resourceManager.Instance.woodResourceTick ();
	}

	public void disablePlacementModes() { //run thru the array of bools and disable them all
		placingWoodGatherer = false;
	}

	public bool placingWoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		woodGatherer woodGather = ((GameObject)Instantiate (GameManager.Instance.woodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<woodGatherer> ();
		//GameObject homeCollider = ((GameObject)Instantiate (homeBaseCollider, pos, Quaternion.Euler (new Vector3 ())));
		woodGather.name = "woodGathererBuilding";
		woodGather.GetComponent<baseGridPosition>().mapPosition.X = x;
		woodGather.GetComponent<baseGridPosition>().mapPosition.Y = y;

		woodGather.GetComponent<baseGridPosition>().topLeft = adjArray [0];
		woodGather.GetComponent<baseGridPosition>().topRight = adjArray [1];
		woodGather.GetComponent<baseGridPosition>().Right = adjArray [2];
		woodGather.GetComponent<baseGridPosition>().bottomRight = adjArray [3];
		woodGather.GetComponent<baseGridPosition>().bottomLeft = adjArray [4];
		woodGather.GetComponent<baseGridPosition>().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = woodGather.gameObject;
		return true;
	}

	private void GrabBuildingInfoPanel() {
		tileInfoPanel = GameObject.Find ("buildingInfoPanel");
		tileText = GameObject.Find ("titleText").GetComponent<Text> ();
		descriptionText = GameObject.Find ("descriptionText").GetComponent<Text> ();

		tileInfoPanel.SetActive (false);
	}
}
