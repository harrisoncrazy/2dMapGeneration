using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Transform swivel, stick;

	float zoom = 1f;

	public float stickMinZoom, stickMaxZoom;
	public float swivelMinZoom, swivelMaxZoom;

	public float moveSpeed;

	void Awake() {
		swivel = transform.GetChild (0);
		stick = swivel.GetChild (0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float zoomDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom (zoomDelta);
		}

		float xDelta = Input.GetAxis ("Horizontal");
		float zDelta = Input.GetAxis ("Vertical");
		if (xDelta != 0f || zDelta != 0f) {
			AdjustPosition (xDelta, zDelta);
		}
	}

	void AdjustZoom (float delta) {
		zoom = Mathf.Clamp01 (zoom + delta);

		float distance = Mathf.Lerp (stickMinZoom, stickMaxZoom, zoom);
		stick.localPosition = new Vector3 (0, 0, distance);

		float angle = Mathf.Lerp (swivelMinZoom, swivelMaxZoom, zoom);
		swivel.localRotation = Quaternion.Euler (angle, 0f, 0f);
	}

	void AdjustPosition (float xDelta, float zDelta) {
		Vector3 direction = new Vector3 (xDelta, zDelta, 0f).normalized;
		float damping = Mathf.Max (Mathf.Abs (xDelta), Mathf.Abs (zDelta));
		float distance = moveSpeed * damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += direction * distance;
		transform.localPosition = position;
	}
}
