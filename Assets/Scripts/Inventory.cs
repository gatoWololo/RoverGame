using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Rover holds an invetory that tells it which items it has collected!
public class Inventory : MonoBehaviour  {
	private List<Item> list;

	private Item lastItem;

	private int maxItems;
	private int currentItems;

	public List<Item> getList(){
		return list;
	}

	//Constructor for our inventory.
	public Inventory(){
		list = new List<Item> ();
		lastItem = null;
		maxItems = 0;
		currentItems = 0;
	}

	public void addElement(Item item){
		//Debug.Log ("Item: " + item.getName () + " has been added to rover's inventory!");
		if(maxItems > 0 && currentItems < maxItems){
			list.Add (item);
			lastItem = item;
			currentItems++;
		}
		return;
	}

	public void removeElement(int index){
		list.RemoveAt(index);
		currentItems--;
	}

	public Item getLastItem(){
		return lastItem;
	}

	public Item getItemAtIndex(int index){
		if(index < list.Count){ 
			Item item = list[index];
			return item;
		}
		else return null;
	}

	public int getInventoryLength(){
		return list.Count;
	}
	
	public void setMaxItems(int n){
		maxItems = n;
	}

	public bool full(){
		if(currentItems > maxItems){ Debug.LogError("ERROR: Inventory bounds surpassed");}
		return (currentItems >= maxItems );
	}

	public int getLength(){
		return list.Count;
	}

}
