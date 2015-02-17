using UnityEngine;
using System.Collections;

/**
 * Simple Class defining a tile object which are what the rover travels on. Tiles have
 * different properties but share common information.
 */
public abstract class Tile: MonoBehaviour {
	public bool isVisible;
	// Whether or not the rover can travel through this type  of tiles.
	public bool canPassThroughIt;
	// Location in the array where this tile is located.
	private int iIndex;
	private int jIndex;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//If object is seen by the rover it should be set to viewable.
	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("Tile dectected " +  this.iIndex.ToString() + ", " + this.jIndex.ToString() + ")");
		this.renderer.enabled = true;

		return;
	}
}
