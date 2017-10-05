using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfindingManager : MonoBehaviour {

	public static pathfindingManager Instance;

	void Start () {
		Instance = this;
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
}
