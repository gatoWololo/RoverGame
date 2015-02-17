using UnityEngine;
using System.Collections;

public enum Direction {Up, Down, Left, Right};

public class RoverScript : MonoBehaviour {
	//The rover knows which grid it's on so the game is able to get information about.
	private int currentGrid;
	//The line of sight for the rover to explore squares around him.
	private int lineOfSight;
	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
	
	}
}
