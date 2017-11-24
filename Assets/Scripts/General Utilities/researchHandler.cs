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
		//TODO make way to clear tree tiles
		enabledBuildingList.Instance.basicLumberer.isEnabled = true;
		enabledBuildingList.Instance.woodGather.isEnabled = false;
		enabledBuildingList.Instance.setArray ();
	}

	public void tierOneStone() {//enables an upgraded stone gatherer, allows for the clearing of stone tiles
		//TODO make way to clear stone tiles
		//TODO make basic quarry tile
	}
}
