using UnityEngine;
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

	public int hp { get; set; }
	private int maxHp { get; set; }

	public bool isDead { get; private set; }
	public int numKilled;
	public int numDeath;


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
		isDead = true;
		numDeath++;
		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		if (gc.CheckIfGameEnds ()) {
			UpdateProfile (false);
			gc.DisplayGameOverMessage ();
		}
		Destroy (this.gameObject);
    }

	public void Killed() {
		numKilled++;
		GameController gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
		if (gc.CheckIfGameEnds ()) {
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
