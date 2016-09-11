using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject mainCamera;
	public GameObject characterPrefab;
	public GameObject coinNumber;
	public float spawnCentreX;
	public float spawnCentreZ;
	public float spawnCentreY;
	public float spawnDistance;

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

	private int numCharacterAlive;
	private Character[] characters;

	private void setCharactersPos() {
		GameObject[] characterObjs = GameObject.FindGameObjectsWithTag("Character");
		int numCharacter = characterObjs.Length;
		// need map size, ground types etc for more adaptive algorithm
		for (int i = 0; i < numCharacter; i++) {
			float xOffset = positions [i].x;
			float zOffset = positions [i].y;
			characterObjs [i].SetActive (false);
			characterObjs [i].transform.position = new Vector3 (
				spawnCentreX + xOffset * spawnDistance,
				spawnCentreY,
				spawnCentreZ + zOffset * spawnDistance);
			characterObjs [i].SetActive (true);
		}
	}

	public void initialiseScene(Character[] characters) {
		this.characters = characters;
		prepareForNextRound ();
	}

	public void prepareForNextRound () {
		foreach (Character c in characters) {
			var characterObj = Instantiate<GameObject> (characterPrefab);
			var charController = characterObj.GetComponent<CharacterController> ();
			var spellController = characterObj.GetComponent<SpellController> ();
			charController.initialise (c);
			spellController.initialise (c);
			if (GlobalState.isCurrentChar (c)) {
				mainCamera.GetComponent<CameraControl> ().m_Target = characterObj.transform;
				charController.setAsMainCharacter ();
				spellController.setAsMainCharacter ();
			}
		}

		numCharacterAlive = characters.Length;
		setCharactersPos();

		mainCamera.GetComponent<CameraControl> ().enabled = true;
	}
		
	public void onCharacterDeath() {
		this.numCharacterAlive -= 1;
		if (numCharacterAlive <= 1) {
			this.finishRound ();
		}
	}

	public void finishRound() {
		Debug.Log ("Round Finished");
		Character winner = this.characters [0];
		// TODO Players having same score not proply handled here
		foreach (Character c in this.characters) {
			if (c.score > winner.score) {
				winner = c;
			}
		}

		bool gameHasFinished = StateController.finishRound();
	}

	// Use this for initialization
	void Start () {
		GlobalState.instance.gameController = this;

		// TODO for test only, will need to read player input
		var current = new Character();
		GlobalState.instance.currentChar = current;
		this.initialiseScene(new Character[] {current, new Character(), new Character(), new Character(), new Character(),
			new Character(), new Character(), new Character()});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
