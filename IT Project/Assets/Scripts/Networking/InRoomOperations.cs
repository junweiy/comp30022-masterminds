using UnityEngine;
using System.Collections;
using Photon;

public class InRoomOperations : Photon.PunBehaviour {


	void OnGUI() {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		// TODO display GAMESTART button if PhotonNetwork.isMasterClient
	}

	public void LeaveRoom() {
		PhotonNetwork.LeaveRoom();
		// Switch scene or Application.LoadLevel
	}

	public PhotonPlayer[] ListAllPlayers() {
		return PhotonNetwork.playerList;
	}

	void OnJoinedRoom() {
		photonView.RPC ("RefreshPlayerDetails", PhotonTargets.All);
	}

	void OnLeftRoom() {
		photonView.RPC ("RefreshPlayerDetails", PhotonTargets.All);
	}


	[PunRPC]
	void RefreshPlayerList() {
		// TODO Display all player details by calling ListAllPlayers
	}

	void GameStartButtonOnClick() {
		photonView.RPC ("StartGame", PhotonTargets.All);
	}

	[PunRPC]
	void StartGame() {
		// TODO Switch scene to GamePlay
	}
		
}

