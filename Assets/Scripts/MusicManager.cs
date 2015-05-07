using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	AudioSource[] playList;
	float timeIncrement;
	float lastStartTime;
	float fadeTime;
	AudioSource current;
	bool startFadeIn = false;
	// Use this for initialization
	void Start () {
		timeIncrement = 300.0f;
		lastStartTime = Time.time;
		fadeTime = 20.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time-lastStartTime >= timeIncrement){
			lastStartTime = Time.time;
			playTrack(1);
			startFadeIn = true;
		}
		/*if(Time.time-lastStartTime <= fadeTime && startFadeIn){
			current.volume = current.volume + .04f;
			if(Time.time == fadeTime){ startFadeIn = false;}
		}*/
	}

	private void playTrack(int n){
		playList = GameObject.Find ("PlayListWrapper").GetComponents<AudioSource>();
		current = playList[n];	
		current.Play();
		//current.volume = 0.0f;
	}
}
