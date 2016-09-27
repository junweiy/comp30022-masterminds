using UnityEngine;
using System.Collections;

public class ResultPageController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void returnToRoom() {
		StateController.SwitchToRoom ();
	}

	public void saveReplay() {
		// TODO
		Debug.LogWarning ("Replay function not implemented yet");
	}
}
