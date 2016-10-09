using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoiceButtonController : Photon.MonoBehaviour {

	private GameObject mainPlayer;
    private GameObject speakerIcon;

	void Start() {
		mainPlayer = FindMainPlayer ();
        speakerIcon = mainPlayer.transform.GetChild(2).GetChild(1).gameObject;
	}

	void ToggleRecording(GameObject mainPlayer) {
		bool isTransimitting = mainPlayer.GetComponent<PhotonVoiceRecorder> ().Transmit;
		mainPlayer.GetComponent<PhotonVoiceRecorder> ().Transmit = !isTransimitting;
	}

	[PunRPC]
    void ToggleSpeakerIcon(int charID)
	{
		GameObject player = PhotonView.Find (charID).gameObject;
		GameObject playerIcon = player.transform.GetChild (2).GetChild (1).gameObject;
		playerIcon.SetActive(!playerIcon.GetActive());
    }
    
	public static GameObject FindMainPlayer() {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		foreach (GameObject player in players) {
			if (player.GetPhotonView ().isMine) {
				return player;
			}
		}
		return null;
	}

	public static T GetMainPlayerController<T>() {
		GameObject mainPlayer = FindMainPlayer ();
		return mainPlayer.GetComponent<T> ();
	}
		

    public void OnClick()
    {
		Debug.Log ("Clicked");
		ToggleRecording (mainPlayer);
		photonView.RPC ("ToggleSpeakerIcon", PhotonTargets.All, mainPlayer.GetComponent<Character> ().charID);
    }
}
