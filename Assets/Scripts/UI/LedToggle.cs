using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LedToggle : MonoBehaviour {


	private GameObject loopLedObject;
	private Image ledImage;
	public bool loopLed;
	private bool loopChanged;

	private GameObject clearLedObject;
	private Image clearImage;
	private bool clearChanged;
	private float triggertime;
	private float flashDuration;



	// Use this for initialization
	void Start () {
		loopLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/LoopLED");
		ledImage = loopLedObject.GetComponent<Image> ();
		ledImage.enabled = false;
		loopLed = false;
		loopChanged = false;

		clearLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/ClearLED");
		clearImage = clearLedObject.GetComponent<Image> ();
		clearImage.enabled = false;
		clearChanged = false;
		triggertime = 0f;
		flashDuration = .15f;

	}
	
	// Update is called once per frame
	void Update () {
		if (loopChanged) {
			loopToggle ();
		}
		if (clearChanged) {
			clearOn();
		}
		if (triggertime != 0f) {
			Debug.Log ("TriggerTime: " + (Time.time-triggertime));
			if(Time.time - triggertime	> flashDuration){
				clearOff();
			}
		}
	}

	// toogles the 'on' LED image based upon the images current state.
	//		ON -> OFF
	//		OFF -> ON
	// also resets the changed flag to default: false
	private void loopToggle(){
		loopChanged = false;
		if (!loopLed) {
			loopLed = true;
			ledImage.enabled = true;
		} else {
			ledImage.enabled = false;
			loopLed = false;
		}
	}
	 
	private void clearOn(){
		clearChanged = false;
		triggertime = Time.time;
		Debug.Log ("TriggerTime: " + triggertime);

		clearImage.enabled = true;
	}

	private void clearOff(){
		clearImage.enabled = false;
		triggertime = 0f;
	}

	// public method called by the UI to set the toggle flag to flip the led state
	public void loopLedToggle(){
		loopChanged = true;
	}

	public void clearLedFlash(){
		clearChanged = true;
	}
}