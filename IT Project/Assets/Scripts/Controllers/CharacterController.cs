using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : Photon.MonoBehaviour {

	// the character model
	public Character character;

	public const float VELOCITY = 100f;
	public const float MAX_VELOCITY = 200f;
	Rigidbody rb;

	// Use this for initialization
	public void Start() {
		this.gameObject.tag = "Character";
		enabled = photonView.isMine;
		character = GetComponent<Character>();
		rb = GetComponent<Rigidbody> ();
	}

	public void SetControllable() {
		CameraControl cc = this.GetComponentInChildren<CameraControl> ();
		Camera c = this.GetComponentInChildren<Camera> ();
		AudioListener al = this.GetComponentInChildren<AudioListener> ();
		cc.m_Target = this.transform;
		cc.enabled = true;
		c.enabled = true;
		al.enabled = true;
	}


	// Update is called once per frame
	void Update () {

		if (!photonView.isMine) {
			return;
		}

		if (Input.GetKey(KeyCode.R)) {
			Debug.Log ("Chatting");
			this.GetComponent<PhotonVoiceRecorder> ().Transmit = true;
		}

		if (Input.GetKeyUp (KeyCode.R)) {
			Debug.Log ("Stop Chatting");
			this.GetComponent<PhotonVoiceRecorder> ().Transmit = false;
		}

		// Detect user input of movement

		Vector3 joyStickMovement = GameObject.FindGameObjectWithTag ("JoyStick").GetComponent<VirtualJoyStick> ().GetStickPosition();
		if (joyStickMovement != Vector3.zero) {
			rb.AddForce (joyStickMovement * VELOCITY, ForceMode.Acceleration);
			Debug.Log (joyStickMovement * VELOCITY);
		}
		rb.velocity = Vector3.ClampMagnitude (rb.velocity,MAX_VELOCITY);

	}
		


}
