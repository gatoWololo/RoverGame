using UnityEngine;
using System.Collections;
/// <summary>
/// The empty action class used to make sure we have a
/// proper finite field. ;)
/// </summary>
public class EmptyAction : RoverAction {

	//Constructor for class, requires direction this represents.
	public EmptyAction(){
		this.actionCounter = 0;
		this.actionName = "\"The Empty Action\"";
		return;
	}
}
