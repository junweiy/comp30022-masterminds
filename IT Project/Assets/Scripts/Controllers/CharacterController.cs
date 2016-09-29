using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : Photon.MonoBehaviour {

	// the character model
	public Character character;
	private NavMeshAgent navMeshAgent;

	// Use this for initialization
	public void Start() {
		this.gameObject.tag = "Character";
		enabled = photonView.isMine;
		character = GetComponent<Character>();
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
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

		if (Input.GetMouseButton (1)) {
			Move ();
		}

		if (Input.GetButton ("Fire1")) {
			Move ();
		}

	}
		
	private void Move()
	{
		if (this.GetComponent<SpellController>().spellRange.enabled == true) {
			this.GetComponent<SpellController>().spellRange.enabled = false;
		}

		// quick fix only
		if (navMeshAgent == null) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100)) {
			navMeshAgent.destination = hit.point;
			navMeshAgent.Resume();

		}
	}

	/* This function will cast spell based on the spell number stored in the character */
	//	private void Cast(Spell s)
	//	{
	//
	//		// Reject casting if cool down has not finished
	//		if (s.currentCooldown < s.cooldown) {
	//			return;
	//		}
	//
	//		if (s.isInstantSpell) {
	//			// Find the transform of spell spawning point for instant spells
	//			Transform t = transform.Find (SPELL_SPAWN_NAME);
	//			s.applyEffect(character, transform, t.position);
	//		} else {
	//			// WuliPangPang please fix this
	//			spellRange.enabled = true;
	//			spellRange.transform.localScale *= s.range+character.range;
	//
	//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	//			RaycastHit hit;
	//			if (Physics.Raycast(ray, out hit, 100))
	//			{
	//				s.applyEffect(character, transform, hit.point);
	//			}
	//			spellRange.enabled = false;
	//		}
	//
	//		// Reset the cool down
	//		s.currentCooldown = 0;
	//	}


}
