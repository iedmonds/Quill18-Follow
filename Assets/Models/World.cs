using UnityEngine;
using System.Collections;

public class World {

	Tile[,] tiles;
	int lastTile_x;
	int lastTile_y;

	public int Width { get; protected set; }
	public int Height {	get; protected set; }

	public World(int width = 100, int height = 100) {
		Width = width;
		Height = height;

		tiles = new Tile[Width,Height];

		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				tiles[x,y] = new Tile(this,x,y);
			}
		}
		Debug.Log("World created with size " + Width + " x " + Height + " ("+ (Width * Height) + ") tiles.");

	}

	// A function to create a random world
	public void RandomizeTiles() {
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
//				if (Random.Range(0,2) == 0) {
					tiles[x,y].Type = TileType.Grass;

//				} else {
//					tiles[x,y].Type = Tile.TileType.Water;
//				}
			}
		}
	}

	public Tile GetTileAt(int x, int y) {
		if (x >= Width || x < 0 || y >= Height || y < 0) {
			Debug.LogWarning("Tile ["+x+","+y+"] is out of range");
			return null;
		}
//		if (x >= Width) x = Width-1;
//		if (x < 0) x = 0;
//		if (y >= Height) y = Height-1;
//		if (y < 0) y = 0;
		return tiles[x,y];
	}
}
