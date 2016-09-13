using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfileController : MonoBehaviour {
	
	public GameObject userNameField;
	public GameObject userIdField;
	public GameObject gamePlayedField;
	public GameObject gameWonField;
	public GameObject gameLostField;
	public GameObject bestScoreField;
	public GameObject numKillField;
	public GameObject numDeathField;

	private Profile p;

	// Use this for initialization
	void Start () {
		p = ProfileMessenger.getProfileById (1);
		Debug.Log (Profile.toJson (p));
		displayInfo ();
	}

	void displayInfo() {
		userNameField.GetComponent<Text> ().text = p.userName;
		userIdField.GetComponent<Text> ().text = p.uid.ToString();
		gamePlayedField.GetComponent<Text> ().text = p.numGamesPlayed.ToString();
		gameWonField.GetComponent<Text> ().text = p.numGamesWon.ToString();
		gameLostField.GetComponent<Text> ().text = p.numGamesLost.ToString ();
		bestScoreField.GetComponent<Text> ().text = p.bestScore.ToString ();
		numKillField.GetComponent<Text> ().text = p.numPlayerKilled.ToString ();
		numDeathField.GetComponent<Text> ().text = p.numDeath.ToString ();
	}

	void Update() {
		displayInfo ();
	}
}
