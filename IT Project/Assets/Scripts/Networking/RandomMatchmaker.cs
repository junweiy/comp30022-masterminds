using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;

public class RandomMatchmaker : Photon.PunBehaviour {

	public const int COUNTDOWN = 10;

	private string status;
	private bool countdownStarted; 
	public float timeLeft;

	// Use this for initialization
	void Start () {
		status = "Connecting to matchmaking server";
		countdownStarted = false;
		timeLeft = COUNTDOWN;
		PhotonNetwork.ConnectUsingSettings("V0.1");

	}

	void RestartCountdown() {
		countdownStarted = false;
		timeLeft = COUNTDOWN;
	}

	void OnGUI() {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		GUI.Label (new Rect(Screen.width/3, Screen.height/2,300,20), status);
		GUI.Label (new Rect(Screen.width/2, Screen.height/3,100,20), ((int)timeLeft).ToString());
	}

	public override void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
	}

	public void SetPlayerName(string name) {
		PhotonNetwork.playerName = name;
	}

	public void CountdownFinished() {
		
	}

	void Update() {
		if (countdownStarted) {
			timeLeft -= Time.deltaTime;
		}

		if (timeLeft <= 0) {
			CountdownFinished ();
		}

	}
		
	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		PhotonNetwork.CreateRoom (null);
	}

	public override void OnJoinedRoom() {
		if (PhotonNetwork.playerList.Length == 1) {
			status = "Waiting For other players to join in";
		} else {
			countdownStarted = true;
			status = "Other players found, game will start in: ";
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer player) {
		status = "Other players found, game will start in: ";
		countdownStarted = true;
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
		if (PhotonNetwork.playerList.Length == 1) {
			status = "Player left the room, waiting For other players to join in";
			RestartCountdown ();
		}
	}

	// Called when click on back to main menu button
	public override void OnLeftRoom() {
		PhotonNetwork.Disconnect ();
		// TODO switch scene
	}

}
