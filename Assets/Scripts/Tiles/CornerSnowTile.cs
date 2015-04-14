using UnityEngine;
using System.Collections;
using System;

/*
 * Generic dirt tile that will cover some of the land.
 */
public class CornerSnowTile : Tile {
	private string edgeEastLocation = "Textures/snowEdgeTileEast";
	private string edgeWestLocation = "Textures/snowEdgeTileWest";
	private string edgeNorthLocation = "Textures/snowEdgeTileNorth";
	private string edgeSouthLocation = "Textures/snowEdgeTileSouth";

	/// <summary>
	/// The number of textures avaliable for tiles of this type. Used to return a random
	/// number corresponding to one of the textures.
	/// </summary>
	private int numberOfTextures = 4;
	//Used for randoms.
	//static private Array values;
	static System.Random random;
	//================================================================================
	// Static initializer used by whole class to pick from among textures.
	static CornerSnowTile(){
		random = new System.Random ();
	}
	//================================================================================
	/// <summary>
	/// Creates new DirtTile object. Takes position for location of dirt tile.
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="isCorner">Actual corner position to specify different look.</param>
	public CornerSnowTile(Vector2 position, Direction dir) : base(position){

		string myTexture = getTexture(dir);
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = "cornerSnowTile";
		
		return;
	}
	//================================================================================
	string getTexture(Direction dir){
		string myTexture = null;

		switch (dir) {
		case Direction.Up:
			myTexture = edgeNorthLocation;
			break;
		case Direction.Down:
			myTexture = edgeSouthLocation;
			break;
		case Direction.Left:
			myTexture = edgeWestLocation;
			break;
		case Direction.Right:
			myTexture = edgeEastLocation;
			break;
		}

		return myTexture;
	}
}