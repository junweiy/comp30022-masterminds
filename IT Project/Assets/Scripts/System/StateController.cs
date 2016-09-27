using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class StateController {

	public static bool finishRound() {
		bool gameHasFinished = GlobalState.instance.numRoundsIncrement ();
		if (gameHasFinished) {
			SwitchToResult ();
		} else {
			SwitchToShop ();
		}
		return gameHasFinished;
	}

	public static void SwitchBackToGamePlay() {
		SceneManager.LoadScene ("scenes/gameplay");
	}

	public static void SwitchToResult() {
		SceneManager.LoadScene ("scenes/result");
	}

	public static void SwitchToShop() {
		SceneManager.LoadScene ("scenes/shop");
	}

	public static void SwitchToRoom() {
		SceneManager.LoadScene ("scenes/room");
	}
		
	public static void SwitchToNewRound() { 
		SceneManager.LoadScene ("scenes/gameplay");
		GlobalState.instance.gameController.prepareForNextRound ();
	}

	public static void SwitchToProfile() {
		SceneManager.LoadScene ("scenes/profile");
	}

	public static void SwitchToMainMenu() {
		SceneManager.LoadScene ("scenes/mainmenu");
	}
		
}
