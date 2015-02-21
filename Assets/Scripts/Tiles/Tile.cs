using UnityEngine;
using System.Collections;

/**
 * Simple Class defining a tile object which are what the rover travels on. Tiles have
 * different properties but share common information.
 */
public abstract class Tile {
	public bool isVisible;
	// Whether or not the rover can travel through this type  of tiles.
	public bool canPassThroughIt;
	//Chunk this tile belongs to.
	private int iChunk;
	private int jChunk;
	// Location in the array where this tile is located.
	private int iIndex;
	private int jIndex;

	protected GameObject gameObject;
	protected SpriteRenderer renderer;
	protected BoxCollider2D collider;

	//Set position for tileIndex.
	public void setTileIndex(int i,int j){
		this.iIndex = i;
		this.jIndex = j;
		return;
	}

	//get position i for tileIndex.
	public int getIndexI(){
		return this.iIndex;
	}

	//get position i for tileIndex.
	public int getIndexJ(){
		return this.jIndex;
	}

	public void setVisible(bool visible){
		gameObject.GetComponent<Renderer> ().renderer.enabled = visible;
		
		return;
	}

	// Update is called once per frame
	void Update () {
	
	}


}
