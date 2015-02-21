using UnityEngine;
using System.Collections;

public class MainCameraZoom : MonoBehaviour {

	public float maxZoomOut;
	public float maxZoomIn;
	public float currentSize;
	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () {
		currentSize = Camera.main.orthographicSize;
				
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && currentSize < maxZoomOut) // back 
			Camera.main.orthographicSize++;

		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && currentSize > maxZoomIn) // forward
			Camera.main.orthographicSize--;

		return;
	}

}
