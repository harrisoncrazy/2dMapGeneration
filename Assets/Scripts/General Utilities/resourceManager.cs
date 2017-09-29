using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceManager : MonoBehaviour {

	public static resourceManager Instance;

	private float woodTotal;
	private float woodPerTick;
	public Text woodOutText;

	private float foodTotal;
	private float foodPerTick;
	public Text foodOutText;

	private float stoneTotal;
	private float stonePerTick;
	public Text stoneOutText;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		woodOutText.text = "" + woodTotal;
		foodOutText.text = "" + foodTotal;
		stoneOutText.text = "" + stoneTotal;
	}


	public void addWoodResource(float addTick) {
		woodPerTick += addTick;
	}

	public void addFoodResource(float addTick) {
		foodPerTick += addTick;
	}

	public void addStoneResource(float addTick) {
		stonePerTick += addTick;
	}

	public void woodResourceTick() {
		woodTotal += woodPerTick;
	}

	public void foodResourceTick() {
		foodTotal += foodPerTick;
	}

	public void stoneResourceTick() {
		stoneTotal += stonePerTick;
	}
}
