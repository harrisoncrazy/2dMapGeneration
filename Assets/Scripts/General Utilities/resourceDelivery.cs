using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceDelivery : MonoBehaviour {

	public baseGridPosition sourceBuilding;//starting point of the resource delivery node
	public baseGridPosition toLocation;//end point of the resource delivery node

	public List<baseGridPosition> pathToFollow;

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

	public struct ResourceToDeliver {
		public string Type;
		public float Amount;
	}

	public ResourceToDeliver nodeDelivery = new ResourceToDeliver();

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
		for (int i = 1; i < pathToFollow.Count; i++) {

			Vector3 a = pathToFollow [i - 1].transform.position;
			Vector3 b = pathToFollow [i].transform.position;

			for (float t = 0f; t < 1f; t += Time.deltaTime * travelSpeed) {
				transform.localPosition = Vector3.Lerp (a, b, t);
				transform.LookAt (b);
				yield return null;
			}
		}
		checkResourceType ();
		Destroy (this.gameObject);
	}

	void checkResourceType() {
		switch (nodeDelivery.Type) {
		case "Wood":
			resourceManager.Instance.addWoodResource (nodeDelivery.Amount);
			break;
		case "Stone":
			resourceManager.Instance.addStoneResource (nodeDelivery.Amount);
			break;
		case "Food":
			resourceManager.Instance.addFoodResource (nodeDelivery.Amount);
			break;
		}
	}
}
