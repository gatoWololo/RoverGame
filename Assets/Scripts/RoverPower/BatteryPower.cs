using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BatteryPower : MonoBehaviour {
	private static int minPower;
	public static int currPower;
	public static Text power;
	public GameObject batteryPower;
	private float delay;
	private float nextMove;
	// Use this for initializtion
	void Start () {
		minPower = 0;
		currPower = 100;
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


	static public void addPower(int charge){
		currPower = currPower + charge;
		Debug.Log ("Current Power = "+currPower);
	}

	static public void removePower(int charge){
		currPower = currPower - charge;
		Debug.Log ("Current Power = "+currPower);
	}
		

}
