﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputHandler : MonoBehaviour {

	public static inputHandler Instance;

	//public bool ifPlacementModeActive = false;

	//public bool isBuildingPanelActive = false;
	public GameObject buildingPanel;
	public GameObject researchPanel;
	public GameObject pausePanel;
	public GameObject controlPanel;
	public bool controlPanelActive = false;

	public GameObject soundPanel;
	public Slider soundBar;

	public bool soundPanelActive = false;

	public bool isPaused = false;

	void Start() {
		Instance = this;

		soundBar.value = 1.0f;
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.isBuildingSelected == false) {
			if (GameManager.Instance.isResearchPanelActive == false) {//opening builing panel
				if (Input.GetKeyDown (KeyCode.B)) {
					toggleBuildingPanel ();
				}
			}
			if (researchHandler.Instance.isResearchEnabled == true) {
				if (GameManager.Instance.isBuildingPanelActive == false) {//opening research panel
					if (Input.GetKeyDown (KeyCode.R)) {
						toggleResearchPanel ();
					}
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused == false) {//Toggle On
				pausePanel.SetActive (true);
				Time.timeScale = 0;
				isPaused = true;
			} else if (isPaused == true) {//Toggle Off
				pausePanel.SetActive (false);
				if (controlPanelActive == true) {
					toggleControlPanel ();
				}
				if (soundPanelActive == true) {
					toggleSoundPanel ();
				}
				Time.timeScale = 1.0f;
				isPaused = false;
			}
		}

		AudioListener.volume = soundBar.value;
		AudioManager.Instance.currentGlobalVolume = soundBar.value;
	}

	void toggleBuildingPanel () {//toggling on or off the building panel, locking or unlocking the camera to the building
		if (GameManager.Instance.isBuildingPanelActive == true) {
			buildingPanel.SetActive (false);
			GameManager.Instance.isBuildingPanelActive = false;
		} else {
			buildingPanel.SetActive (true);
			GameManager.Instance.isBuildingPanelActive = true;
			scrollMenuControl.Instance.ReadActiveBuildings();
		}
	}

	public void toggleResearchPanel() {//toggling on or off the research panel
		if (GameManager.Instance.isResearchPanelActive == true) {
			researchPanel.SetActive (false);
			GameManager.Instance.isResearchPanelActive = false;
		} else { 
			researchPanel.SetActive (true);
			GameManager.Instance.isResearchPanelActive = true;
			researchScrollMenuControl.Instance.ReadActiveResearchs ();
		}
	}

	public void toggleControlPanel() {
		if (controlPanelActive == true) {
			controlPanel.SetActive (false);
			controlPanelActive = false;
		} else {
			controlPanel.SetActive (true);
			controlPanelActive = true;
		}
	}

	public void toggleSoundPanel() {
		if (soundPanelActive == true) {
			soundPanel.SetActive (false);
			soundPanelActive = false;

		} else {
			soundPanel.SetActive (true);
			soundBar.value = AudioManager.Instance.currentGlobalVolume;
			soundPanelActive = true;
		}
	}

	public void disablePlacementMode() { //disable placementmode
		GameManager.Instance.isPlacementModeActive = false;
		GameManager.Instance.disablePlacementModes ();
	}
}
