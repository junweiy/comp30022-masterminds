﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class GameStateRecorder : MonoBehaviour {
    private ReplayState state = ReplayState.Preparing;

    HashSet<GameObject> recordedChars = new HashSet<GameObject>();
    Dictionary<int, Vector3> lastPos = new Dictionary<int, Vector3>();
    Dictionary<int, Vector3> lastScale = new Dictionary<int, Vector3>();
    Dictionary<int, Quaternion> lastRot = new Dictionary<int, Quaternion>();
    Dictionary<int, int> lastHp = new Dictionary<int, int>();

    GameReplay replay;

    const int TARGET_FRAMERATE = 60;

    int frameCount = 0;
    Queue<Record> pending = new Queue<Record>();

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

    string getGameVersion() {
        return "0.1";
    }

	public void AddPutSpellRecord(Spell s, Transform transform, int casterId) {
        pending.Enqueue(new PutSpellRecord(s, transform, casterId));
    }

    void addRecords(List<Record> records) {
        records.ForEach(pending.Enqueue);
    }

    void addTransformRecords() {
        addRecords(StateReader.GetChangedTransFormRecordsWithTag(
            "Character", lastPos, lastRot, lastScale
        ));
    }

	void addHpRecords() {
        addRecords(StateReader.GetChangedHpRecords(lastHp));
	}

    void addInstantiateCharRecords() {
        addRecords(StateReader.GetInstantiateCharRecords(recordedChars, setupNewCharacter));
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

    void setupNewCharacter(GameObject character) {
        character.GetComponent<SpellController>().onCastSpellActions.Add(
            delegate (Spell s, Transform trans, int casterId) {
                AddPutSpellRecord(s, trans, casterId);
            }
        );
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.S)) {
            StartRecording();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            FinishRecording();
        }

        

        if (state == ReplayState.Started) {

            addInstantiateCharRecords();
            addTransformRecords();
            addHpRecords();

            if (GameController.CheckIfGameEnds()) {
                FinishRecording();
            }

            FlushPendingRecodsToReplayObject();
            frameCount += 1;

        }
    }


}
