using UnityEngine;
using System.Collections;
using System;

// Enum for random tile pick.
public enum DirtColor {darker, lighter };
/*
 * Generic dirt tile that will cover some of the land.
 */
public class DirtTile : Tile {
	private string dirtLocation1 = "Textures/dirtTile1";
	private string dirtLocation2 = "Textures/dirtTile2";
	/// <summary>
	/// The number of textures avaliable for tiles of this type. Used to return a random
	/// number corresponding to one of the textures.
	/// </summary>
	private int numberOfTextures = 2;
	private string myTexture;
	//Used for random.
	static private Array values;
	static System.Random random;
	//================================================================================
		/// <summary>
	/// Creates new DirtTile object. Takes position for location of dirt tile.
	/// </summary>
	/// <param name="position">Position.</param>
	public DirtTile(Vector2 position) : base(position){
		random = new System.Random ();

		myTexture = getRandTexture ();
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;

		return;

	}
	//================================================================================
	private string getRandTexture(){
		int color = random.Next (1, numberOfTextures + 1);
		string myTexture;

		if (color == 1)
			myTexture = dirtLocation1;
		else
			myTexture = dirtLocation2;

		return myTexture;
	}
	//================================================================================
}
