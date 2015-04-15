using UnityEngine;
using System.Collections;

public class Battery : Item {
	private string batteryLocation1 = "Textures/battery1";
	//This is so if we add extra
	private string myTexture;
	private int numberOfTextures = 1;

	//Constructor and implicit constructor call.
	public Battery(Vector2 position) : base(position){
		//Constant for all classes, they have a name and an id for items.
		itemId = 1;
		itemName = "battery";

		myTexture = batteryLocation1;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;

		return;
	}
}
