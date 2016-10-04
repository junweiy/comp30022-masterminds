using UnityEngine;
using System;
using System.Collections;

public class GameController : Photon.MonoBehaviour {
	public int playersNumber;

	public const int GAMEPLAY_SCENE_NUMBER = 1;

	public void checkIfGameEnds() {
		int numAlive = GameObject.FindGameObjectsWithTag ("Character").Length;
		if (numAlive == 1) {
			StateController.FinishRound ();
		}
	}

	Vector3 GetNextSpawnPoint(int index) {
		switch (index) {
			case 0:
				return new Vector3 (200,0,200);
			case 1:
				return new Vector3 (200,0,800);
			case 2:
				return new Vector3 (800,0,200);
			case 3:
				return new Vector3 (800,0,800);
			default:
				return new Vector3 (500,0,500);
		}
	}

	int GetIndex() {
		PhotonPlayer[] players = PhotonNetwork.playerList;
		Array.Sort (players);
		for (int i = 0; i < players.Length; i++) {
			if (PhotonNetwork.player == players[i]) {
				return i;
			}
		}
		throw new UnityException ();
	}

	void OnLevelWasLoaded(int level) {
		if (level == GAMEPLAY_SCENE_NUMBER) {
			InitialiseGamePlay ();
		}
	}

	void SpawnPlayer() {
		GameObject player = PhotonNetwork.Instantiate ("Prefabs/Character", GetNextSpawnPoint(GetIndex()),Quaternion.identity, 0);
		player.GetComponent<CharacterController> ().SetControllable();
	}

	public void InitialiseGamePlay() {
		SpawnPlayer ();
	}

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.X)) {
			Debug.Log ("X");
			InitialiseGamePlay();
		}
	}




}
