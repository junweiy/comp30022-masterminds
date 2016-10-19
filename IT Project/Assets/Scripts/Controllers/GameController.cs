﻿using UnityEngine;
using System;
using System.Collections;

public class GameController : Photon.PunBehaviour {
	public const int MAINMENU_SCENE_NUMBER = 0;
	public const int GAMEPLAY_SCENE_NUMBER = 2;
	public const int RESULT_SCENE_NUMBER = 3;

	public bool loadedFromFile;

	public static bool CheckIfGameEnds() {
		GameObject recorder = GameObject.FindGameObjectWithTag ("Recorder");
		GameStateRecorder gsr = recorder.GetComponent<GameStateRecorder> ();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		int numAlive = 0;
		foreach (GameObject player in players) {
			if (!player.GetComponent<Character>().isDead) {
				numAlive++;
			}
		}
		if (numAlive == 1) {
			gsr.FinishRecording ();
			return true;
		} else {
			return false;
		}
	}

	public static Vector3 GetNextSpawnPoint(int index) {
		switch (index) {
			case 0:
				return new Vector3 (200,11,200);
			case 1:
				return new Vector3 (200,11,800);
			case 2:
				return new Vector3 (800,11,200);
			case 3:
				return new Vector3 (800,11,800);
			default:
				return new Vector3 (500,11,500);
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
			if (!loadedFromFile) {
				InitialiseGamePlay ();
			} else {
				if (PhotonNetwork.isMasterClient) {
					GameLoader gl = GameObject.FindGameObjectWithTag ("Loader").GetComponent<GameLoader> ();
					GameSave save = gl.ReadFile ();
					gl.Load (save);
				}
			}
		}
		if (level == MAINMENU_SCENE_NUMBER) {
			Destroy (this.gameObject);
		}
		if (level == RESULT_SCENE_NUMBER) {
			PhotonNetwork.Disconnect ();
		}
	}

	public GameObject SpawnPlayer() {
		GameObject player = PhotonNetwork.Instantiate ("Prefabs/Character", GetNextSpawnPoint(GetIndex()), Quaternion.identity, 0);
		player.GetComponent<CharacterController> ().SetControllable();
		return player;
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
		if (PhotonNetwork.isMasterClient) {
			StartCoroutine ("SwitchToResultWithDelay");
		}
	}
		
		
	void Start() {
		DontDestroyOnLoad (this.gameObject);

	}


	public static Character FindMainCharacter() {
		GameObject mainPlayer = VoiceButtonController.FindMainPlayer ();
		if (mainPlayer != null) {
			return mainPlayer.GetComponent<Character> ();
		}
		return null;
	}


}
