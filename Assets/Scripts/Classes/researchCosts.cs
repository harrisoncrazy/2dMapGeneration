﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchCosts : MonoBehaviour {

	public static researchCosts Instance;

	//STONE ERA
	public researchBuildingClass.technologyInfo tierOneLumber;
	public researchBuildingClass.technologyInfo tierOneStone;
	public researchBuildingClass.technologyInfo tierOneFood;

	// Use this for initialization
	void Start () {
		Instance = this;

		//STONE ERA
		string description =  "Advanced strategies in wood cutting will allow you to cut down larger trees. This technology will allow you to clear forests to place down buildings. " +
			"\nUnlocks the Basic Lumberer, which gains additional wood gathering capabilites when placed adjacent to forest.";
		tierOneLumber = new researchBuildingClass.technologyInfo("tierOneLumber", 50, "Wood Cutting", description);

		description = "Advanced strategies in mining will allow you to break and collect larger stones. This technology will allow you to clear smaller rock tiles to place down buildings. " +
			"\nUnlocks the Basic Quarry, which gains additional stone gathering capabilities when placed adjacent to rocks.";
		tierOneStone = new researchBuildingClass.technologyInfo ("tierOneStone", 50, "Mining", description);
	}
}
