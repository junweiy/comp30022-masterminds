using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public static class StateController {

	public static void switchBackToGamePlay() {
		SceneManager.LoadScene ("scenes/gameplay");
	}

	public static void switchToResult() {
		SceneManager.LoadScene ("scenes/result");
	}

	public static void switchToShop() {
		SceneManager.LoadScene ("scenes/shop");
	}
		
	public static void switchToNewRound() { 
		SceneManager.LoadScene ("scenes/gameplay");
		GlobalState.instance.gameController.prepareForNewRound ();
	}
		
}
