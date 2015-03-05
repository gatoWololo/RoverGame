using UnityEngine;
using System.Collections;

public class MainCameraScroll : MonoBehaviour {
	public float mPosX;
	public float mPosY;
	public int scrollDistance; 
	public float scrollSpeed;
	public int scrollArea;
	public float screenWidth;
	public float screenHeight;
	public float currentSize;

	public Transform myTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		myTransform = Camera.main.transform;
		currentSize = Camera.main.orthographicSize;

		mPosX = Input.mousePosition.x;
		mPosY = Input.mousePosition.y;
		scrollDistance = 5; 
		scrollSpeed = currentSize/3;
		scrollArea = 100;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		
		// Do camera movement by mouse position 
		if (mPosX < scrollArea) {
			myTransform.Translate(Vector3.right * -scrollSpeed * Time.deltaTime);
		} 
		if (mPosX >= Screen.width-scrollArea) {
			myTransform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
		}
		if (mPosY < scrollArea) {
			myTransform.Translate(Vector3.up * -scrollSpeed * Time.deltaTime);
		}
		if (mPosY >= Screen.height-scrollArea) {
			myTransform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
		}
		
		// Do camera movement by keyboard 
		//myTransform.Translate(Vector3(Input.GetAxis("EditorHorizontal") * scrollSpeed * Time.deltaTime, Input.GetAxis("EditorVertical") * scrollSpeed * Time.deltaTime, 0) );

	}
}
