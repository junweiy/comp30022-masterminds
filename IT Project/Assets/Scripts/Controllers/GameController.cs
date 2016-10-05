﻿using UnityEngine;
using System;
using System.Collections;

public class GameController : Photon.PunBehaviour {
	public const int MAINMENU_SCENE_NUMBER = 0;
	public const int GAMEPLAY_SCENE_NUMBER = 2;
	public const int RESULT_SCENE_NUMBER = 3;

	public static bool CheckIfGameEnds() {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		int numAlive = 0;
		foreach (GameObject player in players) {
			if (!player.GetComponent<Character>().isDead) {
				numAlive++;
			}
		}
		return numAlive == 1;
	}

	public static Vector3 GetNextSpawnPoint(int index) {
		switch (index) {
			case 0:
				return new Vector3 (200,5,200);
			case 1:
				return new Vector3 (200,5,800);
			case 2:
				return new Vector3 (800,5,200);
			case 3:
				return new Vector3 (800,5,800);
			default:
				return new Vector3 (500,5,500);
		}
	}

	public static int GetIndex() {
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
		if (level == RESULT_SCENE_NUMBER) {
			PhotonNetwork.Disconnect ();
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
		if (PhotonNetwork.playerList.Length == 1) {
			PhotonNetwork.Disconnect ();
			StateController.SwitchToMainMenu ();
		}
	}

	public IEnumerator SwitchToResultWithDelay() {
		yield return new WaitForSecondsRealtime (1);
		PhotonNetwork.LoadLevel (RESULT_SCENE_NUMBER);
	}
		

	public void DisplayGameOverMessage() {
		// TODO display
		if (PhotonNetwork.isMasterClient) {
			StartCoroutine ("SwitchToResultWithDelay");
		}
	}
		

	void Start() {
		DontDestroyOnLoad (this.gameObject);
	}






}
