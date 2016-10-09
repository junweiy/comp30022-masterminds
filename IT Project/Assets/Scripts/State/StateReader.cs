using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class StateReader {

    public static Dictionary<int, Transform> GetTransformOfObjectsWithTag(string tag) {
        var dict = new Dictionary<int, Transform>();
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objs) {
            dict.Add(obj.GetInstanceID(), obj.transform);
        }
        return dict;
    }

    public static List<Record> GetTransformRecordsOfObjectsWithTag(string tag) {
        List<Record> records = new List<Record>();
        foreach (var entry in GetTransformOfObjectsWithTag(tag)) {
            records.Add(new PositionRecord(entry.Key, entry.Value.position));
            records.Add(new RotationRecord(entry.Key, entry.Value.rotation));
            records.Add(new ScaleRecord(entry.Key, entry.Value.localScale));
        }
        return records;
    }

    // TODO -- for Junwei juju
    public static GroundRecord GetGroundRecord() {
        throw new System.NotImplementedException();
    }


    
}
