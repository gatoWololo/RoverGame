using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {

	private int MAXINVENTORY = 12;

	private int currentLength; // length of the sequence
	
	private List<GameObject> currentInventory; // holds the game objects that represent commands

	private GameObject currentItem;

	private GameObject selectedItem;
	
	private int selectedItemIndex;
	
	public Transform FirstInventoryPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel
	
	private Vector3 vector;
	
	private Sprite sequenceSprite;

	private RoverScript roverScript;

	private Inventory inventory;

	private ItemRef itemRef;

	private Object prefab;
	
	
	void Start () {
		currentInventory = new List<GameObject> ();
		currentLength = 0;
		GameObject roverObject = GameObject.Find("Rover");
		roverScript = roverObject.GetComponent<RoverScript> ();
		inventory = roverObject.GetComponent<Inventory>();
		prefab = Resources.Load("Prefabs/InventoryItem", typeof(GameObject));
		vector = new Vector3 (0f, 0f);	
	}

	// Update is called once per frame
	void Update () {
		if (currentLength < MAXINVENTORY) {
			if (inventory.getInventoryLength () > currentLength) {
				Debug.Log ("backend L: " + inventory.getInventoryLength () + "\tmy L:" + currentLength);
				currentLength++;
				addItemToInventory (inventory.getLastItemType ());	
			}
		}
	}


	private void addItemToInventory(int itemType){ 
		// This method instanciates a new GameObject from prefab, sets the images position within the inventory grid and then sets 
		// the image to match the last item entered using the itemType parameter.
		
		currentItem = Instantiate(prefab, FirstInventoryPosition.position, FirstInventoryPosition.rotation) as GameObject;
		currentItem.transform.SetParent(FirstInventoryPosition);
		calculateGridPosition (currentLength);
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

		itemRef = currentItem.GetComponent<ItemRef>();
		itemRef.setUiItemProperties(itemType,currentInventory.Count);
		Debug.Log ("SI my index is: "+ itemRef.getMyIndex() + " My type is: "+ itemRef.getMyType() );
		currentInventory.Add (currentItem);
		registerButtonAction(itemType);
	}

	private void registerButtonAction(int type){
		Debug.Log ("My type is: "+ type);
		switch(currentInventory.Count-1){
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


	private void calculateGridPosition(int x){
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
	
	public void selectInventoryItem(int index){
		Debug.Log ("SI my index is: "+ index);
		selectedItem = currentInventory[index];
	}

	public Vector2 getSelectedInventory(){
		if(selectedItem!= null){
			int type = selectedItem.GetComponent<ItemRef>().getMyType();
			int index = selectedItem.GetComponent<ItemRef>().getMyIndex();
			selectedItem = null;
			// destroy item and refactor UI to match rover backend.
			return new Vector2(type,index);
		}
		else return new Vector2(-1f,-1f);
	}
	
}