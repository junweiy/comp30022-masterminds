using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : Photon.MonoBehaviour {

	// the character model
	public Character character;

	private NavMeshAgent navMeshAgent;



	public void Awake() {
		this.gameObject.tag = "Character";
	}

	// Use this for initialization
	public void Start() {
		this.GetComponent<PhotonVoiceRecorder> ().Transmit = false;
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




	// For debugging only
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name != "Ground" && collision.gameObject.name != "Lava")
		{
			Debug.Log(collision.gameObject.name);

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

		if (Physics.Raycast(ray, out hit, 100))
		{
			navMeshAgent.destination = hit.point;
			navMeshAgent.Resume();

		}
	}


//	void CmdCastFireBall(FireBall fb, GameObject gO) {
		// TO-DO

		// Supposed to work as constant however the navmesh is not working
		//		Image spellRange = gO.GetComponent<CharacterController>().spellRange;
		//			
		//		spellRange.enabled = true;
		//		spellRange.transform.localScale *= fb.range+character.range;
		//
		//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//		RaycastHit hit;
		//		if (Physics.Raycast(ray, out hit, 100))
		//		{
		//			GameObject go = fb.applyEffect(character, transform, hit.point) as GameObject;
		//			NetworkServer.Spawn (go);
		//		}
		//		spellRange.enabled = false;
//		Transform t = transform.Find (SPELL_SPAWN_NAME);
//		fb.ApplyEffect(gO.GetComponent<CharacterController>().character, transform, t.position);
//	}

//	void CmdCastFireNova(FireNova fn, GameObject gO) {
//		Transform t = transform.Find (SPELL_SPAWN_NAME);
//		fn.ApplyEffect(gO.GetComponent<CharacterController>().character, transform, t.position);
//	}


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
