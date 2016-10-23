public class NetworkCharacter : Photon.MonoBehaviour {
    private int _hp;
    private string _userName;


    // Update is called once per frame
    private void FixedUpdate() {
        if (!PhotonNetwork.connected) {
            return;
        }
		// Update variable only for current player
        if (!photonView.isMine) {
            GetComponent<Character>().Hp = _hp;
            GetComponent<Character>().UserName = _userName;
        }
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            // We own this player: send the others our data
            stream.SendNext(GetComponent<Character>().Hp);
            stream.SendNext(GetComponent<Character>().UserName);
        } else {
            // Network player, receive data
            this._hp = (int) stream.ReceiveNext();
            this._userName = (string) stream.ReceiveNext();
        }
    }
}