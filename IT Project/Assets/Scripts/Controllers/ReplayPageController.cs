﻿using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ReplayPageController : MonoBehaviour {
    public GameObject ButtonPrefab;
    public GameObject ScrollViewContent;

    private void LoadReplays() {
        var filePaths = ReplayIO.GetReplayFilepaths();

        // generate each button for each replay
        foreach (var filePath in filePaths) {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var newButton = GameObject.Instantiate(ButtonPrefab);
            newButton.transform.SetParent(ScrollViewContent.transform);
            newButton.transform.localScale = new Vector3(1, 1, 1);
            newButton.GetComponent<ReplayItemButtonScript>().SetText(fileName);
            newButton.GetComponent<Button>().onClick.AddListener(delegate { OpenReplay(filePath); });
        }
    }

    private void OpenReplay(string filePath) {
        GlobalState.Instance.ReplayToLoad = ReplayIO.LoadReplayFromFilepath(filePath);
        StateController.SwitchToReplayScene();
    }

    public void GoToMainMenu() {
        StateController.SwitchToMainMenu();
    }

    // Use this for initialization
    private void Start() {
        LoadReplays();
    }
}