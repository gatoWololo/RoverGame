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

	// flag for UI to know to dump current Sequence
	private bool stopFlag;
	// tracks how many times the stop button has been pushed.
	private int stopClicks;

	//The Sequencer holds a reference to the rover movement script as it must
	//talk to it!
	RoverMovementScript roverMovement;
	//================================================================================
	// Constructor according to unity.
	void Start () {
		list = new List<RoverAction> ();
		lastAction = new EmptyAction();
		roverMovement = GetComponent<RoverMovementScript> ();
		stopFlag = false;
		stopClicks = 0;
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
	//Called by the ArrowClick and all click functions to add a new command to our
	//list so later the rover can perfom the list of actions.
	public void addActionToList(RoverAction action){
		stopFlag = false;// set the clear flag back to false so the UI knows to display commands again 
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
		stopClicks = 0;
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

	public int getLastActionValue(){
		// This method returns an integer associated with the last action added to the 
		// list. It is called by the UI in order to populate the sequencer. hale - 03/31/15
		switch(lastAction.getActionName()){
			case "": // empty list
				return 0;
				break;
			case "moveUp":
				return 1;
				break;
			case "moveDown":
				return 2;
				break;
			case "moveLeft":
				return 3;
				break;
			case "moveRight":
				return 4;
				break;
			// Add actions here as necessary
			// Ie Scan, Drill, etc...

			default: //Error
			return -1;
		}
	}

	public int getLastActionQty(){	// Lets the UI know how to set the command subscript
		return lastAction.getActionCounter ();
	}

	public int getLengthOfSequence(){ // Lets the UI know how many commands we have in our game list
		return list.Count;
	}

	public bool getPlayStatus (){ // Lets the UI know to clear the sequencer
		return stopFlag;
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
		// TODO chand the name of this method to reset sequence
		// This method should stop the rover on one click and clear the list on second click
		stopClicks++; 
		if (stopClicks == 2) { // if stop button has been pressed twice we want to clear our list.
			stopClicks = 0;
			stopFlag = true;
			list.Clear ();
			lastAction = new EmptyAction ();// added so that the UI will know if the list has been cleared. hale - 03/31/15
		}
		return;
	}
	//================================================================================
}
