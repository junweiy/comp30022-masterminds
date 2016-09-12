using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayProfile : MonoBehaviour {

    public Profile profile;
    public GameObject details;
	
	void Update () {
        SetText("User", profile.username, 0);
        SetText("User", profile.userId.ToString(), 1);
        SetText("numGamesPlayed", profile.numGamesPlayed.ToString(),1);
        SetText("numGamesWin", profile.numGamesWon.ToString(),1);
        SetText("numGamesLost", profile.numGamesLost.ToString(),1);
        SetText("bestScore", profile.bestScore.ToString(),1);
        SetText("numKilled", profile.numPlayerKilled.ToString(),1);
        SetText("numDeath", profile.numDeath.ToString(),1);
    }

    //load content from profile, and set the related text field
    void SetText(string transName, string profileContent, int childNum)
    {
        Transform temp = details.transform.Find(transName);
        Text[] texts;
        texts = temp.GetComponentsInChildren<Text>();
        texts[childNum].text = profileContent;
    }
}
