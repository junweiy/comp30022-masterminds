using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateRecorder : MonoBehaviour {
    private HashSet<GameObject> _recordedChars = new HashSet<GameObject>();
    private Dictionary<int, Vector3> _lastPos = new Dictionary<int, Vector3>();
    private Dictionary<int, Vector3> _lastScale = new Dictionary<int, Vector3>();
    private Dictionary<int, Quaternion> _lastRot = new Dictionary<int, Quaternion>();
    private Dictionary<int, int> _lastHp = new Dictionary<int, int>();
    private float _lastGroundSize = -1;

    protected Queue<IRecord> Pending = new Queue<IRecord>();

    protected string GetGameVersion() {
        return Application.version;
    }

    public virtual void AddPutSpellRecord(Spell s, Transform transform, int casterId) {
        Pending.Enqueue(new PutSpellRecord(s, transform, casterId));
    }

    protected void EnqueueRecords(List<IRecord> records) {
        records.ForEach(Pending.Enqueue);
    }

    protected void AddTransformRecords() {
        EnqueueRecords(StateReader.GetChangedTransFormRecordsWithTag(
            "Character", _lastPos, _lastRot, _lastScale
        ));
    }

    protected void AddHpRecords() {
        EnqueueRecords(StateReader.GetChangedHpRecords(_lastHp));
    }

    protected void AddInstantiateCharRecords() {
        EnqueueRecords(StateReader.GetInstantiateCharRecords(_recordedChars, SetupNewCharacter));
    }

    protected void AddGroundrecord() {
        IRecord rec = StateReader.GetGroundRecord(_lastGroundSize);
        if (rec != null) {
            Pending.Enqueue(rec);
        }
    }

    private void SetupNewCharacter(GameObject character) {
        character.GetComponent<SpellController>().OnCastSpellActions.Add(
            delegate(Spell s, Transform trans, int casterId) { AddPutSpellRecord(s, trans, casterId); }
        );
    }

    // Update is called once per frame
    protected void AddRecords() {
        AddInstantiateCharRecords();
        AddTransformRecords();
        AddHpRecords();
        AddGroundrecord();
    }
}