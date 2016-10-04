using UnityEngine;
using System.Collections;

public class ProfileHandler : MonoBehaviour {

	public const int MAINMENU_SCENE_NUMBER = 0;

	public bool isLogedIn;
	public string userName;
	public int kill;
	public int death;
	public bool won;
	public bool alreadyUpdated;


	// Use this for initialization
	void Start () {
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
			// TODO
			// UploadGameResultToCloud();
		}
	}
}
