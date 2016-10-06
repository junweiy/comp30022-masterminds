using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Replay;
using UnityEngine.UI;

public class ReplaySceneController : MonoBehaviour {

    public GameObject CharacterPrefab;
    public GameObject FireballPrefab;
    public GameObject FireNovaPrefab;

    public Text ButtonLabel;

    enum State { Preparing, Started, Paused, Ended }

    private State _state = State.Preparing;
    private State state {
        get {
            return _state;
        } set {
            _state = value;

            if (ButtonLabel != null) {
                if (value == State.Preparing) {
                    ButtonLabel.text = "Preparing";
                } else if (value == State.Started) {
                    ButtonLabel.text = "Pause";
                } else if (value == State.Paused) {
                    ButtonLabel.text = "Continue";
                } else if (value == State.Ended) {
                    ButtonLabel.text = "Replay Ended";
                }
            }
        }
    }
    
    GameObject[] characterObjs;

    private string path;

    GameReplay replay;
    int frameCount = 0;

    public void SetPlayerHp(int playerId, int hp) {
        characterObjs[playerId].GetComponent<CharacterController>().character.hp = hp;
    }

    // unused for now
    public void SetSpellCast(int playerId, SpellType spellType) {
        var spell = ReplayTypeConverter.GetSpellFromType(spellType);
        characterObjs[playerId].GetComponent<SpellController>().CastSpell(spell);
    }

    public void SetPlayerPosition(int playerId, Vector3 pos) {
        characterObjs[playerId].transform.position = pos;
        //Debug.Log("set player " + playerId.ToString() + " position to " + pos.ToString());
    }

    public void IntantiateSpellWithTransform(SpellType spellType, Vector3 positon, Quaternion rotation) {
        GameObject obj;
        if (spellType == SpellType.Fireball) {
            obj = GameObject.Instantiate(FireballPrefab);
            obj.GetComponent<FireBallController>().enableDamage = false;
            obj.GetComponent<FireBallController>().charID = -1;
        } else if (spellType == SpellType.FireNova) {
            obj = GameObject.Instantiate(FireNovaPrefab);
			obj.GetComponent<FireNovaController> ().castingTime = FireNova.CASTING_TIME;
        } else {
            return;
        }

        obj.transform.position = positon;
        obj.transform.rotation = rotation;
    }
    

    // Use this for initialization
    void Start () {
        path = Application.dataPath + "/Replays/";
        GameReplay p = OpenReplay(path + "1.rep");
        LoadReplay(p);
        StartReplay();
    }

    GameReplay OpenReplay(string filePath) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameReplay)formatter.Deserialize(stream);
    }

    void LoadReplay(GameReplay replay) {
        this.replay = replay;
        var info = replay.info;
        Application.targetFrameRate = info.targetFrameRate;
        LoadCharacters(info.numCharacters);
        frameCount = 0;
    }

    void StartReplay() {
        state = State.Started;
    }

    void LoadCharacters(int numCharacters) {
        List<GameObject> chars = new List<GameObject>();
        for (int i = 0; i < numCharacters; i++) {
            var o = GameObject.Instantiate(CharacterPrefab);
            chars.Add(o);
        }
        this.characterObjs = chars.ToArray();
    }

    void FinishReplay() {
        state = State.Ended;
        // TODO
    }

    public void TriggerPauseContinue() {
        if(state == State.Started) {
            Pause();
        } else if (state == State.Paused) {
            Continue();
        }
    }

    public void Pause() {
        state = State.Paused;
    }

    public void Continue() {
        state = State.Started;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("p")) {
            TriggerPauseContinue();
        }

        if (state == State.Started) {

            if (replay.entries.Count == 0) {
                FinishReplay();
                return;
            }

            var nextEntry = replay.entries.Peek();
            while (nextEntry.frameTime <= frameCount) {
                replay.entries.Dequeue();
                nextEntry.record.applyEffect(this);
                if (replay.entries.Count == 0) {
                    FinishReplay();
                    return;
                }
                nextEntry = replay.entries.Peek();
            }

            frameCount += 1;
        }
	}
}
