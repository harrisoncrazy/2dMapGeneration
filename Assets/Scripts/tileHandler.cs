using UnityEngine;
using System.Collections;

public class tileHandler : MonoBehaviour {

	//tile adjacentcy
	bool foundAdjacent = false;
	public GameObject topLeft;
	public GameObject topRight;
	public GameObject Right;
	public GameObject bottomRight;
	public GameObject bottomLeft;
	public GameObject Left;
	public GameObject[] adjacentTiles;


	public GameObject tileOutlineSprite; //gameobject of highlight Sprite
	public GameObject tileBlacked;
	public GameObject tileGreyed;
	public GameObject tileHighlighter;


	public CircleCollider2D colliderMain;
	public bool shutdown = false;


	//Tile selection values
	private Transform trSelect = null;
	private bool selected = false;


	//Generation type values
	public bool sandSeed = false;
	public bool snowSeed = false;
	public bool oceanSeed = false;
	public bool riverMade = false;
	public bool mountainSeed = false;
	public bool adjacentSand = false;
	public bool adjacentSnow = false;
	public bool adjacentOcean = false;


	//Tile info values
	public string tileType;
	public struct gridPosition {
		public int X, Y;

		public gridPosition(int x, int y) {
			this.X = x;
			this.Y = y;
		}
	}
	public gridPosition mapPosition = new gridPosition();
	public bool isNearWater = false; //if the tile is close by to water


	public SpriteRenderer sr;
	public bool stopSpread = false;
	public bool newSpriteSet = false;


	//Tile seen values
	public bool discovered = false;
	public bool inSight = false;


	// Use this for initialization
	void Start () {
		adjacentTiles = new GameObject[6];

		sr = GetComponent<SpriteRenderer> ();
		colliderMain = gameObject.GetComponent<CircleCollider2D> ();
		tileGreyed.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .75f);
		tileHighlighter.GetComponent<SpriteRenderer> ().color = new Color (200f, 0f, 0f, .65f);
	}

	// Update is called once per frame
	void Update () {
		if (generationManager.Instance.genStepOneDone == true) {
			if (foundAdjacent == false) {//adjacent tile startup
				findAdjacentTiles ();

				checkAdjacentTilesStartup ();
				foundAdjacent = true;
			}
		}

		if (generationManager.Instance.genStepTwoDone == true) {
			
		}

		if (GameManager.Instance.selectedTile != null) {
			trSelect = GameManager.Instance.selectedTile;
		}
		//Swapping selected tile
		if (selected == true && this.transform != trSelect) { //If the currently selected tile and the transform do not equal the new selection, deselect this tile
			selected = false;
			tileOutlineSprite.SetActive (false);
		}

		/*
		if (shutdown == true) {
			if (generationManager.Instance.homePlaced == true) {
				selected = false;
				tileOutlineSprite.SetActive (false);
			}
		}*/

		if (shutdown == false) { //shutting down all tile colliders at game start
			if (generationManager.Instance.genStepTwoDone) {
				StartCoroutine ("shutOffColliders");
				shutdown = true;
			}
		}
			
		checkDiscovery ();
	}


	IEnumerator shutOffColliders() {//turning off colliders after a delay
		yield return new WaitForSeconds (2.0f);
		colliderMain.enabled = false;
	}

	public void OnMouseDown() {
		if (discovered) {//if the tile has been seen and discovered
			if (GameManager.Instance.isBuildingSelected == false) {//not allowing tiles to be selected if a building is selected
				if (selected && transform == trSelect) {
					selected = false;
					trSelect = null;
					tileOutlineSprite.SetActive (false);
				} else {
					selected = true;
					GameManager.Instance.selectedTile = this.transform;
					tileOutlineSprite.SetActive (true);
				}
			}
		}
	}

	void checkDiscovery() { //checking to see if the current tile has been seen or not
		if (discovered == false) { //If not discovered yet
			tileBlacked.SetActive (true);
		} else if (discovered == true) {
			tileBlacked.SetActive (false);
		}
		if (inSight == false && discovered == true) {//If not seen currently
			tileGreyed.SetActive (true);
		} else if (inSight == true) {
			tileGreyed.SetActive (false);
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "viewCollider") {
			discovered = true;
		}
		if (col.gameObject.tag == "sightCollider") {
			inSight = true;
		}

		if (generationManager.Instance.genStepOneDone != true) {
			if (col.gameObject.GetComponent<tileHandler> () != null) {
				if (sandSeed == true) { //if the current tile is the sand seed
					col.gameObject.GetComponent<tileHandler> ().adjacentSand = true;
					col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.defaultSand;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Sand";
				}

				if (snowSeed == true) { //if the current tile is the snow seed
					col.gameObject.GetComponent<tileHandler> ().adjacentSnow = true;
					col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.GrassSnowTile;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Snow";
				}

				if (oceanSeed == true) { //if the current tile is the ocean seed
					col.gameObject.GetComponent<tileHandler> ().adjacentOcean = true;
					col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.oceanTile;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Ocean";
				}

				if (mountainSeed == true) {
					if (stopSpread == false) {
						if (col.gameObject.GetComponent<tileHandler> ().mountainSeed != true) {
							col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.mountainTile;
							col.gameObject.GetComponent<tileHandler> ().mountainSeed = true;
							col.gameObject.GetComponent<tileHandler> ().tileType = "Mountain";
							stopSpread = true;
						}
					}
				}

				if (stopSpread == false) {//if the current tile has not stopped spreading
					if (adjacentSand == true) {//setting any adjacent tiles to sand tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentSand = true;
						col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.defaultSand;
						col.gameObject.GetComponent<tileHandler> ().tileType = "Sand";
						int stopper = Random.Range (0, 5); //stopping spread if a certain value is selected
						if (stopper >= 3) {
							stopSpread = true;
						}
					}

					if (adjacentSnow == true) {//setting any adjacent tiles to sand tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentSnow = true;
						col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.GrassSnowTile;
						col.gameObject.GetComponent<tileHandler> ().tileType = "Snow";
						int stopper = Random.Range (0, 5); //stopping spread if a certain value is selected
						if (stopper >= 3) {
							stopSpread = true;
						}
					}

					if (adjacentOcean == true) {//setting any adjacent tiles to ocean tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentOcean = true;
						col.gameObject.GetComponent<tileHandler> ().sr.sprite = generationManager.Instance.oceanTile;
						col.gameObject.GetComponent<tileHandler> ().tileType = "Ocean";
						int stopper = Random.Range (0, 5); //stopping spread if a certain value is selected
						if (stopper >= 3) {
							stopSpread = true;
						}
					}
				}
			}
		}
	}

	void checkAdjacentTilesStartup() {
		for (int i = 0; i < adjacentTiles.Length; i++) {
			if (adjacentTiles [i] != null) {
				string adjTileType = adjacentTiles [i].GetComponent<tileHandler> ().tileType;
				switch (adjTileType) {
				case "Sand":
					if (!tileType.Contains("Sand")) {
						sr.sprite = generationManager.Instance.defaultDirt;
						tileType = "Dirt";
					}
					break;
				case "Snow":
					if (!tileType.Contains("Snow")) {
						sr.sprite = generationManager.Instance.defaultStone;
						tileType = "Stone";
					}
					break;
				case "Mountain":
					if (!tileType.Contains("Mountain")) {
						sr.sprite = generationManager.Instance.defaultStone;
						tileType = "Stone";
					}
					break;
				case "Ocean":
					isNearWater = true;
					break;
				}
			}
		}
	}

	void findAdjacentTiles() {
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

		adjacentTiles [0] = topLeft;
		adjacentTiles [1] = topRight;
		adjacentTiles [2] = Right;
		adjacentTiles [3] = bottomRight;
		adjacentTiles [4] = bottomLeft;
		adjacentTiles [5] = Left;
	}

	void OnTriggerExit2D (Collider2D col) {//leaving sight, enabling fog of war
		if (col.gameObject.tag == "sightCollider") {
			inSight = false;
		}
	}

	void OnBecameVisible() {//enabling collider when in frame of the camera
		colliderMain.enabled = true;
	}

	void OnBecameInvisible() { //disabling collider when out of frame of the camera
		colliderMain.enabled = false;
	}
}
