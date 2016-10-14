using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class StateController {

	public static void SwitchToGamePlay() {
		SceneManager.LoadScene ("scenes/gameplay");
	}

	public static void SwitchToResult() {
		SceneManager.LoadScene ("scenes/result");
	}
		
	public static void switchToShop() {
		SceneManager.LoadScene ("scenes/shop");
	}

	public static void switchToRoom() {
		SceneManager.LoadScene ("scenes/room");
	}

	public static void switchToProfile() {
		SceneManager.LoadScene ("scenes/Profile");
	}

	public static void SwitchToMainMenu() {
		SceneManager.LoadScene ("scenes/MainMenu");
	}

	public static void SwitchToMatching() {
		SceneManager.LoadScene ("scenes/Matching");
	}

    public static void SwitchToReplayScene() {
        SceneManager.LoadScene("scenes/replaySelection");
    }
		
}
