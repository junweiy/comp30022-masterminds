using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class StateController {

	public static bool FinishRound() {
		bool gameHasFinished = GlobalState.instance.numRoundsIncrement ();
		if (gameHasFinished) {
			switchToResult ();
		} else {
			switchToShop ();
		}
		return gameHasFinished;
	}

	public static void switchBackToGamePlay() {
		SceneManager.LoadScene ("scenes/gameplay");
	}

	public static void switchToResult() {
		SceneManager.LoadScene ("scenes/result");
	}

	public static void switchToShop() {
		SceneManager.LoadScene ("scenes/shop");
	}

	public static void switchToRoom() {
		SceneManager.LoadScene ("scenes/room");
	}
		
	public static void switchToNewRound() { 
		SceneManager.LoadScene ("scenes/gameplay");
		//GlobalState.instance.gameController.prepareForNextRound ();
	}

	public static void switchToProfile() {
		SceneManager.LoadScene ("scenes/profile");
	}

	public static void switchToMainMenu() {
		SceneManager.LoadScene ("scenes/MainMenu");
	}
		
}
