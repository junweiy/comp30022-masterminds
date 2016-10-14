using UnityEngine;
using System.Collections;

public class GameLoader : RecordHandler {
    public void Load(GameSave save) {
        foreach(var record in save.Records) {
            record.applyEffect(this);
        }
    }
}
