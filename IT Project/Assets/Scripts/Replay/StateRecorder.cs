﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateRecorder : MonoBehaviour {

    HashSet<GameObject> recordedChars = new HashSet<GameObject>();
    Dictionary<int, Vector3> lastPos = new Dictionary<int, Vector3>();
    Dictionary<int, Vector3> lastScale = new Dictionary<int, Vector3>();
    Dictionary<int, Quaternion> lastRot = new Dictionary<int, Quaternion>();
    Dictionary<int, int> lastHp = new Dictionary<int, int>();

    protected Queue<Record> pending = new Queue<Record>();

    protected string getGameVersion() {
        return Application.version;
    }

    public virtual void AddPutSpellRecord(Spell s, Transform transform, int casterId) {
        pending.Enqueue(new PutSpellRecord(s, transform, casterId));
    }

    protected void addRecords(List<Record> records) {
        records.ForEach(pending.Enqueue);
    }

    protected void addTransformRecords() {
        addRecords(StateReader.GetChangedTransFormRecordsWithTag(
            "Character", lastPos, lastRot, lastScale
        ));
    }

    protected void addHpRecords() {
        addRecords(StateReader.GetChangedHpRecords(lastHp));
    }

    protected void addInstantiateCharRecords() {
        addRecords(StateReader.GetInstantiateCharRecords(recordedChars, setupNewCharacter));
    }

    void setupNewCharacter(GameObject character) {
        character.GetComponent<SpellController>().onCastSpellActions.Add(
            delegate (Spell s, Transform trans, int casterId) {
                AddPutSpellRecord(s, trans, casterId);
            }
        );
    }

    // Update is called once per frame
    protected void addRecords() {
            addInstantiateCharRecords();
            addTransformRecords();
            addHpRecords();
    }


}
