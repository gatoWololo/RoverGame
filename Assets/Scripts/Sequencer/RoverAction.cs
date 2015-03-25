using UnityEngine;
using System.Collections;
/// <summary>
/// Abstract class that all rover actions inheret from.
/// Rover actions are things like move, where we know the direction
/// and the number of times to do the action.
/// </summary>
abstract public class RoverAction{
	/// <summary>
	/// The number of times the action should be performed.
	/// </summary>
	protected int actionCounter;
	//Name of action for the sake of easy comparison.
	protected string actionName;
	//Getter for action Counter.

	public int getActionCounter(){
		return actionCounter;
	}

	public string getActionName(){
		return actionName;
	}
	//Given an int, will increment the the actionCounter by
	//that passed integer.
	public void increaseActionCounter(int amount){
		actionCounter += amount;

	}
}