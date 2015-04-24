using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIEquipment : MonoBehaviour {

	private GameObject[] currentEquipment; // holds the game objects that represent commands
	private UIInventory inventory;
	private GameObject currentItem;
	public Transform FirstEquipmentPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel
	private Vector3 vector;
	private Object prefab;
	private ItemRef itemRef;
	private RoverScript roverScript;
	private bool hasDrill;

	// Use this for initialization
	void Start () {
		currentEquipment = new GameObject[4];
		for(int i= 0; i<currentEquipment.Length; i++){
			currentEquipment[i]= null;
		}
		GameObject inventoryObject = GameObject.Find("InventoryGrid");
		inventory = inventoryObject.GetComponent<UIInventory>();
		GameObject roverObject = GameObject.Find("Rover");
		roverScript = roverObject.GetComponent<RoverScript> ();
		hasDrill = false;

		prefab = Resources.Load("Prefabs/EquipmentItem", typeof(GameObject));
		vector = new Vector3 (0f, 0f);
		
	}
	
	// Update is called once per frame
	void Update () {

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
		Vector2 v = inventory.getSelectedInventory(); //type, index
			
			if(v.x > -1){ // make sure an item was selected
		
				Debug.Log ("Added item of type "+ v.x/*type*/ + "from inventory index "+ v.y/*index*/+ " to Equipment" + n);
				int type = (int)v.x;
				
				if(n !=3 || n==3 && type == 6){ // drill must be equipped in the Aux Position
					currentItem = Instantiate(prefab, FirstEquipmentPosition.position, FirstEquipmentPosition.rotation) as GameObject;
					currentItem.transform.SetParent(FirstEquipmentPosition);
					vector.x = n*40f; // modify the transform of the new object to match the index in the UI
					currentItem.transform.position = currentItem.transform.position + vector;
			
					
					switch (type) { //TODO integrate this into the setSubcript method and then make this a method call
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
						hasDrill = true;
						roverScript.setHasDrill(hasDrill);
						currentItem.GetComponent<Image>().overrideSprite = Resources.Load("Textures/Items/drill1", typeof(Sprite)) as Sprite;
						break;
						
					}
					
					
					itemRef = currentItem.GetComponent<ItemRef>();
					itemRef.setUiItemProperties(type,n);
					Debug.Log ("ADDEQUIP my index is: "+ itemRef.getMyIndex() + " My type is: "+ itemRef.getMyType() );
					currentEquipment[n] = currentItem;	
					vector.x = 0f;
				}
			}	
			else{
			Debug.Log ("Fucking Broken" );
			}
		
	}
	
	public void addItemToInventory(int n){
		Debug.Log ("Removed Item from Equipment" + n);
		// call add to inventory
		Destroy(currentEquipment[n]);
		currentEquipment[n] = null;
	}

}
