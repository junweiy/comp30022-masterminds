using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

public class RandomMatchmaker : Photon.PunBehaviour {
    public const int COUNTDOWN = 10;

    private string _status;
    private bool _countdownStarted;
    public float TimeLeft { get; set; }

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

    // Check for
    private IEnumerator CheckForPlayers() {
        yield return new WaitForSeconds(2);
        if (PhotonNetwork.playerList.Length > 1) {
            _countdownStarted = true;
            _status = "Other players found, game will start in: ";
            this.photonView.RPC("ResetCountDown", PhotonTargets.All);
        }
    }

    private void RestartCountdown() {
        _countdownStarted = false;
        TimeLeft = COUNTDOWN;
    }

    //void OnGUI() {

    //	GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    //	GUI.Label (new Rect(Screen.width/3, Screen.height/2,300,20), status);
    //	if (countdownStarted) {
    //		GUI.Label (new Rect(Screen.width/2, Screen.height/3,100,20), ((int)timeLeft).ToString());
    //	}

    //}

    public override void OnJoinedLobby() {
        PhotonNetwork.JoinRandomRoom();
    }

    public void SetPlayerName(string name) {
        PhotonNetwork.playerName = name;
    }

    public void CountdownFinished() {
        GameController gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
        if (PhotonNetwork.inRoom && LoadedFromFile && !PhotonNetwork.isMasterClient) {
            _status = "Game loaded from player " + PhotonNetwork.masterClient.name + ".";
        }

        if (_countdownStarted && TimeLeft > 0) {
            TimeLeft -= Time.deltaTime;
        }

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

    public bool LoadFileExists() {
        return File.Exists(Application.persistentDataPath + "/SaveFiles/test.sav");
    }


    public override void OnJoinedRoom() {
        PhotonNetwork.playerName =
            GameObject.FindGameObjectWithTag("ProfileHandler").GetComponent<ProfileHandler>().UserName;
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

    public override void OnPhotonPlayerConnected(PhotonPlayer player) {
        if (LoadedFromFile) {
            this.photonView.RPC("SetLoadFile", PhotonTargets.All, true);
        } else {
            this.photonView.RPC("SetLoadFile", PhotonTargets.All, false);
        }

        _status = "Other players found, game will start in: ";
        _countdownStarted = true;
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        if (PhotonNetwork.playerList.Length == 1) {
            _status = "Player left the room, waiting For other players to join in";
            RestartCountdown();
        }
    }
}