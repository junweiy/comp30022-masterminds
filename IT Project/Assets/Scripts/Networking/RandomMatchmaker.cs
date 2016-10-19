using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class RandomMatchmaker : Photon.PunBehaviour {

	public const int COUNTDOWN = 10;

	private string status;
	private bool countdownStarted; 
	public float timeLeft { get; set; }

	public bool loadedFromFile;

    //UI text
    public Text countDownUI;
    public Text statusUI;
    public Text connectionStatusUI;

	// Use this for initialization
	void Start () {
		PhotonNetwork.automaticallySyncScene = true;
		status = "Connecting to matchmaking server";
		countdownStarted = false;
		timeLeft = COUNTDOWN;
		PhotonNetwork.ConnectUsingSettings("V0.1");
	}

	// Check for
	IEnumerator CheckForPlayers() {
		yield return new WaitForSeconds (2);
		if (PhotonNetwork.playerList.Length > 1) {
			countdownStarted = true;
			status = "Other players found, game will start in: ";
			this.photonView.RPC ("ResetCountDown", PhotonTargets.All);
		}
	}

	void RestartCountdown() {
		countdownStarted = false;
		timeLeft = COUNTDOWN;
	}

	//void OnGUI() {
		
	//	GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	//	GUI.Label (new Rect(Screen.width/3, Screen.height/2,300,20), status);
	//	if (countdownStarted) {
	//		GUI.Label (new Rect(Screen.width/2, Screen.height/3,100,20), ((int)timeLeft).ToString());
	//	}

	//}

	public override void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
	}

	public void SetPlayerName(string name) {
		PhotonNetwork.playerName = name;
	}

	public void CountdownFinished() {
		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gc.loadedFromFile = loadedFromFile;
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.LoadLevel ("scenes/gameplay");
		}

	}

	[PunRPC]
	void SetLoadFile(bool loaded) {
		loadedFromFile = loaded;
	}

	[PunRPC]
	void ResetCountDown() {
		timeLeft = COUNTDOWN;
	}

	void Update() {

		if (countdownStarted) {
			timeLeft -= Time.deltaTime;
		}

		if (timeLeft <= 0) {
			PhotonNetwork.room.visible = false;
			CountdownFinished ();
		}

        // update UI
        countDownUI.text = ((int)timeLeft).ToString();
        statusUI.text = status.ToString();
        connectionStatusUI.text = PhotonNetwork.connectionStateDetailed.ToString();
	}
		
	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		PhotonNetwork.CreateRoom (null);
	}

	public bool LoadFileExists() {
		return File.Exists(Application.persistentDataPath + "/SaveFiles/test.sav");
	}
	

	public override void OnJoinedRoom() {
		PhotonNetwork.playerName = GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().userName;
		if (PhotonNetwork.playerList.Length == 1) {
			if (loadedFromFile && LoadFileExists()) {
				status = "Save loaded, Waiting For other players to join in.";
			} else if (loadedFromFile && !LoadFileExists()) {
				status = "Save file not found, Please return to main menu.";
				PhotonNetwork.Disconnect ();
			} else {
				status = "Waiting For other players to join in.";
			}

			StartCoroutine ("CheckForPlayers");
		} else {
			countdownStarted = true;
			status = "Other players found, game will start in: ";
			this.photonView.RPC ("ResetCountDown", PhotonTargets.All);
		}
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer player) {
		if (loadedFromFile) {
			this.photonView.RPC("SetLoadFile", PhotonTargets.All, true);
		} else {
			this.photonView.RPC("SetLoadFile", PhotonTargets.All, false);
		}

		status = "Other players found, game will start in: ";
		countdownStarted = true;
	}

	public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
		if (PhotonNetwork.playerList.Length == 1) {
			status = "Player left the room, waiting For other players to join in";
			RestartCountdown ();
		}
	}

}
