using UnityEngine;

public class VoiceButtonController : Photon.MonoBehaviour {
	// Main character GameObject
    private GameObject _mainPlayer;
	// Speaker icon of main character
    private GameObject _speakerIcon;

    private void Start() {
        _mainPlayer = GameObjectFinder.FindMainPlayer();
        if (_mainPlayer != null) {
            _speakerIcon = _mainPlayer.transform.GetChild(2).GetChild(1).gameObject;
        }
    }

	// Check if game objects have been found
    private void Update() {
        if (_mainPlayer == null) {
			_mainPlayer = GameObjectFinder.FindMainPlayer();
            if (_mainPlayer != null) {
                _speakerIcon = _mainPlayer.transform.GetChild(2).GetChild(1).gameObject;
            } else {
                return;
            }
        }
    }

	// Toggle the status of recording
    private void ToggleRecording(GameObject mainPlayer) {
        bool isTransimitting = mainPlayer.GetComponent<PhotonVoiceRecorder>().Transmit;
        mainPlayer.GetComponent<PhotonVoiceRecorder>().Transmit = !isTransimitting;
    }

	// Reflect change of speaker icon on all clients
    [PunRPC]
    private void ToggleSpeakerIcon(int charId) {
        GameObject player = PhotonView.Find(charId).gameObject;
        GameObject playerIcon = player.transform.GetChild(2).GetChild(1).gameObject;
        playerIcon.SetActive(!playerIcon.GetActive());
    }

    public void OnClick() {
        Debug.Log("Clicked");
        ToggleRecording(_mainPlayer);
        photonView.RPC("ToggleSpeakerIcon", PhotonTargets.All, _mainPlayer.GetComponent<Character>().CharId);
    }
}