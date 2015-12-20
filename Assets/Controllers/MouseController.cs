using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

	Vector3 lastFramePosition;

	public GameObject circleCursor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 currFramePositon = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		currFramePositon.z = 0;

		// Update cursor position

		Tile tileUnderMouse = GetTileAtWorldCoord(currFramePositon);
		if ( tileUnderMouse != null ) {
			circleCursor.SetActive(true);
			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
			circleCursor.transform.position = cursorPosition;
		} else {
			circleCursor.SetActive(false);
		}

		// Handle screen dragging
		if(Input.GetMouseButton(1) || Input.GetMouseButton(2)) { //Right or middle mouse button

			Vector3 diff = lastFramePosition - currFramePositon;
			Camera.main.transform.Translate(diff);
		}

		lastFramePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		lastFramePosition.z = 0;
	}

	Tile GetTileAtWorldCoord (Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);

		return WorldController.Instance.World.GetTileAt(x,y);
	}
}
