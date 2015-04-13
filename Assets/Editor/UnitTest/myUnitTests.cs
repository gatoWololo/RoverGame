using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class myUnitTests : MonoBehaviour {
	[Test]
	public void testMoveAction(){
		string actionName;
		actionName = MoveAction.getNameString (Direction.Up);
		Assert.AreEqual("Up", actionName);
		return;
	}

	[Test]
	public void testRotate(){
		RoverMovementScript rm = new RoverMovementScript ();
		float result = rm.getAngle (Direction.Up, Direction.Down);
		Assert.AreEqual (180.0f, result); 
		return;
	}
	//Add more tests here or to this directory.
}
