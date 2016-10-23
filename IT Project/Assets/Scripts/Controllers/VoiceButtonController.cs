using UnityEngine;

public class VoiceButtonController : Photon.MonoBehaviour {
    private GameObject _mainPlayer;
    private GameObject _speakerIcon;

    private void Start() {
        _mainPlayer = GameObjectFinder.FindMainPlayer();
        if (_mainPlayer != null) {
            _speakerIcon = _mainPlayer.transform.GetChild(2).GetChild(1).gameObject;
        }
    }

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

    private void ToggleRecording(GameObject mainPlayer) {
        bool isTransimitting = mainPlayer.GetComponent<PhotonVoiceRecorder>().Transmit;
        mainPlayer.GetComponent<PhotonVoiceRecorder>().Transmit = !isTransimitting;
    }

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