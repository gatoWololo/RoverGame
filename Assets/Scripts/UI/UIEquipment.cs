using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIEquipment : MonoBehaviour {

	private GameObject[] currentEquipment; // holds the game objects that represent commands
	private int[] EquipmentType;
	private UIInventory inventory;
	public GameObject currentItem;
	public Transform FirstEquipmentPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel
	private Vector3 vector;
	private Object prefab;
	private ItemRef itemRef;
	private RoverScript roverScript;
	private bool hasDrill;
	public int batteriesEquipped;
	private int type;

	// Use this for initialization
	void Start () {
		currentEquipment = new GameObject[4];
		EquipmentType = new int[4];
		for(int i= 0; i<currentEquipment.Length; i++){
			currentEquipment[i]= null;
			EquipmentType[i] = 0;
		}
		GameObject inventoryObject = GameObject.Find("InventoryGrid");
		inventory = inventoryObject.GetComponent<UIInventory>();
		GameObject roverObject = GameObject.Find("Rover");
		roverScript = roverObject.GetComponent<RoverScript> ();
		hasDrill = false;

		prefab = Resources.Load("Prefabs/EquipmentItem", typeof(GameObject));
		vector = new Vector3 (0f, 0f);
		initEquipment();
		
	}
	
	// Update is called once per frame
	void Update () {

		if( batteriesEquipped == 3 && BatteryPower.currPower<201){
			expelBattery(2);		
		}
		if( batteriesEquipped == 2 && BatteryPower.currPower<101){
			expelBattery(1);
		}
		if(batteriesEquipped == 1 && BatteryPower.currPower<1 ){
			expelBattery(0);
			if(inventory.batteriesInInventory == 0){
				Application.LoadLevel("Game Over Screen");
			}
		}
	}


	public void initEquipment(){
		currentItem = Instantiate(prefab, FirstEquipmentPosition.position, FirstEquipmentPosition.rotation) as GameObject;
		currentItem.transform.SetParent(FirstEquipmentPosition);
		currentItem.transform.position = currentItem.transform.position;
		currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/battery1", typeof(Sprite)) as Sprite;
		batteriesEquipped ++;
		itemRef = currentItem.GetComponent<ItemRef>();
		itemRef.setUiItemProperties(1,0);
		itemRef.setMyEnergy(100);
		currentEquipment[0] = currentItem;
		EquipmentType[0] = 1;
	}


	public void toggleEquipment(GameObject gameObject){
		
		switch(gameObject.name){
		case "1"://EquipButton 1
			Debug.Log ("SI my name is: EquipButton "+ gameObject.name);
			if(currentEquipment[0]==null){
				//instanciate object here
				addItemToEquipment(0);
			}
			else{
				// destroy object here
				addItemToInventory(0);
			}
			break;
		case "2"://EquipButton 2
			Debug.Log ("SI my name is: EquipButton "+ gameObject.name);
			if(currentEquipment[1]==null){
				//instanciate object here
				addItemToEquipment(1);
			}
			else{
				// destroy object here
				addItemToInventory(1);
			}
			break;
		case "3"://EquipButton 3
			Debug.Log ("SI my name is: EquipButton "+ gameObject.name);
			if(currentEquipment[2]==null){
				//instanciate object here
				addItemToEquipment(2);
			}
			else{
				// destroy object here
				addItemToInventory(2);
			}
			break;
		case "4"://Aux
			Debug.Log ("SI my name is: Aux "+ gameObject.name);
			if(currentEquipment[3]==null){
				//instanciate object here
				addItemToEquipment(3);
			}
			else{
				hasDrill = false;
				roverScript.setHasDrill(hasDrill);
				addItemToInventory(3);
			}
			break;
		}
	}

	

	public void addItemToEquipment(int n){
		if( n == 3 && inventory.queryInventory() !=6){
			Debug.Log("Cannot add non drill items to auxiallary");
		}
		else{
			Vector2 otherItemValues = inventory.getSelectedInventory(); //type, energy, index
			type = (int)otherItemValues.x;	
			if(type > -1){ // make sure an item was selected
		
				//Debug.Log ("Added item of type "+ v.x/*type*/ + "from inventory index "+ v.y/*index*/+ " to Equipment" + n);
				EquipmentType[n] = type;
				
				if(n !=3 || n==3 && type == 6){ // drill must be equipped in the Aux Position
					currentItem = Instantiate(prefab, FirstEquipmentPosition.position, FirstEquipmentPosition.rotation) as GameObject;
					currentItem.transform.SetParent(FirstEquipmentPosition);
					vector.x = n*40f; // modify the transform of the new object to match the index in the UI
					currentItem.transform.position = currentItem.transform.position + vector;
					itemRef = currentItem.GetComponent<ItemRef>();
					itemRef.setUiItemProperties(type,n);
					
					
					switch (type) { //TODO integrate this into the setSubcript method and then make this a method call
					case 1:
						currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/battery1", typeof(Sprite)) as Sprite;
						
						BatteryPower.addPower( (int)otherItemValues.y);
						batteriesEquipped ++;
						
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
						hasDrill = true;
						roverScript.setHasDrill(hasDrill); // tell the rover that it has the drill
						currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/drill1", typeof(Sprite)) as Sprite;
						break;
						
					}
					
					//Debug.Log ("ADDEQUIP my index is: "+ itemRef.getMyIndex() + " My type is: "+ itemRef.getMyType() );
					currentEquipment[n] = currentItem;
					if(type == 1){
						itemRef.setMyEnergy(levelBatteryCharge());
					}
					vector.x = 0f;
				}
			}	
			else{
				Debug.Log ("Inventory did not exist." );
			}
		}
		
	}

	private int levelBatteryCharge(){

		int currentCharge = BatteryPower.currPower/batteriesEquipped;
		if(EquipmentType[0]==1){
			currentEquipment[0].GetComponent<ItemRef>().setMyEnergy(currentCharge);
		}
		if(EquipmentType[1]==1){
			currentEquipment[0].GetComponent<ItemRef>().setMyEnergy(currentCharge);
		}
		if(EquipmentType[2]==1){
			currentEquipment[0].GetComponent<ItemRef>().setMyEnergy(currentCharge);
		}
		return currentCharge;
	
	}

	
	public void addItemToInventory(int n){
		// n is the index of the equipment array
		//Debug.Log ("Removed Item from Equipment" + n);
		int charge = 0;
		if(inventory.full()==false){
			if(n == 3 && EquipmentType[n]==6){ hasDrill = false;}
			if(EquipmentType[n]==1){ 
				charge = levelBatteryCharge();
				BatteryPower.removePower(charge);
				batteriesEquipped --;
			}
			ItemRef itemRef = currentEquipment[n].GetComponent<ItemRef>();
			Vector2 itemValues = new Vector2((float)itemRef.getMyType(),(float)itemRef.getMyEnergy());
			//pass the itemref back to the UI inventory
			inventory.returnToInventory(itemValues);
			//DestroyImmediate(itemRef);
			EquipmentType[n] = 0;
			Destroy(currentEquipment[n]);
			currentEquipment[n] = null;
		}
		else{
			// trigger a message to tell the user to do something
		}
	}

	public void expelBattery(int n){
		batteriesEquipped--;
		EquipmentType[n] = 0;
		Destroy(currentEquipment[n]);
		currentEquipment[n] = null;
	}

}
