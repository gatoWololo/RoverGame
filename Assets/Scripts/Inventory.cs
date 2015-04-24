using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rover holds an invetory that tells it which items it has collected!
public class Inventory : MonoBehaviour  {
	private List<Item> list;

	private Item lastItem;

	//Constructor for our inventory.
	public Inventory(){
		list = new List<Item> ();
		lastItem = null;
	}

	public void addElement(Item item){
		//Debug.Log ("Item: " + item.getName () + " has been added to rover's inventory!");
		list.Add (item);
		lastItem = item;
		return;
	}

	public void removeElement(int index){
		int x = list.Count;
		list.RemoveAt(index);
		int y = list.Count;
		Debug.Log ("friggin stop");
	}

	public int getLastItemType(){
		return lastItem.getItemId ();
	}

	public int getIdAtIndex(int index){
		if(index < list.Count){ 
			Item item = list[index];
			return item.getItemId();
		}
		else return -1;
	}

	public int getInventoryLength(){
		return list.Count;
	}

}
