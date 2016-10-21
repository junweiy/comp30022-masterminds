using UnityEngine;
using System.Collections.Generic;
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

    public static List<IRecord> GetTransformRecordsWithTag(string tag) {
        return GetChangedTransFormRecordsWithTag(
            tag,
            new Dictionary<int, Vector3>(),
            new Dictionary<int, Quaternion>(),
            new Dictionary<int, Vector3>()
        );
    }

    public static List<IRecord> GetChangedTransFormRecordsWithTag(
        string tag,
        Dictionary<int, Vector3> lastPos,
        Dictionary<int, Quaternion> lastRot,
        Dictionary<int, Vector3> lastScl) {
        var records = new List<IRecord>();

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

    public static List<IRecord> GetHpRecords() {
        return GetChangedHpRecords(new Dictionary<int, int>());
    }

    public static List<IRecord> GetChangedHpRecords(Dictionary<int, int> lastHp) {
        var records = new List<IRecord>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Character")) {
            int id = obj.GetInstanceID();
            int hp = obj.GetComponent<Character>().Hp;
            if (!lastHp.ContainsKey(id) || lastHp[id] != hp) {
                lastHp[id] = hp;
                records.Add(new PlayerHpRecord(id, hp));
            }
        }
        return records;
    }

    public static List<IRecord> GetInstantiateCharRecords(
        HashSet<GameObject> recordedChars,
        Action<GameObject> onAdded) {
        var records = new List<IRecord>();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Character")) {
            if (!recordedChars.Contains(obj)) {
                recordedChars.Add(obj);
                records.Add(
                    new AddCharacterRecord(
                        obj.GetInstanceID(), obj.GetComponent<Character>().CharId,
                        obj.GetComponent<Character>().UserName
                    ));
                onAdded(obj);
            }
        }
        return records;
    }

    public static List<IRecord> GetInstantiateCharRecords(HashSet<GameObject> recordedChars) {
        return GetInstantiateCharRecords(recordedChars, delegate(GameObject o) { });
    }

    public static GroundRecord GetGroundRecord(float lastGroundSize) {
        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        GroundController gc = ground.GetComponent<GroundController>();
        float scale = ground.transform.localScale.x;
        if (scale != lastGroundSize) {
            if (scale != 100f) {}
            return new GroundRecord(ground.transform.localScale.x, gc.TimePassed);
        } else {
            return null;
        }
    }
}