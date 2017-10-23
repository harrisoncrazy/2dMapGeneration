using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfindingManager : MonoBehaviour {

	public static pathfindingManager Instance;

	void Start () {
		Instance = this;
	}

	baseGridPosition currentPathFrom, currentPathTo;
	bool currentPathExists;

	public void FindPath (baseGridPosition fromCell, baseGridPosition toCell) {//finding distance to the current selected tile
		//System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
		//sw.Start ();

		ClearPath ();
		currentPathFrom = fromCell;
		currentPathTo = toCell;
		currentPathExists = Search (fromCell, toCell);
		ShowPath ();

		//sw.Stop ();
		//Debug.Log (sw.ElapsedMilliseconds);
	}

	void ShowPath() {
		if (currentPathExists) {
			baseGridPosition current = currentPathTo;
			while (current != currentPathFrom) {
				current.selectOutline.SetActive (true);
				current.SetLabel ();
				current = current.PathFrom.GetComponent<baseGridPosition>();
			}
		}
	}

	void ClearPath() {
		if (currentPathExists) {
			baseGridPosition current = currentPathTo;
			while (current != currentPathFrom) {
				current.tileInfoText.text = null;
				current.selectOutline.SetActive (false);
				current = current.PathFrom.GetComponent<baseGridPosition>();
			}
			current.selectOutline.SetActive (false);
			currentPathExists = false;
		}
		currentPathFrom = currentPathTo = null;
	}

	int searchFrontierPhase;

	bool Search (baseGridPosition fromCell, baseGridPosition toCell) {
		searchFrontierPhase += 2;

		fromCell.selectOutline.SetActive (true);
		toCell.selectOutline.SetActive (true);

		List<GameObject> frontier = new List<GameObject> ();//queue of pathfinding
		fromCell.SearchPhase = searchFrontierPhase;
		fromCell.Distance = 0;
		frontier.Add (fromCell.gameObject);//pushing first object to queue
		while (frontier.Count > 0) {
			GameObject current = frontier [0];
			frontier.RemoveAt (0);
			current.GetComponent<baseGridPosition> ().SearchPhase += 1;

			if (current == toCell.gameObject) {
				return true;
			}

			for (int currectDirection = 0; currectDirection <= 5; currectDirection++) {//going through neigbors from top left to left clockwise
				GameObject neighbor = current.GetComponent<baseGridPosition> ().adjacentTiles [currectDirection];

				if (neighbor == null || neighbor.GetComponent<baseGridPosition>().SearchPhase > searchFrontierPhase) {
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
				if (neighbor.GetComponent<baseGridPosition> ().SearchPhase < searchFrontierPhase) {
					neighbor.GetComponent<baseGridPosition> ().SearchPhase = searchFrontierPhase;
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

		return false;
	}
}
