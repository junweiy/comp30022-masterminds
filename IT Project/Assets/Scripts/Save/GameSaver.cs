using UnityEngine;
using System.Collections;

public class GameSaver : StateRecorder {

    public void Save() {
        addRecords();
        SaveInfo info = new SaveInfo();
        GameSave save = new GameSave(pending, info);
        SaveFileMessenger.UploadSaveFile(save);
    }
}
