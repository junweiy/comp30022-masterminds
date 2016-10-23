// Class containing metadata of a game replay
[System.Serializable]
public class ReplayInfo {
    public string GameVersion;
    public int TargetFrameRate;


    public ReplayInfo(string gameVersion, int targetFrameRate) {
        this.GameVersion = gameVersion;
        this.TargetFrameRate = targetFrameRate;
    }
}