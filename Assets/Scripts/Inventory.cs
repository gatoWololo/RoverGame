using UnityEngine;
using System.Collections.Generic;

//Rover holds an invetory that tells it which items it has collected!
public class Inventory {
	private List<Item> list;
	//Constructor for our inventory.
	public Inventory(){
		list = new List<Item> ();

	}

	public void addElement(Item item){
		Debug.Log ("Item: " + item.getName () + " has been added to rover's inventory!");
		list.Add (item);
		return;
	}
}
