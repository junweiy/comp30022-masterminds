using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PlayerHpRecord : IRecord {
    public int Id;
    public int Hp;

    public void ApplyEffect(RecordHandler c) {
        c.SetPlayerHp(Id, Hp);
    }

    public PlayerHpRecord(int objId, int hp) {
        this.Hp = hp;
        this.Id = objId;
    }
}
