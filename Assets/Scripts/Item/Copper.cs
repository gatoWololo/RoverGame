using UnityEngine;
using System.Collections;

public class Copper : Item {
	private string powderLocation1 = "Textures/copper1";
	//This is so if we add extra
	private string myTexture;
	private int numberOfTextures = 1;
	
	//Constructor and implicit constructor call.
	public Copper(Vector2 position) : base(position){
		//Constant for all classes, they have a name and an id for items.
		itemId = 5;
		itemName = "copper";
		
		myTexture = powderLocation1;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;
		isWinItem = true;
		
		return;
	}
}

