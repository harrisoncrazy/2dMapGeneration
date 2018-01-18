using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	public bool isStart;
	public bool isQuit;
	public bool isResume;

	public void OnClicker() {
		if (isStart) {
			SceneManager.LoadScene (1);
		}
		if (isQuit) {
			SceneManager.LoadScene (0);
		}
		if (isResume) {
			inputHandler.Instance.isPaused = false;
			inputHandler.Instance.pausePanel.SetActive (false);
			if (inputHandler.Instance.controlPanelActive == true) {
				inputHandler.Instance.toggleControlPanel();
			}
			if (inputHandler.Instance.soundPanelActive == true) {
				inputHandler.Instance.toggleSoundPanel();
			}

			Time.timeScale = 1.0f;
		}
	}
}
