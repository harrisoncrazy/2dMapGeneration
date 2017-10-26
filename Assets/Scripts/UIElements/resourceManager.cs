﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resourceManager : MonoBehaviour {

	public static resourceManager Instance;

	private float woodTotal = 30;
	private float woodPerTick;
	public Text woodOutText;

	private float foodTotal = 0;
	private float foodPerTick;
	public Text foodOutText;

	private float stoneTotal = 30;
	private float stonePerTick;
	public Text stoneOutText;

	private float manpowerTotal = 0;
	private float manpowerPerTick;
	public Text manpowerOutText;
	public int maxManpower = 5;

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
}
