using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour {
    public GameObject UserNameField;
    public GameObject UserIdField;
    public GameObject GamePlayedField;
    public GameObject GameWonField;
    public GameObject GameLostField;
    public GameObject BestScoreField;
    public GameObject NumKillField;
    public GameObject NumDeathField;

    private Profile _p;

    // Use this for initialization
    private void Start() {
        _p = GlobalState.Instance.Profile;
        DisplayInfo();
    }

    private void DisplayInfo() {
        UserNameField.GetComponent<Text>().text = _p.UserName;
        UserIdField.GetComponent<Text>().text = _p.Uid.ToString();
        GamePlayedField.GetComponent<Text>().text = _p.NumGamesPlayed.ToString();
        GameWonField.GetComponent<Text>().text = _p.NumGamesWon.ToString();
        GameLostField.GetComponent<Text>().text = _p.NumGamesLost.ToString();
        BestScoreField.GetComponent<Text>().text = _p.BestScore.ToString();
        NumKillField.GetComponent<Text>().text = _p.NumPlayerKilled.ToString();
        NumDeathField.GetComponent<Text>().text = _p.NumDeath.ToString();
    }

    private void Update() {
        // TODO investigate on why this must be in update()
        DisplayInfo();
    }

    public void ClosePage() {
        StateController.SwitchToMainMenu();
    }
}