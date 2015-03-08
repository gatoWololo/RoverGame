using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour {
	private int maxPower;
	private int minPower;
	private int currPower;
	public Text power;
	public GameObject batteryPower;
	private float delay;
	private float nextMove;
	// Use this for initializtion
	void Start () {
		maxPower = 100;
		minPower = 0;
		currPower = maxPower;
		power = batteryPower.GetComponent<Text> ();
		power.text = "Battery Power: " + currPower;
		delay = (float)1.2;
		nextMove = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currPower > minPower) {

			if (Input.GetKeyDown (KeyCode.UpArrow) && Time.time > nextMove) {
				usePower ();
			}
			if (Input.GetKeyDown (KeyCode.DownArrow) && Time.time > nextMove) {
				usePower ();
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) && Time.time > nextMove) {
				usePower ();
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow) && Time.time > nextMove) {
				usePower ();
			}
		}
	}

	void usePower(){
		currPower= currPower-5;
		power.text= "Battery Power: " + currPower;
		nextMove = Time.time + delay;
	}
}