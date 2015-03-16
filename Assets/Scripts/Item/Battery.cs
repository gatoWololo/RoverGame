using UnityEngine;
using System.Collections;

public class Battery : Item {
	//Constant for all classes, they have a name and an id for items.
	private const int id = 1;
	private const string itemName = "battery";

	private string batteryLocation1 = "Textures/battery1";
	//This is so if we add extra
	private string myTexture;
	private int numberOfTextures = 1;

	//Constructor and implicit constructor call.
	public Battery(Vector2 position) : base(id, itemName, position){
		myTexture = batteryLocation1;
		renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
		gameObject.name = itemName;

		return;
	}
}
