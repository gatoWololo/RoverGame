using UnityEngine;
using System.Collections;

public class TileExploration : MonoBehaviour {

	// Use this for initialization
	void Start () {	}

	//If object is seen by the rover it should be set to viewable.
	void OnTriggerEnter2D(Collider2D collider){
		GetComponent<Renderer> ().renderer.enabled = true;
		
		return;
	}
	
	// Update is called once per frame
	void Update () {	}
}
