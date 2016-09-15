using System.Collections;
using UnityEngine;

[System.Serializable]
public class Profile {

	public int uid = 0;
	public string userName = "";
	public string email = "";

	public int numGamesPlayed = 0;
	public int numGamesWon = 0;
	public int numGamesLost = 0;
	public int bestScore = 0;
	public int numPlayerKilled = 0;
	public int numDeath = 0;


	public string toJson() {
		return JsonUtility.ToJson (this);
	}

}
