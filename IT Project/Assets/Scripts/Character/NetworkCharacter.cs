using UnityEngine;
using Photon;

public class NetworkCharacter : Photon.MonoBehaviour {
	private Vector3 correctPlayerPos;
	private Quaternion correctPlayerRot;
	private int hp;
	private string userName;

	// Update is called once per frame
	void FixedUpdate() {
		if (!photonView.isMine) {
			transform.position = Vector3.Lerp (transform.position, this.correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp (transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
			GetComponent<Character> ().hp = hp;
			GetComponent<Character> ().userName = userName;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			// We own this player: send the others our data
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
			stream.SendNext (GetComponent<Character> ().hp);
			stream.SendNext (GetComponent<Character> ().userName);
		}
		else {
			// Network player, receive data
			this.correctPlayerPos = (Vector3)stream.ReceiveNext ();
			this.correctPlayerRot = (Quaternion)stream.ReceiveNext ();
			this.hp = (int)stream.ReceiveNext ();
			this.userName = (string) stream.ReceiveNext ();
		}
	}
}