﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchHandler : MonoBehaviour {

	//TODO add way to make higher tier upgrades available if upgrading from an even lower tier

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


	//BRONZE ERA
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

	public void tierOneOre() {//enables ore mining and refinement
		enabledBuildingList.Instance.basicMine.isEnabled = true;
		enabledBuildingList.Instance.basicBlacksmith.isEnabled = true;
		enabledBuildingList.Instance.setArray ();
	}


	//MEDIEVAL ERA
	public void tierTwoOre() {
		enabledBuildingList.Instance.advancedMine.isEnabled = true;
		enabledBuildingList.Instance.basicMine.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (basicMine gameObj in GameObject.FindObjectsOfType<basicMine>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierTwoWood() {
		enabledBuildingList.Instance.sawmill.isEnabled = true;
		enabledBuildingList.Instance.basicLumberer.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (basicLumberer gameObj in GameObject.FindObjectsOfType<basicLumberer>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierTwoStone() {
		enabledBuildingList.Instance.advancedQuarry.isEnabled = true;
		enabledBuildingList.Instance.basicQuarry.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (basicQuarry gameObj in GameObject.FindObjectsOfType<basicQuarry>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierTwoFood() {
		enabledBuildingList.Instance.advancedFarm.isEnabled = true;
		enabledBuildingList.Instance.basicFarm.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (basicFarm gameObj in GameObject.FindObjectsOfType<basicFarm>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierTwoHousing() {
		enabledBuildingList.Instance.stoneHouse.isEnabled = true;
		enabledBuildingList.Instance.woodHouse.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (woodHouse gameObj in GameObject.FindObjectsOfType<woodHouse>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierTwoResearch() {
		enabledBuildingList.Instance.castle.isEnabled = true;
		enabledBuildingList.Instance.chiefsHut.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (chiefsHut gameObj in GameObject.FindObjectsOfType<chiefsHut>()) {
			gameObj.isUpgradeable = true;
		}
	}


	//RENAISSANCE ERA
	public void tierThreeOre() {
		enabledBuildingList.Instance.shaftMine.isEnabled = true;
		enabledBuildingList.Instance.advancedMine.isEnabled = false;
		enabledBuildingList.Instance.setArray ();

		foreach (advancedMine gameObj in GameObject.FindObjectsOfType<advancedMine>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneRefinement() {
		enabledBuildingList.Instance.smeltery.isEnabled = true;
		enabledBuildingList.Instance.basicBlacksmith.isEnabled = false;

		foreach (basicBlacksmith gameObj in GameObject.FindObjectsOfType<basicBlacksmith>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierThreeWood() {
		enabledBuildingList.Instance.forestManager.isEnabled = true;
	}

	public void tierThreeStone() {
		enabledBuildingList.Instance.explosiveQuarry.isEnabled = true;
		enabledBuildingList.Instance.advancedQuarry.isEnabled = false;

		foreach (advancedQuarry gameObj in GameObject.FindObjectsOfType<advancedQuarry>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierThreeFood() {
		enabledBuildingList.Instance.waterReservoir.isEnabled = true;
	}

	public void tierThreeHousing() {
		enabledBuildingList.Instance.multiHouse.isEnabled = true;
		enabledBuildingList.Instance.stoneHouse.isEnabled = false;

		foreach (stoneHouse gameObj in GameObject.FindObjectsOfType<stoneHouse>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierThreeResearch() {
		enabledBuildingList.Instance.guildHouse.isEnabled = true;
		enabledBuildingList.Instance.castle.isEnabled = false;

		foreach (castle gameObj in GameObject.FindObjectsOfType<castle>()) {
			gameObj.isUpgradeable = true;
		}
	}

	public void tierOneGatherNode() {
		enabledBuildingList.Instance.gatherNode.isEnabled = true;
		enabledBuildingList.Instance.setArray ();
	}
}
