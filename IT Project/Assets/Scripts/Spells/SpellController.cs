using UnityEngine;
using System.Collections.Generic;
using System;

public class SpellController : Photon.MonoBehaviour {
    // The distance of spawned fireball from player
    private float _fireballSpawnDistance = 30f;

    private FireBall _fb;
    private FireNova _fn;

    public List<Action<Spell, Transform, int>> OnCastSpellActions = new List<Action<Spell, Transform, int>>();

    // the character model
    public Character Character;

    //Spell UI script
    private SpellIconController _fireBallUi;
    private SpellIconController _fireNovaUi;


    private void Start() {
        _fb = new FireBall();
        _fn = new FireNova();
        if (Character.photonView.isMine) {
            _fireBallUi = GameObject.Find("FireBallIcon").GetComponent<SpellIconController>();
            _fireNovaUi = GameObject.Find("FireNovaIcon").GetComponent<SpellIconController>();
            _fireBallUi.Spell = _fb;
            _fireNovaUi.Spell = _fn;
        }
    }

    private void Update() {
        // Update cool down time for all spells
        UpdateCoolDown(new Spell[] {_fb, _fn});

        if (!photonView.isMine) {
            return;
        }
    }

    public void UpdateCoolDown(Spell[] spells) {
        foreach (Spell s in spells) {
            if (s.CurrentCooldown < s.Cooldown) {
                s.CurrentCooldown += Time.deltaTime;
            }
        }
    }

    [PunRPC]
    private void SetUpVariableFireBall(int viewId) {
        FireBallController fbc = PhotonView.Find(viewId).gameObject.GetComponent<FireBallController>();
        fbc.CharId = photonView.viewID;
        fbc.Damage = _fb.Damage;
    }

    [PunRPC]
    private void SetUpVariableFireNova(int viewId) {
        FireNovaController fnc = PhotonView.Find(viewId).gameObject.GetComponent<FireNovaController>();
        fnc.CharId = photonView.viewID;
        fnc.Damage = _fn.Damage;
        fnc.Power = _fn.Power;
        fnc.Range = _fn.Range;
        fnc.CastingTime = _fn.CastingTime;
    }

	// Cast FireBall according to joystick input
    public FireBallController CastFireBall() {
        GameObject fb;
        Quaternion destinationAngle;
        Vector3 joyStickMovement =
			GameObjectFinder.FindJoyStick().GetComponent<VirtualJoyStick>().GetStickPosition();
        Vector3 spawnPosition = this.transform.position + joyStickMovement*_fireballSpawnDistance;

        if (joyStickMovement != Vector3.zero) {
            destinationAngle = Quaternion.LookRotation(joyStickMovement);
            fb = PhotonNetwork.Instantiate("Prefabs/Fireball", spawnPosition, destinationAngle, 0);
            foreach (var a in OnCastSpellActions) {
                a(new FireBall(), fb.transform, -1);
            }
            return fb.GetComponent<FireBallController>();
        }


        return null;
    }

	// Cast FireNova and get controller
    public FireNovaController CastFireNova() {
        GameObject fn = PhotonNetwork.Instantiate(
            "Prefabs/FireNova", this.transform.position, this.transform.rotation,
            0);

        foreach (var a in OnCastSpellActions) {
            a(new FireNova(), fn.transform, -1); // TODO
        }

        return fn.GetComponent<FireNovaController>();
    }

	// Generic Method to cast spell 
    public void CastSpell(Spell spell) {
        photonView.RPC("PlayAnim", PhotonTargets.All, "Move|Cast");

        if (spell is FireBall) {
            FireBallController fbc = CastFireBall();
            if (fbc == null) {
                return;
            }
            photonView.RPC("SetUpVariableFireBall", PhotonTargets.All, fbc.photonView.viewID);
            _fb.CurrentCooldown = 0;
        }
        if (spell is FireNova) {
            FireNovaController fnc = CastFireNova();
            photonView.RPC("SetUpVariableFireNova", PhotonTargets.All, fnc.photonView.viewID);
            _fn.CurrentCooldown = 0;
        }
    }
}