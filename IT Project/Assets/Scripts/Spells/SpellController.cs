using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class SpellController : Photon.MonoBehaviour {
	// The distance of spawned fireball from player
	private float FIREBALL_SPAWN_DISTANCE = 30f;

	FireBall fb;
	FireNova fn;

	public List<Action<Spell, Transform>> onCastSpellActions = new List<Action<Spell, Transform>>();

	// the character model
	public Character character;

    //Spell UI script
    private SpellIconController fireBallUI;
    private SpellIconController fireNovaUI;


    void Start() {
		fb = new FireBall ();
		fn = new FireNova ();
        if (character.photonView.isMine)
        {
            fireBallUI = GameObject.Find("FireBallIcon").GetComponent<SpellIconController>();
            fireNovaUI = GameObject.Find("FireNovaIcon").GetComponent<SpellIconController>();
            fireBallUI.spell = fb;
            fireNovaUI.spell = fn;
        }
        
	}

	void Update() {
		// Update cool down time for all spells
		updateCoolDown(new Spell[]{fb,fn});

        if (!photonView.isMine) {
			return;
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
		fbc.damage = fb.damage;
	}

	[PunRPC]
	void SetUpVariableFireNova(int viewID) {
		FireNovaController fnc = PhotonView.Find (viewID).gameObject.GetComponent<FireNovaController>();
		fnc.charID = photonView.viewID;
		fnc.damage = fn.damage;
		fnc.power = fn.power;
		fnc.range = fn.range;
		fnc.castingTime = fn.castingTime;
	}


	public FireBallController CastFireBall () {

		GameObject fb;
		Quaternion destinationAngle;
		Vector3 joyStickMovement = GameObject.FindGameObjectWithTag ("JoyStick").GetComponent<VirtualJoyStick> ().GetStickPosition();
		Vector3 spawnPosition = this.transform.position + joyStickMovement * FIREBALL_SPAWN_DISTANCE;

		if (joyStickMovement != Vector3.zero) {
			destinationAngle = Quaternion.LookRotation (joyStickMovement);
			fb = PhotonNetwork.Instantiate ("Prefabs/Fireball", spawnPosition, destinationAngle, 0);
			foreach (var a in onCastSpellActions) {
				a (new FireBall (), fb.transform);
			}
			return fb.GetComponent<FireBallController> ();
		}



		return null;

	}
		

	public FireNovaController CastFireNova() {

		GameObject fn = PhotonNetwork.Instantiate ("Prefabs/FireNova", this.transform.position, this.transform.rotation, 0);

		foreach (var a in onCastSpellActions) {
			a (new FireNova (), fn.transform);
		}

		return fn.GetComponent<FireNovaController> ();

	}
		
    public void CastSpell(Spell spell)
    {
		photonView.RPC ("PlayAnim", PhotonTargets.All, "Move|Cast");

        if (spell is FireBall)
        {
            FireBallController fbc = CastFireBall();
			if (fbc == null) {
				return;
			}
            photonView.RPC("SetUpVariableFireBall", PhotonTargets.All, fbc.photonView.viewID);
            fb.currentCooldown = 0;
        }
        if (spell is FireNova)
        {
            FireNovaController fnc = CastFireNova();
            photonView.RPC("SetUpVariableFireNova", PhotonTargets.All, fnc.photonView.viewID);
            fn.currentCooldown = 0;
        }
    }
}
