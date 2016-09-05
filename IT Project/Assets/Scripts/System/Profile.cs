using System.Collections;
using UnityEngine;

[System.Serializable]
public class Profile {

	public int numGamesPlayed = 0;
	public int numGamesWon = 0;
	public int numGamesLost = 0;

	public static string toJson(Profile profile) {
		return JsonUtility.ToJson (profile);
	}

}
