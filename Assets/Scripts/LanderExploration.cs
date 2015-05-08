using UnityEngine;
using System.Collections;

public class LanderExploration : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		//If it's a lander tile do not do this!
		Color color = renderer.color;
		
		renderer.color = new Color (color.r, color.g, color.b, 1.0f);
		
		return;
	}

	void OnTriggerStay(Collider collider){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		Color color = renderer.color;
		//Keep these tiles highlighted!
		renderer.color = new Color (color.r, color.g, color.b, 1.0f);



		}
}
