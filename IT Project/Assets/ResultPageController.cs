using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResultPageController : MonoBehaviour {
	private bool isWinner;
	Character[] chars;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void returnToRoom() {
		StateController.switchToRoom ();
	}

	public void loadData() {
		Character current = GlobalState.instance.currentChar;
		this.chars = GlobalState.instance.gameController.characters;
		int maxScore = chars.Max (c => c.score);
		Character[] winners = chars.Where (c => c.score == maxScore);
		this.isWinner = winners.Contains (current);
	}

	public void saveReplay() {
		// TODO
		Debug.LogWarning ("Replay function not implemented yet");
	}

}
