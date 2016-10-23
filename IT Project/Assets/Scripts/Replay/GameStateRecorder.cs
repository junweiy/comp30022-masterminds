using UnityEngine;
using System.Collections.Generic;

// The recorder of a game, reads the game state in every update, and generates a GameReplay file
public class GameStateRecorder : StateRecorder {
    private ReplayState _state = ReplayState.Preparing;
    private bool _started;
    private GameReplay _replay;
    private const int TARGET_FRAMERATE = 60;
    private int _frameCount = 0;

    // Use this for initialization
    private void Start() {
        Application.targetFrameRate = TARGET_FRAMERATE;
        _started = false;
        StartRecording();
    }

    // Start recording of the game
    public void StartRecording() {
        _started = true;
        _replay = new GameReplay {
            Info = new ReplayInfo(
                GetGameVersion(),
                TARGET_FRAMERATE
            ),
            Entries = new Queue<GameReplay.Entry>()
        };

        _frameCount = 0;
        _state = ReplayState.Started;
    }

    private void AddEntry(IRecord record) {
        var newEntry = new GameReplay.Entry {
            FrameTime = _frameCount,
            Record = record
        };
        _replay.Entries.Enqueue(newEntry);
    }
    
    // Finish recording of the game, makes the GameReplay ready to be saved
    public void FinishRecording() {
        if (!_started) {
            return;
        }
        _state = ReplayState.Ended;
        FlushPendingRecodsToReplayObject();
        GlobalState.Instance.ReplayToSave = _replay;
        _started = false;
    }

    // Writes the records in queue to the GameReplay object
    // Not an actual flush to disk, but could be changed to do so
    private void FlushPendingRecodsToReplayObject() {
        while (Pending.Count != 0) {
            var record = Pending.Dequeue();
            AddEntry(record);
        }
    }


    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            StartRecording();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            FinishRecording();
        }

        if (_state == ReplayState.Started) {
            AddRecords();
            FlushPendingRecodsToReplayObject();
            _frameCount += 1;
        }
    }
}