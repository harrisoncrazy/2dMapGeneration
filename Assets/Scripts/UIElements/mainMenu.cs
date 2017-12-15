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
			Application.Quit ();
		}
		if (isResume) {
			inputHandler.Instance.isPaused = false;
			inputHandler.Instance.pausePanel.SetActive (false);

			Time.timeScale = 1.0f;
		}
	}
}
