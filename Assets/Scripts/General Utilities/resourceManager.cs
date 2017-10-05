using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceManager : MonoBehaviour {

	public static resourceManager Instance;

	private float woodTotal = 100;
	private float woodPerTick;
	public Text woodOutText;

	private float foodTotal = 0;
	private float foodPerTick;
	public Text foodOutText;

	private float stoneTotal = 50;
	private float stonePerTick;
	public Text stoneOutText;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		woodOutText.text = woodTotal.ToString("F1");
		foodOutText.text = foodTotal.ToString("F1");
		stoneOutText.text = stoneTotal.ToString("F1");
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

	public float returnTotalWood() {
		return woodTotal;
	}

	public float returnTotalStone() {
		return stoneTotal;
	}

	public float returnTotalFood() {
		return foodTotal;
	}

	public void removeWood(int total) {
		woodTotal -= total;
	}

	public void removeFood(int total) {
		foodTotal -= total;
	}

	public void removeStone(int total) {
		stoneTotal -= total;
	}
}
