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
	private bool fluxCapacitorDone;
	private bool copperDone;
	private bool powderDone;
	private bool scrapMetalDone;

	// Use this for initialization
	void Start () {
		//Skip so the rover doesn't try to get a text box too....
		if(GetComponentInParent<RoverScript>() == null)
			displayCounter ();
		
		fluxCapacitorDone = false;
		copperDone = false;
		powderDone = false;
		scrapMetalDone = false;
		
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
					if (currentItem is Copper){
						if (copperCounter != 0){
							copperCounter--;
						}else{
							copperDone = true;
						}
					}
					
					if (currentItem is FluxCapacitor){
						if (fluxCapacitorCounter != 0){
							fluxCapacitorCounter--;
						}else{
							fluxCapacitorDone = true;
						}
					}

					if (currentItem is Powder){
						if (powderCounter != 0){
							powderCounter--;
						}else{
							powderDone = true;	
						}
					}

					if (currentItem is ScrapMetal){
						if (scrapMetalCounter != 0){
							scrapMetalCounter--;
						}else{
							scrapMetalDone = true;
						}
					}

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
		if(copperDone && fluxCapacitorDone && powderDone && scrapMetalDone){
			Application.LoadLevel("Success Screen");
		}

		return;
	}
}