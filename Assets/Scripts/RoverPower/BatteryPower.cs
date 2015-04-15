using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour {
	private static int maxPower;
	private static int minPower;
	public static int currPower;
	public static Text power;
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
		delay = 1.7f;
		nextMove = 0;
	}


	static public void usePower(){
		if(currPower > minPower){
			currPower = currPower - 1;
			power.text = "Battery Power: " + currPower;
		}
		return;
	}	

}
