using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class GameStateRecorder : MonoBehaviour {
    enum State { Preparing, Started, Ended }
    private State state = State.Preparing;

    private GameObject[] characters;
    private Vector3[] lastPos;
    private GameObject[] spells;

    private string folderPath;

    GameReplay replay;

    const int TARGET_FRAMERATE = 60;

    int frameCount = 0;
    Queue<ReplayRecord> recordsInThisFrame = new Queue<ReplayRecord>();

	// Use this for initialization
	void Start () {
        folderPath = Application.dataPath + "/Replays/";
        Application.targetFrameRate = TARGET_FRAMERATE;
	}

    void startRecording() {
        replay = new GameReplay();
        //replay.info = new ReplayInfo(
        //    getGameVersion(),
        //    getCharacters(),
        //    getSpells(),
        //    TARGET_FRAMERATE);
        replay.info = new ReplayInfo(
            getGameVersion(),
            getCharacters().Length,
            TARGET_FRAMERATE
        );

        characters = getCharacters();
        lastPos = new Vector3[characters.Length];
        for (int i = 0; i < characters.Length; i++) {
            lastPos[i] = Vector3.zero;
        }
        replay.entries = new Queue<GameReplay.Entry>();
        frameCount = 0;
        state = State.Started;
    }

    GameObject[] getCharacters() {
        return GameObject.FindGameObjectsWithTag("Character").ToArray();
    }

    GameObject[] getSpells() {
        //throw new System.NotImplementedException();

        return new GameObject[1];
    }

    string getGameVersion() {
        //throw new System.NotImplementedException();

        return "";
    }

    void addSpellRecord(Spell s, Character c) {
        // TODO bad implementation to be improved
        //recordsInThisFrame.Enqueue(new PutSpellRecord(
        //    Array.IndexOf(replay.info.characters, c),
        //    Array.IndexOf(replay.info.spells, s)
        //));

    }

    void addPosRecords() {
        foreach (var charObj in GameObject.FindGameObjectsWithTag("Character")) {
            var c = charObj;
            int idx = Array.IndexOf(characters, c);
            if (lastPos[idx] != c.transform.position) {
                recordsInThisFrame.Enqueue(new TransformRecord(idx, c.transform.position));
                lastPos[idx] = c.transform.position;
            }
        }
    }

    void addEntry(ReplayRecord record) {
        var newEntry = new GameReplay.Entry();
        newEntry.frameTime = frameCount;
        newEntry.record = record;
        replay.entries.Enqueue(newEntry);
    }

    void finishRecording() {
        state = State.Ended;
        saveRecord();
    }

    void saveRecord() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(folderPath + "1.rep", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, replay);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            startRecording();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            finishRecording();
        }


        if (state == State.Started) {

            addPosRecords();

            while (recordsInThisFrame.Count != 0) {
                var record = recordsInThisFrame.Dequeue();
                addEntry(record);
            }
            
            frameCount += 1;

        }
    }


}
