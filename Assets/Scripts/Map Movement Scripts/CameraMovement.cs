using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private float speed = 6f;

	private bool movedStart = false;//if the game has started and the camera is free to move
	private bool cameraReset = true;//reseting the camera zoom and oth after moving with an expedtion

	private float defaultDampening;
	private float defaultZoom;

	// Update is called once per frame
	void Update () {
		if (movedStart == false) {
			if (generationManager.Instance.genStepTwoDone == true) {//handleling the camera during generation
				StartCoroutine ("startCam");
			}
		}

		if (generationManager.Instance.genStepTwoDone == true) {//if finished generating
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				speed = 12f;
			}

			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				speed = 6f;
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.Translate (Vector2.right * speed * Time.deltaTime); 
			}
			if (Input.GetKey (KeyCode.A)) {
				transform.Translate (-Vector2.right * speed * Time.deltaTime);   
			}
			if (Input.GetKey (KeyCode.W)) {
				transform.Translate (Vector2.up * speed * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.S)) {
				transform.Translate (-Vector2.up * speed * Time.deltaTime);  
			}
		}
	}

	IEnumerator startCam(){//cycling the camera while generating to ensure all colliders work
		yield return new WaitForSeconds (1.0f);
		Vector2 outPos = transform.position;
		Vector2 pos = new Vector2 (0, 0);
		GameObject.Find ("CameraMovement").transform.position = pos;
		GameObject.Find ("CameraMovement").transform.position = outPos;
		yield return new WaitForSeconds (2.0f);
		GameObject.Find ("CameraMovement").transform.position = pos;
		movedStart = true;
	}
}
