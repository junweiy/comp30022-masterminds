using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class PutSpellRecord : ReplayRecord {
    public int playerId;
    public int spellId;

    public void applyEffect(ReplaySceneController c) {
        c.putSpell(playerId, spellId);
    }

    public PutSpellRecord(int playerId, int spellId) {
        this.playerId = playerId;
        this.spellId = spellId;
    }
}
