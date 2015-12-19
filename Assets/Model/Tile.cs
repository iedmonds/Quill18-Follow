using UnityEngine;
using System.Collections;

public class Tile {

	public enum TileType { Dirt, Water, Grass };

	TileType type = TileType.Dirt;

	LooseObject looseObject;
	InstalledObject installedObject;
    //test code
	World world;
	int x;
	int y;

	public Tile(World world, int x, int y) {
		this.world = world;
		this.x = x;
		this.y = y;

	}
}
