using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ReplaySceneController : MonoBehaviour {

    public GameObject characterPrefab;

    enum State { Preparing, Started, Ended }

    private State state = State.Preparing;
    
    GameObject[] characters;
    GameObject[] spells;

    private string path;

    GameReplay replay;
    int frameCount = 0;

    public void setPlayerHp(int playerId, float hp) {
        throw new System.NotImplementedException();
    }

    public void putSpell(int playerId, int spellId) {
        throw new System.NotImplementedException();
    }

    public void setPlayerPosition(int playerId, Vector3 pos) {
        characters[playerId].transform.position = pos;
    }
    

    // Use this for initialization
    void Start () {
        path = Application.dataPath + "/Replays/";
        GameReplay p = openReplay(path + "1.rep");
        loadReplay(p);
    }

    GameReplay openReplay(string filePath) {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return (GameReplay)formatter.Deserialize(stream);
    }

    void loadReplay(GameReplay replay) {
        this.replay = replay;
        var info = replay.info;
        Application.targetFrameRate = info.targetFrameRate;
        //characters = info.characters;
        //spells = info.spells;
        loadCharacters(info.numCharacters);
        state = State.Started;
        frameCount = 0;
    }

    void loadCharacters(int numCharacters) {
        List<GameObject> chars = new List<GameObject>();
        for (int i = 0; i < numCharacters; i++) {
            var o = GameObject.Instantiate(characterPrefab);
            chars.Add(o);
        }
        this.characters = chars.ToArray();
    }

    void finishReplay() {
        state = State.Ended;
        // TODO
    }

    // Update is called once per frame
    void Update () {
	    if (state == State.Started) {

            if (replay.entries.Count == 0) {
                finishReplay();
                return;
            }

            var nextEntry = replay.entries.Peek();
            if (nextEntry.frameTime >= frameCount) {
                replay.entries.Dequeue();
                nextEntry.record.applyEffect(this);
            }
            frameCount += 1;

        }
	}
}
