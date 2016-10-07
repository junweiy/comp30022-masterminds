using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class ReplayPageController : MonoBehaviour {

    public GameObject ButtonPrefab;
    public GameObject ScrollViewContent;

    void loadReplays() {
        var filePaths = ReplayIO.GetReplayFilepaths();

        // generate each button for each replay
        foreach (var filePath in filePaths) {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var newButton = GameObject.Instantiate(ButtonPrefab);
            newButton.transform.SetParent(ScrollViewContent.transform);
            newButton.GetComponent<ReplayItemButtonScript>().setText(fileName);
            newButton.GetComponent<Button>().onClick.AddListener(delegate {
                openReplay(filePath);
            });
        }
    }

    void openReplay(string filePath) {
        GlobalState.instance.ReplayToLoad = ReplayIO.LoadReplayFromFilepath(filePath);
        StateController.SwitchToReplayScene();
    }

    public void goToMainMenu() {
        StateController.SwitchToMainMenu();
    }

	// Use this for initialization
	void Start () {
        loadReplays();
    }
}
