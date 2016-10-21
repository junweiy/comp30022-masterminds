using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : Photon.MonoBehaviour {

	// the character model
	public Character Character;

	public const float VELOCITY = 200f;
	public const float MAX_VELOCITY = 300f;
    public bool IsSpeaking;
    private Rigidbody _rb;

	private bool _controlling;

	private Quaternion _lastRotation;

	// Use this for initialization
	public void Start() {
		this.gameObject.tag = "Character";
		enabled = photonView.isMine;
		Character = GetComponent<Character>();
		_rb = GetComponent<Rigidbody> ();
        IsSpeaking = GetComponent<PhotonVoiceRecorder>().IsTransmitting;
		_controlling = false;
	}

	public void SetControllable() {
		CameraControl cc = this.GetComponentInChildren<CameraControl> ();
		Camera c = this.GetComponentInChildren<Camera> ();
		AudioListener al = this.GetComponentInChildren<AudioListener> ();
		cc.MTarget = this.transform;
		cc.enabled = true;
		c.enabled = true;
		al.enabled = true;
	}


	// Update is called once per frame
    private void Update () {

		if (!photonView.isMine) {
			return;
		}
			
		if (!_controlling && PhotonNetwork.playerName == this.gameObject.GetComponent<Character> ().UserName) {
			SetControllable ();
			_controlling = true;
		}

		// Detect user input of movement
		GameObject joyStick = GameObject.FindGameObjectWithTag ("JoyStick");
		if (joyStick == null) {
			return;
		}
		VirtualJoyStick vjs = joyStick.GetComponent<VirtualJoyStick> ();
		Vector3 joyStickMovement = vjs.GetStickPosition();
		if (joyStickMovement != Vector3.zero) {
			_rb.AddForce (joyStickMovement * VELOCITY, ForceMode.Acceleration);
		}
		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity,MAX_VELOCITY);


		if (!joyStickMovement.Equals (Vector3.zero)) {
			Vector3 targetDir = joyStickMovement;
			float step = 10;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			_lastRotation = Quaternion.LookRotation (newDir);
			transform.rotation = _lastRotation;

			photonView.RPC ("PlayAnim", PhotonTargets.All, "Move|Move");

		} else {
			transform.rotation = _lastRotation;
			photonView.RPC ("PlayAnim", PhotonTargets.All, "Move|Idle");

		}

	}

	[PunRPC]
	private void PlayAnim(string name) {
		Animation anim = transform.GetChild(3).GetComponent<Animation>();
		if (anim.IsPlaying ("Move|Cast")) {
			return;
		}
		anim.Play (name);
	}





}
