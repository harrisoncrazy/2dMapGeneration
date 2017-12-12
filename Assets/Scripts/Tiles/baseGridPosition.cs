using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseGridPosition : MonoBehaviour {
	//TODO comment and format

	//Pathfinding values
	public GameObject PathFrom;// { get; set; }

	public GameObject selectOutline;

	public GameObject hexOutline;

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

	//tile adjacency
	public GameObject topLeft;
	public GameObject topRight;
	public GameObject Right;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject Left;
	public GameObject[] adjacentTiles = new GameObject[6];

	//Arrows
	public GameObject topLeftArrow;
	public GameObject topRightArrow;
	public GameObject RightArrow;
	public GameObject bottomRightArrow;
	public GameObject bottomLeftArrow;
	public GameObject LeftArrow;
	public GameObject[] arrowList = new GameObject[6];

	public bool isHoverMode = false;

	public struct gridPosition {
		public int X, Y;

		public gridPosition(int x, int y) {
			this.X = x;
			this.Y = y;
		}
	}
	public gridPosition mapPosition = new gridPosition();

	public bool isGatherPoint = false;

	// Use this for initialization
	void Start () {
		findHexOutline ();
		//adjacentTiles = new GameObject[6];	

		//tileInfoText.text = "[" + mapPosition.X + "] [" + mapPosition.Y + "]";
		tileInfoText.text = "";

		setAdjArrayVals ();

		if (this.GetComponent<tileHandler> () == null) {
			setArrowsVals ();
			disableArrows ();
		}
	}

	public void findHexOutline() {
		hexOutline = transform.Find ("higlightHex").gameObject;
		hexOutline.transform.localScale = new Vector3 (hexOutline.transform.localScale.x, 10f, hexOutline.transform.localScale.z);
		hexOutline.SetActive (false);
		tileInfoText.text = "";
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void OnMouseDown() {
		if (isHoverMode == false) {
			if (Input.GetKey (KeyCode.LeftShift)) {//if left shift is pressed, sets cell to search from
				if (GameManager.Instance.searchFromCell = this) {
					selectOutline.SetActive (false);//deslecting
				}

				GameManager.Instance.searchFromCell = this;//setting this tile as start point
				this.selectOutline.SetActive (true);

			} else if (GameManager.Instance.searchFromCell && GameManager.Instance.searchFromCell != this) {
				this.selectOutline.SetActive (true);
				pathfindingManager.Instance.FindPath (GameManager.Instance.searchFromCell, GameManager.Instance.selectedTile.gameObject.GetComponent<baseGridPosition> ());
			}
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
		if (topLeft != null)
			topLeft.GetComponent<baseGridPosition>().bottomRight = this.gameObject;

		if (topRight != null)
			topRight.GetComponent<baseGridPosition> ().bottomLeft = this.gameObject;

		if (Right != null)
			Right.GetComponent<baseGridPosition> ().Left = this.gameObject;

		if (bottomRight != null)
			bottomRight.GetComponent<baseGridPosition> ().topLeft = this.gameObject;

		if (bottomLeft != null)
			bottomLeft.GetComponent<baseGridPosition> ().topRight = this.gameObject;

		if (Left != null)
			Left.GetComponent<baseGridPosition> ().Right = this.gameObject;
	}


	public void setArrowsVals() {
		if (topLeftArrow != null)
			arrowList [0] = topLeftArrow;

		if (topRightArrow != null)
			arrowList [1] = topRightArrow;

		if (RightArrow != null)
			arrowList [2] = RightArrow;

		if (bottomRightArrow != null)
			arrowList [3] = bottomRightArrow;

		if (bottomLeftArrow != null)
			arrowList [4] = bottomLeftArrow;

		if (LeftArrow != null)
			arrowList [5] = LeftArrow;
	}

	public void enableArrows(GameObject[] adjTiles, resourceBuildingClass.adjBonus[] bonus, resourceBuildingClass.adjPenalty[] penalty) {//reads and activates arrows based on adjacent tiles and bonuses
		for (int i = 0; i < bonus.Length; i++) {//only reads off of the length of bonus[], so both arrays must be the same length (make dummy values, look at basic lumberer for example)
			string tempTileTypeBonus = bonus [i].tileType;
			string tempTileTypePenalty = penalty [i].tileType;

			for (int j = 0; j < adjTiles.Length; j++) {
				if (adjTiles [j] != null) {
					if (adjTiles [j].GetComponent<tileHandler> () != null) { //if a default tile with no building
						if (adjTiles [j].GetComponent<tileHandler> ().tileType.Contains (tempTileTypeBonus)) {//if the tiletype matches the current bonus read
							arrowList [j].SetActive (true);
							arrowList [j].GetComponent<Renderer> ().material.color = Color.green;
						} else if (adjTiles [j].GetComponent<tileHandler> ().tileType.Contains (tempTileTypePenalty)) {//if the tiletype matches the current penalty read
							arrowList [j].SetActive (true);
							arrowList [j].GetComponent<Renderer> ().material.color = Color.red;
						} else {
							arrowList [j].SetActive (false);
						}
					} else { //if a tile with a building
						if (adjTiles [j].name.Contains (tempTileTypeBonus)) {//if the tiletype matches the current bonus read
							arrowList [j].SetActive (true);
							arrowList [j].GetComponent<Renderer> ().material.color = Color.green;
						} else if (adjTiles [j].name.Contains (tempTileTypePenalty)) {//if the tiletype matches the current penalty read
							arrowList [j].SetActive (true);
							arrowList [j].GetComponent<Renderer> ().material.color = Color.red;
						} else {
							arrowList [j].SetActive (false);
						}
					}
				} else if (adjTiles [j] == null) {
					arrowList [j].SetActive (false);
				}
			}
		}
	}

	public void disableArrows() {
		for (int i = 0; i < arrowList.Length; i++) {
			if (arrowList [i] != null) {
				arrowList [i].SetActive (false);
			}
		}
	}
}
