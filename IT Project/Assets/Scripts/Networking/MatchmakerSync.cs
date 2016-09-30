using UnityEngine;
using Photon;

public class MatchmakerSync : Photon.MonoBehaviour {
	public float timeLeft;

	// Update is called once per frame
	void Update() {
		if (!PhotonNetwork.connected) {
			return;
		}

		if (PhotonNetwork.room == null) {
			return;
		}


		if (!PhotonNetwork.isMasterClient) {
			this.GetComponent<RandomMatchmaker> ().timeLeft = timeLeft;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			// We own this player: send the others our data
			stream.SendNext(this.GetComponent<RandomMatchmaker>().timeLeft);
		}
		else {
			// Network player, receive data
			this.timeLeft = (float)stream.ReceiveNext();
		}
	}
}