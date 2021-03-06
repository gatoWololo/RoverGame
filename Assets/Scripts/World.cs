﻿using UnityEngine;
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
	private int maxWorldChunks = 3;
	public static float chunkSize = 50.0f;
	//Tiny offset of the world due to tile size.
	private float offset = 0.5f;
	//GameObject containing the world O_o.
	private GameObject gameObject;
	//Box collider for chunks. When rover hits a chunk collider it will
	//Signal the next chunk in the world to be generated.
	private EdgeCollider2D bottom;
	private EdgeCollider2D top;
	private EdgeCollider2D left;
	private EdgeCollider2D right;
	//Coordinates definining location of whole world object.
	private int xCoord = 0;
	private int yCoord = 0;
	private float sizeOfWorld;
	//Set the end of the world 3 tiles short in all directions!
	private float colliderSize = 3.0f;
	public static System.Random fluxPicker;
	public bool fluxCapacitorFlag = false;
	//================================================================================
	// Use this for initialization
	void Start () {
		sizeOfWorld = maxWorldChunks * chunkSize;
		int s = maxWorldChunks;
		world = new Chunk[s,s];

		chunkCreator = new ChunkCreator ();
		//Create whole world!
		for (int i = 0; i < s; i++)
			for (int j = 0; j < s; j++) {
				//Skip starting chunk for now.
				if (i == 1 && j == 1)
					continue;
				world [i, j] = chunkCreator.createNewChunk(i * chunkSize + offset, j * chunkSize + offset, false);
			}

		//Create the base on the world[1,1] spot.
		world [1, 1] = chunkCreator.createNewChunk(chunkSize + offset, chunkSize + offset, true);
		LanderTile.createLander(world [1,1], chunkSize + offset);

		//Pick place to put in flux Capacitor.
		fluxPicker = new System.Random ();
		addFluxCapacitor ();
		
		//Place drill randomly on first tile.
		addDrill(world [1, 1]);

		//Initialize to beggining chunk index.
		currChunkX = 1;
		currChunkY = 1;
		//Actual pixel location of current chunk.
		chunkCoord = new Vector2 (chunkSize + offset, chunkSize + offset);

		this.gameObject = new GameObject();
		bottom = gameObject.AddComponent<EdgeCollider2D> ();
		top = gameObject.AddComponent<EdgeCollider2D> ();
		left = gameObject.AddComponent<EdgeCollider2D> ();
		right = gameObject.AddComponent<EdgeCollider2D> ();
		
		//Make the points for our hitboxes in our chunk.
		Vector2 topLeft = new Vector2 (xCoord + colliderSize, yCoord + sizeOfWorld - colliderSize);
		Vector2 topRight = new Vector2 (xCoord + sizeOfWorld - colliderSize, yCoord + sizeOfWorld - colliderSize);
		Vector2 bottomLeft = new Vector2 (xCoord + colliderSize, yCoord + colliderSize);
		Vector2 bottomRight = new Vector2 (xCoord + sizeOfWorld - colliderSize, yCoord + colliderSize);
		
		//Set the new edgeColliders to their appropriate points, counter clockwise order.
		left.points = getEdgePoints (bottomLeft, topLeft);
		left.isTrigger = true;
		
		bottom.points = getEdgePoints (bottomRight, bottomLeft);
		bottom.isTrigger = true;
		
		top.points = getEdgePoints (topLeft, topRight);
		top.isTrigger = true;
		
		right.points = getEdgePoints (topRight, bottomRight);
		right.isTrigger = true;
		
		gameObject.name = "world";

		return;
	}
	//================================================================================
	/// <summary>
	/// Creates a Vector2 array to create the edgeColliders needed for all four sides.
	/// chunk.
	/// </summary>
	/// <returns>The edge.</returns>
	/// <param name="start">The start coordinate.</param>
	/// <param name="end">The end coordinate.</param>
	public Vector2[] getEdgePoints(Vector2 start, Vector2 end){
		return new Vector2[] {start, end};
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
	/// <summary>
	/// Randomly picks a chunk and a spot and creates the flux capacitor at this spot.
	/// </summary>
	public void addFluxCapacitor(){
		//Pick chunk put it in.
		int fluxChunkX = fluxPicker.Next(0, maxWorldChunks -1);
		int fluxChunkY = fluxPicker.Next(0, maxWorldChunks -1);
		Chunk chunk = world [fluxChunkX, fluxChunkY];
		Tile[,] tileArray = chunk.getTileArray ();
		float xPosition = chunk.getPositionX();
		float yPosition = chunk.getPositionY ();

		while(true){
			int fluxX = fluxPicker.Next (0, (int) chunkSize);
			int fluxY = fluxPicker.Next (0, (int) chunkSize);
			Tile myTile = tileArray[fluxX, fluxY];
			//Keep going!
			if(myTile.hasItem() == false && myTile.getCanPassThrough() == true){
				//Else we have found where to put it!.
				Vector2 position = new Vector2(fluxX + xPosition, fluxY + yPosition);
				myTile.setItem(new FluxCapacitor(position));
				break;
			}
		}
		fluxCapacitorFlag = true;

		return;
	}
	//================================================================================
	/// <summary>
	/// Adds the drill to the given chunk, which should be the same chunk as the rover
	/// is spawned.
	/// </summary>
	/// <param name="chunk">Chunk.</param>
	public void addDrill(Chunk chunk){
		float xPosition = chunk.getPositionX();
		float yPosition = chunk.getPositionY ();
		Tile[,] tileArray = chunk.getTileArray ();
		
		while(true){
			//We make it 35 as user needs should find drill after not too long.
			int drillX = fluxPicker.Next (0, (int) 35);
			int drillY = fluxPicker.Next (0, (int) 35);
			Tile myTile = tileArray[drillX, drillY];

			//Keep going!
			if(myTile.hasItem() == false && myTile.getCanPassThrough() == true){
				//Else we have found where to put it!.
				Vector2 position = new Vector2(drillX + xPosition, drillY + yPosition);
				myTile.setItem(new Drill(position));
				break;
			}
		}
		
		return;
	}
	//================================================================================
}
