using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseGridPosition : MonoBehaviour {

	public GameObject PathFrom { get; set; }

	public GameObject selectOutline;

	public int SearchHeuristic { get; set; }

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

	public int SearchPriority {
		get {
			return distance + SearchHeuristic;
		}
	}

	void UpdateDistanceLabel() {//updating the debug text on tiles
		tileInfoText.text = Distance == int.MaxValue ? "" : Distance.ToString();
	}

	public void FindPath (baseGridPosition fromCell, baseGridPosition toCell) {//finding distance to the current selected tile
		StartCoroutine (Search (fromCell, toCell));
	}

	IEnumerator Search (baseGridPosition fromCell, baseGridPosition toCell) {
		for (int i = 0; i <= generationManager.Instance.mapSizeX - 1; i++) {//setting all tiles distance value to max for pathfinding reasons
			for (int j = 0; j <= generationManager.Instance.mapSizeY - 1; j++) {
				generationManager.Instance.map [i] [j].GetComponent<baseGridPosition> ().Distance = int.MaxValue;
				generationManager.Instance.map [i] [j].GetComponent<baseGridPosition> ().selectOutline.SetActive (false);
			}
		}
		fromCell.selectOutline.SetActive (true);
		toCell.selectOutline.SetActive (true);
			
		WaitForSeconds delay = new WaitForSeconds (0.0f);//delay for viewing debuging
		List<GameObject> frontier = new List<GameObject> ();//queue of pathfinding
		fromCell.Distance = 0;
		frontier.Add (fromCell.gameObject);//pushing first object to queue
		while (frontier.Count > 0) {
			yield return delay;
			GameObject current = frontier [0];
			frontier.RemoveAt (0);

			if (current == toCell.gameObject) {
				current = current.GetComponent<baseGridPosition> ().PathFrom;
				while (current != fromCell) {
					current.GetComponent<baseGridPosition> ().selectOutline.SetActive (true);
					current = current.GetComponent<baseGridPosition> ().PathFrom;
				}
				break;
			}

			for (int currectDirection = 0; currectDirection <= 5; currectDirection++) {//going through neigbors from top left to left clockwise
				GameObject neighbor = current.GetComponent<baseGridPosition> ().adjacentTiles [currectDirection];

				if (neighbor == null) {
					continue;
				}
				int distance = current.GetComponent<baseGridPosition> ().Distance;
				if (neighbor.GetComponent<tileHandler> () != null) {//making ocean and mountain tiles impassible
					if (neighbor.GetComponent<tileHandler> ().tileType == "Ocean") {
						continue;
					}
					if (neighbor.GetComponent<tileHandler> ().tileType == "Mountain") {
						continue;
					}
					if (neighbor.GetComponent<tileHandler> ().tileType.Contains ("Sand")) {
						distance += 2;
					}
					if (neighbor.GetComponent<tileHandler> ().tileType.Contains ("Heavy Rock")) {
						distance += 2;
					}
				}
				distance += 1;
				if (neighbor.GetComponent<baseGridPosition> ().Distance == int.MaxValue) {
					neighbor.GetComponent<baseGridPosition> ().Distance = distance;
					neighbor.GetComponent<baseGridPosition> ().PathFrom = current;
					neighbor.GetComponent<baseGridPosition> ().SearchHeuristic = neighbor.GetComponent<baseGridPosition> ().DistanceTo (toCell);
					frontier.Add (neighbor);
				} else if (distance < neighbor.GetComponent<baseGridPosition> ().Distance) {
					neighbor.GetComponent<baseGridPosition> ().Distance = distance;
					neighbor.GetComponent<baseGridPosition> ().PathFrom = current;
				}
				frontier.Sort ((x, y) => x.GetComponent<baseGridPosition> ().SearchPriority.CompareTo (y.GetComponent<baseGridPosition> ().SearchPriority));
			}
		}
	}

	public int DistanceTo (baseGridPosition other) {
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
		if (Input.GetKey (KeyCode.LeftShift)) {//if left shift is pressed, sets cell to search from
			if (GameManager.Instance.searchFromCell = this) {
				selectOutline.SetActive (false);//deslecting
			}

			GameManager.Instance.searchFromCell = this;//setting this tile as start point
			this.selectOutline.SetActive (true);

		} else if (GameManager.Instance.searchFromCell && GameManager.Instance.searchFromCell != this) {
			this.selectOutline.SetActive (true);
			FindPath (GameManager.Instance.searchFromCell, GameManager.Instance.selectedTile.gameObject.GetComponent<baseGridPosition>());
		}
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
