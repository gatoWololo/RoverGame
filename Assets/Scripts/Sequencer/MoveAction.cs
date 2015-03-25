using UnityEngine;
using System.Collections;
/// <summary>
/// Move actions define direction and number of steps the
/// rover should move in the grid. ActionName for this class
/// are in the form: "moveUp" | "moveDown" | "moveLeft" | "moveRight"
/// </summary>
public class MoveAction : RoverAction {
	//Direction this action tells the rover it should move.
	Direction direction;

	//Constructor for class, requires direction this represents.
	public MoveAction(Direction direction){
		this.direction = direction;
		this.actionCounter = 1;
		this.actionName = "move" + getNameString (direction);
		return;
	}

	//Getter for direction.
	public Direction getDirection(){
		return direction;
	}

	//Given a direction it returns the equivalent string.
	static public string getNameString(Direction direction){
		string name = null;

		switch (direction) {
		case Direction.Down:
			name = "Down";
			break;
		case Direction.Up:
			name = "Up";
			break;
		case Direction.Left:
			name = "Left";
			break;
		case Direction.Right:
			name = "Right";
			break;
		default:
			name = "Error"; //This should never happen!
			break;
		}
		return name;
	}
}
