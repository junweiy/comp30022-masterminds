using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameSave {
    public List<IRecord> Records;
    public SaveInfo Info;

    public GameSave(IEnumerable<IRecord> records, SaveInfo info) {
        this.Records = new List<IRecord>(records);
        this.Info = info;
    }
}
