using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameMainMenuButtonLogic : MonoBehaviour {

	private bool mainMenuClicked = false;


	public void killGame(){
		Application.Quit();
	}

	public void restartRoverGame(){
		Application.LoadLevel("roverGame");
	}

	public void openMainMenu(){
		
		Button quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
		Button restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
		Image quitImage = GameObject.Find("QuitButton").GetComponent<Image>();
		Image restartImage = GameObject.Find("RestartButton").GetComponent<Image>();
		Image menuImage = GameObject.Find("MenuOptions").GetComponent<Image>();
		
		if(mainMenuClicked){
			menuImage.enabled = false;
			restartImage.enabled = false;
			quitImage.enabled = false;
			quitButton.interactable = false;
			restartButton.interactable = false;
			mainMenuClicked = false;
		}else{
			menuImage.enabled = true;
			restartImage.enabled = true;
			quitImage.enabled = true;
			quitButton.interactable = true;
			restartButton.interactable = true;
			mainMenuClicked = true;
		}
	
	}

}
