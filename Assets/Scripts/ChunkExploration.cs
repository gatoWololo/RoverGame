using UnityEngine;
using System.Collections;

public class ChunkExploration : MonoBehaviour {

	// Use this for initialization
	void Start () {	}

	//If object is seen by the rover it should be set to viewable.
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.GetType ().IsAssignableFrom (typeof(BoxCollider2D)))
			return;

		Debug.Log ("In collider for edges!");
		Debug.Log (collider.tag);
		/*
		if (collider.CompareTag ("top"))
			Debug.Log ("Top!");
		if (collider.CompareTag ("bottom"))
			Debug.Log ("Bottom!");
		if (collider.CompareTag ("left"))
			Debug.Log ("Left!");
		if (collider.CompareTag ("right"))
			Debug.Log ("Right!");*/
		/*
		Component[] myComponents = collider.gameObject.GetComponentsInParent<Component>();
		for (int i = 0; i < myComponents.Length; i ++)
			Debug.Log (myComponents [i].GetType ().Name);
		//collider.
		*/
		Debug.Log ("Rover hit corner of Chunk!");

		return;
	}
	
	// Update is called once per frame
	void Update () {	}
}
