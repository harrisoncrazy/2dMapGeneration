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

		resourceBuildingClass.resourceTypeCost woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		resourceBuildingClass.resourceTypeCost stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		resourceBuildingClass.resourceTypeCost manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		woodGatherBuidlingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		}; 

		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		stoneGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};

		woodCost = new resourceBuildingClass.resourceTypeCost ("Wood", 5);
		stoneCost = new resourceBuildingClass.resourceTypeCost ("Stone", 5);
		manpowerCost = new resourceBuildingClass.resourceTypeCost ("Manpower", 1);
		foodGatherBuildingCost = new resourceBuildingClass.resourceTypeCost[] {
			woodCost,
			stoneCost,
			manpowerCost
		};
	} 
}
