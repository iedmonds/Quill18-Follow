using UnityEngine;
using System.Collections;
using System;

public class WorldController : MonoBehaviour {

	public static WorldController Instance { get; protected set; }
	public Sprite waterSprite;
	public Sprite grassSprite;
	public World World { get; protected set; }

	// Use this for initialization
	void Start () {
		// Create a world
		if(Instance != null) {
			Debug.LogError("There should never be two world controllers.");
		}
		Instance = this;

		World = new World ();

		// Create a game object for each tile, so they show visually
		for (int x = 0; x < World.Width; x++) {
			for (int y = 0; y < World.Height; y++) {
				Tile tile_data = World.GetTileAt(x,y);
				GameObject tile_go = new GameObject();
				tile_go.name = "Tile_"+x+"_"+y;
				tile_go.transform.position = new Vector3(tile_data.X,tile_data.Y,0);
				tile_go.transform.SetParent(this.transform, true);

				tile_go.AddComponent<SpriteRenderer>();

				tile_data.RegisterTileTypeChangeCallback( (tile) => {OnTileTypeChanged(tile,tile_go);} );
			}
		}

		World.RandomizeTiles();
	}



	void Update () {

	}

	void OnTileTypeChanged(Tile tile_data, GameObject tile_go) {

		if(tile_data.Type == Tile.TileType.Grass) {
			tile_go.GetComponent<SpriteRenderer>().sprite = grassSprite;
		} else if (tile_data.Type == Tile.TileType.Water) {
			tile_go.GetComponent<SpriteRenderer>().sprite = waterSprite;
		} else {
			Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
		}
	}
}
