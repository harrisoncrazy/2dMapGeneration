using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchHandler : MonoBehaviour {

	public static researchHandler Instance;

	public bool isResearchEnabled = false;

	public GameObject researchUIParent;
	public GameObject researchStartPanel;

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


}
