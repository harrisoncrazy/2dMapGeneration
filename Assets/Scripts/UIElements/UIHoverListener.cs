using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverListener : MonoBehaviour {

	public static UIHoverListener Instance;

	public bool isOverUI { get; private set; }

	void Start() {
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		isOverUI = EventSystem.current.IsPointerOverGameObject ();
	}
}
