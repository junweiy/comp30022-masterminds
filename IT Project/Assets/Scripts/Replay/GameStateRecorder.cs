﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

public class GameStateRecorder : MonoBehaviour {
    enum State { Preparing, Started, Paused, Ended }
    private State state = State.Preparing;

    private Dictionary<int, int> idMap = new Dictionary<int, int>();
    List<GameObject> characterObjs = new List<GameObject>();
    List<GameObject> spellObjs = new List<GameObject>();
    List<Character> characters = new List<Character>();
    private int numCharRecorded = 0;
    private Dictionary<GameObject, Vector3> lastPos = new Dictionary<GameObject, Vector3>();

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

    void StartRecording() {
        Debug.Log("Started Recording");
        replay = new GameReplay();
        UpdateCharacterList();

        replay.info = new ReplayInfo(
            getGameVersion(),
            characters.Count,
            TARGET_FRAMERATE
        );

        foreach (var charObj in characterObjs) {
			charObj.GetComponent<SpellController> ().onCastSpellActions.Add(delegate(Spell s, Transform trans) {
				AddPutSpellRecord(s, trans);
			});
        }

        replay.entries = new Queue<GameReplay.Entry>();
        frameCount = 0;
        state = State.Started;
    }


    string getGameVersion() {
        return "0.1";
    }

    //public void AddHpRecord(int hp, Character c) {
    //    recordsInThisFrame.Enqueue(new PlayerHpRecord(
    //        hp, characters.IndexOf(c)
    //    ));
    //}

    public int getPlayerIdFromObject(GameObject characterObj) {
        return idMap[characterObj.GetInstanceID()];
    }

	public void AddPutSpellRecord(Spell s, Transform transform) {
        recordsInThisFrame.Enqueue(new PutSpellRecord(s, transform));
	}


    //void addSpellRecord(Spell s, Character c) {
    //    recordsInThisFrame.Enqueue(new CastSpellRecord(
    //        characters.IndexOf(c),
    //        ReplayTypeConverter.GetTypeFromSpell(s)
    //    ));
    //}

    void addPosRecords() {
        int i = 0;
        foreach (var charObj in characterObjs) {
            if (charObj != null) {
                var pos = charObj.transform.position;
                if (!lastPos.ContainsKey(charObj) || lastPos[charObj] != pos) {
                    recordsInThisFrame.Enqueue(new TransformRecord(i, pos));
                    lastPos[charObj] = pos;
                }
            }
            i += 1;
        }
    }

	void addHpRecords() {
		int i = 0;
		foreach (var charObj in characterObjs) {
			if (charObj != null) {
                int hp = charObj.GetComponent<Character>().hp;
                recordsInThisFrame.Enqueue(new PlayerHpRecord(hp, i));
			}
			i += 1;
		}
	}

    void addEntry(ReplayRecord record) {
        var newEntry = new GameReplay.Entry();
        newEntry.frameTime = frameCount;
        newEntry.record = record;
        replay.entries.Enqueue(newEntry);
    }

    void FinishRecording() {
        Debug.Log("Finished Recording");
        state = State.Ended;
        Flush();
        SaveRecord();
    }

    void SaveRecord() {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(folderPath + "1.rep", FileMode.Create, FileAccess.Write, FileShare.None);
        formatter.Serialize(stream, replay);
    }

    // Not an actual flush to disk, but could be changed to do so
    void Flush() {
        while (recordsInThisFrame.Count != 0) {
            var record = recordsInThisFrame.Dequeue();
            addEntry(record);
        }
    }

    void UpdateCharacterList() {
        foreach (var charObj in GameObject.FindGameObjectsWithTag("Character")) {
            int objId = charObj.GetInstanceID();
            if (!idMap.ContainsKey(objId)) {
                idMap.Add(objId, numCharRecorded);
                numCharRecorded += 1;
                characterObjs.Add(charObj);
                characters.Add(charObj.GetComponent<Character>());
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.S)) {
            StartRecording();
        } else if (Input.GetKeyDown(KeyCode.E)) {
            FinishRecording();
        }

        

        if (state == State.Started) {

            //addInstantiateRecords();
            addPosRecords();
            addHpRecords();
            //addDestroyRecords();

            if (GameController.CheckIfGameEnds()) {
                FinishRecording();
            }

            Flush();
            frameCount += 1;

        }
    }


}
