using UnityEngine;
using System.Collections;
using Replay;

[System.Serializable]
public class ReplayInfo {
    public string gameVersion;
    public int numCharacters;
    public int targetFrameRate;


    public ReplayInfo(string gameVersion, int numCharacters, int targetFrameRate) {
        this.gameVersion = gameVersion;
        this.numCharacters = numCharacters;
        this.targetFrameRate = targetFrameRate;
    }

}
