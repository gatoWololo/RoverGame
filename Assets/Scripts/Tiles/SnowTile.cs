using UnityEngine;
using System.Collections;
using System;

/*
 * Generic Ice tile tile that will cover most of the land.
 */
public class SnowTile : Tile {
	private string iceLocation1 = "Textures/snowTile";

	/// <summary>
	/// The number of textures avaliable for tiles of this type. Used to return a random
	/// number corresponding to one of the textures.
	/// </summary>
	private int numberOfTextures = 1;
	private string myTexture;
	//Used for random.
	//static private Array values;
	static System.Random random;
	//================================================================================
	// Static initializer used by whole class to pick from among textures.
	static SnowTile(){
		random = new System.Random ();
	}
	//================================================================================
	/// <summary>
	/// Creates new iceTile object. Takes position for location of iceTile.
	/// </summary>
	/// <param name="position">Position.</param>
	public SnowTile(Vector2 position) : base(position){
		myTexture = getRandTexture ();
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = "snowTile";
		
		return;
	}
	//================================================================================
	private string getRandTexture(){
		return iceLocation1;
	}
	//================================================================================
}
