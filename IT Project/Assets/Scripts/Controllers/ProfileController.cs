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
		p = GlobalState.instance.profile;
		DisplayInfo ();
	}

	void DisplayInfo() {
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
		// TODO investigate on why this must be in update()
		DisplayInfo ();
	}

	public void ClosePage() {
		StateController.switchToMainMenu();
	}
}
