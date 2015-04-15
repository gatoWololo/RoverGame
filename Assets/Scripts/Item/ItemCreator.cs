using UnityEngine;
using System.Collections;
using System; 
/// <summary>
/// Item creator, script to add random items through each chunk.
/// </summary>
public class ItemCreator {

	//Static random item picker common to all objects.
	static System.Random randomPicker;
	//Array of items to use to randomly populate the map.
	private readonly Type[] itemTypes = {typeof(Battery), typeof(ScrapMetal)};
	//Number of random items per chunk.

	private readonly int itemAmount = 50;

	//Get random tiles to place items.
	System.Random randomInt;
	//================================================================================
	/// <summary>
	/// Static initializer, we need to share the same random picker among objects.
	/// </summary>
	static ItemCreator(){
		randomPicker = new System.Random ();

	}
	//================================================================================
	public ItemCreator(){
		randomInt = new System.Random ();

	}
	//================================================================================
	/// <summary>
	/// Adds the items to the passed chunks based on parameters frequency.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	public void addItemsToChunk(Chunk chunk){
		Tile[,] tileArray = chunk.getTileArray ();
		int size = (int)World.chunkSize - 1;
		//Positions for item's transpose.
		float xPosition = chunk.getPositionX();
		float yPosition = chunk.getPositionY();

		//Add itemAmount number of random iteams to the map!
		for(int i = 0; i < itemAmount; i++){
			//Places to randomly place items.
			int xTile = randomInt.Next (0, size);
			int yTile = randomInt.Next (0, size);
			Tile thisTile = tileArray[xTile, yTile];

			//Make sure there is no item here and that this tile can be passed through.
			if(thisTile.getCanPassThrough() == false || thisTile.hasItem() == true)
				continue;
			//Get new item and place it on the map!
			Item newItem = getRandomItem(new Vector2(xPosition + xTile, yPosition + yTile));
			thisTile.setItem(newItem);
			//Set item as child of parent.
			newItem.getGameObject().transform.parent = thisTile.getGameObject().transform;

		}
		return;
	}
	//================================================================================
	//Gets a tile from the possible tile types we have.
	private Item getRandomItem(Vector2 position){
		int itemIndex = randomPicker.Next(0, itemTypes.Length);
		Type itemType = itemTypes [itemIndex];
		//Create new tile from given Type object and pass it the one paramater objects
		//that derive from Tile have in the constructor.
		Item item = (Item)Activator.CreateInstance (itemType, new object[1] {position});
		
		return item;
	}
	//================================================================================
}
