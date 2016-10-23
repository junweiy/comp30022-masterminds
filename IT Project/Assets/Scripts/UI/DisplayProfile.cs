using UnityEngine;
using UnityEngine.UI;

public class DisplayProfile : MonoBehaviour {
    public Profile Profile;

	void Start() {
		SetText("User", Profile.userName, 0);
		SetText("User", Profile.uid.ToString(), 1);
		SetText("NumGamesPlayed", Profile.numGamesPlayed.ToString(), 1);
		SetText("NumGamesWon", Profile.numGamesWon.ToString(), 1);
		SetText("NumGamesLost", Profile.numGamesLost.ToString(), 1);
		SetText("NumKilled", Profile.numPlayerKilled.ToString(), 1);
		SetText("NumDeath", Profile.numDeath.ToString(), 1);
	}

    // Load content from profile, and set the related text field
    private void SetText(string transName, string profileContent, int childNum) {
		Transform temp = this.gameObject.transform.FindChild(transName);
        Text[] texts;
        texts = temp.GetComponentsInChildren<Text>();
        texts[childNum].text = profileContent;
    }
}