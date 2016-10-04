using UnityEngine;
using System;
using System.Collections;

public class GameController : Photon.PunBehaviour {
	public const int MAINMENU_SCENE_NUMBER = 0;
	public const int GAMEPLAY_SCENE_NUMBER = 2;

	public bool CheckIfGameEnds() {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		int numAlive = 0;
		foreach (GameObject player in players) {
			if (!player.GetComponent<Character>().isDead) {
				numAlive++;
			}
		}
		return numAlive == 1;
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
		if (level == MAINMENU_SCENE_NUMBER) {
			Destroy (this.gameObject);
		}
	}

	void SpawnPlayer() {
		GameObject player = PhotonNetwork.Instantiate ("Prefabs/Character", GetNextSpawnPoint(GetIndex()), Quaternion.identity, 0);
		player.GetComponent<CharacterController> ().SetControllable();
	}

	public void InitialiseGamePlay() {
		SpawnPlayer ();
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
		if (CheckIfGameEnds()) {
			PhotonNetwork.Disconnect ();
			StateController.SwitchToResult ();
		}
	}

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}






}
