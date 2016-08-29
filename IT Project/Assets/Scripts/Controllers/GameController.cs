using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int numPlayer;

	private static void setCharactersPos() {
		// TODO
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
		// need map size, ground types etc
	}

	public static void prepareForNewRound () {
		// TODO
		setCharactersPos();
	}

	static public void onCharacterDeath() {
		if (GameObject.FindGameObjectsWithTag ("Character").Length <= 1) {
			StateController.switchToResult ();
		}
	}

	// Use this for initialization
	void Start () {
		prepareForNewRound ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
