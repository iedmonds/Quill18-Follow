using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	public GameObject circleCursor;

	Vector3 lastFramePosition;
	Vector3 currFramePosition;

	Vector3 dragStartPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		currFramePosition.z = 0;

		UpdateCursor();
		UpdateDragging();
		UpdateCameraMovement();

		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}

	Tile GetTileAtWorldCoord (Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);

		return WorldController.Instance.World.GetTileAt(x,y);
	}

	void UpdateCursor() {
		// Update the circle cursor position
		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePosition);
		if ( tileUnderMouse != null ) {
			circleCursor.SetActive(true);
			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
			circleCursor.transform.position = cursorPosition;
		} else {
			circleCursor.SetActive(false);
		}
		
		if (GetTileAtWorldCoord(currFramePosition) != null && GetTileAtWorldCoord(lastFramePosition) != null && GetTileAtWorldCoord(currFramePosition) != GetTileAtWorldCoord(lastFramePosition)) Debug.Log ("Hover tile: "+tileUnderMouse.X+", "+tileUnderMouse.Y);
	}

	void UpdateCameraMovement() {
		if(Input.GetMouseButton(1) || Input.GetMouseButton(2)) { //Right or middle mouse button
			
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
		}
	}

	void UpdateDragging() {
		if (Input.GetMouseButtonDown(0)) {
			dragStartPosition = currFramePosition;
		}
		
		// End drag
		if (Input.GetMouseButtonUp(0)) {
			int start_x = Mathf.FloorToInt( dragStartPosition.x );
			int end_x   = Mathf.FloorToInt( currFramePosition.x );
			if (end_x < start_x) {
				int tmp = end_x;
				end_x = start_x;
				start_x = tmp;
			}
			
			int start_y = Mathf.FloorToInt( dragStartPosition.y );
			int end_y   = Mathf.FloorToInt( currFramePosition.y );
			if (end_y < start_y) {
				int tmp = end_y;
				end_y = start_y;
				start_y = tmp;
			}
			
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = WorldController.Instance.World.GetTileAt(x,y);
					if (t != null) {
						if (t.Type == Tile.TileType.Grass) {
							t.Type = Tile.TileType.Water;
						} else if (t.Type == Tile.TileType.Water) {
							t.Type = Tile.TileType.Grass;
						}
					}
				}				
			}
		}
	}
}
