using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class TransformRecord : ReplayRecord {
    public int playerId;
    public float x;
    public float y;
    public float z;


    public void applyEffect(ReplaySceneController c) {
        c.SetPlayerPosition(playerId, new Vector3(x, y, z));
    }

    public TransformRecord(int playerId, Vector3 position) {
        this.playerId = playerId;
        this.x = position.x;
        this.y = position.y;
        this.z = position.z;
    }

}
