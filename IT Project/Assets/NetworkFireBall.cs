using UnityEngine;
using Photon;

public class NetworkFireBall : Photon.MonoBehaviour {
//	private int charID;
//	private Quaternion playerRotation;
//	private float velocity;
//	private float range;
//	private int damage;
//	private Vector3 positionChange;
//
//	void Update() {
//		if (!photonView.isMine) {
//			transform.position = Vector3.Lerp(transform.position, this.correctPlayerPos, Time.deltaTime * 5);
//			transform.rotation = Quaternion.Lerp(transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
//		}
//	}
//
//	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
//		if (stream.isWriting) {
//			// We own this player: send the others our data
//			stream.SendNext(this.GetComponent<FireBallController>().charID);
//			stream.SendNext(this.GetComponent<FireBallController>().playerRotation);
//			stream.SendNext(this.GetComponent<FireBallController>().velocity);
//			stream.SendNext(this.GetComponent<FireBallController>().range);
//			stream.SendNext(this.GetComponent<FireBallController>().damage);
//			stream.SendNext(this.GetComponent<FireBallController>().positionChange);
//		}
//		else {
//			// Network player, receive data
//			this.charID = (Vector3)stream.ReceiveNext();
//		}
//	}
}