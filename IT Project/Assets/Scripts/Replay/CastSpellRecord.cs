using UnityEngine;
using System.Collections;
using System;
using Replay;

[System.Serializable]
public class CastSpellRecord : ReplayRecord {
    public int playerId;
    public SpellType spellType;

    public void applyEffect(ReplaySceneController c) {
        c.SetSpellCast(playerId, spellType);
    }

    public CastSpellRecord(int playerId, SpellType spellType) {
        this.playerId = playerId;
        this.spellType = spellType;
    }
}
