using UnityEngine;
using System.Collections;

public class ChunkExploration : MonoBehaviour {

	// Use this for initialization
	void Start () {	}

	//If object is seen by the rover it should be set to viewable.
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.GetType ().IsAssignableFrom (typeof(BoxCollider2D)))
			return;
		//Get Current position of rover.
		int x = RoverMovementScript.xTile;
		int y = RoverMovementScript.yTile;
		//Get objects we need to make new chunk.
		Direction nextLocation = getDirection(x, y);
		Vector2 indexVec = getWorldIndex (World.currChunkX, World.currChunkY, nextLocation);
		int xIndex = (int) indexVec.x;
		int yIndex = (int)indexVec.y;

		//Check if chunk already created!
		if ((object)(World.world [xIndex, yIndex]) == null) {
			Vector2 newCoord = getNewCoord (getDirection (x, y), World.chunkCoord);
			Chunk newChunk = World.chunkCreator.createNewChunk (newCoord.x, newCoord.y);
			World.world [xIndex, yIndex] = newChunk;
		}
		return;
	}
	
	// Update is called once per frame
	void Update () {	}

	//Returns the new location in the world where the tile should be placed.
	private Vector2 getWorldIndex(int x, int y, Direction dir){
		Vector2 vector = new Vector2 (0, 0);

		switch (dir) {
		case Direction.Down:
			vector = new Vector2 (x, y - 1);
			break;
		case Direction.Up:
			vector = new Vector2 (x, y + 1);
			break;
		case Direction.Left:
			vector = new Vector2 (x - 1, y);
			break;
		case Direction.Right:
			vector = new Vector2 (x + 1, y);
			break;
		}

		return vector;
	}

	//Given the location of the rover it will return a direction showing which
	//chunk it should autogenerate the next chunk in (Assumes 50x50 chunks)
	private Direction getDirection(int x, int y){
		Direction dir = RoverMovementScript.previousDir;
		if (dir == Direction.Down)
			return Direction.Down;
		if (dir == Direction.Left)
			return Direction.Left;
		if (dir == Direction.Right)
			return Direction.Right;
		if (dir == Direction.Right)
			return Direction.Up;

		return Direction.Down;
	}

	/// <summary>
	/// Gets the coordinate the new chunk should be created in.
	/// </summary>
	/// <returns>The new coordinate.</returns>
	/// <param name="dir">Dir.</param>
	/// <param name="currCoord">Curr coordinate.</param>
	private Vector2 getNewCoord(Direction dir, Vector2 currCoord){
		float x = currCoord.x;
		float y = currCoord.y;

		if(Direction.Left == dir)
			return new Vector2(x - 50.0f, y);
		if(Direction.Right == dir)
			return new Vector2(x + 50.0f, y);
		if(Direction.Up == dir)
			return new Vector2(x, y + 50.0f);
		if (Direction.Down == dir)
			return new Vector2 (x, y - 50.0f);

		return new Vector2(0.0f, 0.0f);
	}
}
