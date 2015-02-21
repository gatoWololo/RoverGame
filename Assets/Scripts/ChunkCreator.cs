using UnityEngine;
using System.Collections;
using System;

public static class ChunkCreator {
	//Dimensions of array
	public const int chunkSize = 100;
	
	//================================================================================
	/// <summary>
	/// Create terain of tiles in a 100 x100 fashion, given bottom left corner position
	/// for this chunk.
	/// </summary>
	/// <returns>The new chunk.</returns>
	/// <param name="xCoord">X coordinate.</param>
	/// <param name="yCoord">Y coordinate.</param>
	public static Chunk createNewChunk(float xCoord, float yCoord){
		int size = ChunkCreator.chunkSize;
		Chunk chunk = new Chunk (xCoord, yCoord);
		Tile[,] tileArray = new Tile[size, size];

		//Used for random.
		Array values = Enum.GetValues(typeof(DirtColor));
		System.Random random = new System.Random ();

		/*Initialize all tiles to dirt tiles for now.*/
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++) {
				DirtColor myColor = (DirtColor)values.GetValue (random.Next (values.Length));
				Vector2 position = new Vector2 (xCoord + i, yCoord + j);
				tileArray[i,j] = new DirtTile(position,myColor);
				tileArray[i,j].setTileIndex(i,j);
				tileArray[i,j].setVisible(false);
			}
		}
		
		return chunk;
	}
	//================================================================================

	

}
