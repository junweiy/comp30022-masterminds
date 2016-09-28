using UnityEngine;
using System.Collections;
using Photon;

public class RandomMatchmaker : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("V0.1");
	}

	void OnGUI() {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	public override void OnJoinedLobby () {
		PhotonNetwork.JoinRandomRoom ();
	}

	void Update() {
		Debug.Log (PhotonNetwork.connectionStateDetailed.ToString());
	}

	public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
		Debug.Log ("Cannot join a random room");
		PhotonNetwork.CreateRoom (null);
	}

	public override void OnJoinedRoom () {
		GameObject player = PhotonNetwork.Instantiate ("Prefabs/Character", new Vector3(1000,0,1000),Quaternion.identity, 0);
		player.GetComponent<CharacterController> ().SetControllable();
	}


}
