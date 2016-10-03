using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpellController : Photon.MonoBehaviour {
	// The name of object used to spawn spells
	private string SPELL_SPAWN_NAME = "SpellSpawn";

	// The image of spell range
	public Image spellRange;

	FireBall fb;
	FireNova fn;

	// the character model
	public Character character;

	void Start() {
		fb = new FireBall ();
		fn = new FireNova ();
		spellRange.enabled = false;
	}

	void Update() {
		// Update cool down time for all spells
		updateCoolDown(new Spell[]{fb,fn});

		if (!photonView.isMine) {
			return;
		}

		// Detect user input of casting spells
		if (Input.GetKeyDown ("1")) {
			FireBallController fbc = CastFireBall ();
			photonView.RPC ("SetUpVariableFireBall", PhotonTargets.All, fbc.photonView.viewID);
			fb.currentCooldown = 0;
		}
		if (Input.GetKeyDown ("2")) {
			FireNovaController fnc = CastFireNova ();
			photonView.RPC ("SetUpVariableFireNova", PhotonTargets.All, fnc.photonView.viewID);
			fn.currentCooldown = 0;
		}

	}

	public void updateCoolDown(Spell[] spells) {
		foreach (Spell s in spells) {
			if (s.currentCooldown < s.cooldown) {
				s.currentCooldown+=Time.deltaTime;
			}
		}
	}

	[PunRPC]
	void SetUpVariableFireBall(int viewID) {
		FireBallController fbc = PhotonView.Find (viewID).gameObject.GetComponent<FireBallController>();
		fbc.charID = photonView.viewID;
		fbc.damage = fb.damage + character.baseAttack;
	}

	[PunRPC]
	void SetUpVariableFireNova(int viewID) {
		FireNovaController fnc = PhotonView.Find (viewID).gameObject.GetComponent<FireNovaController>();
		fnc.charID = photonView.viewID;
		fnc.damage = fn.damage + character.baseAttack;
		fnc.power = fn.power;
		fnc.castingTime = fn.castingTime;
	}


	public FireBallController CastFireBall () {
		Transform t = transform.Find (SPELL_SPAWN_NAME);
		Vector3 pos = this.transform.position + (t.position - this.transform.position) / 2;
		GameObject fb = PhotonNetwork.Instantiate ("Prefabs/Fireball", pos, new Quaternion(0,0,0,0), 0);
		return fb.GetComponent<FireBallController> ();
	}

	public FireNovaController CastFireNova() {
		GameObject fn = PhotonNetwork.Instantiate ("Prefabs/FireNova", this.transform.position, this.transform.rotation, 0);
		return fn.GetComponent<FireNovaController> ();
	}

//	void Cast(Spell s) {
//		if (s.currentCooldown < s.cooldown) {
//			return;
//		}
//		if (s.isInstantSpell) {
//			Transform t = transform.Find (SPELL_SPAWN_NAME);
//			s.ApplyEffect(character, transform, t.position);
//		} else {
//
//		}
//		s.currentCooldown = 0;
//	}




}
