using UnityEngine;
using System.Collections;

public class ItemRef : MonoBehaviour {

	public int myType;

	public int myIndex;

	public int myEnergy;
	
	public int getMyType(){
		return myType;
	}

	public int getMyIndex(){
		return myIndex;
	}

	public void setUiItemProperties(int type, int index){
		//Debug.Log ("ItemRef being set to: "+ index + " My type is: "+ type );
		myIndex = index;
		if(type ==1){
			myEnergy = 100;
		}
		else myEnergy = -1;
		myType = type;
		
	}

	public void setMyEnergy(int n){
		myEnergy = n;
	}

	public int getMyEnergy(){
		return myEnergy;
	}	

}
