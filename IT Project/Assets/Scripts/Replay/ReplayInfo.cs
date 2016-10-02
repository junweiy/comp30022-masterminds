using UnityEngine;
using System.Collections;

[System.Serializable]
public class ReplayInfo {
    public string gameVersion;
    public int numCharacters;
    //public GameObject[] characters;
    //public GameObject[] spells;
    public int targetFrameRate;


    public ReplayInfo(string gameVersion, int numCharacters, int targetFrameRate) {
        this.gameVersion = gameVersion;
        this.numCharacters = numCharacters;
        this.targetFrameRate = targetFrameRate;
    }
    //public ReplayInfo(string gameVersion, GameObject[] characters, GameObject[] spells, int targetFrameRate) {
    //    this.gameVersion = gameVersion;
    //    this.characters = characters;
    //    this.spells = spells;
    //    this.targetFrameRate = targetFrameRate;
    //}
}
