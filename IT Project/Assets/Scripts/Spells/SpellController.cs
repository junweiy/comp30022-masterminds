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
        
        spellRange.enabled = false;
	}

	void Update() {
		// Update cool down time for all spells
		updateCoolDown(new Spell[]{fb,fn});

        if (!photonView.isMine) {
			return;
		}

        // Detect user input of casting spells
        if (Input.GetKeyDown ("1") && fb.currentCooldown >= fb.cooldown) {
        //if (fireBallUI.isClicked && fb.currentCooldown >= fb.cooldown) { 
			FireBallController fbc = CastFireBall ();
			photonView.RPC ("SetUpVariableFireBall", PhotonTargets.All, fbc.photonView.viewID);
			fb.currentCooldown = 0;
            //fireBallUI.isClicked = false;
		}
        if (Input.GetKeyDown ("2")) {
        //if (fireNovaUI.isClicked && fn.currentCooldown >= fn.cooldown) { 
			FireNovaController fnc = CastFireNova ();
			photonView.RPC ("SetUpVariableFireNova", PhotonTargets.All, fnc.photonView.viewID);
			fn.currentCooldown = 0;
            //fireNovaUI.isClicked = false;
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
		Transform t = transform.Find (SPELL_SPAWN_NAME);
		Vector3 pos = this.transform.position + (t.position - this.transform.position) / 2;
		GameObject fb = PhotonNetwork.Instantiate ("Prefabs/Fireball", pos, new Quaternion(0,0,0,0), 0);
		return fb.GetComponent<FireBallController> ();
	}

	public FireNovaController CastFireNova() {
		GameObject fn = PhotonNetwork.Instantiate ("Prefabs/FireNova", this.transform.position, this.transform.rotation, 0);
		return fn.GetComponent<FireNovaController> ();
	}

    public void CastSpell(Spell spell)
    {
        if(spell is FireBall)
        {
            FireBallController fbc = CastFireBall();
            photonView.RPC("SetUpVariableFireBall", PhotonTargets.All, fbc.photonView.viewID);
            fb.currentCooldown = 0;
        }
        if(spell is FireNova)
        {
            FireNovaController fnc = CastFireNova();
            photonView.RPC("SetUpVariableFireNova", PhotonTargets.All, fnc.photonView.viewID);
            fn.currentCooldown = 0;
        }
    }

}
