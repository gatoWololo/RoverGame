using UnityEngine;
using System.Collections;
using System;

public class ChunkCreator {
	//Dimensions of array
	private const int chunkSize = 100;
	private readonly Type[] tileTypes = {typeof(DirtTile), typeof(WaterTile) };
	private System.Random randomPicker;
	//================================================================================
	public ChunkCreator(){
		randomPicker = new System.Random ();

		return;
	}
	//================================================================================
	/// <summary>
	/// Create terain of tiles in a 100 x100 fashion, given bottom left corner position
	/// for this chunk.
	/// </summary>
	/// <returns>The new chunk.</returns>
	/// <param name="xCoord">X coordinate.</param>
	/// <param name="yCoord">Y coordinate.</param>
	public Chunk createNewChunk(float xCoord, float yCoord){
		int size = ChunkCreator.chunkSize;
		Chunk chunk = new Chunk (xCoord, yCoord);
		Tile[,] tileArray = new Tile[size, size];

		/*Initialize all tiles.*/
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++) {
				Vector2 position = new Vector2 (xCoord + i, yCoord + j);
				tileArray[i,j] = getRandomTile(position);
				//Set info for new object.
				tileArray[i,j].setTileIndex(i,j);
				tileArray[i,j].setVisible(false);
			}
		}
		
		return chunk;
	}
	//================================================================================
	private Tile getRandomTile(Vector2 position){
		int tileIndex = randomPicker.Next(0, tileTypes.Length);
		Type tileType = tileTypes [tileIndex];
		//Create new tile from given Type object and pass it the one paramater objects
		//that derive from Tile have in the constructor.
		Tile newTile = (Tile)Activator.CreateInstance (tileType, new object[1] {position});

		return newTile;
	}
	//================================================================================


}
