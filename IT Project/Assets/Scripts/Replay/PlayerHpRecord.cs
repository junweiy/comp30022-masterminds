using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PlayerHpRecord : Record {
    public int id;
    public int hp;

    public void applyEffect(RecordHandler c) {
        c.SetPlayerHp(id, hp);
    }

    public PlayerHpRecord(int hp, int objId) {
        this.hp = hp;
        this.id = objId;
    }
}
