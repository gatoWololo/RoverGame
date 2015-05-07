using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {
	public static UIInventory staticThis;
	private int MAXINVENTORY = 12;

	public int currentLength; // length of the sequence
	
	private GameObject[] currentInventory; // holds the game objects that represent commands

	private GameObject currentItem;

	private GameObject selectedItem;
	
	private int selectedItemIndex;
	
	public Transform FirstInventoryPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel
	
	public Vector3 vector;
	
	private Sprite sequenceSprite;

	private RoverScript roverScript;

	private Inventory inventory;

	private Object prefab;
	
	
	void Start () {
		currentInventory = new GameObject[12];
		currentLength = 0;
		GameObject roverObject = GameObject.Find("Rover");
		roverScript = roverObject.GetComponent<RoverScript> ();
		inventory = roverObject.GetComponent<Inventory>();
		inventory.setMaxItems(MAXINVENTORY);
		prefab = Resources.Load("Prefabs/InventoryItem", typeof(GameObject));
		vector = new Vector3 (0f, 0f);	
		staticThis = this;
	}

	// Update is called once per frame
	void Update () {
		if (currentLength < MAXINVENTORY ) {
			if (inventory.getInventoryLength () > currentLength) { // you never fixed the back end so update is screwing everything up!!!!
				//Debug.Log ("backend L: " + inventory.getInventoryLength () + "\tmy L:" + currentLength);
				addItemToInventory (inventory.getLastItem());
				currentLength++;	
			}
		}
	}


	public void addItemToInventory(Item item){ 
		// This method instanciates a new GameObject from prefab, sets the images position within the inventory grid and then sets 
		// the image to match the last item entered using the itemType parameter.
		int itemType = item.getItemId();
		currentItem = Instantiate(prefab, FirstInventoryPosition.position, FirstInventoryPosition.rotation) as GameObject;
		currentItem.transform.SetParent(FirstInventoryPosition);
		calculateGridPosition (currentLength+1);
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
			break;
		
		}
		
		ItemRef itemRef = currentItem.GetComponent<ItemRef>();
		itemRef.setUiItemProperties(itemType,currentLength);
		
		if(item.getItemEnergy()>-1){ //check to see if this itemref is a used object from the equipment menu
			itemRef.setMyEnergy(item.getItemEnergy()); // set the energy in our new item ref to match the old object we are representing
		}

		//Debug.Log ("SI my index is: "+ itemRef.getMyIndex() + " My type is: "+ itemRef.getMyType() );
		currentInventory[currentLength] = currentItem ;
		registerButtonAction(currentLength);
	}

	private void registerButtonAction(int index){
		Debug.Log ("My Action index is: "+ index);
		switch(currentLength){
		case 0:
			UnityEngine.Events.UnityAction action0 = () => { selectInventoryItem(0); };
			currentItem.GetComponent<Button>().onClick.AddListener(action0);
			break;
		case 1:
			UnityEngine.Events.UnityAction action1 = () => { selectInventoryItem(1); };
			currentItem.GetComponent<Button>().onClick.AddListener(action1);
			break;
		case 2:
			UnityEngine.Events.UnityAction action2 = () => { selectInventoryItem(2); };
			currentItem.GetComponent<Button>().onClick.AddListener(action2);
			break;
		case 3:
			UnityEngine.Events.UnityAction action3 = () => { selectInventoryItem(3); };
			currentItem.GetComponent<Button>().onClick.AddListener(action3);
			break;
		case 4:
			UnityEngine.Events.UnityAction action4 = () => { selectInventoryItem(4); };
			currentItem.GetComponent<Button>().onClick.AddListener(action4);
			break;
		case 5:
			UnityEngine.Events.UnityAction action5 = () => { selectInventoryItem(5); };
			currentItem.GetComponent<Button>().onClick.AddListener(action5);
			break;
		case 6:
			UnityEngine.Events.UnityAction action6 = () => { selectInventoryItem(6); };
			currentItem.GetComponent<Button>().onClick.AddListener(action6);
			break;
		case 7:
			UnityEngine.Events.UnityAction action7 = () => { selectInventoryItem(7); };
			currentItem.GetComponent<Button>().onClick.AddListener(action7);
			break;
		case 8:
			UnityEngine.Events.UnityAction action8 = () => { selectInventoryItem(8); };
			currentItem.GetComponent<Button>().onClick.AddListener(action8);
			break;
		case 9:
			UnityEngine.Events.UnityAction action9 = () => { selectInventoryItem(9); };
			currentItem.GetComponent<Button>().onClick.AddListener(action9);
			break;
		case 10:
			UnityEngine.Events.UnityAction action10 = () => { selectInventoryItem(10); };
			currentItem.GetComponent<Button>().onClick.AddListener(action10);
			break;
		case 11:
			UnityEngine.Events.UnityAction action11 = () => { selectInventoryItem(11); };
			currentItem.GetComponent<Button>().onClick.AddListener(action11);
			break;
		}


	}


	public void calculateGridPosition(int x){
			// modifies the class field 'vector' in order to calculate the offset for the next game object within the sequencer
			// TODO remove hardcoded numbers and replace with defined constants
		switch(x%4){
		case 0:
			vector.x = 120f;
			break;
		case 1:
			if(x>1){
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

	public void resetInventoryVector(){
		vector.x = 0f;
		vector.y = 0f;
	}

	public void compressInventory(){
		//destroy all the inventory objects currently in the  UI layer inventory
		for(int i = currentInventory.Length-1;i>-1;i--){
			DestroyImmediate(currentInventory[i]);
		}
		resetInventoryVector();
	
		int inventoryLength = inventory.getInventoryLength();
		
		// traverse the back end and call add inventory to create a new representation 
		// in the UI layer	
		for(currentLength = 0 ; currentLength<inventoryLength ; currentLength++){
			Item item = inventory.getItemAtIndex(currentLength);
			addItemToInventory(item);	
		}	
	}
	
	public void selectInventoryItem(int index){
		//Debug.Log ("Button Click - My index is:"+ index);
		selectedItem = currentInventory[index];
		if(selectedItem == null){
			Debug.LogError ("ERROR: Selected Item out of bounds");
		}
	}


	// this method exports an item ref object to the equipment menu
	// and then destroys the inventory representation of the object
	public Vector2 getSelectedInventory(){
		if(selectedItem != null){
			ItemRef itemRef = selectedItem.GetComponent<ItemRef>();
			Vector2 itemValues = new Vector2((float)itemRef.getMyType(),(float)itemRef.getMyEnergy());
			int index = itemRef.getMyIndex();
			selectedItem = null;
			// destroy item and refactor UI to match rover backend.
			inventory.removeElement(index); //remove from the backend as well
			compressInventory();
			return itemValues;
		}
		else {
			//Debug.LogError ("ERROR: Selected Item was Null");
			
			return new Vector3(-1f,-1f,-1f);
		}
	}

	// this method recieves an itemref represenation of an object from the 
	// Equipment menu and then instaciates an actual item and adds it to the
	// rover inventory
	// the update method handles rebuilding a UI represenation that matches
	// the inventory in the rover.
	public void returnToInventory(Vector2 itemValues){
		int itemId = (int)itemValues.x;
		Debug.Log ("ReturnToInventory ItemId: "+ itemId);
		Item item = null;
		Vector2 vec = new Vector2(0f,0f);
		switch(itemId){
			case 1:
				item = new Battery(vec);
				((Battery)item).setMyEnergy((int)itemValues.y);
				break;
			case 2:
				item = new ScrapMetal(vec);
				break;
			case 3:
				item = new FluxCapacitor(vec);
				break;
			case 4:
				item = new Powder(vec);
				break;
			case 5:
				item = new Copper(vec);
				break;
			case 6:
				item = new Drill(vec);
				break;
		}
		if(item == null){Debug.LogError("ERROR: Invalid Item type passed from Equipment Module");}
		inventory.addElement(item);
	}

	public bool full(){
		return currentLength == MAXINVENTORY;
	}
	
}