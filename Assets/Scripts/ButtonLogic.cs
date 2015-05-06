﻿using UnityEngine;
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

	public void killGame(){
		Application.Quit();
	}	

}
