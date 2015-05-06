using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
	public void testGetAngle(){
		RoverMovementScript rm = new RoverMovementScript ();
		float result = rm.getAngle (Direction.Up, Direction.Down);
		Assert.AreEqual (180.0f, result); 
		return;
	}
	
	[Test]
	public void testIsOpposite(){
		RoverMovementScript rm = new RoverMovementScript ();
		bool result = rm.isOpposite (Direction.Up, Direction.Down);
		Assert.AreEqual (true, result); 
		return;
	}
	[Test]
	public void testUsePower(){
		BatteryPower.currPower = 10;
		BatteryPower.power.text = "Battery Power: " + BatteryPower.currPower;
		BatteryPower.usePower ();
		Assert.AreEqual (9, BatteryPower.currPower);

	}

	[Test]
	public void testAddPower(){
		BatteryPower.currPower = 10;
		BatteryPower.addPower (30);
		Assert.AreEqual (40, BatteryPower.currPower);
		
	}

	[Test]
	public void testRemovePower(){
		BatteryPower.currPower = 40;
		BatteryPower.removePower (20);
		Assert.AreEqual (20, BatteryPower.currPower);
		
	}

	[Test]
	public void testCreateChunk(){
		ChunkCreator cc = new ChunkCreator ();
		//Create new chunk.
		Chunk newChunk = cc.createNewChunk (10.0f, 10.0f, false);
		Assert.AreEqual (newChunk.getPositionX (), 10.0f);

	}

	[Test]
	public void testGetRandomPositions(){
		ChunkCreator cc = new ChunkCreator ();

		Vector2[] positions = cc.getRandomPositionTiles (ChunkCreator.chunkSize, 5, 3);
		Assert.AreEqual (3, positions.Length);
	}

	[Test]
	public void testGetRandomTile(){
		ChunkCreator cc = new ChunkCreator ();
		Tile tile = cc.getRandomTile (new Vector2 (5.0f, 10.0f));
		Assert.AreEqual (5.0f, tile.getPosition ().x);
		                         

	}
	
	[Test]
	public void testPow2(){
		ChunkCreator cc = new ChunkCreator ();
		//The world is now safe ;)
		Assert.AreEqual (25.0f, cc.pow2 (5.0f));		
	}

	[Test]
	public void testIsSame(){
		ChunkCreator cc = new ChunkCreator ();
		Assert.AreEqual(cc.isSame(3,3, 3), true);
	}

	[Test]
	public void testFarEnough(){
		ChunkCreator cc = new ChunkCreator ();
		Assert.AreEqual (cc.farEnough (new Vector2 (1.0f, 1.0f), new Vector2 (2.0f, 3.0f)), false);

	}
	
	[Test]
	public void testGetProperEdgeTile(){
		ChunkCreator cc = new ChunkCreator ();
		Tile tile = cc.getProperEdgeTile (4, 4, new Vector2 (0.0f, 0.0f));
		Assert.AreEqual (tile is CornerSnowTile, true);
	}

	[Test]
	public void testGetWorldIndex(){
		ChunkExploration ce = new ChunkExploration ();
		Vector2 v = ce.getWorldIndex (3, 4, Direction.Right);
		Assert.AreEqual (new Vector2 (4.0f, 4.0f), v);
	}

	[Test]
	public void testGetNewCoord(){
		ChunkExploration ce = new ChunkExploration ();
		Vector2 v = ce.getNewCoord (Direction.Up, new Vector2 (0.0f, 50.0f));
		Assert.AreEqual (100.0f, v.y);

	}

	[Test]
	public void testAddElement(){
		Inventory inventory = new Inventory ();
		inventory.addElement (new Item(new Vector2(0.0f, 0.0f)));
		Assert.AreEqual (inventory.getLength (), 1);

	}

	[Test]
	public void testRemoveElement(){
		Inventory inventory = new Inventory ();
		inventory.addElement (new Item (new Vector2 (0.0f, 0.0f)));
		inventory.addElement (new Item (new Vector2 (0.0f, 0.0f)));
		inventory.addElement (new Item (new Vector2 (0.0f, 0.0f)));
		inventory.removeElement(2);
        Assert.AreEqual (inventory.getLength (), 2);
		                      
	}


	[Test]
	public void testGetItemAtIndex(){
		Inventory inventory = new Inventory ();
		inventory.addElement (new Battery(new Vector2 (0.0f, 0.0f)));
		inventory.addElement (new Powder(new Vector2 (0.0f, 0.0f)));
		inventory.addElement (new Copper(new Vector2 (0.0f, 0.0f)));
		Assert.AreEqual ("powder", inventory.getItemAtIndex(1).getName());
	}

	[Test]
	public void addItemsToChunk(){
		ItemCreator ic = new ItemCreator ();
		ChunkCreator cc = new ChunkCreator ();
		//Create new chunk.
		Chunk newChunk = cc.createNewChunk (10.0f, 10.0f, false);
		ic.addItemsToChunk (newChunk, false);
		bool hasItem = false;
		Tile[,] array = newChunk.getTileArray ();

		for (int i = 0; i < 50; i++)
			for (int j = 0; j < 50; j++) {
				Tile tile = array [i, j];
				if(tile.hasItem())
					hasItem = true;
			}

		Assert.AreEqual (true, hasItem);
	}

	[Test]
	public void testDestroyGameObject(){
		Item i = new Item(new Vector2(0.0f, 0.0f));
		i.destroyGameObject ();
		Assert.AreEqual (i.getGameObject () == null, true);

	}

	[Test]
	public void testOnTriggerEnter2D(){
		ItemExploration ie = new ItemExploration ();
		ie.OnTriggerEnter2D (new Collider2D ());
		Assert.AreEqual (ie.renderer != null, true);

	}

	[Test]
	public void testOnTriggerExit2D(){
		ItemExploration ie = new ItemExploration ();
		SpriteRenderer sr = new SpriteRenderer ();
		ie.OnTriggerExit2D (new CircleCollider2D ());
		Assert.AreEqual (sr.renderer.enabled, false);	
	}

	[Test]
	public void testHideItem(){
		ItemExploration ie = new ItemExploration ();
		SpriteRenderer sr = new SpriteRenderer ();
		ie.hideItem (sr);
		Assert.AreEqual (sr.enabled, false);
		
	}

	[Test]
	public void testUiItemProperties(){
		ItemRef ir = new ItemRef ();
		ir.setUiItemProperties (1, 1);
		Assert.AreEqual (ir.myEnergy, 100);

	}

	[Test]
	public void testGetEdgePoints(){
		World w = new World ();
		Vector2 v1 = new Vector2(1.0f, 2.0f);
		Vector2 v2 = new Vector2(3.0f, 4.0f);
		Vector2[] array = w.getEdgePoints (v1, v2);
		Assert.AreEqual (array [0].x, 1.0f);
	}

	[Test]
	public void testAddFluxCapacitor(){
		World w = new World();
		w.addFluxCapacitor();
		Assert.AreEqual(true, w.fluxCapacitorFlag);

	}

	[Test]
	public void testAddDrill(){
		Chunk c = new Chunk (0.0f, 0.0f);
		World w = new World ();
		World.fluxPicker = new System.Random ();

		w.addDrill (c);
		bool hasDrill = false;

		Tile[,] array = c.getTileArray ();
		for (int i = 0; i < 50; i++){
			if (hasDrill)
				break;
			for (int j = 0; j < 50; j++)
				if (array [i, j].getItem () is Drill) {
					hasDrill = true;
					break;
				}
		}
		Assert.AreEqual (true, hasDrill);
	}

	[Test]
	public void testAddItemToInventory(){
		UIInventory ui = new UIInventory ();
		ui.addItemToInventory (new Drill (new Vector2 (0.0f, 0.0f)));
		Assert.AreEqual (ui.currentLength, 1);

	}

	[Test]
	public void testResetInventoryVector(){
		UIInventory ui = new UIInventory ();
		ui.resetInventoryVector ();
		Assert.AreEqual (ui.vector.x, 0.0f);	
	}

	[Test]
	public void testCalculateGridPosition(){
		UIInventory ui = new UIInventory ();
		ui.calculateGridPosition (14);
		Assert.AreEqual (ui.vector.x, 40.0f);	
	}

	[Test]
	public void testAddActionToList(){
		Sequencer se = new Sequencer();
		se.publicStart ();
		se.addActionToList(new DrillAction());
		Assert.AreEqual(se.getLengthOfSequence(), 1);
	
	}
	
	[Test]
	public void testSetLoop(){
		Sequencer se = new Sequencer();
		se.publicStart ();
		se.setLoop ();
		Assert.AreEqual (se.loop, true);
	}
	
	[Test]
	public void testUpArrowClick(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.upArrowClick ();
		se.upArrowClick ();
		se.upArrowClick ();
		Assert.AreEqual(se.getLengthOfSequence(), 1);

	}
	[Test]
	public void testDownArrowClick(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.upArrowClick ();
		se.downArrowClick ();
		se.upArrowClick ();
		Assert.AreEqual (se.getLengthOfSequence(), 3);
		
	}

	[Test]
	public void testLeftArrowClick(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.downArrowClick ();
		se.upArrowClick ();
		se.leftArrowClick ();
		se.leftArrowClick ();
		se.leftArrowClick ();
		se.leftArrowClick ();
		Assert.AreEqual (se.getLengthOfSequence(), 3);
		
	}

	[Test]
	public void testRightArrowClick(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.rightArrowClick ();
		se.rightArrowClick ();
		se.rightArrowClick ();
		se.rightArrowClick ();

		se.rightArrowClick ();
		se.rightArrowClick ();
		se.rightArrowClick ();
		se.rightArrowClick ();


		Assert.AreEqual (se.getLengthOfSequence(), 2);
		
	}

	[Test]
	public void testDrillClick(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.drillClick ();
		Assert.AreEqual (se.getLengthOfSequence(), 1);
		
	}

	[Test]
	public void testClearList(){
		Sequencer se = new Sequencer ();
		se.publicStart ();	
		se.upArrowClick ();
		se.downArrowClick ();
		se.upArrowClick ();
		se.clearList ();
		Assert.AreEqual (se.getLengthOfSequence(), 0);
		
	}

	[Test]
	public void testClearList2(){
		Sequencer se = new Sequencer ();
		se.publicStart ();
		se.stopRover ();
		Assert.AreEqual (se.stopFlag, true);
	}

	[Test]
	public void testLoopToggle(){
		LedToggle lt = new LedToggle ();
		lt.clearLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/ClearLED");
		lt.clearImage = lt.clearLedObject.GetComponent<Image> ();
		lt.loopLedToggle ();
		Assert.AreEqual (lt.loopLed, false);
	}

	[Test]
	public void testClearOn(){
		LedToggle lt = new LedToggle ();
		lt.clearLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/ClearLED");
		lt.clearImage = lt.clearLedObject.GetComponent<Image> ();
		lt.clearOn ();
		Assert.AreEqual (false, lt.clearChanged);
	}

	[Test]
	public void testClearOff(){
		LedToggle lt = new LedToggle ();
		lt.clearLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/ClearLED");
		lt.clearImage = lt.clearLedObject.GetComponent<Image> ();
		lt.clearOff ();
		Assert.AreEqual (0.0f, lt.triggertime);
	}

	[Test]
	public void testClearLedFlash(){
		LedToggle lt = new LedToggle ();
		lt.clearLedObject = GameObject.Find("Interface/InterfaceBackground/RoutineControlGrid/ClearLED");
		lt.clearImage = lt.clearLedObject.GetComponent<Image> ();
		lt.clearLedFlash ();
		Assert.AreEqual (lt.clearChanged, true);
	}

	[Test]
	public void testIncreaseActionCounter(){
		RoverAction r = new EmptyAction ();
		r.increaseActionCounter (5);
		Assert.AreEqual (r.getActionCounter (), 5);
	}

	[Test]
	public void testAddActionToUISequencer(){
		UISequencer us = new UISequencer();
		us.FirstSequencerPosition = new GameObject ().transform;
		us.addActionToUISequence (1);
		Assert.AreEqual (us.currentLength, 1);
	}

	[Test]
	public void testCalculateGridPosition2(){
		UISequencer us = new UISequencer ();
		us.addActionToUISequence (1);
		us.addActionToUISequence (1);
		us.addActionToUISequence (1);
		us.addActionToUISequence (1);
		us.calculateGridPosition ();

		Assert.AreEqual(us.vector.x, 0);
	}

	[Test]
	public void testInitEquimpment(){
		UIEquipment ue = new UIEquipment ();
		ue.initEquipment ();
		Assert.AreEqual (ue.currentItem != null, true);
	}

	[Test]
	public void testExpelBattery(){
		UIEquipment ue = new UIEquipment ();
		ue.expelBattery(0);
		Assert.AreEqual(ue.batteriesEquipped,0);
	}

	//Ad more tests here or to this directory.
}
