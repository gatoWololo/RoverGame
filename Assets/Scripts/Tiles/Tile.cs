using UnityEngine;
using System.Collections;

/**
 * Simple Class defining a tile object which are what the rover travels on. Tiles have
 * different properties but share common information.
 */
public abstract class Tile {
	public bool isVisible;
	// Whether or not the rover can travel through this type  of tiles.
	protected bool canPassThroughIt;
	//Chunk this tile belongs to.
	private int iChunk;
	private int jChunk;
	// Location in the array where this tile is located.
	private int iIndex;
	private int jIndex;

	protected GameObject gameObject;
	protected SpriteRenderer renderer;
	protected BoxCollider2D collider;

	//================================================================================
	/// <summary>
	/// Initializes a new instance of the <see cref="Tile"/> class.
	/// Default constructor used by array of Tiles.
	/// </summary>
	protected Tile(){
		return;
	}
	//================================================================================
	/// <summary>
	/// Initializes a new instance of the <see cref="Tile"/> class.
	/// All tile objects should have this basic properties so we initialize them.
	/// This constructor is called from the derived class' constructor.
	/// </summary>
	/// <param name="position">Position.</param>
	protected Tile(Vector2 position){
		//Create new empty gameobject and attach 
		gameObject = new GameObject ();
		renderer = gameObject.AddComponent<SpriteRenderer>();
		collider = gameObject.AddComponent<BoxCollider2D>();
		gameObject.AddComponent<TileExploration>();
		
		//Set position and hitbox.
		Vector3 finalPos = new Vector3 (position.x, position.y, 0);
		gameObject.transform.position = finalPos;
		collider.size = new Vector3 (0.1f, 0.1f);

		return;
	}
	//================================================================================
	//Set position for tileIndex.
	public void setTileIndex(int i,int j){
		this.iIndex = i;
		this.jIndex = j;
		return;
	}
	//================================================================================
	//get position i for tileIndex.
	public int getIndexI(){
		return this.iIndex;
	}
	//================================================================================
	//get position i for tileIndex.
	public int getIndexJ(){
		return this.jIndex;
	}
	//================================================================================
	public void setVisible(bool visible){
		gameObject.GetComponent<Renderer> ().renderer.enabled = visible;
		
		return;
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
	public GameObject getGameObject(){
		return this.gameObject;	
		
	}	
	//================================================================================
}
