using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameLoader : RecordHandler {
    public void Load(GameSave save) {
        foreach (var record in save.Records) {
            record.ApplyEffect(this);
        }
    }

    public GameSave ReadFile() {
        string saveFilePath = Application.persistentDataPath + "/SaveFiles/";
        Directory.CreateDirectory(saveFilePath);
        string filePath = saveFilePath + "/test.sav";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameSave) formatter.Deserialize(stream);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            var save = ReadFile();
            Load(save);
        }
    }
}