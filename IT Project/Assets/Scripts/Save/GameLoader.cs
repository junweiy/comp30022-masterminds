using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameLoader : RecordHandler {
    public void Load(GameSave save) {
        foreach(var record in save.Records) {
            record.applyEffect(this);
        }
    }

    public GameSave ReadFile() {
        string filePath = Application.dataPath + "/test.sav";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameSave)formatter.Deserialize(stream);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            var save = ReadFile();
            Load(save);
        }
    }
}
