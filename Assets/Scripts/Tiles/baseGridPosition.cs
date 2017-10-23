using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseGridPosition : MonoBehaviour {

	//Pathfinding values
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
		}
	}

	public int SearchPriority {
		get {
			return distance + SearchHeuristic;
		}
	}

	public int SearchPhase { get; set; }

	public void SetLabel() {//updating the debug text on tiles
		tileInfoText.text = Distance == int.MaxValue ? "" : Distance.ToString();
	}
		
	public int DistanceTo (baseGridPosition other) {
		return (mapPosition.X < other.mapPosition.X ? other.mapPosition.X - mapPosition.X : mapPosition.X - other.mapPosition.X) +
		(mapPosition.Y < other.mapPosition.Y ? other.mapPosition.Y - mapPosition.Y : mapPosition.Y - other.mapPosition.Y);
	}
	//end of pathfinding values


	public TextMesh tileInfoText;

	public GameObject topLeft;
	public GameObject topRight;
	public GameObject Right;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject Left;
	public GameObject[] adjacentTiles = new GameObject[6];

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
		//adjacentTiles = new GameObject[6];	

		//tileInfoText.text = "[" + mapPosition.X + "] [" + mapPosition.Y + "]";
		tileInfoText.text = "";


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
			pathfindingManager.Instance.FindPath (GameManager.Instance.searchFromCell, GameManager.Instance.selectedTile.gameObject.GetComponent<baseGridPosition>());
		}
	}

	public void findAdjacentTiles() {
		if (mapPosition.Y % 2 == 0) {//if on an even y row
			if ((mapPosition.X - 1) > 0 && (mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topLeft = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y + 1)];
			}
			if ((mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topRight = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y + 1)];
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX) {
				Right = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y)];
			}
			if ((mapPosition.Y - 1) > 0) {
				bottomRight = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y - 1)];
			}
			if ((mapPosition.X - 1) > 0 && (mapPosition.Y - 1) > 0) {
				bottomLeft = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y - 1)];
			}
			if ((mapPosition.X - 1) >= 0) {
				Left = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y)]; 
			}
		} else if (mapPosition.Y % 2 == 1) {//if on an odd y row
			if ((mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topLeft = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y + 1)];
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX && (mapPosition.Y + 1) < generationManager.Instance.mapSizeY) {
				topRight = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y + 1)];
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX) {
				Right = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y)];
			}
			if ((mapPosition.X + 1) < generationManager.Instance.mapSizeX && (mapPosition.Y - 1) >= 0) {
				bottomRight = generationManager.Instance.map [(mapPosition.X + 1)] [(mapPosition.Y - 1)];
			}
			if ((mapPosition.Y - 1) >= 0) {
				bottomLeft = generationManager.Instance.map [(mapPosition.X)] [(mapPosition.Y - 1)];
			}
			if ((mapPosition.X - 1) >= 0) {
				Left = generationManager.Instance.map [(mapPosition.X - 1)] [(mapPosition.Y)];
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
	}
}
