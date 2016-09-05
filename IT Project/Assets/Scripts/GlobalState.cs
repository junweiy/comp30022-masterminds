using UnityEngine;
using System.Collections;

public class GlobalState {

	// Singleton implementation
	private static GlobalState _instance;
	public static GlobalState instance {
		get {
			if (_instance == null) {
				_instance = new GlobalState ();
			}
			return _instance;
		}
	}

	private Character _currentChar = null;
	public Character currentChar {
		get {
			return _currentChar;
		}
		set {
			if (_currentChar != null) {
				Debug.LogWarning ("currentChar is overriden"); // defensive
			}
			_currentChar = value;
		}
	}

	private GameController _gameController = null;
	public GameController gameController {
		get {
			return _gameController;
		}
		set {
			if (_gameController != null) {
				Debug.LogWarning ("gameController is overriden");
			}
			_gameController = value;
		}
	}

	public static bool isCurrentChar(Character c) {
		return c == GlobalState.instance.currentChar;
	}

}
