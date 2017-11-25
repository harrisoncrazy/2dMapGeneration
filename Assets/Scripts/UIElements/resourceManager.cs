using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceManager : MonoBehaviour {

	public static resourceManager Instance;

	private float woodTotal = 999;//start of 15
	private float woodPerTick;
	public Text woodOutText;

	private float foodTotal = 999;//start of 5?
	private float foodPerTick;
	public Text foodOutText;

	private float stoneTotal = 999;//start of 15
	private float stonePerTick;
	public Text stoneOutText;

	private float manpowerTotal = 999;
	private float manpowerPerTick;
	public Text manpowerOutText;
	public int maxManpower = 999;

	private float researchTotal = 999;
	private float researchTick;
	public Text researchOutText;

	// Use this for initialization
	void Start () {
		Instance = this;

		addManpowerResource (1.0f);//adding the default tick from home base
	}
	
	// Update is called once per frame
	void Update () {
		woodOutText.text = woodTotal.ToString("F1");
		foodOutText.text = foodTotal.ToString("F1");
		stoneOutText.text = stoneTotal.ToString("F1");
		manpowerOutText.text = "" + manpowerTotal.ToString("F0") + "/" + maxManpower.ToString("F0");
		researchOutText.text = "" + researchTotal.ToString("F1");

		manpowerTotal = Mathf.Clamp (manpowerTotal, 0, maxManpower);
	}


	//ADDING
	public void addWoodResource(float addAmount) {
		woodTotal += addAmount;
	}

	public void addFoodResource(float addAmount) {
		foodTotal += addAmount;
	}

	public void addStoneResource(float addAmount) {
		stoneTotal += addAmount;
	}

	public void addManpowerResource(float addTick) {
		manpowerPerTick += addTick;
	}

	public void addToManpowerTotal(int addAmount) {
		maxManpower += addAmount;
	}

	public void addResearchResource(float addTick) {
		researchTick += addTick;
	}
		

	//TICKS
	/*
	public void woodResourceTick() {
		woodTotal += woodPerTick;
	}

	public void foodResourceTick() {
		foodTotal += foodPerTick;
	}

	public void stoneResourceTick() {
		stoneTotal += stonePerTick;
	}*/

	public void manpowerResourceTick() {
		manpowerTotal += manpowerPerTick;
	}

	public void researchResourceTick() {
		researchTotal += researchTick;
	}


	//RETURNS
	public float returnTotalWood() {
		return woodTotal;
	}

	public float returnTotalStone() {
		return stoneTotal;
	}

	public float returnTotalFood() {
		return foodTotal;
	}

	public float returnTotalManpower() {
		return manpowerTotal;
	}

	public float returnTotalResearch() {
		return researchTotal;
	}


	//REMOVAL
	public void removeWood(float total) {
		woodTotal -= total;
	}

	public void removeFood(float total) {
		foodTotal -= total;
	}

	public void removeStone(float total) {
		stoneTotal -= total;
	}

	public void removeManpower(float total) {
		manpowerTotal -= total;
	}

	public bool purchaseResearch(float cost) {
		if (cost <= researchTotal) {
			researchTotal -= cost;
			return true;
		} else {
			return false;
		}
	}
}
