using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */ 
public class Character : Photon.MonoBehaviour {

    private const float DEFAULT_HP = 100f;
    public const int MAXIMUM_NUMBER_OF_ITEM = 6;

	public int charID;

	public int hp;
	private int maxHp { get; set; }

	public bool isDead { get; private set; }
	public int numKilled;
	public int numDeath;

	public List<Action> onDeathActions;

	public float range { get; set; }

	private HealthBarUI healthBarUI;
    
    void Start()
    {
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		charID = photonView.viewID;
        maxHp = 100;
        hp = 100;
		numDeath = 0;
		numKilled = 0;
		isDead = false;
		onDeathActions = new List<Action> ();
    }

	void Update() {
		healthBarUI.SetHealthUI(hp,maxHp);
	}
		
    public void TakeDamage(int f)
    {
        hp -= f;
        if (hp <= 0 && !isDead) {
            OnDeath();
        }

    }

    private void OnDeath()
    {
		if (isDead) {
			return;
		}
		isDead = true;
		numDeath++;
		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		if (GameController.CheckIfGameEnds ()) {
			UpdateProfile (false);
			gc.DisplayGameOverMessage ();
		}
		if (photonView.isMine) {
			DisableAndObserveOtherPlayer ();
		}

		foreach (var a in onDeathActions) {
			a ();
		}

    }

	void DisableAndObserveOtherPlayer() {
		FocusCameraOnOtherPlayer ();
		MoveToHiddenPlace ();
		DisableUI ();
	}

	void MoveToHiddenPlace() {
		this.transform.position = new Vector3 (0,0,1000);
		this.transform.localScale = new Vector3 (0, 0, 0);
		this.GetComponent<CharacterController> ().enabled = false;
	}

	void FocusCameraOnOtherPlayer() {
		GameObject anotherPlayer = FindAnotherPlayerAlive ();
		transform.FindChild ("CameraRig").gameObject.SetActive (false);
		GameObject cameraRig = anotherPlayer.transform.FindChild ("CameraRig").gameObject;
		Debug.Log (anotherPlayer.GetComponent<Character>().charID);
		cameraRig.GetComponent<CameraControl> ().enabled = true;
		cameraRig.GetComponentInChildren<Camera> ().enabled = true;
		cameraRig.GetComponentInChildren<AudioListener> ().enabled = true;
	}

	GameObject FindAnotherPlayerAlive() {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Character");
		foreach (GameObject player in players) {
			if (!player.GetComponent<Character> ().isDead) {
				return player;
			}
		}
		return null;
	}

	void DisableUI() {
		GameObject.FindGameObjectWithTag ("JoyStick").SetActive (false);
		GameObject.FindGameObjectWithTag ("SpellButton").SetActive (false);
	}


	public void Killed() {
		numKilled++;
		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		if (GameController.CheckIfGameEnds ()) {
			UpdateProfile (true);
			gc.DisplayGameOverMessage ();
		}
	}
		
	public void UpdateProfile(bool win) {
		if (photonView.isMine) {
			ProfileHandler ph = GameObject.FindGameObjectWithTag ("ProfileHandler").GetComponent<ProfileHandler>();
			ph.UpdateProfile (this.numKilled, this.numDeath, win);
		}
	}
		
		
}
