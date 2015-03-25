using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Sequencer holds all methods an handles all events related to the UI for the rover's control being
/// pressed. The rover contains a sequencer object. From here we can click on the controls and using
/// a list we will keep track of the commands that the user wants to do. When play is pressed then
/// all the commands will be made my the rover by having the sequencer call the RoverMovement Script.
/// </summary>
public class Sequencer : MonoBehaviour {
	//This list will hold the event that need to be performed.
	private List<RoverAction> list;
	//We keep a reference to the most recently clicked action for ease of access
	//and efficiency.
	private RoverAction lastAction;
	// Amount of time to pass before doing next command.
	float delay = 1.8f;
	//The Sequencer holds a reference to the rover movement script as it must
	//talk to it!
	RoverMovementScript roverMovement;
	//================================================================================
	// Constructor according to unity.
	void Start () {
		list = new List<RoverAction> ();
		lastAction = new EmptyAction();
		roverMovement = GetComponent<RoverMovementScript> ();
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
	//Called by the ArrowClick and all click functions to add a new command to our
	//list so later the rover can perfom the list of actions.
	public void addActionToList(RoverAction action){
		string lastActionStr = lastAction.getActionName ();
		string currentActionStr = action.getActionName ();

		//If the next action is the same we increase this action's
		//actionCounter else we create a new action and add it to the list!
		if (lastActionStr.Equals (currentActionStr)) {
			lastAction.increaseActionCounter (1);
			Debug.Log ("Counter for: " + lastActionStr + " increased to: " + lastAction.getActionCounter ());
		}
		else {
			list.Add(action);
			lastAction = action;
			Debug.Log("New action added to list: " + currentActionStr);
		}

		return;
	}
	//================================================================================
	//Wrapper for do actions functions which actually does the work. Needs to be done
	//this way to appropriately have the delays for the rover.
	public void doSequence(){
		StartCoroutine (doActions (list));
		return;
	}
	//================================================================================
	//Iterates over the list of actions performig the actions.
	public IEnumerator doActions(List<RoverAction> actions){
		//Iterate through the elements of our list.
		foreach (RoverAction action in actions){
			if(action is MoveAction){
				Direction direction = ((MoveAction)action).getDirection();
				//Do the command n number of times:
				int nTimes = action.getActionCounter(); //nyTimes?

				for(int i = 0; i < nTimes; i ++){
					roverMovement.moveRoverAction(direction);
					yield return new WaitForSeconds (delay);
				}
			}
			//else??? TODO other actions.
		}
	}
	//================================================================================          
	//These actions are called when the user clicks on the UI for the rover game.
	public void upArrowClick(){
		MoveAction moveAction = new MoveAction (Direction.Up);
		addActionToList (moveAction);
		return;
	}
	//================================================================================
	public void downArrowClick(){
		MoveAction moveAction = new MoveAction (Direction.Down);
		addActionToList (moveAction);
		return;
	}
	//================================================================================
	public void leftArrowClick(){
		MoveAction moveAction = new MoveAction (Direction.Left);
		addActionToList (moveAction);
		return;
	}
	//================================================================================
	public void rightArrowClick(){
		MoveAction moveAction = new MoveAction (Direction.Right);
		addActionToList (moveAction);
		return;
	}
	//================================================================================
	//Clears the list field of this class ;)
	public void clearList(){
		list.Clear ();
		return;
	}
	//================================================================================
}
