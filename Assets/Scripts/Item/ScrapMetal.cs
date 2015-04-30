using UnityEngine;
using System.Collections;

public class ScrapMetal : Item {
	private string scrapMetalLocation1 = "Textures/scrapMetal";
	//This is so if we add extra
	private string myTexture;
	private int numberOfTextures = 1;
	
	//Constructor and implicit constructor call.
	public ScrapMetal(Vector2 position) : base( position){
		//Constant for all classes, they have a name and an id for items.
		itemId = 2;
		itemName = "scrapMetal";

		myTexture = scrapMetalLocation1;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;
		isWinItem = true;

		return;
	}
}
