using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class ReplayPageController : MonoBehaviour {

    GameObject buttonPrefab;

    void loadReplays(string folderPath) {
        // load all files with extension rep in the replay folder
        string[] filePaths = Directory.GetFiles(folderPath, "*.rep");

        // generate each button for each replay
        foreach (var filePath in filePaths) {
            var fileName = Path.GetFileName(filePath);
            var newButton = GameObject.Instantiate(buttonPrefab);
            newButton.GetComponent<ReplayItemButtonScript>().setText(fileName);
            newButton.GetComponent<Button>().onClick.AddListener(delegate {
                openReplay(filePath);
            });
        }
    }

    void openReplay(string filePath) {
        //throw new System.NotImplementedException();
    }

    public void goToMainMenu() {
        StateController.SwitchToMainMenu();
    }

	// Use this for initialization
	void Start () {
        //loadReplays(Application.dataPath + "/Replays/";);
    }
}
