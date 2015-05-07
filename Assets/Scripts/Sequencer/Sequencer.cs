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
	private int MAXSTEPS = 4; // maximum number of steps per action (corresponds to Action.actionCounter)
	private int MAXACTIONS = 16; // maximum number of actions in our <RoverAction>List

	//This list will hold the event that need to be performed.
	public List<RoverAction> list;
	//We keep a reference to the most recently clicked action for ease of access
	//and efficiency.
	public RoverAction lastAction;
	// Amount of time to pass before doing next command.
	float delay = 1.0f;

	//flag for UI to tell the current sequnce to stop iterating
	public bool stopFlag;

	// flag for UI to know to dump current Sequence
	private bool clearFlag;
	
	// stops the player from initializing the doSequences and crashing the game if the roverActionlist is empty
	private bool sequencerEmpty;

	// toggles the loop function
	public bool loop;

	//The Sequencer holds a reference to the rover movement script as it must
	//talk to it!
	private RoverMovementScript roverMovement;
	private RoverScript roverScript;
	//If it is doing something do not allow the user to span the button.
	private bool isDoingSequence = false;
	//Size of radius for rover.
	private int radiusSize = 3;
	//Random generator for item drops.
	private System.Random randomPicker;
	//================================================================================
	// Constructor according to unity.
	void Start () {
		list = new List<RoverAction> ();
		lastAction = new EmptyAction();
		roverMovement = GetComponent<RoverMovementScript> ();
		roverScript = GetComponent<RoverScript> ();
		clearFlag = false;
		stopFlag = false;
		loop = false;
		sequencerEmpty = true;
		randomPicker = new System.Random ();
	}
	// Constructor according to unity.
	public void publicStart () {
		list = new List<RoverAction> ();
		lastAction = new EmptyAction();
		//roverMovement = GetComponent<RoverMovementScript> ();
		//roverScript = GetComponent<RoverScript> ();
		clearFlag = false;
		stopFlag = false;
		loop = false;
		sequencerEmpty = true;
		randomPicker = new System.Random ();
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
	//Called by the ArrowClick and all click functions to add a new command to our
	//list so later the rover can perfom the list of actions.
	public void addActionToList(RoverAction action){
		sequencerEmpty = false;
		clearFlag = false;// set the clear flag back to false so the UI knows to display commands again
		string lastActionStr = lastAction.getActionName ();
		string currentActionStr = action.getActionName ();

		// if the action is the same and the action counter is less than MAXSTEPS increase actionCounter
		if (lastActionStr.Equals (currentActionStr) && lastAction.getActionCounter()<MAXSTEPS) {
				lastAction.increaseActionCounter (1);
		}
		// otherwise just add it to the Action list if there is room
		else if(list.Count < MAXACTIONS){
			list.Add(action);
			lastAction = action;
		}

		return;
	}// hale - 04/01/15

	//================================================================================
	// togggles the loop function between doSequenceLoop() and do Sequence()
	public void setLoop(){
		if (!loop) {
			loop = true;
		} else {
			loop = false;
		}
	}
	//================================================================================
	//Wrapper for do actions functions which actually does the work. Needs to be done
	//this way to appropriately have the delays for the rover.
	public void doSequence(){
		//Make sure our sequence is empty and the user cannot spam the button.
			if (!sequencerEmpty && !isDoingSequence) {
				stopFlag = false;
				StartCoroutine (doActions(list));
			}
		return;
	}

	//================================================================================
	//Iterates over the list of actions performig the actions.
	public IEnumerator doActions(List<RoverAction> actions){
		isDoingSequence = true;
		do {

			//Iterate through the elements of our list.
			for (int j = 0; j < list.Count; j++) { //Cannot use iterator as we will 
				                                   //be changing this list while it's running.
				RoverAction action = actions[j];

				//Take care of movement actions.
				if (action is MoveAction) {
					Direction direction = ((MoveAction)action).getDirection ();
					//Do the command n number of times:
					int nTimes = action.getActionCounter (); //nyTimes?

					//Tell movement script and by how much.
					for(int k = 0; k < nTimes; k++){
						if(stopFlag){ // if user hits stop button stop moving the rover
							break;
						}
						//Check if there is even power to do things.
						if(BatteryPower.currPower == 0){
							isDoingSequence = false;
							yield break;
						}
						//Move the game object!
						roverMovement.updateMovement(direction, 1);
						yield return new WaitForSeconds (delay);
					}

					if(stopFlag){ // if user hits stop button stop moving the rover
						break;
					}
				}

				//Take care of scanner actions.
				if(action is ScannerAction){
					CircleCollider2D cc = GetComponentInParent<CircleCollider2D>();
					int nTimes = action.getActionCounter();

					//If multiple scans each scan will increase the range of the scan.
					//Tell movement script and by how much.
					for(int k = 1; k <= nTimes; k++){
						//Check if there is even power to do things.
						if(BatteryPower.currPower == 0){
							isDoingSequence = false;
							yield break;
						}
						cc.radius = cc.radius + 0.5f * k;
						//Consume power.
						//Consume some power :)
						BatteryPower.usePower();
						VisualPower.consumePower = true;
						yield return new WaitForSeconds(delay);
					}
					cc.radius = radiusSize;
				}

				//Take care of drill action.
				if (action is DrillAction) {
					//Current direction the rover is facing.
					Direction direction = roverMovement.getDirection();
					//Do the command n number of times:
					int nTimes = action.getActionCounter (); //nyTimes?
					
					//Tell movement script and by how much.
					for(int k = 0; k < nTimes; k++){
						//If user has no drill continue
						if(roverScript.getHasDrill() == false)
							continue;
						//Check if there is even power to do things.
						if(BatteryPower.currPower == 0){
							isDoingSequence = false;
							yield break;
						}
						//Attemp to drill once in front based on the type of tile found there.
						Tile frontTile = RoverMovementScript.getAdjacentTile(direction);
						//Use some power to move and some to drill!
						BatteryPower.usePower();
						//If tile can be drilled it will and replaced by snow!
						if(frontTile.getcanDrill() == true){
							UnityEngine.Object.Destroy(frontTile.getGameObject());
							Tile tile = new SnowTile(frontTile.getPosition());

							//Randomly generate random item drop for this tile.
							int randomNumber  = randomPicker.Next(0,100);
							if(randomNumber <= 20){
								//Get position of tile and use it for item!
								float xCoord = tile.getPosition().x + frontTile.getIndexI();
								float yCoord = tile.getPosition().y + frontTile.getIndexJ();

								if(frontTile is MountainTile)
									tile.setItem(new Powder (new Vector2(xCoord, yCoord)));
								if(frontTile is CopperOreTile)
									tile.setItem(new Copper (new Vector2(xCoord, yCoord)));
								//Add more here if any.
							}
							//Set tile and wait!
							RoverMovementScript.setAdjacentTile(tile, direction);
							yield return new WaitForSeconds (delay);
						}

						yield return new WaitForSeconds (delay);
					}
				}

				//else??? TODO other actions.
			}
		} while(loop == true && stopFlag == false);
		isDoingSequence = false;
	}
	//================================================================================
	public int getLastActionValue(){
		// This method returns an integer associated with the last action added to the 
		// list. It is called by the UI in order to populate the sequencer. // hale - 03/31/15
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
	}// hale - 03/31/15

	//================================================================================
	public int getLastActionQty(){	// Lets the UI know how to set the command subscript
		return lastAction.getActionCounter ();
	}// hale - 04/01/15

	//================================================================================
	public int getLengthOfSequence(){ // Lets the UI know how many commands we have in our game list
		return list.Count;
	}// hale - 04/01/15

	//================================================================================
	public bool getPlayStatus (){ // Lets the UI know to clear the sequencer
		return clearFlag;
	}// hale - 04/01/15

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
	public void scannerClick(){
		ScannerAction scannerAction = new ScannerAction();
		addActionToList (scannerAction);
		return;
	}
	//================================================================================
	public void drillClick(){
		DrillAction drillAction = new DrillAction();
		addActionToList (drillAction);
		return;
	}
	//================================================================================
	//Clears the list field of this class ;)
	public void clearList(){ 
		sequencerEmpty = true;
		clearFlag = true;
		stopFlag = true;
		list.Clear ();
		lastAction = new EmptyAction ();// added so that the UI will know if the list has been cleared.
		return;
	}// hale - 04/01/15
	//================================================================================
	//Clears the list field of this class ;)
	public void stopRover(){ 
		stopFlag = true;
		return;
	}// hale - 04/01/15
	//================================================================================
}
