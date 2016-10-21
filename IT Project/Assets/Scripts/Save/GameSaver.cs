using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaver : StateRecorder {
    public void Save() {
        AddRecords();
        SaveInfo info = new SaveInfo();
        GameSave save = new GameSave(Pending, info);
        //SaveFileMessenger.UploadSaveFile(save);
        CreateFile(save);
    }

    public void CreateFile(GameSave save) {
        string saveFilePath = Application.persistentDataPath + "/SaveFiles/";
        if (!Directory.Exists(saveFilePath)) {
            Directory.CreateDirectory(saveFilePath);
        }
        string filePath = saveFilePath + "/test.sav";
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, save);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            Save();
        }
    }
}