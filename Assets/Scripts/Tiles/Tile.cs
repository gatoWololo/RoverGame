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
	//Actual game object that Unity uses, this class wraps it all.
	protected GameObject gameObject;
	protected SpriteRenderer renderer;
	protected BoxCollider2D collider;
	//Tile may contain an item on top of it :)
	Item item;
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

		//The tile exploration script will need to know which color that tile was originally.
		//Add the script to our game object, and set it's color so it can be restored when a
		//collision happens.
		TileExploration te = gameObject.AddComponent<TileExploration>();
		te.saveColor (renderer.color);

		//Set position and hitbox.
		Vector3 finalPos = new Vector3 (position.x, position.y, 0);
		gameObject.transform.position = finalPos;
		collider.size = new Vector3 (0.1f, 0.1f);
		//Tiles can be passed through unless specified otherwise in subclass.
		canPassThroughIt = true;
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
	//Set the tile to the specified color. Original color has been saved so it can be
	//restored later, used for the tile exploration.
	public void changeAlpha(float alpha){
		Color color = renderer.color;
		renderer.color = new Color (color.r, color.g, color.b, alpha);
		return;
	}
	//================================================================================
	//Set item to this tile TODO: Should there be a check to make sure items don't
	//get placed on a tile if there is already something?
	public void setItem(Item item){
		this.item = item;
		return;
	}
	//================================================================================
	public bool hasItem(){
		return item != null;
	}
	//================================================================================
	//Returns item in this tile and deletes it from tile, foreer >:(
	public Item getItem(){
		Item item = this.item;
		this.item = null;
		return item;
	}
	//================================================================================
	public bool getCanPassThrough(){
		return canPassThroughIt;
	}
}