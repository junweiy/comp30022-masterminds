using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameSave {
    public List<Record> Records;
    public SaveInfo Info;

    public GameSave(IEnumerable<Record> records, SaveInfo info) {
        this.Records = new List<Record>(records);
        this.Info = info;
    }
}
