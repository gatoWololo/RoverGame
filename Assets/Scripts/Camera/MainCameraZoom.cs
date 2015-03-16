using UnityEngine;
using System.Collections;

public class MainCameraZoom : MonoBehaviour {
	
	public float maxZoomOut;
	public float maxZoomIn;


	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () {

		float currentSize = Camera.main.orthographicSize;
		maxZoomIn = 5.0f;
		maxZoomOut = 50.0f;

		Transform mainCamTransform = Camera.main.transform;

		// get mouse pointers current position
		float mPosX = Input.mousePosition.x;
		float mPosY = Input.mousePosition.y;

		// calculate the vector from the center of screen to mouse pointers current position
		Vector3 scrollDirection = getVectorforZoomDirection (mPosX, mPosY);

		// zoom out
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && currentSize < maxZoomOut) { 
			Camera.main.orthographicSize++;
		}

		// zoom in
		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && currentSize > maxZoomIn) {
			Camera.main.orthographicSize--;
			mainCamTransform.Translate(scrollDirection); // zoom in towards mouse pointer

		}

		return;
	}


	Vector3 getVectorforZoomDirection(float mPosX, float mPosY){
		// This method generates a vector that represents the direction the camera should take
		// to arrive at the mouse pointers current position from the current center of the screen.
		float centerScreenWidth = Screen.width/2;
		float centerScreenHeight = Screen.height/2;
		float widthDiffMPosCenter = (mPosX - centerScreenWidth)/Screen.width;
		float heightDiffMPosCenter = (mPosY - centerScreenHeight)/Screen.height;
		Vector3 scrollDirection = new Vector3 (widthDiffMPosCenter, heightDiffMPosCenter, 0.0f);
		return scrollDirection;
	}

}
