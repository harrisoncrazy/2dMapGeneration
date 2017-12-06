using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceDelivery : MonoBehaviour {

	public baseGridPosition sourceBuilding;//starting point of the resource delivery node
	public baseGridPosition toLocation;//end point of the resource delivery node

	public List<baseGridPosition> pathToFollow;

	public bool isDestroyed = false;

	public float travelSpeed = 0.55f;

	public baseGridPosition Location {
		get {
			return location;
		}
		set {
			location = value;
			transform.localPosition = value.transform.position;
		}
	}

	public List<resourceBuildingClass.resourceTypeCost> nodeDelivery = new List<resourceBuildingClass.resourceTypeCost>();

	baseGridPosition location;

	public static Vector3 GetDerivative (Vector3 a, Vector3 b, Vector3 c, float t) {
		return 2f * ((1f - t) * (b - a) + t * (c - b));
	}

	// Use this for initialization
	void Start () {
		if (sourceBuilding == null || toLocation == null) {
			Debug.Log ("Error in initilizing resource delivery node");
			Destroy (this.gameObject);
		}

		Move (pathToFollow);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(List<baseGridPosition> path) {
		Location = path [path.Count - 1];
		StartCoroutine (TravelPath ());
	}

	IEnumerator TravelPath () {
		Vector3 a, b, c = pathToFollow [0].transform.position;

		float t = Time.deltaTime* travelSpeed;
		for (int i = 1; i < pathToFollow.Count; i++) {
			a = c;
			b = pathToFollow [i - 1].transform.position;
			try {
			c = (b + pathToFollow [i].transform.position) * 0.5f;
			}
			catch {
				Destroy (this.gameObject);
			}

			for (; t < 1f; t += Time.deltaTime * travelSpeed) {
				transform.localPosition = Bezier.GetPoint (a, b, c, t);
				Vector3 d = Bezier.GetDerivative (a, b, c, t);
				transform.localRotation = Quaternion.LookRotation (d);
				yield return null;
			}
			t -= 1f;
		}

		a = c;
		b = pathToFollow[pathToFollow.Count - 1].transform.position;
		c = b;
		for (; t < 1f; t += Time.deltaTime * travelSpeed) {
			transform.localPosition = Vector3.Lerp(a, b, t);
			Vector3 d = Bezier.GetDerivative (a, b, c, t);
			transform.localRotation = Quaternion.LookRotation (d);
			yield return null;
		}
			
		checkResourceType ();
		Destroy (this.gameObject);
	}

	void checkResourceType() {
		for (int i = 0; i < nodeDelivery.Count; i++) {
			switch (nodeDelivery[i].resourceType) {
			case "Wood":
				resourceManager.Instance.addWoodResource (nodeDelivery[i].cost);
				break;
			case "Stone":
				resourceManager.Instance.addStoneResource (nodeDelivery[i].cost);
				break;
			case "Food":
				resourceManager.Instance.addFoodResource (nodeDelivery[i].cost);
				break;
			case "Ore":
				resourceManager.Instance.addOreResource (nodeDelivery [i].cost);
				break;
			}
		}
	}

	/*
	void OnTriggerEnter(Collider other) {
		Debug.Log (nodeDelivery.Count);
		Debug.Log (nodeDelivery [0].cost);
		if (this.isDestroyed) {
			for (int i = 0; i <= other.GetComponent<resourceDelivery> ().nodeDelivery.Count - 1; i++) {
				resourceBuildingClass.resourceTypeCost temp = new resourceBuildingClass.resourceTypeCost ();
				temp.resourceType = nodeDelivery [i].resourceType;
				temp.cost = nodeDelivery [i].cost;

				other.GetComponent<resourceDelivery> ().nodeDelivery.Add (temp);
			}

			Destroy (this.gameObject);
		} else {
			resourceDelivery otherDeliverer = other.gameObject.GetComponent<resourceDelivery> ();
			if (otherDeliverer)
				otherDeliverer.isDestroyed = true;
		}
	}*/
}
