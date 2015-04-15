using UnityEngine;
using System.Collections;
using System; 

public class ChunkCreator {
	//Dimensions of array
	public const int chunkSize = 50;
	private readonly Type[] tileTypes = {typeof(SnowTile)};
	private static System.Random randomPicker;
	private int lakeSizeX = 5;
	private int lakeSizeY = 5;
	private int mountainSizeX = 15;
	private int moutainSizeY = 2;
	private float alpha = 0.0f;
	//================================================================================
	//Constructor for class (Constructs).
	static ChunkCreator(){
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
		ItemCreator itemCreator = new ItemCreator ();
		Tile[,] tileArray = chunk.getTileArray();
		GameObject chunkObject = chunk.getGameObject();
		int x, y;

		/*Initialize all tiles.*/
		for (int i = 0; i < size; i++){
			for (int j = 0; j < size; j++) {
				Vector2 position = new Vector2 (xCoord + i, yCoord + j);
				tileArray[i,j] = getRandomTile(position);
				//Set info for new object.
				tileArray[i,j].setTileIndex(i,j);
				tileArray[i, j].changeAlpha(alpha);
				//Set tile as child of Chunk.
				tileArray[i,j].getGameObject().transform.parent = chunkObject.transform;
			}
		}

		/*Pick three random spots to make lakes out of for each tile.*/
		Vector2[] spots = getRandomPositionTiles (size, 10, 3);
		for (int i = 0; i < 3; i++) {
			x = (int) spots[i].x;
			y = (int) spots[i].y;
			makeLake(x,y, tileArray, xCoord, yCoord, chunkObject);
		}

		//Make rock features for the chunk.
		spots = getRandomPositionTiles (size, 16, 3);
		x = (int) spots[0].x;
		y = (int) spots[0].y;
		makeMountain(x,y, tileArray, xCoord, yCoord, chunkObject, false);
		x = (int) spots[1].x;
		y = (int) spots[1].y;
		makeMountain(x,y, tileArray, xCoord, yCoord, chunkObject, true);
		x = (int) spots[2].x;
		y = (int) spots[2].y;
		makeMountain(x,y, tileArray, xCoord, yCoord, chunkObject, false);

		//This is beggining item for tutorial.
		tileArray [11, 18].setItem (new Battery (new Vector2 (xCoord + 11, yCoord + 18)));

		itemCreator.addItemsToChunk (chunk);
		return chunk;
	}
	//================================================================================
	//Make the actual lake with the given info.
	//x and y are the indices for the center of the lake in ther array.
	//xCoord and y Coord are the actual float location of the sprites on the map.
	private void makeLake(int x, int y, Tile[,] tileArray, float xCoord, float yCoord, GameObject chunkObject){
		//New tile to make for every loop iteration.
		Tile newTile;
		for (int i = -lakeSizeX; i < lakeSizeX; i ++)
			for (int j = -lakeSizeY; j < lakeSizeY; j++) {
				Vector2 position = new Vector2(x + xCoord + i,y + yCoord + j);

				//Keep corners of snow dirt!
				if(Math.Abs(i) >=4 && Math.Abs(j) >=3)
					continue;

				//If we are at the edges we wanna use the the edge tiles.
				newTile = getProperEdgeTile(i,j, position);

				//Don't ask why the indices work... they just do!
				UnityEngine.Object.Destroy(tileArray[x + i, y + j].getGameObject());
				tileArray[x + i, y + j]  = newTile;
				tileArray[x + i, y + j].getGameObject().transform.parent = chunkObject.transform;
				tileArray[x + i, y + j].changeAlpha(alpha);
			}

		//Make corners of lake dirt!
		return;
	}
	//================================================================================
	//Make the actual mountain with the given info.
	//x and y are the indices for the center of the lake in ther array.
	//xCoord and y Coord are the actual float location of the sprites on the map.
	//Flip is used to get some variety in formations.
	private void makeMountain(int x, int y, Tile[,] tileArray, float xCoord, float yCoord, GameObject chunkObject, bool flip){
		//New tile to make for every loop iteration.
		Tile newTile;
		int xRange = mountainSizeX;
		int yRange = moutainSizeY;

		if(flip == true){
			xRange = moutainSizeY;
			yRange = mountainSizeX;
		}
		for (int i = -xRange; i < xRange; i ++)
			for (int j = -yRange; j < yRange; j++) {
				Vector2 position = new Vector2(x + xCoord + i,y + yCoord + j);
				newTile = new MountainTile(position);

				//Don't ask why the indices work... they just do!
				UnityEngine.Object.Destroy(tileArray[x + i, y + j].getGameObject());
				tileArray[x + i, y + j] = newTile;
				tileArray[x + i, y + j].getGameObject().transform.parent = chunkObject.transform;
				tileArray[x + i, y + j].changeAlpha(alpha);
			}
		
		//Make corners of lake dirt!
		return;
	}
	//================================================================================
	//Choses random tiles in each chunk to create lakes, makes sure tiles are far enough
	//from each other and keeps trying until it finds some.
	private Vector2[] getRandomPositionTiles(int chunkSize, int distanceFromEdge, int number){
		Vector2[] indices = new Vector2[3];
		int picked = 0;
		//Keep trying until we find 3 good spots.
		while (picked < number) {
			//Pick a point not too close to the edge of the chunk!
			Vector2 newIndex = new Vector2(randomPicker.Next(distanceFromEdge, chunkSize- distanceFromEdge),
			                               randomPicker.Next(distanceFromEdge, chunkSize- distanceFromEdge));

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
	//Gets a tile from the possible tile types we have.
	private Tile getRandomTile(Vector2 position){
		int tileIndex = randomPicker.Next(0, tileTypes.Length);
		Type tileType = tileTypes [tileIndex];
		//Create new tile from given Type object and pass it the one paramater objects
		//that derive from Tile have in the constructor.
		Tile newTile = (Tile)Activator.CreateInstance (tileType, new object[1] {position});

		return newTile;
	}
	//================================================================================
	//
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
	//================================================================================
	private Tile getProperEdgeTile(int i, int j,Vector2 position){
		if (j + 1 == lakeSizeX)
			return new CornerSnowTile (position, Direction.Down);
		if (j == - lakeSizeX)
			return new CornerSnowTile (position, Direction.Up);
		if (i + 1 == lakeSizeY)
			return new CornerSnowTile (position, Direction.Left);
		if (i == - lakeSizeY)
			return new CornerSnowTile (position, Direction.Right);

		return new DirtTile(position);

	}
	//================================================================================

}
