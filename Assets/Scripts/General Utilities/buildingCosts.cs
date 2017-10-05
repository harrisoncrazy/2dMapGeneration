using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingCosts : MonoBehaviour {

	public static buildingCosts Instance;

	public resourceBuildingClass.resourceTypeCost[] woodGatherBuidlingCost;
	public resourceBuildingClass.resourceTypeCost[] stoneGatherBuildingCost;
	public resourceBuildingClass.resourceTypeCost[] foodGatherBuildingCost;

	void Start () {
		Instance = this;

		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 25);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 10);
		woodGatherBuidlingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost
		}; 

		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 20);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		stoneGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost
		};

		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 30);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 15);
		foodGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost
		};
	} 
}
