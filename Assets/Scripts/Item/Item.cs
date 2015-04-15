using UnityEngine;
using System.Collections;

//Abstract Class that holds items that the user will use to derive all
//actual objects the Rover can hold. 
public class Item {
	protected int itemId;
	protected string itemName;

	//Actual game object that Unity uses, this class wraps it all.
	protected GameObject gameObject;
	protected SpriteRenderer renderer;
	protected BoxCollider2D collider;
	//================================================================================
	//Le constructor!
	public Item(Vector2 position){
		gameObject = new GameObject ();
		renderer = gameObject.AddComponent<SpriteRenderer> ();
		collider = gameObject.AddComponent<BoxCollider2D>();
		//Add script that handles collisions with Rover:
		gameObject.AddComponent<ItemExploration> ();

		//Set item information.
		this.itemId = -1;
		this.itemName = "Item";

		//Set position of item in the world.
		Vector3 finalPosition = new Vector3 (position.x, position.y, 0);
		gameObject.transform.position = finalPosition;

		//Item objects alway go on top of anything else in the world
		renderer.sortingOrder = 1;
		//Item is not viewable until it is explored.
		collider.size = new Vector3(0.1f, 0.1f);
		setVisible (false);

		return;
	}
	//================================================================================
	//Item should not be visible in the map unless that area has been explored by a
	//rover.
	public void setVisible(bool visible){
		renderer.enabled = visible;
		
		return;
	}
	//================================================================================
	//Once rover has hit the item we destroy the actual game object associated with the
	//item so it is no longer in the world but we still keep the reference to it as it
	//is now housed in the Rover's inventory.
	public void destroyGameObject(){
		Object.Destroy (gameObject);
		gameObject = null;
		return;
	}
	//================================================================================
	public string getName(){
		return itemName;
	}
	//================================================================================
	public GameObject getGameObject(){
		return gameObject;

	}
}
