using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {
	/// <summary>
	/// Array of chunks holding our entire world.
	/// </summary>
	private Chunk[,] world;
	/// <summary>
	/// Total number of chunks the world can be in any direction, odd since we
	/// want the rover to start in the middle.
	/// </summary>
	private int maxWorldSize = 11;
	// Use this for initialization
	void Start () {
		int s = maxWorldSize;
		//Instantiate 
		world = new Chunk[s,s];
		//Notice that we can create new chunks of 100x100 of the map like this. This takes a 
		//while to load but realize it's creating 1000x1000 blocks so here we have over 4000 blocks.
        world[s/2,s/2] = ChunkCreator.createNewChunk (0.0f, 0.0f);
		//Commented out right now for more speed.
		//world [4, 6] = ChunkCreator.createNewChunk (-100.0f, -100.0f);
		//world [4, 6] = ChunkCreator.createNewChunk (-100.0f, 0.0f);
		//world [4, 6] = ChunkCreator.createNewChunk (0.0f, -100.0f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
