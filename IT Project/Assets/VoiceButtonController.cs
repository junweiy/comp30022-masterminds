using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoiceButtonController : MonoBehaviour {

	void ToggleRecording() {
		bool isTransimitting = this.GetComponent<PhotonVoiceRecorder> ().Transmit;
		this.GetComponent<PhotonVoiceRecorder> ().Transmit = !isTransimitting;
	}
		

    public void OnClick()
    {
		Debug.Log ("Clicked");
		ToggleRecording ();
    }
}
