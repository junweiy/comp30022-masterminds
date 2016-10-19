using UnityEngine;
using System.Collections;
using Replay;

[System.Serializable]
public class ReplayInfo {
    public string gameVersion;
    public int targetFrameRate;


    public ReplayInfo(string gameVersion, int targetFrameRate) {
        this.gameVersion = gameVersion;
        this.targetFrameRate = targetFrameRate;
    }

}
