using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkingLight : MonoBehaviour {
	private float minimum = 0.0f;  //alpha channel value at fadeout end
	private float maximum = 1f;    //alpha channel value at fadeout begin
	private float duration = .5f; //duration of fadeout
	private float fadeoutTimer;   //fadeout stopwatch
	public SpriteRenderer blinkingSprite;
	public Transform otherRoverTransform; //rover transform
	public Transform lightTransform;      //light transform

	void Start () {
		fadeoutTimer = Time.time; //start stopwatch
	}
	

	void Update(){
		//compute current alpha value of sprite based upon stopwatch value
		float t = (Time.time - fadeoutTimer) / duration;
		blinkingSprite.color = new Color (1f, 1f, 1f, Mathf.SmoothStep (maximum,minimum,t));
		if (blinkingSprite.color.a <= 0.0f) {
			//reset stopwatch
			//current opacity is dependent on stopwatch, so resets alpha channel to maximum value
			fadeoutTimer = Time.time;
			//reposition light to rover
			lightTransform.position = otherRoverTransform.position;
		}
	}
}
