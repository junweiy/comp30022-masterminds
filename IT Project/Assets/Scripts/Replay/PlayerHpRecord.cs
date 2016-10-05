using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PlayerHpRecord : ReplayRecord {
    public int playerId;
    public int hp;

    public void applyEffect(ReplaySceneController c) {
        c.SetPlayerHp(playerId, hp);
    }

    public PlayerHpRecord(int hp, int playerId) {
        this.hp = hp;
        this.playerId = playerId;
    }
}
