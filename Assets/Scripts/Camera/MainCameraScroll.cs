using UnityEngine;
using System.Collections;

public class MainCameraScroll : MonoBehaviour {
		
	public int scrollArea;
	public float currentSize;
	public float horizontalScrollSpeed;
	public float verticalScrollSpeed;

	// Use this for initialization
	void Start () {
		//Initialize Position of Camera!
		Transform pos = GetComponentInParent<Transform> ();
		float s = World.chunkSize;
		//Camera starts at the [10][10] tile!
		float offset = 10.5f;
		pos.transform.position = new Vector3 (s + 10.5f, s + 10.5f, -30f);

		return;
	}
	
	// Update is called once per frame
	void Update () {
		//get the transform of main camera so we can modify it
		Transform myTransform = Camera.main.transform;
		currentSize = Camera.main.orthographicSize;

		// get the mouse pointer coords
		float mPosX = Input.mousePosition.x;
		float mPosY = Input.mousePosition.y;

		// get the screen width and height
		float screenWidth = Screen.width;
		float screenHeight = Screen.height;

		// set the depth of the scroll bounds around the edges of the screen
		scrollArea = 75;

		// calculate the horizontal and vertical scroll speeds relative to the mouses position within 
		// the scroll boundary
		horizontalScrollSpeed = getHorizontalScrollSpeed(mPosX,scrollArea, screenWidth);
		verticalScrollSpeed = getVerticalScrollSpeed(mPosY, scrollArea, screenHeight);
		
		// Do camera movement by mouse position 
		if (mPosX < scrollArea) {
			myTransform.Translate(Vector3.right * -horizontalScrollSpeed * Time.deltaTime);
		} 
		if (mPosX >= screenWidth-scrollArea) {
			myTransform.Translate(Vector3.right * horizontalScrollSpeed * Time.deltaTime);
		}
		if (mPosY < scrollArea) {
			myTransform.Translate(Vector3.up * -verticalScrollSpeed * Time.deltaTime);
		}
		if (mPosY >= screenHeight-scrollArea) {
			myTransform.Translate(Vector3.up * verticalScrollSpeed * Time.deltaTime);
		}

	}

	float getHorizontalScrollSpeed(float mPosX, float scrollArea, float screenWidth){
		// This method calculates the rate at which the screen should scroll horizontaly by isolating
		// the mouses position within the scroll bounds and then setting the scroll speed accordingly
		// params:
		//		float mPosX : the mouses x position on the screen
		//		float scrollArea : the width of the scroll boundary
		//		float screen width: the total width of the screen
		//
		// returns:
		//		float scrollSpeed: a value from 1.0 -> 7.0


		// calculate the mouse depth within x scroll bound
		float xDistance = Mathf.Abs ((screenWidth / 2)- mPosX) ;
		float scrollPosition = xDistance - ((screenWidth / 2) - scrollArea);

		int scrollSpeed;
		// set scroll speed based on depth
		if (scrollPosition > 0) {
			scrollSpeed = (int)scrollPosition / 35;
		} 
		else scrollSpeed = 0;

		return (float)scrollSpeed * MainCameraZoom.currentSize / 10;

	}

	float getVerticalScrollSpeed(float mPosY, float scrollArea, float screenHeight){
		// This method calculates the rate at which the screen should scroll vertically by isolating
		// the mouses position within the scroll bounds and then setting the scroll speed accordingly
		// params:
		//		float mPosY : the mouses y position on the screen
		//		float scrollArea : the width of the scroll boundary
		//		float screen width: the total width of the screen
		//
		// returns:
		//		float scrollSpeed: a value from 1.0 -> 7.0


		// calculate the mouses depth within the Y scroll bound
		float yDistance = Mathf.Abs (mPosY - (screenHeight / 2));
		float scrollPosition = yDistance - ((screenHeight / 2) - scrollArea);
		int scrollSpeed;
		// set scroll speed based on depth
		if (scrollPosition > 0) {
			scrollSpeed = (int)scrollPosition / 35;
		} 
		else scrollSpeed = 0;

		return (float)scrollSpeed * MainCameraZoom.currentSize / 10;

	}

}
