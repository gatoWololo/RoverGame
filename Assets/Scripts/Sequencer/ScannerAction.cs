using UnityEngine;
using System.Collections;
/// <summary>
// Scanner action performs a scan of the area revealing the
// any times and exploring the general vacinity.
/// </summary>
public class ScannerAction : RoverAction {

	//Constructor for class, requires direction this represents.
	public ScannerAction(){
		this.actionCounter = 1;
		this.actionName = "scan";
		return;
	}

}