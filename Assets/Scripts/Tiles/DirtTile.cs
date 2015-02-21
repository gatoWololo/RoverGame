using UnityEngine;
using System.Collections;

// Enum for the sake of proof of concept.
public enum DirtColor {darker, lighter };
/*
 * Generic dirt tile that will cover some of the land.
 */
public class DirtTile : Tile {
	private string dirtLocation1 = "Textures/dirtTile1";
	private string dirtLocation2 = "Textures/dirtTile2";
	private string myTexture;

	/// <summary>
	/// Creates new DirtTile object. Takes position for location of dirt tile.
	/// </summary>
	/// <param name="position">Position.</param>
	public DirtTile(Vector2 position, DirtColor color){
		//Create new empty gameobject and attach 
		gameObject = new GameObject ();
		renderer = gameObject.AddComponent<SpriteRenderer>();
		collider = gameObject.AddComponent<BoxCollider2D>();
		gameObject.AddComponent<TileExploration>();

		Vector3 finalPos = new Vector3 (position.x, position.y, 0);

		if (color == DirtColor.darker)
			myTexture = dirtLocation1;
		else
			myTexture = dirtLocation2;

		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.transform.position = finalPos;
		collider.size = new Vector3 (0.1f, 0.1f);

		return;

	}
	// Use this for initialization
	void Start () {
		//canPassThroughIt = true;
		//myRenderer = new SpriteRenderer ();
		//myRenderer.sprite = Resources.Load (dirtLocation1, typeof(Sprite)) as Sprite;
	}

	// Update is called once per frame
	void Update () {
	
	}


}
