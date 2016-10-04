using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MatchingSceneManager : Photon.MonoBehaviour {

	public void OnClick() {
		StateController.SwitchToMainMenu ();
		PhotonNetwork.Disconnect ();
	}
}
