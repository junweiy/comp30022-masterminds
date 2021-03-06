﻿using UnityEngine;

public class GroundController : Photon.MonoBehaviour {
    // Initial scale
    public Vector3 InitialScale = new Vector3(100, 0, 100);
    // Seconds between every shrink
    public float SecondsBetweenShrinking;
    // The minimum size presented as a ratio to the initial size
    public float MinScalableSizeRatio;
    // Size shrunk every time
    public float SizeShrunkPerTime;
    // Time passed since last shrink
    public float TimePassed;
    // MiniMap Ground
    public GameObject MiniMap;


    private void Start() {
        TimePassed = 0;
    }

    private void Update() {
        TimePassed += Time.deltaTime;
        if (TimePassed >= SecondsBetweenShrinking && transform.localScale.x > InitialScale.x*MinScalableSizeRatio) {
            TimePassed -= SecondsBetweenShrinking;
            Scale();
        }
    }

	// Update timePassed variable for ground on all clients
    public void SetTimePassedForAll(float timePassed) {
        this.photonView.RPC("SetTimePassedRPC", PhotonTargets.All, timePassed);
    }

    [PunRPC]
    public void SetTimePassedRPC(float timePassed) {
        this.TimePassed = timePassed;
    }

	// Set scale of Ground on all clients
    public void SetScaleForAll(float scale) {
        this.photonView.RPC("SetScaleRPC", PhotonTargets.All, scale);
    }

    [PunRPC]
    public void SetScaleRPC(float scale) {
        this.transform.localScale = new Vector3(scale, 1, scale);
		if (MiniMap != null) {
			MiniMap.transform.localScale = this.transform.localScale;
		}
    }

	// Shrink the ground with given properties
    private void Scale() {
        Vector3 tempSize = transform.localScale;
        tempSize.x = transform.localScale.x - SizeShrunkPerTime;
        tempSize.y = transform.localScale.y;
        tempSize.z = transform.localScale.z - SizeShrunkPerTime;
        transform.localScale = tempSize;
		if (MiniMap != null) {
			MiniMap.transform.localScale = tempSize;
		}
        
    }
}