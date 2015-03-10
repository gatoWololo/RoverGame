	using UnityEngine;
using System.Collections;
using System; 

public class ChunkCreator {
	//Dimensions of array
	public const int chunkSize = 50;
	private readonly Type[] tileTypes = {typeof(DirtTile)};
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
		Chunk chunk = new Chunk (xCoord, yCoord);;

		Tile[,] tileArray = chunk.getTileArray();
		GameObject chunkObject = chunk.getGameObject();

		/*Initialize all tiles.*/
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++) {
				Vector2 position = new Vector2 (xCoord + i, yCoord + j);
				tileArray[i,j] = getRandomTile(position);
				//Set info for new object.
				tileArray[i,j].setTileIndex(i,j);
				tileArray[i,j].setVisible(false);
				//Set tile as child of Chunk.
				tileArray[i,j].getGameObject().transform.parent = chunkObject.transform;
			}
		}

		/*Pick three random spots to make lakes out of for each tile.*/
		Vector2[] spots = getLakeRandomTiles (size);
		for (int i = 0; i < 3; i++) {
			int x = (int) spots[i].x;
			int y = (int) spots[i].y;
			makeLake(x,y, tileArray, xCoord, yCoord, chunkObject);
		}

		return chunk;
	}
	//================================================================================
	//Make the actual lake with the given info.
	//x and y are the indices for the center of the lake in ther array.
	//xCoord and y Coord are the actual float location of the sprites on the map.
	private void makeLake(int x, int y, Tile[,] tileArray, float xCoord, float yCoord, GameObject chunkObject){
		for (int i = -5; i < 5; i ++)
			for (int j = -5; j < 5; j++) {
				//Keep corners of array dirt!
				if(Math.Abs(i) >=4 && Math.Abs(j) >=3)
				   continue;
				UnityEngine.Object.Destroy(tileArray[x + i, y + j].getGameObject());
				Vector2 position = new Vector2(x + xCoord + i,y + yCoord + j);
				tileArray[x + i, y + j]  = new WaterTile(position);
				tileArray[x + i, y + j].setVisible(false);
				tileArray[x + i, y + j].getGameObject().transform.parent = chunkObject.transform;
			}

		//Make corners of lake dirt!
		return;
	}
	//================================================================================
	private Vector2[] getLakeRandomTiles(int size){
		Vector2[] indices = new Vector2[3];
		int picked = 0;
		//Keep trying until we find 3 good spots.
		while (picked < 3) {
			//Pick a point not too close to the edge of the chunk!
			Vector2 newIndex = new Vector2(randomPicker.Next(10, size- 10),  randomPicker.Next(10, size- 10));

			//First point to be picked, it's good no matter what!
			if(picked == 1)
				if( farEnough(indices[0], newIndex) == false)
					continue;
			else if(picked == 2)
				if(! (farEnough(indices[0], newIndex) && farEnough(indices[1], newIndex)))
					continue;

			//If we got here our choice was good!
			indices[picked] = newIndex;
			picked++;

		}
		return indices;
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
	private float pow2(float x){
		return x * x;
	}
	//================================================================================
	private bool isSame(int i, int j, int number){
		return (i == number && j == number);
	}
	//================================================================================
	//Given two vectors compares their distance to make sure they are far enough.
	private bool farEnough(Vector2 x, Vector2 y){
		int prevDistance = (int)Math.Sqrt (pow2 (x.x - y.x) + pow2 (x.y - y.y));
		bool results = prevDistance > 6.0f;

		return results;
	}
}
