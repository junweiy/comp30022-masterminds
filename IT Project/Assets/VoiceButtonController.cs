using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoiceButtonController : MonoBehaviour {

	private GameObject mainPlayer;

	void Start() {
		mainPlayer = FindMainPlayer ();
	}

	void ToggleRecording(GameObject mainPlayer) {
		bool isTransimitting = mainPlayer.GetComponent<PhotonVoiceRecorder> ().Transmit;
		mainPlayer.GetComponent<PhotonVoiceRecorder> ().Transmit = !isTransimitting;
	}

	public static GameObject FindMainPlayer() {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		foreach (GameObject player in players) {
			if (player.GetPhotonView ().isMine) {
				return player;
			}
		}
		throw new UnityException ();
		
	}

	public static T GetMainPlayerController<T>() {
		GameObject mainPlayer = FindMainPlayer ();
		return mainPlayer.GetComponent<T> ();
	}
		

    public void OnClick()
    {
		Debug.Log ("Clicked");
		ToggleRecording (mainPlayer);
    }
}
