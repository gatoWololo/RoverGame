using UnityEngine;
using System.Collections;

public class ButtonLogic : MonoBehaviour {

	


	public void LaunchChosenGame(int gameId){
		if(gameId == 1){
			Application.LoadLevel("roverGame");
		}else{
			;//LAUNCH INVASION FOREVER
		}
	}

	public void killGame(){
		Application.Quit();
	}	

}
