using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour {

	


	public void LaunchChosenGame(int gameId){
		Button roverGameButton;
		Button invForGameButton;
		Image RoverGameImage;
		Image invForGameImage;
		roverGameButton = GameObject.Find("Invasion Forever Button").GetComponent<Button>();
		invForGameButton = GameObject.Find("Rover Game Button").GetComponent<Button>();
		roverGameButton.interactable = false;
		invForGameButton.interactable = false;
		RoverGameImage= GameObject.Find("Invasion Forever Button").GetComponentInChildren<Image>();
		invForGameImage= GameObject.Find("Rover Game Button").GetComponentInChildren<Image>();
		RoverGameImage.enabled = false;
		invForGameImage.enabled = false;

		Image loadingImage = GameObject.Find("Loading").GetComponent<Image>();
		loadingImage.enabled = true;

		
		
		
		if(gameId == 1){
			Application.LoadLevel("roverGame");
		}else{
			;//LAUNCH INVASION FOREVER
		}
	}

	public void ActivateGameButtons(){
		Button chooseGameButton;
		Button roverGameButton;
		Button invForGameButton;
		Image chooseGameImage;
		Image RoverGameImage;
		Image invForGameImage;

		chooseGameButton = GameObject.Find("ChooseGameButton").GetComponent<Button>();
		roverGameButton = GameObject.Find("Invasion Forever Button").GetComponent<Button>();
		invForGameButton = GameObject.Find("Rover Game Button").GetComponent<Button>();

		chooseGameButton.interactable = false;
		roverGameButton.interactable = true;
		invForGameButton.interactable = true;

		chooseGameImage = GameObject.Find("ChooseGameButton").GetComponentInChildren<Image>();
		RoverGameImage= GameObject.Find("Invasion Forever Button").GetComponentInChildren<Image>();
		invForGameImage= GameObject.Find("Rover Game Button").GetComponentInChildren<Image>();
		
		chooseGameImage.enabled = false;
		RoverGameImage.enabled = true;
		invForGameImage.enabled = true;
	}

	public void displayAboutInfo(){
		Button closeButton;
		Image aboutInfoImage;
		Image chooseGameImage;
		
		closeButton = GameObject.Find("About").GetComponent<Button>();
		aboutInfoImage = GameObject.Find("About").GetComponent<Image>();
		chooseGameImage = GameObject.Find("ChooseGameButton").GetComponentInChildren<Image>();

		closeButton.interactable = true;
		aboutInfoImage.enabled = true;
		chooseGameImage.enabled = false;

	}

	public void hideAboutInfo(){
		Button closeButton;
		Image aboutInfoImage;
		Image chooseGameImage;
		
		closeButton = GameObject.Find("About").GetComponent<Button>();
		aboutInfoImage = GameObject.Find("About").GetComponent<Image>();
		chooseGameImage = GameObject.Find("ChooseGameButton").GetComponentInChildren<Image>();
		
		closeButton.interactable = false;
		aboutInfoImage.enabled = false;
		chooseGameImage.enabled = true;
	}

	public void killGame(){
		Application.Quit();
	}

	public void restartRoverGame(){
		Button quitButton = GameObject.Find("Quit Button").GetComponent<Button>();
		Button restartButton = GameObject.Find("ResetButton").GetComponent<Button>();
		Image quitImage = GameObject.Find("Quit Button").GetComponentInChildren<Image>();
		Image restartImage = GameObject.Find("ResetButton").GetComponentInChildren<Image>();
		Image loadingImage = GameObject.Find("Loading").GetComponent<Image>();

		restartImage.enabled = false;
		quitImage.enabled = false;
		quitButton.interactable = false;
		restartButton.interactable = false;
		loadingImage.enabled = true;
		
		Application.LoadLevel("roverGame");
	}	

}
