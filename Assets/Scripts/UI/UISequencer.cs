using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISequencer : MonoBehaviour {

	private int MAXSEQUENCE = 16;

	private int nextAction; // integer value representing the action we are going to add
	
	private int currentLength; // length of the sequence

	private int lastStepValue;

	private int stepValue;

	private List<GameObject> currentRoutine; // holds the game objects that represent commands

	GameObject currentCommand;

	public Transform FirstSequencerPosition; // a blank GameObject used for parenting the sequencer objects within the sequence grid panel

	private Vector3 vector;

	private Sprite sequenceSprite;

	Sequencer sequencer;

	Object prefab;


	void Start () {
		currentRoutine = new List<GameObject> ();
		currentLength = 0;
		nextAction = 0;
		GameObject roverObject = GameObject.Find("Rover");
		sequencer = roverObject.GetComponent<Sequencer> ();
		prefab = Resources.Load("Prefabs/sequencerStepImage", typeof(GameObject));
		vector = new Vector3 (0f, 0f);
		lastStepValue = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (sequencer.getPlayStatus ()) {
			currentCommand = null;
			resetSequencer();
		}
		if (currentLength < MAXSEQUENCE) { // check to see if there is room in the sequencer
			if (currentLength < sequencer.getLengthOfSequence ()) { // check to see if there has been a command added to the sequence.
				currentLength++;
				nextAction = sequencer.getLastActionValue ();

				addActionToUISequence(nextAction);
			}
		}

		stepValue = sequencer.getLastActionQty (); // get the number of steps in the current command object

		if (stepValue > lastStepValue) { // if it has increased set the subscript on the current command
			if (sequencer.getLastActionQty () > 1) {
				setSubscript (sequencer.getLastActionQty (), sequencer.getLastActionValue ());
			}
		}
		lastStepValue = stepValue;
	}

	private void addActionToUISequence(int nextAction){ 
	// This method instanciates a new GameObject from prefab, sets the images position within the sequencer grid and then sets 
	// the image to match the last command entered using the nextAction parameter.

		currentCommand = Instantiate(prefab, FirstSequencerPosition.position, FirstSequencerPosition.rotation) as GameObject;
		currentCommand.transform.SetParent(FirstSequencerPosition);
		calculateGridPosition ();
		currentCommand.transform.position = currentCommand.transform.position + vector;

		switch (nextAction) { //TODO integrate this into the setSubcript method and then make this a method call
			case 1:
				currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/UpArrow1", typeof(Sprite)) as Sprite;
				break;
			case 2:
			currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/DownArrow1", typeof(Sprite)) as Sprite;
				break;
			case 3:
			currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/LeftArrow1", typeof(Sprite)) as Sprite;
				break;
			case 4:
			currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/RightArrow1", typeof(Sprite)) as Sprite;
				break;
		}

		currentRoutine.Add (currentCommand);
	}

	private void calculateGridPosition(){
		// modifies the class field 'vector' in order to calculate the offset for the next game object within the sequencer
		// TODO remove hardcoded numbers and replace with defined constants
		switch(currentLength%4){
			case 0:
				vector.x = 150f;
				break;
			case 1:
				if(currentLength>1){
					vector.y = vector.y - 40f;
				}
				vector.x = 0f;
				break;
			case 2:
				vector.x = 50f;
				break;
			case 3:
				vector.x = 100f;
				break;
		}
	}

	private void setSubscript(int sub, int currentAction){
		// Im sorry about this. Unity will not let me programmatically layer a UI text element on top of a UI image element, So I must 
		// flip flop images during runtime :(
		switch (currentAction) {
		case 1:
			switch (sub) {
				case 2:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/UpArrow2", typeof(Sprite)) as Sprite;
					break;
				case 3:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/UpArrow3", typeof(Sprite)) as Sprite;
					break;
				case 4:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/UpArrow4", typeof(Sprite)) as Sprite;
					break;
			}
			break;
		case 2:
			switch (sub) {
				case 2:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/DownArrow2", typeof(Sprite)) as Sprite;
					break;
				case 3:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/DownArrow3", typeof(Sprite)) as Sprite;
					break;
				case 4:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/DownArrow4", typeof(Sprite)) as Sprite;
					break;
			}
			break;
		case 3:
			switch (sub) {
				case 2:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/LeftArrow2", typeof(Sprite)) as Sprite;
					break;
				case 3:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/LeftArrow3", typeof(Sprite)) as Sprite;
					break;
				case 4:
					currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/LeftArrow4", typeof(Sprite)) as Sprite;
					break;
			}
			break;
		case 4:
			switch (sub) {
			case 2:
				currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/RightArrow2", typeof(Sprite)) as Sprite;
				break;
			case 3:
				currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/RightArrow3", typeof(Sprite)) as Sprite;
				break;
			case 4:
				currentCommand.GetComponent<Image>().overrideSprite = Resources.Load("Textures/RightArrow4", typeof(Sprite)) as Sprite;
				break;
			}
			break;
		}
	}

	private void resetSequencer(){
		// destroys all the sequencer commands and resets relevant class fields for next command sequence
		foreach (GameObject command in currentRoutine) {
			Destroy(command);
		}
		currentLength = 0;
		vector.x = 0f;
		vector.y = 0f;
		lastStepValue = 1;
	}

	
}
