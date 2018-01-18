using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsOptions : MonoBehaviour {

	public GameObject menu;

	private bool showOptions = false;
	public int ResX;
	public int ResY;
	public bool Fullscreen;

	// Use this for initialization
	void Start () {
		showOptions = false;
		ResX = Screen.currentResolution.width;
		ResY = Screen.currentResolution.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncreaseQuality() {
		QualitySettings.IncreaseLevel();
	}

	public void DecreaseQuality() {
		QualitySettings.DecreaseLevel();
	}

	public void OneEightyP() {
		Screen.SetResolution(1920, 1080, Fullscreen);
		ResX = 1920;
		ResY = 1080;
		Debug.Log ("1080p");
	}

	public void SevenTwentyP() {
		Screen.SetResolution(1280, 720, Fullscreen);
		ResX = 1280;
		ResY = 720;
		Debug.Log ("720p");
	}

	public void FourEightyP() {
		Screen.SetResolution(640, 480, Fullscreen);
		ResX = 640;
		ResY = 480;
		Debug.Log ("480p");
	}

	public void vSyncOn() {
		QualitySettings.vSyncCount = 1;
	}

	public void vSyncOff() {
		QualitySettings.vSyncCount = 0;
	}

	public void antiAliasingYes() {
		QualitySettings.antiAliasing = 4;
		Debug.Log ("4 x AA");
	}

	public void antiAliasingNo() {
		QualitySettings.antiAliasing = 0;
		Debug.Log ("0 AA");
	}

	public void fullscreenYes() {
		Fullscreen = true;
		Screen.SetResolution(ResX, ResY, Fullscreen);
	} 

	public void fullscreenNo() {
		Fullscreen = false;
		Screen.SetResolution(ResX, ResY, Fullscreen);
	}

	public void ToggleOn () {
		menu.SetActive (true);
	}

	public void ToggleOff(){
		menu.SetActive (false);
	}
}
