using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceManager : MonoBehaviour {

	public static resourceManager Instance;

	public float woodTotal;
	public float woodPerTick;
	public Text woodOutText;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		woodOutText.text = "" + woodTotal;
	}


	public void addWoodResource(float addTick) {
		woodPerTick += addTick;
	}

	public void woodResourceTick() {
		woodTotal += woodPerTick;
	}
}
