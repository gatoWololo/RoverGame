using UnityEngine;
using System.Collections;
/// <summary>
/// This class creates the lander tiles which recharge the rover if he is under the
/// tiles. Uses the lander sprite so there should be only 9 of these tiles in a
/// 3x3 fashion where the middle tile is the center tile.
/// </summary>
public class LanderTile : Tile {
	private string myTexture = "Textures/lander";
	private string otherTexture = "Textures/snowTile";
	private const int basePositionX = 8;
	private const int basePositionY = 11;
	//================================================================================
	/// <summary>
	/// Creates new LandeTile object.
	/// </summary>
	/// <param name="position">Position of the tile on the map.</param>
	/// <param name="centerTile">Whether or not this is the tile in the center,
	/// this is needed as only the center tile has the sprite renderer.</param>
	private LanderTile(Vector2 position, bool centerTile, bool passThrough) : base(position){
		gameObject.name = "LanderTile";

		if (centerTile == true) {
			renderer.sprite = Resources.Load (myTexture, typeof(Sprite)) as Sprite;
			renderer.sortingOrder = 3;
		}else
			renderer.sprite = Resources.Load (otherTexture, typeof(Sprite)) as Sprite;

		canPassThroughIt = passThrough;
		return;
	}
	//================================================================================
	/// <summary>
	/// Given a chunk it will create a base for the rover in  this chunk at position
	/// given by class variables basePostionX and basePositionY.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	public static void createLander(Chunk chunk, float chunkPosition){
		Tile[,] tileArray = chunk.getTileArray ();
		int x = basePositionX;
		int y = basePositionY;
		bool mainTile = false;
		bool passThrough = false;

		//Create 8 around tiles.
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				mainTile = false;
				passThrough = false;
				//Edges should be able be passed through.
				if(i == 0 || j == 0)
					passThrough = true;
				//Create main tile.
				if (i == 0 && j == 0)
					mainTile = true;
				Vector2 position = new Vector2 (chunkPosition + i + x, chunkPosition + j + y);
				//Destroy current snow tile:
				Tile myTile = tileArray[x + i, y + j];
				UnityEngine.Object.Destroy(myTile.getGameObject());
				myTile = new LanderTile (position, mainTile, passThrough);
				tileArray[x + i, y + j] = myTile;
				//Set tile as child of chunk.
				myTile.getGameObject().transform.parent = chunk.getGameObject().transform;

			}
		}
		return;
	}
	//================================================================================
}
