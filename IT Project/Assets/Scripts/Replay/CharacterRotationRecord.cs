using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class CharacterRotationRecord : ReplayRecord {
    public int playerId;
    public float x;
    public float y;
    public float z;
    public float w;


    public void applyEffect(ReplaySceneController c) {
        c.SetPlayerRotation(playerId, new Quaternion(x, y, z, w));
    }

    public CharacterRotationRecord(int playerId, Quaternion rotation) {
        this.playerId = playerId;
        this.x = rotation.x;
        this.y = rotation.y;
        this.z = rotation.z;
        this.w = rotation.w;
    }

}
