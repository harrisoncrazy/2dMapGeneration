using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tileHandler : MonoBehaviour {


	//tile adjacentcy
	bool foundAdjacent = false;

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
	public bool isNearWater = false; //if the tile is close by to water


	public SpriteRenderer sr;
	public bool stopSpread = false;
	public bool newSpriteSet = false;


	//Tile seen values
	public bool discovered = false;
	public bool inSight = false;


	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		colliderMain = gameObject.GetComponent<CircleCollider2D> ();
		tileGreyed.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .75f);
		tileHighlighter.GetComponent<SpriteRenderer> ().color = new Color (200f, 0f, 0f, .65f);

		discovered = true;
		inSight = true; 
	}

	// Update is called once per frame
	void Update () {
		if (generationManager.Instance.genStepOneDone == true) {
			if (foundAdjacent == false) {//adjacent tile startup
				this.GetComponent<baseGridPosition>().findAdjacentTiles ();

				checkAdjacentTilesStartup ();

				foundAdjacent = true;
			}
		}

		if (generationManager.Instance.genStepThreeDone == true) {
			this.GetComponent<baseGridPosition>().findAdjacentTiles ();
			this.GetComponent<baseGridPosition>().setAdjArrayVals ();
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
		this.GetComponent<baseGridPosition>().setAdjArrayVals ();
		if (discovered) {//if the tile has been seen and discovered
			if (inputHandler.Instance.checkPlacementStatus() == false) {
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
			} else { //if placing a building
				if (GameManager.Instance.placingWoodGatherer) {
					if (GameManager.Instance.placingWoodGathererTile (this.GetComponent<baseGridPosition> ().mapPosition.X, this.GetComponent<baseGridPosition> ().mapPosition.Y, transform.position, this.GetComponent<baseGridPosition> ().adjacentTiles)) {
						inputHandler.Instance.disablePlacementMode ();
						sr.sprite = null;
						//Destroy (this.gameObject);
					} 
				} else if (GameManager.Instance.placingStoneGatherer) {
					if (GameManager.Instance.placingStoneGathererTile (this.GetComponent<baseGridPosition> ().mapPosition.X, this.GetComponent<baseGridPosition> ().mapPosition.Y, transform.position, this.GetComponent<baseGridPosition> ().adjacentTiles)) {
						inputHandler.Instance.disablePlacementMode ();
						sr.sprite = null;
						//Destroy (this.gameObject);
					}
				} else if (GameManager.Instance.placingFoodGatherer) {
					if (GameManager.Instance.placingFoodGathererTile (this.GetComponent<baseGridPosition> ().mapPosition.X, this.GetComponent<baseGridPosition> ().mapPosition.Y, transform.position, this.GetComponent<baseGridPosition> ().adjacentTiles)) {
						inputHandler.Instance.disablePlacementMode ();
						sr.sprite = null;
						//Destroy (this.gameObject);
					}
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
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.gameObject.tag == "viewCollider") {
			discovered = true;
			inSight = true;
		}
		if (col.gameObject.tag == "sightCollider") {
			discovered = true;
			inSight = true;
		}

		if (generationManager.Instance.genStepOneDone != true) {
			if (col.gameObject.GetComponent<tileHandler> () != null) {
				if (sandSeed == true) { //if the current tile is the sand seed
					col.gameObject.GetComponent<tileHandler> ().adjacentSand = true;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Sand";
				}

				if (snowSeed == true) { //if the current tile is the snow seed
					col.gameObject.GetComponent<tileHandler> ().adjacentSnow = true;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Snow";
				}

				if (oceanSeed == true) { //if the current tile is the ocean seed
					col.gameObject.GetComponent<tileHandler> ().adjacentOcean = true;
					col.gameObject.GetComponent<tileHandler> ().tileType = "Ocean";
				}

				if (mountainSeed == true) {
					if (stopSpread == false) {
						if (col.gameObject.GetComponent<tileHandler> ().mountainSeed != true) {
							col.gameObject.GetComponent<tileHandler> ().mountainSeed = true;
							col.gameObject.GetComponent<tileHandler> ().tileType = "Mountain";
							stopSpread = true;
						}
					}
				}

				if (stopSpread == false) {//if the current tile has not stopped spreading
					if (adjacentSand == true) {//setting any adjacent tiles to sand tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentSand = true;
						col.gameObject.GetComponent<tileHandler> ().tileType = "Sand";
						int stopper = Random.Range (0, 5); //stopping spread if a certain value is selected
						if (stopper >= 3) {
							stopSpread = true;
						}
					}

					if (adjacentSnow == true) {//setting any adjacent tiles to sand tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentSnow = true;
						col.gameObject.GetComponent<tileHandler> ().tileType = "Snow";
						int stopper = Random.Range (0, 5); //stopping spread if a certain value is selected
						if (stopper >= 3) {
							stopSpread = true;
						}
					}

					if (adjacentOcean == true) {//setting any adjacent tiles to ocean tiles
						col.gameObject.GetComponent<tileHandler> ().adjacentOcean = true;
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
		int numAdjOcean = 0;

		for (int i = 0; i < this.GetComponent<baseGridPosition>().adjacentTiles.Length; i++) {
			if (this.GetComponent<baseGridPosition>().adjacentTiles [i] != null && this.GetComponent<baseGridPosition>().adjacentTiles[i].GetComponent<tileHandler>() != null) {
				string adjTileType = this.GetComponent<baseGridPosition>().adjacentTiles [i].GetComponent<tileHandler> ().tileType;
				switch (adjTileType) {
				case "Sand":
					if (!tileType.Contains("Sand")) {
						//sr.sprite = generationManager.Instance.defaultDirt;
						tileType = "Dirt";
					}
					break;
				case "Snow":
					if (!tileType.Contains("Snow")) {
						//sr.sprite = generationManager.Instance.defaultStone;
						tileType = "Stone";
					}
					break;
				case "Mountain":
					if (!tileType.Contains("Mountain")) {
						//sr.sprite = generationManager.Instance.defaultStone;
						tileType = "Stone";
					}
					break;
				case "Ocean":
					isNearWater = true;
					numAdjOcean++;
					break;
				}
			}
		}
		if (numAdjOcean == 6) {
			//sr.sprite = generationManager.Instance.oceanTile;
			tileType = "Ocean";
		}
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
