using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchHandler : MonoBehaviour {

	public static researchHandler Instance;

	public bool isResearchEnabled = false;

	public GameObject researchUIParent;
	public GameObject researchStartPanel;

	public GameObject techUnlockPopup;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startResearch() {
		isResearchEnabled = true;

		Instantiate ((GameObject)researchStartPanel, researchUIParent.transform.position, Quaternion.Euler (new Vector3 ()), researchUIParent.transform);

		inputHandler.Instance.buildingPanel.SetActive (false);
		GameManager.Instance.isBuildingPanelActive = false;
	}

	public void researchUnlockPopup(string info) {
		unlockedTechPopup popUp = ((GameObject)Instantiate (techUnlockPopup, researchUIParent.transform.position, Quaternion.Euler (new Vector3 ()), researchUIParent.transform)).GetComponent<unlockedTechPopup> ();
		popUp.techDescription = info;
		popUp.setText ();
	}

	public void tierOneLumber() { //enables an upgraded lumber gatherer, allows for the clearing of forest tiles
		enabledBuildingList.Instance.forestClear.isEnabled = true;
		enabledBuildingList.Instance.basicLumberer.isEnabled = true;
		enabledBuildingList.Instance.woodGather.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		//finds all active wood gatherer's, and allows them to be upgraded
		foreach (woodGatherer gameObj in GameObject.FindObjectsOfType<woodGatherer>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneStone() {//enables an upgraded stone gatherer, allows for the clearing of stone tiles
		enabledBuildingList.Instance.stoneClear.isEnabled = true;
		enabledBuildingList.Instance.basicQuarry.isEnabled = true;
		enabledBuildingList.Instance.stoneGather.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		//finds all active stone gatherer's, and allows them to be upgraded
		foreach (stoneGatherer gameObj in GameObject.FindObjectsOfType<stoneGatherer>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneFood() {//enables an upgraded food gatherer, brings in lots of food slower than usual
		enabledBuildingList.Instance.basicFarm.isEnabled = true;
		enabledBuildingList.Instance.foodGather.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		//finds all active food gatherer's, and allows them to be upgraded
		foreach (foodGatherer gameObj in GameObject.FindObjectsOfType<foodGatherer>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneHousing() {//enables wooden houses, adds more to total manpower, brings in more manpower per tick
		enabledBuildingList.Instance.woodHouse.isEnabled = true;
		enabledBuildingList.Instance.leanToHouse.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		//finds all active lean tos and allows them to be upgraded
		foreach (leanToHouse gameObj in GameObject.FindObjectsOfType<leanToHouse>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneResearch() {//enables chiefs hut
		enabledBuildingList.Instance.chiefsHut.isEnabled = true;
		enabledBuildingList.Instance.wiseWomanHut.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		//finds all active wise women huts and allows them to be upgraded
		foreach (wiseWomanHut gameObj in GameObject.FindObjectsOfType<wiseWomanHut>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneOre() {//enables ore miner
		enabledBuildingList.Instance.basicMine.isEnabled = true;
		enabledBuildingList.Instance.basicBlacksmith.isEnabled = true;
		enabledBuildingList.Instance.setArray ();
	}

	public void tierOneGatherNode() {
		enabledBuildingList.Instance.gatherNode.isEnabled = true;
		enabledBuildingList.Instance.setArray ();
	}
}
