﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class generationManager : MonoBehaviour {

	public static generationManager Instance;

	public GameObject DefaultTile;
	public GameObject homeBase;
	public GameObject homeBaseCollider;


	public GameObject objToDelete;

	public bool homePlaced = false;

	//Grass tile sprites
	public Sprite HeavyForestTile;
	public Sprite LightForestTile;
	public Sprite GrassTile;
	public Sprite HeavyRockTile;
	public Sprite LightRockTile;

	//Desert tile Sprites
	public Sprite defaultSand;
	public Sprite lightCactusSand1;
	public Sprite lightCactusSand2;
	public Sprite heavyCactusSand;
	public Sprite lightRocksSand1;
	public Sprite lightRocksSand2;
	public Sprite lightRocksSand3;

	//Dirt tile Sprites
	public Sprite defaultDirt;
	public Sprite lightForestDirt;
	public Sprite heavyForestDirt;
	public Sprite lightRocksDirt1;
	public Sprite lightRocksDirt2;

	//Snow tile Sprites
	public Sprite HeavyForestSnowTile;
	public Sprite LightForestSnowTile;
	public Sprite GrassSnowTile;
	public Sprite HeavyRockSnowTile;
	public Sprite LightRockSnowTile;

	//Stone tile Sprites
	public Sprite defaultStone;
	public Sprite LightForestStone;
	public Sprite HeavyForestStone;

	public Sprite oceanTile;

	public Sprite mountainTile;

	private float generationTimerOne = 1.0f;
	private float generationTimerTwo = 1.0f;
	public bool genStepOneDone = false;
	public bool genStepTwoDone = false;
	private int sandRand;
	private int snowRand;
	private int oceanRand;
	private int mountainRand;

	//Map Size ints, and floats for generation
	public int mapSizeX = 11;
	public int mapSizeY = 11;
	private float iDiff = 0;
	private float jDiff = 0;

	public List <List<GameObject>> map = new List<List<GameObject>>();

	// Use this for initialization
	void Start () {
		Instance = this;
		generateMap ();
		sandRand = Random.Range (1, 2);//default 2,4
		for (int i = 0; i <= sandRand; i++) { //Generating random number of sand biomes
			Debug.Log ("Sand seed planted.");
			generateSand ();
		}
		snowRand = Random.Range (1, 3);//default 1,5
		for (int i = 0; i <= snowRand; i++) { //Generating random number of snow biomes
			Debug.Log ("Snow seed planted.");
			generateSnow ();
		}
		oceanRand = Random.Range (3, 5);//default 5,8
		for (int i = 0; i <= oceanRand; i++) { //Generating random number of ocean biomes
			Debug.Log ("Ocean seed planted.");
			generateOcean ();
		}
		mountainRand = Random.Range (4, 10);//default 8,15
		for (int i = 0; i <= mountainRand; i++) { //Generating random number of mountain biomes
			Debug.Log ("Mountain seed planted.");
			generateMountain();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (genStepOneDone != true) {
			generationTimerOne -= Time.deltaTime;//timer for step one
			if (generationTimerOne <= 0) {
				genStepOneDone = true;
				Debug.Log ("GENERATION STEP ONE DONE");
			}
		}

		if (genStepTwoDone != true) {
			if (genStepOneDone == true) { //timer for step 2
				generationTimerTwo -= Time.deltaTime;
				if (generationTimerTwo <= 0) {
					genStepTwoDone = true;
					Debug.Log ("GENERATION STEP TWO DONE");
				}
			}
		}

		if (genStepTwoDone == true) {//after the second gen step is done
			for (int i = 0; i <= mapSizeX - 1; i++) {
				for (int j = 0; j <= mapSizeY - 1; j++) {
					if (map [i] [j].GetComponent<tileHandler> () != null) {
						if (map [i] [j].GetComponent<tileHandler> ().tileType == "Grassland") {
							int rand = Random.Range (1, 101);//finding a random placement for the home base, in a grass tile
							if (homePlaced == false) {
								if (i >= 5 && j >= 5 && j <= mapSizeY - 5) {
									if (rand <= 2) {
										Vector3 pos = map [i] [j].transform.position;

										baseHandler home = ((GameObject)Instantiate (homeBase, pos, Quaternion.Euler (new Vector3 ()))).GetComponent<baseHandler> ();
										GameObject homeCollider = ((GameObject)Instantiate (homeBaseCollider, pos, Quaternion.Euler (new Vector3 ())));
										home.name = "homeBase";
										home.GetComponent<baseGridPosition>().mapPosition.X = i;
										home.GetComponent<baseGridPosition>().mapPosition.Y = j;
										//objToDelete = homeCollider;
										//StartCoroutine ("destroyThing");
										homePlaced = true;

										home.GetComponent<baseGridPosition> ().topLeft = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [0];
										home.GetComponent<baseGridPosition>().topRight = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [1];
										home.GetComponent<baseGridPosition>().Right = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [2];
										home.GetComponent<baseGridPosition>().bottomRight = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [3];
										home.GetComponent<baseGridPosition>().bottomLeft = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [4];
										home.GetComponent<baseGridPosition>().Left = map [i] [j].GetComponent<baseGridPosition> ().adjacentTiles [5];

										Destroy (map [i] [j].gameObject);
										map [i] [j] = home.gameObject;
									}
								}
							}
						}
						if (map [i] [j].GetComponent<tileHandler> ().tileType == "Sand") {//Iterating thru the list of tiles, finding those marked as default sand and selecting a random tile for them to be
							if (map [i] [j].GetComponent<tileHandler> ().newSpriteSet == false) {
								int rand = Random.Range (1, 101);
								if (rand <= 65) {
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
								} else if (rand >= 60 && rand <= 75) {
									int rand2 = Random.Range (1, 3);
									if (rand2 == 1) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightCactusSand1;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Cactus Sand";
									} else if (rand2 == 2) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightCactusSand2;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Cactus Sand";
									}
								} else if (rand >= 75 && rand <= 85) {
									int rand2 = Random.Range (1, 4);
									if (rand2 == 1) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightRocksSand1;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Sand";
									} else if (rand2 == 2) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightRocksSand2;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Sand";
									} else if (rand2 == 3) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightRocksSand3;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Sand";
									}
								} else if (rand >= 85 && rand <= 100) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = heavyCactusSand;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Heavy Cactus Sand";
								}
							}
						}
						if (map [i] [j].GetComponent<tileHandler> ().tileType == "Dirt") {//finding the dirt tiles and randomizing
							if (map [i] [j].GetComponent<tileHandler> ().newSpriteSet == false) {
								int rand = Random.Range (1, 101);
								if (rand <= 35) {
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
								} else if (rand >= 35 && rand <= 70) {
									int rand2 = Random.Range (1, 2);
									if (rand2 == 1) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightForestDirt;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Forest Dirt";
									}
								} else if (rand >= 70 && rand <= 80) {
									int rand2 = Random.Range (1, 3);
									if (rand2 == 1) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightRocksDirt1;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Dirt";
									} else if (rand2 == 2) {
										map [i] [j].GetComponent<tileHandler> ().sr.sprite = lightRocksDirt2;
										map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
										map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Dirt";
									}
								} else if (rand >= 80 && rand <= 100) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = heavyForestDirt;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Heavy Forest Dirt";
								}
							}
						}

						if (map [i] [j].GetComponent<tileHandler> ().tileType == "Snow") {//finding the snow tiles and randomizing
							if (map [i] [j].GetComponent<tileHandler> ().newSpriteSet == false) {
								int rand = Random.Range (1, 101);
								if (rand <= 35) {
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									;
								} else if (rand >= 35 && rand <= 60) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = LightForestSnowTile;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Light Forest Snow";
								} else if (rand >= 60 && rand <= 90) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = HeavyForestSnowTile;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Heavy Forest Snow";
								} else if (rand >= 90 && rand <= 97) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = LightRockSnowTile;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Light Rocks Snow";
								} else if (rand >= 97 && rand <= 100) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = HeavyRockSnowTile;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Heavy Rocks Snow";
								}
							}
						}

						if (map [i] [j].GetComponent<tileHandler> ().tileType == "Stone") {//finding the stone tiles and randomizing
							if (map [i] [j].GetComponent<tileHandler> ().newSpriteSet == false) {
								int rand = Random.Range (1, 101);
								if (rand <= 45) {
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									;
								} else if (rand >= 45 && rand <= 75) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = LightForestStone;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Light Forest Stone";
								} else if (rand >= 75 && rand <= 100) {
									map [i] [j].GetComponent<tileHandler> ().sr.sprite = HeavyForestStone;
									map [i] [j].GetComponent<tileHandler> ().newSpriteSet = true;
									map [i] [j].GetComponent<tileHandler> ().tileType = "Heavy Forest Stone";
								}
							}
						}
					}
				}
			}
		}
	}

	void generateMap() { //Generating default map
		map = new List<List<GameObject>>(); //generatign the playing field, making a grid of tile prefabs, and storing their positiosn in a 2d list
		for (int i = 0; i < mapSizeX; i++) {
			List <GameObject> row = new List<GameObject>();
			for (int j = 0; j < mapSizeY; j++) {
				if (i == 0) {
					iDiff = 0.8f;
				}
				//detects wether or not its an edge tile or not and pushes a row in if it needs to be
				if (j % 2 == 0) {
					iDiff = i + (.2f * (i+1));
				} else if (i != 0) {
					iDiff = i + 0.6f + (.2f * (i+1)); //Offsetting the X
				}
				jDiff = j + (.03f * j); //Offsetting the Y
				int rand = Random.Range (1, 101);//Randomly selecting a base grass tile for the base grid
				if (rand <= 35) {
					tileHandler tile = ((GameObject)Instantiate (DefaultTile, new Vector3 (iDiff, jDiff, 0), Quaternion.Euler (new Vector3 ()))).GetComponent<tileHandler> ();
					tile.GetComponent<baseGridPosition>().mapPosition.X = i;
					tile.GetComponent<baseGridPosition>().mapPosition.Y = j;
					tile.tileType = "Heavy Forest";
					tile.sr.sprite = HeavyForestTile;
					tile.name = "Tile X:" + i + " Y:" + j;
					row.Add (tile.gameObject);
				} else if (rand >= 35 && rand <= 60) {
					tileHandler tile = ((GameObject)Instantiate (DefaultTile, new Vector3 (iDiff, jDiff, 0), Quaternion.Euler (new Vector3 ()))).GetComponent<tileHandler> ();
					tile.GetComponent<baseGridPosition>().mapPosition.X = i;
					tile.GetComponent<baseGridPosition>().mapPosition.Y = j;
					tile.tileType = "Light Forest";
					tile.sr.sprite = LightForestTile;
					tile.name = "Tile X:" + i + " Y:" + j;
					row.Add (tile.gameObject);
				} else if (rand >= 60 && rand <= 90) {
					tileHandler tile = ((GameObject)Instantiate (DefaultTile, new Vector3 (iDiff, jDiff, 0), Quaternion.Euler (new Vector3 ()))).GetComponent<tileHandler> ();
					tile.GetComponent<baseGridPosition>().mapPosition.X = i;
					tile.GetComponent<baseGridPosition>().mapPosition.Y = j;
					tile.tileType = "Grassland";
					tile.sr.sprite = GrassTile;
					tile.name = "Tile X:" + i + " Y:" + j;
					row.Add (tile.gameObject);
				} else if (rand >= 90 && rand <= 97) {
					tileHandler tile = ((GameObject)Instantiate (DefaultTile, new Vector3 (iDiff, jDiff, 0), Quaternion.Euler (new Vector3 ()))).GetComponent<tileHandler> ();
					tile.GetComponent<baseGridPosition>().mapPosition.X = i;
					tile.GetComponent<baseGridPosition>().mapPosition.Y = j;
					tile.sr.sprite = HeavyForestTile;
					tile.tileType = "Light Rocks";
					tile.sr.sprite = LightRockTile;
					tile.name = "Tile X:" + i + " Y:" + j;
					row.Add (tile.gameObject);
				} else if (rand >= 97 && rand <= 100) {
					tileHandler tile = ((GameObject)Instantiate (DefaultTile, new Vector3 (iDiff, jDiff, 0), Quaternion.Euler (new Vector3 ()))).GetComponent<tileHandler> ();
					tile.GetComponent<baseGridPosition>().mapPosition.X = i;
					tile.GetComponent<baseGridPosition>().mapPosition.Y = j;
					tile.sr.sprite = HeavyForestTile;
					tile.tileType = "Heavy Rocks";
					tile.sr.sprite = HeavyRockTile;
					tile.name = "Tile X:" + i + " Y:" + j;
					row.Add (tile.gameObject);
				}
			}
			map.Add(row);
		}
	}

	IEnumerator destroyThing() {
		yield return new WaitForSeconds (2f);
		Destroy (objToDelete);
	}

	void generateSand() {
		int xPlacer = Random.Range (0, mapSizeX - 1);//Generating random place to put the sand seed
		int yPlacer = Random.Range (0, mapSizeY - 1);
		Debug.Log (xPlacer);
		Debug.Log (yPlacer);
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().sandSeed = true;//placing the sand
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().sr.sprite = defaultSand;
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().tileType = "Sand";
	}

	void generateSnow() {
		int xPlacer = Random.Range (0, mapSizeX - 1);//Generating random place to put the snow seed
		int yPlacer = Random.Range (0, mapSizeY - 1);
		Debug.Log (xPlacer);
		Debug.Log (yPlacer);
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().snowSeed = true;//placing the snow
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().sr.sprite = GrassSnowTile;
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().tileType = "Snow";
	}

	void generateOcean() {
		int xPlacer = Random.Range (0, mapSizeX - 1);//Generating random place to put the ocean seed
		int yPlacer = Random.Range (0, mapSizeY - 1);
		Debug.Log (xPlacer);
		Debug.Log (yPlacer);
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().oceanSeed = true;//placing the ocean
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().sr.sprite = oceanTile;
		map [xPlacer] [yPlacer].GetComponent<tileHandler>().tileType = "Ocean";
		//Destroy (rivCol.gameObject);
	}
		
	void generateMountain() {
		int xPlacer1 = Random.Range (0, mapSizeX);//Generating random place to put the mountain seed
		int yPlacer1 = Random.Range (0, mapSizeY);
		map [xPlacer1] [yPlacer1].GetComponent<tileHandler>().mountainSeed = true;//placing the mountain
		map [xPlacer1] [yPlacer1].GetComponent<tileHandler>().sr.sprite = mountainTile;
		map [xPlacer1] [yPlacer1].GetComponent<tileHandler>().tileType = "Mountain";
	}
}