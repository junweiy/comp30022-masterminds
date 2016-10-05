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
    public bool isSpeaking;
	Rigidbody rb;

	// Use this for initialization
	public void Start() {
		this.gameObject.tag = "Character";
		enabled = photonView.isMine;
		character = GetComponent<Character>();
		rb = GetComponent<Rigidbody> ();
        isSpeaking = GetComponent<PhotonVoiceRecorder>().IsTransmitting;
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

		// Detect user input of movement

		Vector3 joyStickMovement = GameObject.FindGameObjectWithTag ("JoyStick").GetComponent<VirtualJoyStick> ().GetStickPosition();
		if (joyStickMovement != Vector3.zero) {
			rb.AddForce (joyStickMovement * VELOCITY, ForceMode.Acceleration);
		}
		rb.velocity = Vector3.ClampMagnitude (rb.velocity,MAX_VELOCITY);

	}	


}
