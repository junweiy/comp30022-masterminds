﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
				Debug.LogWarning ("currentChar is overriden");
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

	private Dictionary<Character, int> charToRoundsWon = new Dictionary<Character,int>();

	public void recordWinner(Character c) {
		if (charToRoundsWon.ContainsKey (c)) {
			charToRoundsWon [c] += 1;
		} else {
			charToRoundsWon.Add (c, 0);
		}
	}

	public Character getFinalWinner() {
		return charToRoundsWon.FirstOrDefault (
			x => x.Value == charToRoundsWon.Values.Max ()
		).Key;
	}

	public void clearWinnerRecord() {
		charToRoundsWon.Clear ();
	}

	public static bool isCurrentChar(Character c) {
		return c == GlobalState.instance.currentChar;
	}

	private int _totalNumRounds = 5;
	public int totalNumRounds {
		get {
			return _totalNumRounds;
		} set {
			_totalNumRounds = value;
		}
	}

	private int numRoundsFinished = 0;

	public bool numRoundsIncrement() {
		numRoundsFinished += 1;
		Debug.Log ("round " + numRoundsFinished + "/" + totalNumRounds + " finished");
		return numRoundsFinished >= totalNumRounds;
	}

	public void resetNumRoundsCounter() {
		numRoundsFinished = 0;
	}

	private Profile _profile;
	public Profile profile {
		get {
			return _profile;
		} set {
			_profile = value;
		}
	}

	public static void loadProfileWithUid(int userid) {
		GlobalState.instance._profile = ProfileMessenger.getProfileById (userid);
	}


}
