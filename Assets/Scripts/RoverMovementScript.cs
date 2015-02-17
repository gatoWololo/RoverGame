using UnityEngine;
using System.Collections;


//Probably should be updated so if a collision is caused then we stop automatically
//instead of using friction to slow down...
public class RoverMovementScript : MonoBehaviour {
	public float speed = 1.0f;
	//We need to keep track of the current direction of rover to know how
	//much to rotate rover by.
	private Direction previousDir = Direction.Up;
	// Use this for initialization
	void Start () {

	}

	//Given the new direction to move, rover will move that way and rotate itself.
	public void updateMovement(Direction direction){
		int x = 0, y = 0;
		Quaternion rotate = this.transform.rotation;
		float newRotate = 0;

		newRotate = getAngle (direction, this.previousDir);
		//Update our direction.
		this.previousDir = direction;
		Debug.Log (newRotate);

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
		
		return;
	}

	// Update is called once per frame
	void Update () {
	//If object is moving don't allow any further movement.
	if (this.rigidbody2D.velocity.magnitude > 0.05)
		return;

	if (Input.GetKey (KeyCode.UpArrow)) 
		updateMovement (Direction.Up);
	if ( Input.GetKey(KeyCode.DownArrow))
		updateMovement (Direction.Down);
	if ( Input.GetKey(KeyCode.RightArrow))
		updateMovement (Direction.Right);
	if (Input.GetKey (KeyCode.LeftArrow))
		updateMovement (Direction.Left);
	return;
	}

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

	//Given two direction will return the proper angle to rotate byl
	public float getAngle(Direction d1, Direction d2){

		if (isOpposite (d1, this.previousDir))
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
}