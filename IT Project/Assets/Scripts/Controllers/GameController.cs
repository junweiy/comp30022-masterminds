using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GameController : NetworkBehaviour {

	

	private int numCharacterAlive;
	public Character[] characters { get; private set; }



	public void onCharacterDeath() {
		this.numCharacterAlive = GameObject.FindGameObjectsWithTag ("Character").Length;
		if (numCharacterAlive <= 1) {
			this.finishRound ();
		}
	}

	public void finishRound() {
		Debug.Log ("Round Finished");
		bool gameHasFinished = StateController.finishRound();
		if (gameHasFinished) {

		}
	}

	// Use this for initialization
	void Start () {
		
		GlobalState.instance.gameController = this;

		// TODO for test only, will need to read player input
		var current = new Character();
		GlobalState.instance.currentChar = current;
		Character kevin = new Character();
		kevin.player = new Profile();
		kevin.player.uid = 6;
		kevin.player.userName = "Pangpang";

		current.player = new Profile();
		current.player.userName = "Jack";
		current.player.uid = 1;

	}

	// Update is called once per frame
	void Update () {
		

	}
}