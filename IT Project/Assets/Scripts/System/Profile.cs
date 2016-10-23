using UnityEngine;

// Class for a player's profile, containing basic information and stats
[System.Serializable]
public class Profile {
    public int Uid = 0;
    public string UserName = "";
    public string Email = "";

    public int NumGamesPlayed = 0;
    public int NumGamesWon = 0;
    public int NumGamesLost = 0;
    public int BestScore = 0;
    public int NumPlayerKilled = 0;
    public int NumDeath = 0;
}