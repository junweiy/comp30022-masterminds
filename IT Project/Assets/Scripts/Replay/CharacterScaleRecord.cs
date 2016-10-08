using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class CharacterScaleRecord : ReplayRecord {
    public int playerId;
    public float x;
    public float y;
    public float z;


    public void applyEffect(ReplaySceneController c) {
        c.SetPlayerScale(playerId, new Vector3(x, y, z));
    }

    public CharacterScaleRecord(int playerId, Vector3 scale) {
        this.playerId = playerId;
        this.x = scale.x;
        this.y = scale.y;
        this.z = scale.z;
    }

}
