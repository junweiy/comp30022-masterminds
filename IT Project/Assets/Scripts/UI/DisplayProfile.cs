using UnityEngine;
using UnityEngine.UI;

public class DisplayProfile : MonoBehaviour {
    public Profile Profile;
    public GameObject Details;

    private void Update() {
        SetText("User", Profile.userName, 0);
        SetText("User", Profile.uid.ToString(), 1);
        SetText("numGamesPlayed", Profile.numGamesPlayed.ToString(), 1);
        SetText("numGamesWin", Profile.numGamesWon.ToString(), 1);
        SetText("numGamesLost", Profile.numGamesLost.ToString(), 1);
        SetText("bestScore", Profile.bestScore.ToString(), 1);
        SetText("numKilled", Profile.numPlayerKilled.ToString(), 1);
        SetText("numDeath", Profile.numDeath.ToString(), 1);
    }

    //load content from profile, and set the related text field
    private void SetText(string transName, string profileContent, int childNum) {
        Transform temp = Details.transform.Find(transName);
        Text[] texts;
        texts = temp.GetComponentsInChildren<Text>();
        texts[childNum].text = profileContent;
    }
}