using UnityEngine;
using System;
using System.Collections;

public class GameController : Photon.PunBehaviour {
    public const int MAINMENU_SCENE_NUMBER = 1;
    public const int GAMEPLAY_SCENE_NUMBER = 3;
    public const int RESULT_SCENE_NUMBER = 4;

    public bool LoadedFromFile;

	// Check if the game ends and handle recording accordingly
    public static bool CheckIfGameEnds() {
		GameStateRecorder gsr = GameObjectFinder.FindGameStateRecorder();
		GameObject[] players = GameObjectFinder.FindAllCharacters();
        int numAlive = 0;
        foreach (GameObject player in players) {
            if (!player.GetComponent<Character>().IsDead) {
                numAlive++;
            }
        }
        if (numAlive == 1) {
            gsr.FinishRecording();
            return true;
        } else {
            return false;
        }
    }

	// Get coordinates of spawn points for all players
    public static Vector3 GetNextSpawnPoint(int index) {
        switch (index) {
            case 0:
                return new Vector3(200, 11, 200);
            case 1:
                return new Vector3(200, 11, 800);
            case 2:
                return new Vector3(800, 11, 200);
            case 3:
                return new Vector3(800, 11, 800);
            default:
                return new Vector3(500, 11, 500);
        }
    }

	// Get the index of current player for finding spawning point
    public static int GetIndex() {
        PhotonPlayer[] players = PhotonNetwork.playerList;
        Array.Sort(players);
        for (int i = 0; i < players.Length; i++) {
            if (PhotonNetwork.player == players[i]) {
                return i;
            }
        }
        throw new UnityException();
    }
		
    private void OnLevelWasLoaded(int level) {
        if (level == GAMEPLAY_SCENE_NUMBER) {
            if (!LoadedFromFile) {
				// Initialise game normally
                InitialiseGamePlay();
            } else {
				// Master client loads from the save file and allocate instantiated
				// characters to other clients according to user name
                if (PhotonNetwork.isMasterClient) {
					GameLoader gl = GameObjectFinder.FindGameLoader ();
                    GameSave save = gl.ReadFile();
                    gl.Load(save);
                }
            }
        }
        if (level == MAINMENU_SCENE_NUMBER) {
            Destroy(this.gameObject);
        }
        if (level == RESULT_SCENE_NUMBER) {
            PhotonNetwork.Disconnect();
        }
    }

	// Spawn player in the network and set controllable
    public GameObject SpawnPlayer() {
        GameObject player = PhotonNetwork.Instantiate(
            "Prefabs/Character", GetNextSpawnPoint(GetIndex()),
            Quaternion.identity, 0);
        player.GetComponent<CharacterController>().SetControllable();
        return player;
    }

    public void InitialiseGamePlay() {
        SpawnPlayer();
    }

	// Terminate the game when there is only one player left during gameplay
    public override void OnPhotonPlayerDisconnected(PhotonPlayer player) {
        if (PhotonNetwork.playerList.Length == 1) {
            PhotonNetwork.Disconnect();
            StateController.SwitchToMainMenu();
        }
    }

	// Switch to result with delay to allow stats storage
    public IEnumerator SwitchToResultWithDelay() {
        yield return new WaitForSecondsRealtime(1);
        PhotonNetwork.LoadLevel(RESULT_SCENE_NUMBER);
    }

	// End the game on master client and other players will also 
	// be taken into result page
    public void DisplayGameOverMessage() {
        if (PhotonNetwork.isMasterClient) {
            StartCoroutine("SwitchToResultWithDelay");
        }
    }


    private void Start() {
        DontDestroyOnLoad(this.gameObject);
    }
		
}