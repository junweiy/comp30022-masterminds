using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class StateReader {

    public static Dictionary<int, Transform> GetTransformsWithTag(string tag) {
        var dict = new Dictionary<int, Transform>();
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objs) {
            dict.Add(obj.GetInstanceID(), obj.transform);
        }
        return dict;
    }

    public static List<Record> GetTransformRecordsWithTag(string tag) {
        return GetChangedTransFormRecordsWithTag(
            tag,
            new Dictionary<int, Vector3>(),
            new Dictionary<int, Quaternion>(),
            new Dictionary<int, Vector3>()
        );
    }

    public static List<Record> GetChangedTransFormRecordsWithTag(
        string tag,
        Dictionary<int, Vector3> lastPos,
        Dictionary<int, Quaternion> lastRot,
        Dictionary<int, Vector3> lastScl) {

        var records = new List<Record>();

        foreach (var entry in GetTransformsWithTag(tag)) {
            int id = entry.Key;
            var pos = entry.Value.position;
            var rot = entry.Value.rotation;
            var scl = entry.Value.localScale;

            if (!lastPos.ContainsKey(id) || lastPos[id] != pos) {
                lastPos[id] = pos;
                records.Add(new PositionRecord(id, pos));
            }

            if (!lastRot.ContainsKey(id) || lastRot[id] != rot) {
                lastRot[id] = rot;
                records.Add(new RotationRecord(id, rot));
            }

            if (!lastScl.ContainsKey(id) || lastScl[id] != scl) {
                lastScl[id] = scl;
                records.Add(new ScaleRecord(id, scl));
            }

            records.Add(new PositionRecord(entry.Key, entry.Value.position));
            records.Add(new RotationRecord(entry.Key, entry.Value.rotation));
            records.Add(new ScaleRecord(entry.Key, entry.Value.localScale));
        }

        return records;

    }

    public static List<Record> GetHpRecords() {
        return GetChangedHpRecords(new Dictionary<int, int>());
    }

    public static List<Record> GetChangedHpRecords(Dictionary<int, int> lastHp) {
        var records = new List<Record>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Character")) {
            int id = obj.GetInstanceID();
            int hp = obj.GetComponent<Character>().hp;
            if (!lastHp.ContainsKey(id) || lastHp[id] != hp) {
                lastHp[id] = hp;
                records.Add(new PlayerHpRecord(id, hp));
            }
        }
        return records;
    }

    public static List<Record> GetInstantiateCharRecords(
        HashSet<GameObject> recordedChars, Action<GameObject> onAdded) {

        var records = new List<Record>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Character")) {
            if (!recordedChars.Contains(obj)) {
                recordedChars.Add(obj);
                records.Add(new AddCharacterRecord(
                    obj.GetInstanceID(), obj.GetComponent<Character>().charID
                ));
                onAdded(obj);
            }
        }
        return records;

    }

    public static List<Record> GetInstantiateCharRecords(HashSet<GameObject> recordedChars) {
        return GetInstantiateCharRecords(recordedChars, delegate (GameObject o) { });
    }

    // TODO -- for Junwei juju
    public static GroundRecord GetGroundRecord() {
        throw new System.NotImplementedException();
    }


    
}
