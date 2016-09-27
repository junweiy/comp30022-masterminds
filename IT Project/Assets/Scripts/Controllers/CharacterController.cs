using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CharacterController : NetworkBehaviour {

	// the character model
	public Character character;

	private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;
	public bool isMainCharacter { get; set;}
	public GameObject mainCamera;

	// spell model

	// The name of object used to spawn spells
	private string SPELL_SPAWN_NAME = "SpellSpawn";
	// The image of spell range
	public Image spellRange;

	// Use this for initialization
	public void Start() {
		character = GetComponent<Character>();

		this.isMainCharacter = false;
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		this.gameObject.tag = "Character";
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		if (isLocalPlayer) {
			mainCamera.GetComponent<CameraControl> ().m_Target = transform;
			mainCamera.GetComponent<CameraControl> ().enabled = true;
			mainCamera.GetComponentInChildren<Camera> ().enabled = true;
			mainCamera.GetComponentInChildren<Camera> ().GetComponent<AudioListener> ().enabled = true;
		} else {
			mainCamera.GetComponent<CameraControl> ().enabled = false;
			mainCamera.GetComponentInChildren<Camera> ().enabled = false;
			mainCamera.GetComponentInChildren<Camera> ().GetComponent<AudioListener> ().enabled = false;
		}

		spellRange.enabled = false;
	}
		

	// Update is called once per frame
	void Update () {
		healthBarUI.SetHealthUI(character.HP,character.MaxHP);
		// Update cool down time for all spells
		foreach (Spell s in character.spells) {
			if (s.currentCooldown < s.cooldown) {
				s.currentCooldown++;
			}
		}

		if (!isLocalPlayer) {
			return;
		}
			

		// Detect user input of movement

		if (character.canMove) {
			if (Input.GetMouseButton (1)) {
				Move ();
			}

			if (Input.GetButton ("Fire1")) {
				Move ();
			}
		}
			

		// Detect user input of casting spells
		if (Input.GetKeyDown ("1")) {
			Cast (character.spells[0]);
		}
		if (Input.GetKeyDown ("2")) {
			Cast (character.spells[1]);
		}
			

    }

	void Cast(Spell s) {
		if (s.currentCooldown < s.cooldown) {
			return;
		}
		if (s is FireBall) {
			CmdCastFireBall (s as FireBall, gameObject);
		}
		if (s is FireNova) {
			CmdCastFireNova (s as FireNova, gameObject);
		}

		s.currentCooldown = 0;
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
		if (spellRange.enabled == true) {
			spellRange.enabled = false;
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

	[Command]
	void CmdCastFireBall(FireBall fb, GameObject gO) {
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
		Transform t = transform.Find (SPELL_SPAWN_NAME);
		fb.ApplyEffect(gO.GetComponent<CharacterController>().character, transform, t.position);
	}

	[Command]
	void CmdCastFireNova(FireNova fn, GameObject gO) {
		Transform t = transform.Find (SPELL_SPAWN_NAME);
		fn.ApplyEffect(gO.GetComponent<CharacterController>().character, transform, t.position);
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
