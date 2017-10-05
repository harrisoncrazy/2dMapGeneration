using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	public static Zoom Instance;

	/* Orthographic zooming
	public float zoomSpeed = 1;
	public float targetOrtho;//the current othographic
	public float smoothSpeed = 2.0f;//the dampening speed
	public float minOrtho = 1.0f;//min/max orth
	public float maxOrtho = 20.0f;
	*/

	public Transform cameraMover;

	float minZ = -10f;
	float maxZ = -45f;
	float speed = 300f;



	void Start() {
		Instance = this;
	}

	void Update () {
		if (generationManager.Instance.genStepTwoDone) {
			float scrollAxis = Input.GetAxis ("Mouse ScrollWheel");
			//Debug.Log (scrollAxis);

			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				transform.Translate (Vector3.forward * Time.deltaTime * scrollAxis * speed, Space.Self);
				transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				transform.Translate (Vector3.forward * Time.deltaTime * scrollAxis * speed, Space.Self);
				transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
			}

			float zPos = transform.position.z;
			zPos = Mathf.Clamp (zPos, maxZ, minZ);
			transform.position = new Vector3 (transform.position.x, transform.position.y, zPos);
		}


		/*perspective fov change
		float fov = Camera.main.fieldOfView;
		fov += Input.GetAxis ("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp (fov, minFov, maxFov);
		Camera.main.fieldOfView = fov;

		/*orthographic zooming
		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0.0f) {
			targetOrtho -= scroll * zoomSpeed;
			targetOrtho = Mathf.Clamp (targetOrtho, minOrtho, maxOrtho);
		}

		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);*/
	}
}