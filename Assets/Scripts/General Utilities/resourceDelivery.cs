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
		Vector3 a, b, c = pathToFollow [0].transform.position;

		float t = Time.deltaTime* travelSpeed;
		for (int i = 1; i < pathToFollow.Count; i++) {
			a = c;
			b = pathToFollow [i - 1].transform.position;
			c = (b + pathToFollow [i].transform.position) * 0.5f;

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
		case "Manpower":
			resourceManager.Instance.addManpowerResource (nodeDelivery.Amount);
			break;
		}
	}
}
