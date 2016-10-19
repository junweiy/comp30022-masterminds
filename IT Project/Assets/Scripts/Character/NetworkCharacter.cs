using UnityEngine;
using Photon;

public class NetworkCharacter : Photon.MonoBehaviour {
	private int hp;
	private string userName;


	// Update is called once per frame
	void FixedUpdate() {
		if (!PhotonNetwork.connected) {
			return;
		}
		if (!photonView.isMine) {
			GetComponent<Character> ().hp = hp;
			GetComponent<Character> ().userName = userName;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			// We own this player: send the others our data
			stream.SendNext (GetComponent<Character> ().hp);
			stream.SendNext (GetComponent<Character> ().userName);
		}
		else {
			// Network player, receive data
			this.hp = (int)stream.ReceiveNext ();
			this.userName = (string) stream.ReceiveNext ();
		}
	}
}