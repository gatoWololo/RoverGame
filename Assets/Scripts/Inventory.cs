using UnityEngine;
using System.Collections.Generic;

//Rover holds an invetory that tells it which items it has collected!
public class Inventory {
	private List<Item> list;

	private Item lastItem;

	//Constructor for our inventory.
	public Inventory(){
		list = new List<Item> ();
		lastItem = null;
	}

	public void addElement(Item item){
		Debug.Log ("Item: " + item.getName () + " has been added to rover's inventory!");
		list.Add (item);
		lastItem = item;
		return;
	}

	public int getLastItemType(){
		return lastItem.getItemId ();
	}

	public int getInventoryLength(){
		return list.Count;
	}




}
