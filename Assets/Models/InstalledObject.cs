using UnityEngine;
using System.Collections;

public class InstalledObject {

    // This represents the base tile of the object.  In practice it may occupy multiple tiles.
	Tile tile;

    // This "ojbectType" will be queried by the visual system to know what sprite to render for this object
	string objectType;

	float movementCost;

    int width;
    int height;

    // This is used by our object factory to create the prototypical object
    public InstalledObject ( string objectType, float movementCost = 1f, int width = 1, int height = 1) {
        this.objectType = objectType;
        this.movementCost = movementCost;
        this.width = width;
        this.height = height;
    }

    protected InstalledObject ( InstalledObject proto, Tile tile) {
        this.objectType = proto.objectType;
        this.movementCost = proto.movementCost;
        this.width = proto.width;
        this.height = proto.height;

        this.tile = tile;
    }
}
