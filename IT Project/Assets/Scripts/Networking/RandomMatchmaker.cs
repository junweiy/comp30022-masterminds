using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;

public class RandomMatchmaker : Photon.PunBehaviour {
    public const int COUNTDOWN = 10;
	// String to display on screen of current status
    private string _status;
	// Whether countdown has started
    private bool _countdownStarted;
	// Time left before game starts
    public float TimeLeft { get; set; }
	// Whether save file needs to be loaded
    public bool LoadedFromFile;

    //UI text
    public Text CountDownUi;
    public Text StatusUi;
    public Text ConnectionStatusUi;

    // Use this for initialization
    private void Start() {
        PhotonNetwork.automaticallySyncScene = true;
        _status = "Connecting to matchmaking server";
        _countdownStarted = false;
        TimeLeft = COUNTDOWN;
        PhotonNetwork.ConnectUsingSettings("V0.1");
    }

    // Keep checking for players joining
    private IEnumerator CheckForPlayers() {
        yield return new WaitForSeconds(2);
        if (PhotonNetwork.playerList.Length > 1) {
            _countdownStarted = true;
            _status = "Other players found, game will start in: ";
            this.photonView.RPC("ResetCountDown", PhotonTargets.All);
        }
    }

	// Reset the countdown
    private void RestartCountdown() {
        _countdownStarted = false;
        TimeLeft = COUNTDOWN;
    }

    public override void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    public void SetPlayerName(string name) {
        PhotonNetwork.playerName = name;
    }

	// Switch to game play after countdown finished
    public void CountdownFinished() {
		GameController gc = GameObjectFinder.FindGameController();
        gc.LoadedFromFile = LoadedFromFile;
        if (PhotonNetwork.isMasterClient) {
            PhotonNetwork.LoadLevel("scenes/gameplay");
        }
    }

    [PunRPC]
    private void SetLoadFile(bool loaded) {
        LoadedFromFile = loaded;
    }

    [PunRPC]
    private void ResetCountDown() {
        TimeLeft = COUNTDOWN;
    }

    private void Update() {
		// Update status to reflect the change that save file has been loaded 
		// under certain condition
        if (PhotonNetwork.inRoom && LoadedFromFile && !PhotonNetwork.isMasterClient) {
            _status = "Game loaded from player " + PhotonNetwork.masterClient.name + ".";
        }

		// Update countdown time
        if (_countdownStarted && TimeLeft > 0) {
            TimeLeft -= Time.deltaTime;
        }

		// Initialise gameplay when countdown finished
        if (TimeLeft <= 0) {
            PhotonNetwork.room.visible = false;
            CountdownFinished();
        }

        // update UI
        if (_countdownStarted) {
            CountDownUi.text = ((int) TimeLeft).ToString();
        } else {
            CountDownUi.text = "";
        }

        StatusUi.text = _status.ToString();
        ConnectionStatusUi.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
        PhotonNetwork.CreateRoom(null);
    }

	// Check if load file exists
    public bool LoadFileExists() {
        return File.Exists(Application.persistentDataPath + "/SaveFiles/test.sav");
    }

	// Handle different conditions when the player joined quick match
    public override void OnJoinedRoom() {
        PhotonNetwork.playerName =
			GameObjectFinder.FindProfileHandler().UserName;
        if (PhotonNetwork.playerList.Length == 1) {
            if (LoadedFromFile && LoadFileExists()) {
                _status = "Save loaded, Waiting For other players to join in.";
            } else if (LoadedFromFile && !LoadFileExists()) {
                _status = "Save file not found, Please return to main menu.";
                PhotonNetwork.Disconnect();
            } else {
                _status = "Waiting For other players to join in.";
            }

            StartCoroutine("CheckForPlayers");
        } else {
            _countdownStarted = true;
            _status = "Other players found, game will start in: ";
            this.photonView.RPC("ResetCountDown", PhotonTargets.All);
        }
    }

	// Handle different conditions when other players join the quick match
    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
        if (LoadedFromFile) {
            this.photonView.RPC("SetLoadFile", PhotonTargets.All, true);
        } else {
            this.photonView.RPC("SetLoadFile", PhotonTargets.All, false);
        }

        _status = "Other players found, game will start in: ";
        _countdownStarted = true;
    }

	// Restart countdown when there is only one player left
    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        if (PhotonNetwork.playerList.Length == 1) {
            _status = "Player left the room, waiting For other players to join in";
            RestartCountdown();
        }
    }
}