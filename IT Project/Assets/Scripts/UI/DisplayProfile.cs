using UnityEngine;
using UnityEngine.UI;

public class DisplayProfile : MonoBehaviour {
    public Profile Profile;
    public GameObject Details;

    private void Update() {
        SetText("User", Profile.UserName, 0);
        SetText("User", Profile.Uid.ToString(), 1);
        SetText("numGamesPlayed", Profile.NumGamesPlayed.ToString(), 1);
        SetText("numGamesWin", Profile.NumGamesWon.ToString(), 1);
        SetText("numGamesLost", Profile.NumGamesLost.ToString(), 1);
        SetText("bestScore", Profile.BestScore.ToString(), 1);
        SetText("numKilled", Profile.NumPlayerKilled.ToString(), 1);
        SetText("numDeath", Profile.NumDeath.ToString(), 1);
    }

    //load content from profile, and set the related text field
    private void SetText(string transName, string profileContent, int childNum) {
        Transform temp = Details.transform.Find(transName);
        Text[] texts;
        texts = temp.GetComponentsInChildren<Text>();
        texts[childNum].text = profileContent;
    }
}