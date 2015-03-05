using UnityEngine;
using System.Collections;

public class MainCameraZoom : MonoBehaviour {
	
	public float maxZoomOut;
	public float maxZoomIn;

	public float verticalZoomScrollIncrement;
	public float horizontalZoomScrollIncrement;

	// Use this for initialization
	void Start () {	}
	
	// Update is called once per frame
	void Update () {

		float currentSize = Camera.main.orthographicSize;
		maxZoomIn = 5.0f;
		maxZoomOut = 50.0f;

		Transform mainCamTransform = Camera.main.transform;

		float mPosX = Input.mousePosition.x;
		float mPosY = Input.mousePosition.y;

		verticalZoomScrollIncrement = getVerticalZoomScrollincrement (mPosY, currentSize);
		horizontalZoomScrollIncrement = getHorizontalZoomScrollincrement (mPosX, currentSize);
				
		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && currentSize < maxZoomOut) // back 
			Camera.main.orthographicSize++;

		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && currentSize > maxZoomIn) { // forward
			Camera.main.orthographicSize--;
				mainCamTransform.Translate(Vector3.up * verticalZoomScrollIncrement);
				mainCamTransform.Translate(Vector3.right * horizontalZoomScrollIncrement);
		}

		return;
	}

	float getHorizontalZoomScrollincrement(float mPosX, float currentSize){
			float centerScreenWidth = Screen.width/2;
			float widthDiffMPosCenter = mPosX - centerScreenWidth;
			float horizontalZoomScrollIncrement = widthDiffMPosCenter / (currentSize*currentSize);

			if (horizontalZoomScrollIncrement < -0.25f) {
				horizontalZoomScrollIncrement = -0.25f;
			}
			if (horizontalZoomScrollIncrement > 0.25f) {
				horizontalZoomScrollIncrement = 0.25f;
			}

			return horizontalZoomScrollIncrement;
	}

	float getVerticalZoomScrollincrement(float mPosY, float currentSize){
			float centerScreenHeight = Screen.height/2;
			float heightDiffMPosCenter = mPosY - centerScreenHeight;
			float verticalZoomScrollIncrement = heightDiffMPosCenter/ (currentSize*currentSize) ;

			if (verticalZoomScrollIncrement < -0.25f) {
				verticalZoomScrollIncrement = -0.25f;
			}
			if (verticalZoomScrollIncrement > 0.25f) {
				verticalZoomScrollIncrement = 0.25f;
			}

			return verticalZoomScrollIncrement;
	}

}
