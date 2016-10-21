﻿using UnityEngine;

public class MiniMapController : MonoBehaviour {
    private void Start() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject player in players) {
            Renderer miniMapDot = player.transform.GetChild(4).gameObject.GetComponent<Renderer>();
            //Camera miniMapCamera = player.transform.GetChild(5).gameObject.GetComponent<Camera>();
            if (player.GetPhotonView().isMine) {
                miniMapDot.material = Resources.Load<Material>("Materials/Blue");
                //miniMapCamera.enabled = true;
                break;
            }
        }
    }
}