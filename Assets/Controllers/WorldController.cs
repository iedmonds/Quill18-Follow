using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class WorldController : MonoBehaviour {

	public static WorldController Instance { get; protected set; }
	public Sprite waterSprite;
	public Sprite grassSprite;
	Dictionary<Tile, GameObject> tileGameObjectMap;

	// The world and tile data
	public World World { get; protected set; }



	// Use this for initialization
	void Start () {
		// Create a world
		if(Instance != null) {
			Debug.LogError("There should never be two world controllers.");
		}
		Instance = this;

		World = new World ();

		// Instantiate our dictionary that tracks which GameObject is rendering which Tile data.
		tileGameObjectMap = new Dictionary<Tile, GameObject>();

		// Create a game object for each tile, so they show visually
		for (int x = 0; x < World.Width; x++) {
			for (int y = 0; y < World.Height; y++) {
				Tile tile_data = World.GetTileAt(x,y);
				GameObject tile_go = new GameObject();

				tileGameObjectMap.Add(tile_data, tile_go);

				tile_go.name = "Tile_"+x+"_"+y;
				tile_go.transform.position = new Vector3(tile_data.X,tile_data.Y,0);
				tile_go.transform.SetParent(this.transform, true);

				tile_go.AddComponent<SpriteRenderer>();

				// Register our callback so that our GameObject gets updated whenever
				// the tile's type changes.
				tile_data.RegisterTileTypeChangeCallback( OnTileTypeChanged );
			}
		}

		World.RandomizeTiles();
	}

	void Update () {

	}

	// THIS IS AN EXAMPLE AND NOT CURRENTLY IMPLEMENTED
	void DestroyAllTileGameObjects() {
		// This function might get called when changing levels/floors.
		// We need to destroy all visual **GameObjects** -- but not the actual tile data

		while(tileGameObjectMap.Count > 0) {
			Tile tile_data = tileGameObjectMap.Keys.First();
			GameObject tile_go = tileGameObjectMap[tile_data];

			// Remove the pair from the map
			tileGameObjectMap.Remove(tile_data);

			// Unregister the callback
			tile_data.UnregisterTileTypeChangeCallback(OnTileTypeChanged);

			// Destroy the visual GameObject
			Destroy(tile_go);
		}
	}

	void OnTileTypeChanged(Tile tile_data) {

		if(tileGameObjectMap.ContainsKey(tile_data) == false) {
			Debug.LogError("tileGameObjectMap doesn't contain the tile_data.  Did you forget to add the tile to the dictionary, or unregister a callback?");
			return;
		}

		GameObject tile_go = tileGameObjectMap[tile_data];

		if(tile_go == null) {
			Debug.LogError("tileGameObjectMap's returned GameObject is null.  Did you forget to add the tile to the dictionary, or unregister a callback?");
				return;
		}

		if(tile_data.Type == TileType.Grass) {
			tile_go.GetComponent<SpriteRenderer>().sprite = grassSprite;
		} else if (tile_data.Type == TileType.Water) {
			tile_go.GetComponent<SpriteRenderer>().sprite = waterSprite;
		} else {
			Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
		}
	}
		
	public Tile GetTileAtWorldCoord (Vector3 coord) {
		int x = Mathf.FloorToInt(coord.x);
		int y = Mathf.FloorToInt(coord.y);
		
		return WorldController.Instance.World.GetTileAt(x,y);
	}
}
