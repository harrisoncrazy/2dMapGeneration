using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class researchHandler : MonoBehaviour {

	public static researchHandler Instance;

	public bool isResearchEnabled = false;

	// Use this for initialization
	void Start () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
