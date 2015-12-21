using UnityEngine;
using System.Collections;
using System;

// TileType is the base type of the tile.
public enum TileType { Empty, Water, Grass };

public class Tile {

	private TileType _type = TileType.Empty;
	public TileType Type {
		get { return _type;	}
		set {
			TileType oldType = _type;
			_type = value;
			//Call the callback in WorldController, let things know it's changed
			if(cbTileTypeChanged != null && oldType != _type) {
				cbTileTypeChanged(this);
			}
		}
	}

	// The function we call every time our type changes
	Action<Tile> cbTileTypeChanged;

	World world;
	public int X { get; protected set; }
	public int Y { get; protected set; }

	LooseObject looseObject;
	InstalledObject installedObject;

	public Tile(World world, int x, int y) {
		this.world = world;
		this.X = x;
		this.Y = y;

	}
	/// <summary>
	/// Register a function to be called when our tile type changes
	/// </summary>
	/// <param name="callback">Callback.</param>
	public void RegisterTileTypeChangeCallback(Action<Tile> callback) {
		cbTileTypeChanged += callback;
	}

	/// <summary>
	/// Unregisters the tile type change callback.
	/// </summary>
	/// <param name="callback">Callback.</param>
	public void UnregisterTileTypeChangeCallback(Action<Tile> callback) {
		cbTileTypeChanged -= callback;
	}
}
