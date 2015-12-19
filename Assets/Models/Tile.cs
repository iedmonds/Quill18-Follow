using UnityEngine;
using System.Collections;
using System;

public class Tile {

	public enum TileType { Water, Grass };

	TileType type;

	Action<Tile> cbTileTypeChanged;

	public TileType Type {
		get {
			return type;
		}
		set {
			type = value;
			//Call the callback in WorldController, let things know it's changed
			if(cbTileTypeChanged != null)
				cbTileTypeChanged(this);
		}
	}

	public int X {
		get {
			return x;
		}
	}

	public int Y {
		get {
			return y;
		}
	}

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;
	int x;
	int y;

	public Tile(World world, int x, int y) {
		this.world = world;
		this.x = x;
		this.y = y;

	}

	public void RegisterTileTypeChangeCallback(Action<Tile> callback) {
		cbTileTypeChanged += callback;
	}
}
