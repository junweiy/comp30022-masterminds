﻿public class MatchingSceneManager : Photon.MonoBehaviour {
    public void OnClick() {
        StateController.SwitchToMainMenu();
        PhotonNetwork.Disconnect();
    }
}