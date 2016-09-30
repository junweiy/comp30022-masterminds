using UnityEngine;
using System.Collections;

public class ReplayController : MonoBehaviour {

    // TODO
    private const string PATH = "";

    void loadReplays(string folderPath) {
        throw new System.NotImplementedException();
    }

    void playVideo(string videoPath) {
        Handheld.PlayFullScreenMovie(videoPath);
    }

    public void goToMainMenu() {
        StateController.switchToMainMenu();
    }

	// Use this for initialization
	void Start () {
        loadReplays(PATH);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
