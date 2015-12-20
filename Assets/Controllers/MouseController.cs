using UnityEngine;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	public GameObject circleCursorPrefab;

	Vector3 lastFramePosition;
	Vector3 currFramePosition;

	Vector3 dragStartPosition;
	List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start () {
		dragPreviewGameObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		currFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		currFramePosition.z = 0;

		//UpdateCursor();
		UpdateDragging();
		UpdateCameraMovement();

		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}

//	void UpdateCursor() {
//		// Update the circle cursor position
//
//		Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord(currFramePosition);
//		if ( tileUnderMouse != null ) {
//			circleCursor.SetActive(true);
//			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
//			circleCursor.transform.position = cursorPosition;
//		} else {
//			circleCursor.SetActive(false);
//		}
//		
//		if (WorldController.Instance.GetTileAtWorldCoord(currFramePosition) != null && WorldController.Instance.GetTileAtWorldCoord(lastFramePosition) != null && WorldController.Instance.GetTileAtWorldCoord(currFramePosition) != WorldController.Instance.GetTileAtWorldCoord(lastFramePosition)) {
//			Debug.Log ("Hover tile: "+tileUnderMouse.X+", "+tileUnderMouse.Y);
//		}
//	}

	/// <summary>
	/// Updates the camera movement.
	/// Middle and right mouse buttons drag to scroll.
	/// </summary>

	void UpdateCameraMovement() {
		if(Input.GetMouseButton(1) || Input.GetMouseButton(2)) { //Right or middle mouse button
			
			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate(diff);
		}

		// Zoom control
		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel") * 2f;
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
	}

	/// <summary>
	/// Updates the screen when dragging with the left
	/// mouse button.
	/// </summary>

	void UpdateDragging() {
		// Start drag
		if (Input.GetMouseButtonDown(0)) {
			dragStartPosition = currFramePosition;
		}

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

		// Clean up old drag previews
		while(dragPreviewGameObjects.Count > 0) {
			GameObject go = dragPreviewGameObjects[0];
			dragPreviewGameObjects.RemoveAt(0);
			SimplePool.Despawn (go);
		}

		// Dragging
		if (Input.GetMouseButton(0)) {
			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					// Display the building cursor on this tile
					GameObject go = SimplePool.Spawn (circleCursorPrefab, new Vector3 (x,y,0), Quaternion.identity);
					go.transform.SetParent(this.transform, true);
					dragPreviewGameObjects.Add(go);
				}
			}
		}
		
		// End drag
		if (Input.GetMouseButtonUp(0)) {
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
