﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseGridPosition : MonoBehaviour {

	//pathfinding stuff
	int distance;
	public int Distance {
		get {
			return distance;
		}
		set {
			distance = value;
			UpdateDistanceLabel ();
		}
	}

	void UpdateDistanceLabel() {
		tileInfoText.text = Distance == int.MaxValue ? "" : Distance.ToString();
	}

	public void FindDistanceTo (baseGridPosition node) {
		StartCoroutine (Search (node));
	}

	IEnumerator Search (baseGridPosition cell) {
		for (int i = 0; i <= generationManager.Instance.mapSizeX - 1; i++) {
			for (int j = 0; j <= generationManager.Instance.mapSizeY - 1; j++) {
				generationManager.Instance.map [i] [j].GetComponent<baseGridPosition> ().Distance = int.MaxValue;
			}
		}
			
		WaitForSeconds delay = new WaitForSeconds (1 / 60f);
		Queue<GameObject> frontier = new Queue<GameObject> ();
		cell.Distance = 0;
		frontier.Enqueue (cell.gameObject);
		while (frontier.Count > 0) {
			yield return delay;

			GameObject current = frontier.Dequeue ();

			for (int currectDirection = 0; currectDirection <= 5; currectDirection++) {//going through neigbors from top left to left clockwise
				GameObject neighbor = current.GetComponent<baseGridPosition>().adjacentTiles [currectDirection];
				if (neighbor != null) {
					if (neighbor.GetComponent<tileHandler> () != null) {
						if (neighbor.GetComponent<tileHandler> ().tileType == "Ocean") {
							continue;
						}
						if (neighbor.GetComponent<tileHandler> ().tileType == "Mountain") {
							continue;
						}
					}
					if (neighbor != null && neighbor.GetComponent<baseGridPosition> ().Distance == int.MaxValue) {
						neighbor.GetComponent<baseGridPosition> ().Distance = current.GetComponent<baseGridPosition> ().Distance + 1;
						neighbor.GetComponent<baseGridPosition> ().UpdateDistanceLabel ();
						frontier.Enqueue (neighbor);
					}
				}
			}

			if (current.GetComponent<baseGridPosition> ().Distance >= 10) {
				break;
			}
		}
	}

	public int DistanceTo(baseGridPosition other) {
		return (mapPosition.X < other.mapPosition.X ? other.mapPosition.X - mapPosition.X : mapPosition.X - other.mapPosition.X) +
			(mapPosition.Y < other.mapPosition.Y ? other.mapPosition.Y - mapPosition.Y : mapPosition.Y - other.mapPosition.Y);
	}

	public TextMesh tileInfoText;

	public GameObject topLeft;
	public GameObject topRight;
	public GameObject Right;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject Left;
	public GameObject[] adjacentTiles;

	public struct gridPosition {
		public int X, Y;

		public gridPosition(int x, int y) {
			this.X = x;
			this.Y = y;
		}
	}
	public gridPosition mapPosition = new gridPosition();

	// Use this for initialization
	void Start () {
		adjacentTiles = new GameObject[6];	

		tileInfoText.text = "[" + mapPosition.X + "] [" + mapPosition.Y + "]";

		setAdjArrayVals ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown() {
		FindDistanceTo (this);
		UpdateDistanceLabel ();
	}

	public void findAdjacentTiles() {
		if (mapPosition.Y % 2 == 0) {//if on an even y row
			if ((mapPosition.X - 1) > 0 && (mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topLeft = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y + 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topRight = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y + 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX) {
				Right = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.Y - 1) > 0) {
				bottomRight = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y - 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X - 1) > 0 && (mapPosition.Y - 1) > 0) {
				bottomLeft = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y - 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X - 1) >= 0) {
				Left = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y)].GetComponent<tileHandler> ().gameObject; 
			}
		} else if (mapPosition.Y % 2 == 1) {//if on an odd y row
			if ((mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topLeft = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y + 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX && (mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topRight = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y + 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX) {
				Right = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX && (mapPosition.Y - 1) > 0) {
				bottomRight = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y - 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.Y - 1) > 0) {
				bottomLeft = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y - 1)].GetComponent<tileHandler> ().gameObject;
			}
			if ((mapPosition.X - 1) >= 0) {
				Left = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y)].GetComponent<tileHandler> ().gameObject;
			}
		}

		setAdjArrayVals ();
	}

	public void setAdjArrayVals() {
		adjacentTiles [0] = topLeft;
		adjacentTiles [1] = topRight;
		adjacentTiles [2] = Right;
		adjacentTiles [3] = bottomRight;
		adjacentTiles [4] = bottomLeft;
		adjacentTiles [5] = Left;
	}

	public void fixAdjacentTilesAdjacency() { //when placing a new building, add it to the adjacent tiles' adjacency
		topLeft.GetComponent<baseGridPosition>().bottomRight = this.gameObject;

		topRight.GetComponent<baseGridPosition> ().bottomLeft = this.gameObject;

		Right.GetComponent<baseGridPosition> ().Left = this.gameObject;

		bottomRight.GetComponent<baseGridPosition> ().topLeft = this.gameObject;

		bottomLeft.GetComponent<baseGridPosition> ().topRight = this.gameObject;

		Left.GetComponent<baseGridPosition> ().Right = this.gameObject;
		/*
		if (checkIfBuildingOrTile (topLeft)) { //fixing top left tile
			topLeft.GetComponent<tileHandler> ().bottomRight = this.gameObject;
		} else {
			topLeft.GetComponent<defaultBuilding> ().bottomRight = this.gameObject;
		}

		if (checkIfBuildingOrTile (topRight)) { //fixing top right tile
			topRight.GetComponent<tileHandler> ().bottomLeft = this.gameObject;
		} else {
			topRight.GetComponent<defaultBuilding> ().bottomLeft = this.gameObject;
		}

		if (checkIfBuildingOrTile (Right)) { //fixing top left tile
			Right.GetComponent<tileHandler> ().Left = this.gameObject;
		} else {
			Right.GetComponent<defaultBuilding> ().Left = this.gameObject;
		}

		if (checkIfBuildingOrTile (bottomRight)) { //fixing top left tile
			bottomRight.GetComponent<tileHandler> ().topLeft = this.gameObject;
		} else {
			bottomRight.GetComponent<defaultBuilding> ().topLeft = this.gameObject;
		}

		if (checkIfBuildingOrTile (bottomLeft)) { //fixing top left tile
			bottomLeft.GetComponent<tileHandler> ().topRight = this.gameObject;
		} else {
			bottomLeft.GetComponent<defaultBuilding> ().topRight = this.gameObject;
		}

		if (checkIfBuildingOrTile (Left)) { //fixing top left tile
			Left.GetComponent<tileHandler> ().Right = this.gameObject;
		} else {
			Left.GetComponent<defaultBuilding> ().Right = this.gameObject;
		}*/
	}
	/*
	bool checkIfBuildingOrTile(GameObject obj) {
		if (obj.GetComponent<tileHandler> () != null) {
			return true;
		} else if (obj.GetComponent<defaultBuilding> () != null) {
			return false;
		} else {
			return false;
		}
	}*/
}
