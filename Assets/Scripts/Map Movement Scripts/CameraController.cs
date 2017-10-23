using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public static CameraController Instance;

	Transform swivel, stick;

	float zoom = 1f;

	public float stickMinZoom, stickMaxZoom;
	public float swivelMinZoom, swivelMaxZoom;

	public float moveSpeed;

	public float rotationSpeed;
	float rotationAngle;

	void Awake() {
		swivel = transform.GetChild (0);
		stick = swivel.GetChild (0);
	}

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		float zoomDelta = Input.GetAxis ("Mouse ScrollWheel");
		if (zoomDelta != 0f) {
			AdjustZoom (zoomDelta);
		}

		float rotationDelta = Input.GetAxis ("Rotation");
		if (rotationDelta != 0f) {
			AdjustRotation (rotationDelta);
		}

		if (GameManager.Instance.isBuildingSelected == false) {
			float xDelta = Input.GetAxis ("Horizontal");
			float yDelta = Input.GetAxis ("Vertical");
			if (xDelta != 0f || yDelta != 0f) {
				AdjustPosition (xDelta, yDelta);
			}
		}
	}

	void AdjustZoom (float delta) {
		zoom = Mathf.Clamp01 (zoom + delta);

		float distance = Mathf.Lerp (stickMinZoom, stickMaxZoom, zoom);
		stick.localPosition = new Vector3 (0, 0, distance);

		float angle = Mathf.Lerp (swivelMinZoom, swivelMaxZoom, zoom);
		swivel.localRotation = Quaternion.Euler (angle, 0f, 0f);
	}

	void AdjustRotation (float delta) {
		rotationAngle += delta * rotationSpeed * Time.deltaTime;
		if (rotationAngle < 0f) {
			rotationAngle += 360f;
		} else if (rotationAngle >= 360f) {
			rotationAngle -= 360f;
		}
		transform.localRotation = Quaternion.Euler (0f, 0f, rotationAngle);
	}

	void AdjustPosition (float xDelta, float yDelta) {
		Vector3 direction = transform.localRotation * new Vector3 (xDelta, yDelta, 0f).normalized;
		float damping = Mathf.Max (Mathf.Abs (xDelta), Mathf.Abs (yDelta));
		float distance = moveSpeed * damping * Time.deltaTime;

		Vector3 position = transform.localPosition;
		position += direction * distance;
		transform.localPosition = ClampPosition(position);
	}

	Vector3 ClampPosition (Vector3 position) {
		float xMax = generationManager.Instance.mapSizeX + 15f;
		float yMax = generationManager.Instance.mapSizeY;

		position.x = Mathf.Clamp (position.x, 0f, xMax);
		position.y = Mathf.Clamp (position.y, 0f, yMax);

		return position;
	}

	public void setCameraPos(float xPos, float yPos) {
		Vector3 position = new Vector3 (xPos, yPos, transform.localPosition.z);

		transform.localPosition = position;
	}
}
