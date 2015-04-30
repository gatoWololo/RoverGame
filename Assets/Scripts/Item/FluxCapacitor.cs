using UnityEngine;
using System.Collections;

public class FluxCapacitor : Item {
	private string fluxCapacitorLocation = "Textures/fluxCapacitor";
	//This is so if we add extra
	private int numberOfTextures = 1;
	private string myTexture;
	
	//Constructor and implicit constructor call.
	public FluxCapacitor(Vector2 position) : base( position){
		//Constant for all classes, they have a name and an id for items.
		itemId = 3;
		itemName = "fluxCapacitor";
		
		myTexture = fluxCapacitorLocation;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;
		isWinItem = true;
		
		return;
	}
}
