﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Probably should be updated so if a collision is caused then we stop automatically
//instead of using friction to slow down...
public class RoverMovementScript : MonoBehaviour {
	//Chunk rover is in.
	int ChunkNumber;
	//Tile the rover is in.
	public static int xTile;
	public static int yTile;
	//We need to keep track of the current direction of rover to know how
	//much to rotate rover by.
	public static Direction previousDir = Direction.Up;
	//Rover has reference to current chunk.
	private Chunk chunk;
	private int chunkSize;
	//Rovers current position.
	Vector3 newPos;
	Transform roverTransform;
	float speed = 1.0f;
	//===================================================================================
	// Use this for initialization
	void Start () {
		xTile = 10;
		yTile = 10;
		chunkSize = (int)World.chunkSize;
		roverTransform = GetComponentInParent<Transform> ();
		//Initialize to current position.
		newPos = roverTransform.position;
	}
	//===================================================================================
	/// <summary>
	/// Updates the movement and rotation of the rover.
	/// </summary>
	/// <param name="dir">New direction to move.</param>
	/// <param name="times">Times it is expected to move in that direction.</param>
	public void updateMovement(Direction dir, int times){
		//Calculate the position we should move to.
		newPos = calculateNewPos (roverTransform.position, dir, times);

		//Rotate to face proper direction.
		float newRotate = getAngle (dir, RoverMovementScript.previousDir);
		this.transform.Rotate (0, 0, newRotate);
		//Update our direction.
		RoverMovementScript.previousDir = dir;

		return;
	}
	//===================================================================================
	/// <summary>
	/// Calculates the new position of the rover. Needed for the
	/// Move torwards function.
	/// </summary>
	/// <returns>The new position.</returns>
	/// <param name="currentPos">Current position.</param>
	/// <param name="dir">Dir.</param>
	/// <param name="times">Times.</param>
	private Vector3 calculateNewPos(Vector3 currentPos,Direction dir,int times){
		Vector3 newPos = new Vector3 ();
		float x = currentPos.x;
		float y = currentPos.y;

		switch (dir) {
		case Direction.Up:
			newPos = new Vector3(x, y + times);
			break;
		case Direction.Down:
			newPos = new Vector3(x, y - times);
			break;
		case Direction.Right:
			newPos = new Vector3(x + times, y);
			break;
		case Direction.Left:
			newPos = new Vector3(x - times, y);
			break;
		default:
			break; //Should be impossible to happen.
		}
		return newPos;
	}
//===================================================================================
	// Update is called once per frame
	void Update () {
	moveRover();
	//Rover moved see if there is any items to collect in the new tile!
	collectItem ();
	return;
	}
//===================================================================================
	//If called move the coordinates of the rover up by one depending on
	//the prevDirection.
public void updateRoverCoordinates (Direction dir){

	switch (dir) {
	case Direction.Up:
		yTile++;
		//We have moved to a new chunk.
		if( yTile == World.chunkSize){
			yTile = 0;
			World.currChunkY++;
		}
		break;
	case Direction.Down:
		yTile--;
		//We have moved to a new chunk.
		if( yTile == -1){
			yTile = (int) World.chunkSize - 1;
			World.currChunkY--;
		}
		break;
	case Direction.Right:
		xTile++;
		//We have moved to a new chunk.
		if( xTile == World.chunkSize){
			xTile = 0;
			World.currChunkX++;
		}
		break;
	case Direction.Left:
		xTile--;
		//We have moved to a new chunk.
		if( xTile == -1){
			xTile = (int) World.chunkSize - 1;
			World.currChunkX--;
		}
		break;
	default:
		break; //Should be impossible to happen.
	}
	return;
	
}
//===================================================================================
	/// <summary>
	/// Moves the rover, called by update() relies on the fact that newPos has been
	// set by the sequencer.
	/// </summary>
	private void moveRover(){
		float step = speed * Time.deltaTime;
		roverTransform.position = Vector3.
			MoveTowards(roverTransform.position, newPos, step);

		return;
	}

//===================================================================================
	private void collectItem(){
		//We moved so attempt to collect it whatever is in their new block.
		Chunk currentChunk = World.world [World.currChunkX, World.currChunkY];
		Tile currentTile = currentChunk.getTileArray()[xTile, yTile];
		
		//Add item to our inventory.
		if(currentTile.hasItem() == true){
			Item itemFound = currentTile.getItem();
			GetComponent<RoverScript>().inventory.addElement(itemFound);
			itemFound.destroyGameObject();
		}

		return;
	}
	//===================================================================================
	//Given to directions it will figure out whether the opposite direction
	//has been called.
	public bool isOpposite(Direction d1, Direction d2){
		if (d1 == Direction.Up && d2 == Direction.Down)
			return true;
		if (d1 == Direction.Down && d2 == Direction.Up)
			return true;
		if (d1 == Direction.Left && d2 == Direction.Right)
			return true;
		if (d1 == Direction.Right && d2 == Direction.Left)
			return true;

		return false;
	}
//===================================================================================
	//Given two direction will return the proper angle to rotate byl
	public float getAngle(Direction d1, Direction d2){

		if (isOpposite (d1, RoverMovementScript.previousDir))
			return 180.0f;
	
		switch (d2) {
		case Direction.Up:
			if (d1 == Direction.Left)
				return 90.0f;
			if (d1 == Direction.Right)
				return -90.0f;
			return 0;
		case Direction.Down:
			if (d1 == Direction.Right)
				return 90.0f;
			if (d1 == Direction.Left)
				return -90.0f;
			return 0;
		case Direction.Right:
			if (d1 == Direction.Up)
				return 90.0f;
			if (d1 == Direction.Down)
				return -90.0f;
			return 0;
		case Direction.Left:
			if (d1 == Direction.Down)
				return 90.0f;
			if (d1 == Direction.Up)
				return -90.0f;
			return 0;
		}

		//Should never get here...
		return 0;
	}

	//Get X Coordinate.
	public int getXTile(){
		return xTile;
	}

	//Get X Coordinate.
	public int getYTile(){
		return yTile;
	}
	//===================================================================================
}