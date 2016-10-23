using System.Collections.Generic;

// Class representing a saved game
[System.Serializable]
public class GameSave {
    public List<IRecord> Records;
    public SaveInfo Info;

    public GameSave(IEnumerable<IRecord> records, SaveInfo info) {
        this.Records = new List<IRecord>(records);
        this.Info = info;
    }
}