using UnityEngine;
using System.Collections;

public static class GameObjectFinder {

	public static GameSaver FindGameSaver() {
		return GameObject.FindGameObjectWithTag("Saver").GetComponent<GameSaver>();
	}

	public static GameObject[] FindAllCharacters() {
		return GameObject.FindGameObjectsWithTag ("Character");
	}

	public static GameObject[] FindAllProfileHandler() {
		return GameObject.FindGameObjectsWithTag ("ProfileHandler");
	}

	public static RandomMatchmaker FindRandomMatchMaker() {
		return GameObject.FindGameObjectWithTag("MatchMaker").GetComponent<RandomMatchmaker>();
	}

	public static ResultPageController FindResultPageController() {
		return GameObject.FindGameObjectWithTag("ResultPageController").GetComponent<ResultPageController>();
	}

	public static ProfileHandler FindProfileHandler() {
		return GameObject.FindGameObjectWithTag ("ProfileHandler").GetComponent<ProfileHandler> ();
	}

	public static GameController FindGameController() {
		return GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}

	public static GameLoader FindGameLoader() {
		return GameObject.FindGameObjectWithTag("Loader").GetComponent<GameLoader>();
	}

	public static GameObject FindJoyStick() {
		return GameObject.FindGameObjectWithTag ("JoyStick");
	}

	public static GameObject FindSpellIcon() {
		return GameObject.FindGameObjectWithTag ("SpellButton");
	}

	public static GameStateRecorder FindGameStateRecorder() {
		return GameObject.FindGameObjectWithTag ("Recorder").GetComponent<GameStateRecorder> ();
	}

	public static GroundController FindGroundController() {
		return GameObject.FindGameObjectWithTag ("Ground").GetComponent<GroundController> ();
	}

	public static GameObject FindGround() {
		return GameObject.FindGameObjectWithTag("Ground");
	}
		
	public static GameObject FindMainPlayer() {
		GameObject[] players = FindAllCharacters();
		foreach (GameObject player in players) {
			if (player.GetPhotonView().isMine) {
				return player;
			}
		}
		return null;
	}

	public static Character FindMainCharacter() {
		GameObject mainPlayer = FindMainPlayer();
		if (mainPlayer != null) {
			return mainPlayer.GetComponent<Character>();
		}
		return null;
	}


	public static GameObject FindAnotherPlayerAlive() {
		GameObject[] players = FindAllCharacters();
		foreach (GameObject player in players) {
			if (!player.GetComponent<Character>().IsDead) {
				return player;
			}
		}
		return null;
	}

	// Generic method to find controller attached to main character
	public static T GetMainPlayerController<T>() {
		GameObject mainPlayer = FindMainPlayer();
		return mainPlayer.GetComponent<T>();
	}
		
	public static GameObject FindCharacterWithUserName(string userName) {
		GameObject[] characters = GameObjectFinder.FindAllCharacters();
		foreach (GameObject character in characters) {
			if (character.GetComponent<Character>().UserName == userName) {
				return character;
			}
		}
		return null;
	}
		
}
