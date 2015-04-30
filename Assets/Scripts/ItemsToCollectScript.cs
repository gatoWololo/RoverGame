using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ItemsToCollectScript : MonoBehaviour {
	public static int copperCounter = 5;
	public static int fluxCapacitorCounter = 1;
	public static int powderCounter = 8;
	public static int scrapMetalCounter = 25;
	public string copperStr = "Copper: ";
	public string fluxCapacitorStr = "Flux Capacitor: ";
	public string powderStr = "Powder: ";
	public string scrapMetalStr = "Scrap Metal: ";
	public string headingStr = "Items to Collect";

	// Use this for initialization
	void Start () {
		//Skip so the rover doesn't try to get a text box too....
		if(GetComponentInParent<RoverScript>() == null)
			displayCounter ();
	}
	
	// Update is called once per frame
	void Update () {
		//Skip so the rover doesn't try to get a text box too....
		if(GetComponentInParent<RoverScript>() == null)
			displayCounter ();
	}

	public void displayCounter(){
		Text text = GetComponentInParent<Text> ();
		string displayString = headingStr + "\n" + fluxCapacitorStr + fluxCapacitorCounter.ToString ()
			+ "\n" + copperStr + copperCounter.ToString () + "\n" + powderStr + powderCounter.ToString ()
			+ "\n" + scrapMetalStr + scrapMetalCounter.ToString (); 

		text.text = displayString; 
	}

	public void subtractRoverItems(){
		//Add collection here!
		Inventory inventory = GetComponentInParent<Inventory> ();
		bool foundOne = true;
		while (foundOne) {
			foundOne = false;
			for (int i = 0; i < inventory.getLength(); i ++) {
				Item currentItem = inventory.getItemAtIndex (i);

				if (currentItem.getIsWinItem ()) {
					if (currentItem is Copper)
					if (copperCounter != 0)
						copperCounter--;
					if (currentItem is FluxCapacitor)
					if (fluxCapacitorCounter != 0)
						fluxCapacitorCounter--;
					if (currentItem is Powder)
					if (powderCounter != 0)
						powderCounter--;
					if (currentItem is ScrapMetal)
					if (scrapMetalCounter != 0)
						scrapMetalCounter--;

					//Destroy item from Rover's inventory as well as the game object.
					inventory.removeElement (i);
					currentItem.destroyGameObject ();
					//Add line to destroy in inventory here!
					UIInventory.staticThis.compressInventory();
					//We found an item, keep looping!
					foundOne = true;
					break;
				}
			
			}
		}

		return;
	}
}