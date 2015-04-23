using UnityEngine;
using System.Collections;

public class Drill : Item {
	private string fluxCapacitorLocation = "Textures/drill1";
	//This is so if we add extra
	private int numberOfTextures = 1;
	private string myTexture;
	
	//Constructor and implicit constructor call.
	public Drill(Vector2 position) : base( position){
		//Constant for all classes, they have a name and an id for items.
		itemId = 6;
		itemName = "drill";
		
		myTexture = fluxCapacitorLocation;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;
		
		return;
	}
}
