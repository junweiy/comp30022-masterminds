using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// naive way of determining relative spawn positions
	private static Vector2[] positions = {
		new Vector2(1f, 0f),
		new Vector2(-1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(0f, -1f),
		new Vector2(0.707f, 0.707f),
		new Vector2(-0.707f, -0.707f),
		new Vector2(-0.707f, 0.707f),
		new Vector2(0.707f, -0.707f),
	};

	private int numCharacter;
	public float spawnCentreX;
	public float spawnCentreZ;
	public float spawnCentreY;
	public float spawnDistanceFactor;

	private void setCharactersPos() {
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
		int numCharacter = characters.Length;
		// need map size, ground types etc
		for (int i = 0; i < numCharacter; i++) {
			float xOffset = positions [i].x;
			float zOffset = positions [i].y;
			characters [i].transform.position = new Vector3 (spawnCentreX + xOffset * spawnDistanceFactor,
				spawnCentreY,
				spawnCentreZ + zOffset * spawnDistanceFactor);
		}
	}

	public void prepareForNewRound () {
		setCharactersPos();
	}
		
	public void onCharacterDeath() {
		this.numCharacter -= 1;
		if (numCharacter <= 1) {
			Debug.Log ("Round Finished");
			StateController.switchToResult ();
		}
	}

	// Use this for initialization
	void Start () {
		GlobalState.instance.gameController = this;
		numCharacter = GameObject.FindGameObjectsWithTag("Character").Length;

		prepareForNewRound ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
