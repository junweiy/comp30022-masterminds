using UnityEngine;
using System.Collections;

public class ProfileHandler : Photon.MonoBehaviour {

	public const int MAINMENU_SCENE_NUMBER = 1;
	public const int MATCHING_SCENE_NUMBER = 2;
	public const int RESULT_SCENE_NUMBER = 4;

	public bool loadedFromFile;
	public bool isLogedIn;
	public string userName;
	public int kill;
	public int death;
	public bool won;
	public bool alreadyUpdated;


	// Use this for initialization
	void Start () {
		loadedFromFile = false;
		isLogedIn = false;
		alreadyUpdated = true;
		DontDestroyOnLoad (this.gameObject);
		if (GameObject.FindGameObjectsWithTag ("ProfileHandler").Length == 2) {
			Destroy (this.gameObject);
		}
	}
		
	public void UpdateProfile(int kill, int death, bool won) {
		this.kill = kill;
		this.death = death;
		this.won = won;
		this.alreadyUpdated = false;
		Debug.Log ("Updated with " + kill + " " + death);
	}

	public void ResetStats() {
		this.alreadyUpdated = true;
		loadedFromFile = false;
	}

	public void LoggedIn(string userName) {
		this.isLogedIn = true;
		this.userName = userName;
	}

	void OnLevelWasLoaded(int level) {

		if (level == MAINMENU_SCENE_NUMBER) {
			if (isLogedIn) {
				GameObject.Find("Canvas").transform.FindChild ("Login").gameObject.SetActive (false);
				GameObject.Find("Canvas").transform.FindChild ("MainMenu").gameObject.SetActive (true);
			}
			if (isLogedIn && !alreadyUpdated) {
				Profile oldProfile = ProfileMessenger.GetProfileByEmail(userName);
				oldProfile.numGamesPlayed += 1;
				oldProfile.numDeath += death;
				oldProfile.numPlayerKilled += kill;
				if (won) {
					oldProfile.numGamesWon++;
				} else {
					oldProfile.numGamesLost++;
				}
				ResetStats ();
				ProfileMessenger.submitNewProfile (oldProfile);
			}
		}

		if (level == MATCHING_SCENE_NUMBER) {
			RandomMatchmaker rm = GameObject.FindGameObjectWithTag ("MatchMaker").GetComponent<RandomMatchmaker> ();
			rm.loadedFromFile = loadedFromFile;
		}

		if (level == RESULT_SCENE_NUMBER) {
			ResultPageController rpc = GameObject.FindGameObjectWithTag ("ResultPageController").GetComponent<ResultPageController> ();
			rpc.isWinner = won;
			rpc.userName = userName;
			rpc.kill = kill;
			rpc.death = death;
		}
	}


}
