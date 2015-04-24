using UnityEngine;
using System.Collections;

public class ItemRef : MonoBehaviour {

	public int myType;

	public int myIndex;
	
	public int getMyType(){
		return myType;
	}

	public int getMyIndex(){
		return myIndex;
	}

	public void setUiItemProperties(int type, int index){
		Debug.Log ("ItemRef being set to: "+ index + " My type is: "+ type );
		myIndex = index;
		myType = type;
	}	

}
