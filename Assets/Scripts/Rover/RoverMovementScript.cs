using UnityEngine;
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
	public float speed = 1.0f;
	//We need to keep track of the current direction of rover to know how
	//much to rotate rover by.
	public static Direction previousDir = Direction.Up;
	//Rover has reference to current chunk.
	private Chunk chunk;
	private float delay = 1.7f;
	private float nextMove = 0.0f;
	private int chunkSize;
//===================================================================================
	// Use this for initialization
	void Start () {
		xTile = 10;
		yTile = 10;
		chunkSize = (int)World.chunkSize;
	}
	//===================================================================================
	//Given the new direction to move, rover will move that way and rotate itself.
	public void updateMovement(Direction direction){
		int x = 0, y = 0;
		float newRotate = 0;

		newRotate = getAngle (direction, RoverMovementScript.previousDir);
		//Update our direction.
		RoverMovementScript.previousDir = direction;

		//Figure out new velocity for rover.
		switch (direction) {
			case Direction.Up:
				x = 0;
				y = 1;
				break;
			case Direction.Down:
				x = 0;
				y = -1;
				break;
			case Direction.Left:
				x = -1;
				y = 0;
				break;
			case Direction.Right:
				x = 1;
				y = 0;
				break;
		}

		this.transform.Rotate (0, 0, newRotate);
		this.rigidbody2D.velocity = new Vector2 (x * speed, y * speed);
		
		//Restart  time!
		nextMove = Time.time + delay;
		
		return;
	}
//===================================================================================
	// Update is called once per frame
	void Update () {

	//If cooldown not done then skip.
	if(Time.time < nextMove)
		return;

	//If out of battery do not allow any movements:
	if (BatteryPower.currPower <= 0)
		return;

	//Attempt to move Rover.
	moveRover ();

	//Rover moved see if there is any items to collect in the new tile!
	collectItem ();

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
	//Move rover based on user input.
	private bool moveRover(){
		bool roverMoved = false;
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) { 
			updateMovement (Direction.Up);
			roverMoved = true;
			yTile++;
			//We have moved to a new chunk.
			if( yTile == World.chunkSize){
				yTile = 0;
				World.currChunkY++;
			}
		}
		if ( Input.GetKeyDown(KeyCode.DownArrow)){
			updateMovement (Direction.Down);
			roverMoved = true;
			yTile--;
			//We have moved to a new chunk.
			if( yTile == -1){
				yTile = (int) World.chunkSize - 1;
				World.currChunkY--;
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			updateMovement (Direction.Right);
			roverMoved = true;
			xTile++;
			//We have moved to a new chunk.
			if( xTile == World.chunkSize){
				xTile = 0;
				World.currChunkX++;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			updateMovement (Direction.Left);
			roverMoved = true;
			xTile--;
			//We have moved to a new chunk.
			if( xTile == -1){
				xTile = (int) World.chunkSize - 1;
				World.currChunkX--;
			}
		}

		return roverMoved;
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
	//===================================================================================
	public void upClick(){
		if (nextMove < Time.time) {
			updateMovement (Direction.Up);
			yTile++;

			//We have moved to a new chunk.
			if( yTile == World.chunkSize){
				yTile = 0;
				World.currChunkY++;
			}
		}
		return;
	}
	//===================================================================================
	public void DownClick(){
		if (nextMove < Time.time) {
			updateMovement (Direction.Down);
			yTile--;
		}

		//We have moved to a new chunk.
		if( yTile == -1){
			yTile = (int) World.chunkSize - 1;
			World.currChunkY--;
		}
		return;
	}
	//===================================================================================
	public void RightClick(){
		if (nextMove < Time.time) {
			updateMovement (Direction.Right);
			xTile++;
		}
		//We have moved to a new chunk.
		if( xTile == World.chunkSize){
			xTile = 0;
			World.currChunkX++;
		}
		return;
	}
	//===================================================================================
	public void LeftClick(){
		if (nextMove < Time.time) {
			updateMovement (Direction.Left);
			xTile--;
		}
		//We have moved to a new chunk.
		if( xTile == -1){
			xTile = (int) World.chunkSize - 1;
			World.currChunkX--;
		}
		return;
	}
	//===================================================================================
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