using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class GameStateRecorder : StateRecorder {
    private ReplayState _state = ReplayState.Preparing;

    private bool _started;

    private GameReplay _replay;

    private const int TARGET_FRAMERATE = 60;

    private int _frameCount = 0;

	// Use this for initialization
    private void Start () {
        Application.targetFrameRate = TARGET_FRAMERATE;
		_started = false;
		StartRecording ();
	}

    private void StartRecording() {
        Debug.Log("Started Recording");
		_started = true;
        _replay = new GameReplay
        {
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
        var newEntry = new GameReplay.Entry();
        newEntry.FrameTime = _frameCount;
        newEntry.Record = record;
        _replay.Entries.Enqueue(newEntry);
    }

    public void FinishRecording() {
		if (!_started) {
			return;
		}
        Debug.Log("Finished Recording");
        _state = ReplayState.Ended;
        FlushPendingRecodsToReplayObject();
        GlobalState.Instance.ReplayToSave = _replay;
		_started = false;
    }

    // Not an actual flush to disk, but could be changed to do so
    private void FlushPendingRecodsToReplayObject() {
        while (Pending.Count != 0) {
            var record = Pending.Dequeue();
            AddEntry(record);
        }
    }


    // Update is called once per frame
    private void Update() {
		GameObject mainChar = GameObject.FindGameObjectWithTag ("Character");
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
