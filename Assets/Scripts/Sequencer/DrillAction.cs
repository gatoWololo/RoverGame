using UnityEngine;
using System.Collections;
/// <summary>
// Drill can drill through certain kinds of
// obstacles to gather minerals from it.
/// </summary>
public class DrillAction : RoverAction {
	
	//Constructor for class, requires direction this represents.
	public DrillAction(){
		this.actionCounter = 1;
		this.actionName = "drill";
		return;
	}
	
}