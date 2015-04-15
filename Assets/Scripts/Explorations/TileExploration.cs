using UnityEngine;
using System.Collections;

public class TileExploration : MonoBehaviour {
	private readonly float alphaMax = 1.0f;
	private readonly float alphaMedium = 0.3f;

	//Actual color of sprite, when tile is explored this color gets set.
	protected Color realColor;
	//================================================================================
	// Use this for initialization
	void Start () {	}
	//================================================================================
	//If object is seen by the rover it have it's alpha set high again.
	void OnTriggerEnter2D(Collider2D collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		Color color = renderer.color;
		renderer.color = new Color (color.r, color.g, color.b, alphaMax);

		return;
	}
	//================================================================================
	// Update is called once per frame
	void Update () {	}
	//================================================================================
	//When the collider exits make the tiles medium dark to simulate fog of war.
	void OnTriggerExit2D(Collider2D collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		StartCoroutine(darkenTile(renderer));
		return;
	}
	//================================================================================
	public IEnumerator darkenTile(SpriteRenderer renderer){
		yield return new WaitForSeconds (0.0f);
		Color color = renderer.color;
		renderer.color = new Color (color.r, color.g, color.b, alphaMedium);
	}
	//================================================================================

}
