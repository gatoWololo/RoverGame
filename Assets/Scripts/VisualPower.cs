using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualPower : MonoBehaviour {
	private RectTransform powerTransform;
	public GameObject powerTransform0;
	private Image visual;
	public GameObject visual0;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private int currentHealth;
	private int maxHealth=100;
	float delay = 1.7f;
	float nextMove = 0;
	// Use this for initialization
	void Start () {
		visual = visual0.GetComponent<Image> ();
		powerTransform = powerTransform0.GetComponent<RectTransform> ();
		cachedY = powerTransform.position.y;
		maxXValue = powerTransform.position.x;
		minXValue = powerTransform.position.x - powerTransform.rect.width;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth > 0) {
			if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) && Time.time > nextMove) {
				currentHealth = currentHealth - 2;
				HandlePower ();
			}
		}
	}

	private void HandlePower(){
		float currentXValue = MapValues (currentHealth, 0, maxHealth, minXValue, maxXValue);
		powerTransform.position = new Vector2 (currentXValue, cachedY);

		if (currentHealth > maxHealth / 2) {
			visual.color = new Color32 ((byte)MapValues (currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
		} else {
			visual.color = new Color32 (255, (byte)MapValues (currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
		}
		nextMove = Time.time + delay;
	}

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return(x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	public void upClick(){
		if (nextMove < Time.time) {
			if (currentHealth > 0) {
				currentHealth = currentHealth - 2;
				HandlePower ();
			}
		}
	}
}
