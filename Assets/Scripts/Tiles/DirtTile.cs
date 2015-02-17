using UnityEngine;
using System.Collections;

/*
 * Generic dirt tile that will cover some of the land.
 */
public class DirtTile : Tile {

	public bool test;
	// Use this for initialization
	void Start () {
		canPassThroughIt = true;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
