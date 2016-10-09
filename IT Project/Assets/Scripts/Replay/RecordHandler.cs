﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Replay;
using System;

public class RecordHandler : MonoBehaviour {

    public GameObject CharacterPrefab;
    public GameObject FireballPrefab;
    public GameObject FireNovaPrefab;

    protected Dictionary<int, GameObject> gameObjMap = new Dictionary<int, GameObject>();

    public void SetCharId(int objId, int charId) {
        gameObjMap[objId].GetComponent<Character>().charID = charId;
    }

    public void SetPlayerHp(int objId, int hp) {
        gameObjMap[objId].GetComponent<Character>().hp = hp;
    }

    public void SetPosition(int objId, Vector3 position) {
        gameObjMap[objId].transform.position = position;
    }

    public void SetRotation(int objId, Quaternion rotation) {
        gameObjMap[objId].transform.rotation = rotation;
    }

    public void SetScale(int objId, Vector3 scale) {
        gameObjMap[objId].transform.localScale = scale;
    }

	public void SetGround(float scale, float time) {
		GameObject ground = GameObject.FindGameObjectWithTag ("Ground");
		Vector3 tempScale = new Vector3 (scale, 1, scale);
		ground.transform.localScale = tempScale;
		ground.GetComponent<GroundController> ().timePassed = time;
    }

    public void InstantiateSpellWith(SpellType type, int casterId, Vector3 position, Quaternion rotation) {
        throw new System.NotImplementedException();
    }

    public void ApplyRecord(Record record) {
        record.applyEffect(this);
    }

    public void AddCharacter(int recordObjId) {
        var o = GameObject.Instantiate(CharacterPrefab);
        gameObjMap[recordObjId] = o;
    }


}
