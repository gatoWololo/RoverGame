using UnityEngine;
using System.Collections;

public class ItemExploration : MonoBehaviour {
	//If object is seen by the rover it have it's alpha set high again.
	void OnTriggerEnter2D(Collider2D collider){
		SpriteRenderer renderer =  	GetComponent<SpriteRenderer> ();
		renderer.enabled = true;
		
		return;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
