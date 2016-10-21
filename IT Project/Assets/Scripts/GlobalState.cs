using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GlobalState {
    // Singleton implementation
    private static GlobalState _instance;

    public static GlobalState Instance {
        get {
            if (_instance == null) {
                _instance = new GlobalState();
            }
            return _instance;
        }
    }

    private Character _currentChar = null;

    public Character CurrentChar {
        get { return _currentChar; }
        set {
            if (_currentChar != null) {
                Debug.LogWarning("currentChar is overritten");
            }
            _currentChar = value;
        }
    }

    private GameController _gameController = null;

    public GameController GameController {
        get { return _gameController; }
        set {
            if (_gameController != null) {
                Debug.LogWarning("gameController is overritten");
            }
            _gameController = value;
        }
    }

    private Dictionary<Character, int> _charToRoundsWon = new Dictionary<Character, int>();

    public void RecordWinner(Character c) {
        if (_charToRoundsWon.ContainsKey(c)) {
            _charToRoundsWon[c] += 1;
        } else {
            _charToRoundsWon.Add(c, 0);
        }
    }

    public Character GetFinalWinner() {
        return _charToRoundsWon.FirstOrDefault(
            x => x.Value == _charToRoundsWon.Values.Max()
        ).Key;
    }

    public void ClearWinnerRecord() {
        _charToRoundsWon.Clear();
    }

    public static bool IsCurrentChar(Character c) {
        return c == GlobalState.Instance.CurrentChar;
    }

    private int _totalNumRounds = 5;

    public int TotalNumRounds {
        get { return _totalNumRounds; }
        set { _totalNumRounds = value; }
    }

    private int _numRoundsFinished = 0;

    public bool NumRoundsIncrement() {
        _numRoundsFinished += 1;
        Debug.Log("round " + _numRoundsFinished + "/" + TotalNumRounds + " finished");
        return _numRoundsFinished >= TotalNumRounds;
    }

    public void ResetNumRoundsCounter() {
        _numRoundsFinished = 0;
    }

    private Profile _profile;

    public Profile Profile {
        get { return _profile; }
        set { _profile = value; }
    }

    public static bool LoadProfileWithUid(int userid) {
        GlobalState.Instance._profile = ProfileMessenger.GetProfileById(userid);
        return true;
    }

    public static bool LoadProfileWithEmail(string email) {
        GlobalState.Instance._profile = ProfileMessenger.GetProfileByEmail(email);
        return true;
    }

    private GameReplay _replayToSave;

    public GameReplay ReplayToSave {
        get { return _replayToSave; }
        set { _replayToSave = value; }
    }


    private GameReplay _replayToLoad;

    public GameReplay ReplayToLoad {
        get { return _replayToLoad; }
        set { _replayToLoad = value; }
    }
}