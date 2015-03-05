using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	/// <summary>
	/// Array of chunks holding our entire world.
	/// </summary>
	public static Chunk[,] world;
	public static ChunkCreator chunkCreator;
	//Current Chunk number rover is in.
	public static int currChunkX;
	public static int currChunkY;

	public static Vector2 chunkCoord;
	/// <summary>
	/// Total number of chunks the world can be in any direction, odd since we
	/// want the rover to start in the middle.
	/// </summary>
	private int maxWorldSize = 11;
	// Use this for initialization
	void Start () {
		int s = maxWorldSize;
		world = new Chunk[s,s];

		//Set whole grid to nulls.
		for (int i = 0; i < s; i++)
			for (int j = 0; j < s; j++)
				world [i, j] = null;

		chunkCreator = new ChunkCreator ();
		//Notice that we can create new chunks of 50x50 of the map like this.
        world[s/2,s/2] = chunkCreator.createNewChunk (0.5f, 0.5f);
		Debug.Log (world [s / 2, s / 2]);
		//Initialize to beggining coordinates.
		currChunkX = s / 2;
		currChunkY = s / 2;
		//Actual pixel location of current chunk.
		chunkCoord = new Vector2 (0.5f, 0.5f);

		return;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
