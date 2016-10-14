using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class GameStateRecorder : StateRecorder {
    private ReplayState state = ReplayState.Preparing;

    GameReplay replay;

    const int TARGET_FRAMERATE = 60;

    int frameCount = 0;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = TARGET_FRAMERATE;
	}

    void StartRecording() {
        Debug.Log("Started Recording");
        replay = new GameReplay();

        replay.info = new ReplayInfo(
            getGameVersion(),
            TARGET_FRAMERATE
        );

        replay.entries = new Queue<GameReplay.Entry>();
        frameCount = 0;
        state = ReplayState.Started;
    }

    void addEntry(Record record) {
        var newEntry = new GameReplay.Entry();
        newEntry.frameTime = frameCount;
        newEntry.record = record;
        replay.entries.Enqueue(newEntry);
    }

    void FinishRecording() {
        Debug.Log("Finished Recording");
        state = ReplayState.Ended;
        FlushPendingRecodsToReplayObject();
        GlobalState.instance.ReplayToSave = replay;
    }

    // Not an actual flush to disk, but could be changed to do so
    void FlushPendingRecodsToReplayObject() {
        while (pending.Count != 0) {
            var record = pending.Dequeue();
            addEntry(record);
        }
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            StartRecording();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            FinishRecording();
        }

        if (state == ReplayState.Started) {
            addRecords();

            if (GameController.CheckIfGameEnds()) {
                FinishRecording();
            }

            FlushPendingRecodsToReplayObject();
            frameCount += 1;
        }
    }


}
