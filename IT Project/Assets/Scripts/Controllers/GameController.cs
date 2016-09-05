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

	private int numCharacterAlive;
	private Character[] characters;
	public GameObject mainCamera;
	public GameObject characterPrefab;
	public GameObject coinNumber;
	public float spawnCentreX;
	public float spawnCentreZ;
	public float spawnCentreY;
	public float spawnDistance;

	private void setCharactersPos() {
		GameObject[] characterObjs = GameObject.FindGameObjectsWithTag("Character");
		int numCharacter = characterObjs.Length;
		Debug.Log (numCharacter);
		// need map size, ground types etc
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
			charController.initialise (c,false);
			if (GlobalState.isCurrentChar (c)) {
				//charController.coinNumber = coinNumber.gameObject.GetComponent<DisplayPlayerCoin> ();
				mainCamera.GetComponent<CameraControl> ().m_Target = characterObj.transform;
				charController.setAsMainCharacter ();
			}
		}

		numCharacterAlive = characters.Length;
		setCharactersPos();

		mainCamera.GetComponent<CameraControl> ().enabled = true;
	}
		
	public void onCharacterDeath() {
		this.numCharacterAlive -= 1;
		if (numCharacterAlive <= 1) {
			Debug.Log ("Round Finished");
			StateController.switchToResult ();
		}
	}

	// Use this for initialization
	void Start () {
		GlobalState.instance.gameController = this;

		// TODO for test only
		var current = new Character();
		GlobalState.instance.currentChar = current;
		this.initialiseScene(new Character[] {current, new Character(), new Character(), new Character(), new Character(),
			new Character(), new Character(), new Character()});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
