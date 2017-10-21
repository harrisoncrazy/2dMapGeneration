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
	public GameObject stoneGatherPrefab;
	public GameObject foodGatherPrefab;

	//building bools
	public bool placingWoodGatherer;/* REPLACE WTIH ARRAY OF BOOLS */
	public bool placingFoodGatherer;
	public bool placingStoneGatherer;

	//pop up panel values
	public GameObject tileInfoPanel;
	public Text tileText;
	public Text descriptionText;

	private float timerTick = 1.0f;

	private int manpowerTickDown = 5;//delaying manpower add

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
		resourceManager.Instance.stoneResourceTick ();
		resourceManager.Instance.foodResourceTick ();
		manpowerTickDown--;
		if (manpowerTickDown <= 0) {
			resourceManager.Instance.manpowerResourceTick ();
			manpowerTickDown = 5;
		}
	}

	public void disablePlacementModes() { //run thru the array of bools and disable them all
		placingWoodGatherer = false;
		placingStoneGatherer = false;
		placingFoodGatherer = false;
	}

	public bool placingWoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		woodGatherer woodGather = ((GameObject)Instantiate (woodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<woodGatherer> ();

		woodGather.name = "woodGathererBuilding";
		woodGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		woodGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		woodGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		woodGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		woodGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		woodGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		woodGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		woodGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = woodGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.woodGatherBuidlingCost);

		return true;
	}

	public bool placingStoneGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		stoneGatherer stoneGather = ((GameObject)Instantiate (stoneGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<stoneGatherer> ();
		//GameObject homeCollider = ((GameObject)Instantiate (homeBaseCollider, pos, Quaternion.Euler (new Vector3 ())));
		stoneGather.name = "stoneGathererBuilding";
		stoneGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		stoneGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		stoneGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		stoneGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		stoneGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		stoneGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		stoneGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		stoneGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = stoneGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.stoneGatherBuildingCost);

		return true;
	}

	public bool placingFoodGathererTile(int x, int y, Vector3 pos, GameObject[] adjArray) {
		foodGatherer foodGather = ((GameObject)Instantiate (foodGatherPrefab, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<foodGatherer> ();
		//GameObject homeCollider = ((GameObject)Instantiate (homeBaseCollider, pos, Quaternion.Euler (new Vector3 ())));
		foodGather.name = "foodGathererBuilding";
		foodGather.GetComponent<baseGridPosition> ().mapPosition.X = x;
		foodGather.GetComponent<baseGridPosition> ().mapPosition.Y = y;

		foodGather.GetComponent<baseGridPosition> ().topLeft = adjArray [0];
		foodGather.GetComponent<baseGridPosition> ().topRight = adjArray [1];
		foodGather.GetComponent<baseGridPosition> ().Right = adjArray [2];
		foodGather.GetComponent<baseGridPosition> ().bottomRight = adjArray [3];
		foodGather.GetComponent<baseGridPosition> ().bottomLeft = adjArray [4];
		foodGather.GetComponent<baseGridPosition> ().Left = adjArray [5];

		generationManager.Instance.map [x] [y] = foodGather.gameObject;

		resourceBuildingClass.removeResourcesFromPlacement (buildingCosts.Instance.foodGatherBuildingCost);

		return true;
	}

	private void GrabBuildingInfoPanel() {
		tileInfoPanel = GameObject.Find ("buildingInfoPanel");
		tileText = GameObject.Find ("titleText").GetComponent<Text> ();
		descriptionText = GameObject.Find ("descriptionText").GetComponent<Text> ();

		tileInfoPanel.SetActive (false);
	}
}
