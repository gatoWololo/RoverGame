using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIEquipment : MonoBehaviour {

	private int MAXINVENTORY = 12;

	private int currentLength; // length of the sequence
	
	private List<GameObject> currentInventory; // holds the game objects that represent commands
	
	GameObject currentItem;

	private Item selectedInventoryItem;
	
	public Transform FirstInventoryPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel

	public Transform FirstEquipmentPosition;
	
	private Vector3 vector;
	
	private Sprite sequenceSprite;

	private RoverScript roverScript;

	private bool hasDrillEquipped;
	
	private bool HasDrillInInventory;

	
	Object prefab;
	
	
	void Start () {
		currentInventory = new List<GameObject> ();
		currentLength = 0;
		GameObject roverObject = GameObject.Find("Rover");
		roverScript = roverObject.GetComponent<RoverScript> ();
		prefab = Resources.Load("Prefabs/InventoryItem", typeof(GameObject));
		vector = new Vector3 (0f, 0f);
	}

	// Update is called once per frame
	void Update () {

		if (currentLength < MAXINVENTORY) {
			if (roverScript.inventory.getInventoryLength () > currentLength) {
				Debug.Log ("backend L: " + roverScript.inventory.getInventoryLength () + "\tmy L:" + currentLength);
				currentLength++;
				addItemToInventory (roverScript.inventory.getLastItemType ());
			}
		}
		// query inventory list if something changed call add item to inventory
	}

	private void addItemToInventory(int itemType){ 
		// This method instanciates a new GameObject from prefab, sets the images position within the inventory grid and then sets 
		// the image to match the last item entered using the itemType parameter.
		
		currentItem = Instantiate(prefab, FirstInventoryPosition.position, FirstInventoryPosition.rotation) as GameObject;
		currentItem.transform.SetParent(FirstInventoryPosition);
		calculateGridPosition ();
		currentItem.transform.position = currentItem.transform.position + vector;
		
		switch (itemType) { //TODO integrate this into the setSubcript method and then make this a method call
		case 1:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/battery1", typeof(Sprite)) as Sprite;
			break;
		case 2:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/scrapMetal", typeof(Sprite)) as Sprite;
			break;
		case 3:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/fluxCapacitor", typeof(Sprite)) as Sprite;
			break;
		case 4:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/powder1", typeof(Sprite)) as Sprite;
			break;
		case 5:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/copper1", typeof(Sprite)) as Sprite;
			break;
		case 6:
			currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/drill1", typeof(Sprite)) as Sprite;
			HasDrillInInventory = true;
			break;
		
		}
		
		currentInventory.Add (currentItem);
	}

	private void calculateGridPosition(){
			// modifies the class field 'vector' in order to calculate the offset for the next game object within the sequencer
			// TODO remove hardcoded numbers and replace with defined constants
		switch(currentLength%4){
		case 0:
			vector.x = 120f;
			break;
		case 1:
			if(currentLength>1){
				vector.y = vector.y - 40f;
			}
			vector.x = 0f;
			break;
		case 2:
			vector.x = 40f;
			break;
		case 3:
			vector.x = 80f;
			break;
		}	
	}
	
	private void hasDrill(){
		roverScript.setHasDrill(hasDrillEquipped);
	}

	public void selectInventoryItem(){
		
	}
	
	
}