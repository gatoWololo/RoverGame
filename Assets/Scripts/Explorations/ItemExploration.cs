using UnityEngine;
using System.Collections;

public class ItemExploration : MonoBehaviour {
	public static readonly float delay = 0.0f;
	//================================================================================
	//If object is seen by the rover it have it's alpha set high again.
	public void OnTriggerEnter2D(Collider2D collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.enabled = true;
		return;
	}
	//================================================================================
	//If object is seen by the rover it have it's alpha set high again.
	public void OnTriggerExit2D(Collider2D collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		StartCoroutine(hideItem(renderer));
		return;
	}
	//================================================================================
	public IEnumerator hideItem(SpriteRenderer renderer){
		yield return new WaitForSeconds (delay);
		renderer.enabled = false;	
	}
	//================================================================================
	// Use this for initialization
	void Start () {
	
	}
	//================================================================================
	// Update is called once per frame
	void Update () {
	
	}
	//================================================================================
}
