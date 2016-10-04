using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PlayerHpRecord : ReplayRecord {
    public int playerId;
    public float hp;

    public void applyEffect(ReplaySceneController c) {
        c.SetPlayerHp(playerId, hp);
    }
}
