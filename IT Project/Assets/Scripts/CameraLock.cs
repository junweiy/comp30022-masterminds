using UnityEngine;
using System.Collections;
using Manager;

public class CameraLock : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.up * 30 + Vector3.back * 15 + PlayerManager.GetLocalPlayer().GetPosition();
	}
}
