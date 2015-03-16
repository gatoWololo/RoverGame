using UnityEngine;
using System.Collections;

public class TileExploration : MonoBehaviour {
	//Actual color of sprite, when tile is explored this color gets set.
	protected Color realColor;

	// Use this for initialization
	void Start () {	}

	//If object is seen by the rover it have it's alpha set high again.
	void OnTriggerEnter2D(Collider2D collider){
		SpriteRenderer renderer =  	GetComponent<SpriteRenderer> ();
		Color color = renderer.color;
		renderer.color = new Color (color.r, color.g, color.b, 1.0f);

		return;
	}
	
	// Update is called once per frame
	void Update () {	}
	//================================================================================
	//Set the tile to the specified color. Original color has been saved so it can be
	//restored later, used for the tile exploration.
	public void saveColor(Color color){
		//Debug.Log ("saveColor color: " + color.ToString ());
		realColor = color;
		return;
	}
	//================================================================================                  
}
