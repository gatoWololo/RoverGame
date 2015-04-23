using UnityEngine;
using System.Collections;

public enum Direction {Up, Down, Left, Right};

public class RoverScript : MonoBehaviour {
	//The rover knows which grid it's on so the game is able to get information about.
	private int currentGrid;
	//The line of sight for the rover to explore squares around him.
	private int lineOfSight;
	public Inventory inventory;
	//Do we have the drill yet?
	private bool hasDrill;

	// Use this for initialization
	void Start () {
		inventory = new Inventory ();
		//Initialize Position of rover!
		Transform roverPosition = GetComponentInParent<Transform> ();
		float s = World.chunkSize;
		//Rover starts at the [10][10] tile!
		float offset = 10.5f;
		roverPosition.transform.position = new Vector3 (s + 10.5f, s + 10.5f, 0f);
		//At the begginging we don't have the drill.
		hasDrill = false;
	}


	// Update is called once per frame
	void Update () {
	
	}

	public void setHasDrill(bool setting){
		hasDrill = setting;
		return;
	}

	public bool getHasDrill(){
		return hasDrill;
	}
}
