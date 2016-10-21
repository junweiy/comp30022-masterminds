using UnityEngine;
using System.Collections;
using Replay;

[System.Serializable]
public class ReplayInfo {
    public string GameVersion;
    public int TargetFrameRate;


    public ReplayInfo(string gameVersion, int targetFrameRate) {
        this.GameVersion = gameVersion;
        this.TargetFrameRate = targetFrameRate;
    }
}