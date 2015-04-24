using UnityEngine;
using System.Collections;

public class ItemRef : MonoBehaviour {

	public int myType;

	public int myIndex;
	
	// Use this for initialization
	void Start (int index, int type) {
		myType = -1;
		myIndex = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getMyType(){
		return myType;
	}

	public int getMyIndex(){
		return myIndex;
	}

	public void setUiItemProperties(int type, int index){
		myIndex = index;
		myType = type;
	}

	

}
