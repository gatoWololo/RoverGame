using UnityEngine;
using System.Collections;

public class WaterTile : Tile {
	//Eventually this may get hairy, we could create a class that hold a mapping between
	//all locations of textures and classes?
	private string texture1 = "Textures/snowTile";
	// Use this for initialization

	public WaterTile(Vector2 position): base(position){
		renderer.sprite = Resources.Load (texture1, typeof(Sprite)) as Sprite;
		gameObject.name = "waterTile";
		canPassThroughIt = false;
		
		return;
		
	}
}
