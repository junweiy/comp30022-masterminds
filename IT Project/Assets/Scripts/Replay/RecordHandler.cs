using UnityEngine;
using System.Collections.Generic;
using Replay;
using System;

// Base class that contains shared logic for handling game records
public class RecordHandler : MonoBehaviour {
    public GameObject CharacterPrefab;
    public GameObject FireballPrefab;
    public GameObject FireNovaPrefab;

    protected Dictionary<int, GameObject> GameObjMap = new Dictionary<int, GameObject>();

    // Changes HP of the character with the specified GameObject instance ID
    public void SetCharacterHp(int objId, int hp) {
        if (PhotonNetwork.connected) {
            GameObjMap[objId].GetComponent<Character>().SetHpForAll(hp);
        } else {
            GameObjMap[objId].GetComponent<Character>().Hp = hp;
        }
    }

    // Sets the position of the GameObject with specified instance ID
    public void SetPosition(int objId, Vector3 position) {
        if (PhotonNetwork.connected) {
            if (GameObjMap[objId].GetComponent<Character>() != null) {
                GameObjMap[objId].GetComponent<Character>().SetPositionForAll(position);
            } else {
                GameObjMap[objId].transform.position = position;
            }
        } else {
            GameObjMap[objId].transform.position = position;
        }
    }

    // Sets the rotation of the GameObject with specified instance ID
    public void SetRotation(int objId, Quaternion rotation) {
        if (PhotonNetwork.connected) {
            if (GameObjMap[objId].GetComponent<Character>() != null) {
                //gameObjMap [objId].GetComponent<Character> ().SetRotationForAll (rotation);
            } else {
                GameObjMap[objId].transform.rotation = rotation;
            }
        } else {
            GameObjMap[objId].transform.rotation = rotation;
        }
    }

    // Sets the scale of the GameObject with specified instance ID
    public void SetScale(int objId, Vector3 scale) {
        GameObjMap[objId].transform.localScale = scale;
    }

    // Sets the state of the ground
    public void SetGround(float scale, float time) {
		GroundController gc = GameObjectFinder.FindGroundController();
        if (PhotonNetwork.connected) {
            gc.SetTimePassedForAll(time);
            gc.SetScaleForAll(scale);
        } else {
            gc.TimePassed = time;
            gc.transform.localScale = new Vector3(scale, 1, scale);
        }
    }

    // Instantiate a spell with given spell type, caster ID and initial state
    public void InstantiateSpellWith(SpellType type, int casterId, Vector3 position, Quaternion rotation) {
        GameObject obj;
        if (!PhotonNetwork.connected) {
            if (type == SpellType.Fireball) {
				Debug.Log ("Cast fireball");
                obj = GameObject.Instantiate(FireballPrefab);
                obj.GetComponent<FireBallController>().EnableDamage = false;
                obj.GetComponent<FireBallController>().CharId = casterId;
            } else if (type == SpellType.FireNova) {
				Debug.Log ("Cast firenova");
                obj = GameObject.Instantiate(FireNovaPrefab);
                obj.GetComponent<FireNovaController>().CastingTime = FireNova.CASTING_TIME;
                obj.GetComponent<FireNovaController>().CharId = casterId;
            } else {
                return;
            }

            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
    }

    // Applys the given record
    public void ApplyRecord(IRecord record) {
        record.ApplyEffect(this);
    }

    // Instantiate a character with given ID and user name
    public void InstantiateCharacterWith(int recordObjId, int charId, string userName) {
        if (!PhotonNetwork.connected) {
            GameObject o = Instantiate(CharacterPrefab);
            o.GetComponent<Character>().CharId = charId;
            o.GetComponent<Character>().UserName = userName;
            GameObjMap[recordObjId] = o;
        }
        if (PhotonNetwork.connected && PhotonNetwork.isMasterClient) {
            GameObject o = PhotonNetwork.Instantiate("Prefabs/Character", Vector3.zero, Quaternion.identity, 0);
            PhotonView pv = o.GetComponent<PhotonView>();
            pv.TransferOwnership(FindIdByName(userName));
            o.GetComponent<Character>().CharId = charId;
            o.GetComponent<Character>().UserName = userName;
            GameObjMap[recordObjId] = o;
        }
    }

    public int FindIdByName(string userName) {
        PhotonPlayer[] pps = PhotonNetwork.playerList;
        foreach (PhotonPlayer pp in pps) {
            if (pp.name == userName) {
                return pp.ID;
            }
        }
        throw new Exception();
    }
}