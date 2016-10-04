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

	public float hp { get; set; }
	private float maxHp { get; set; }
	public int score { get; private set; }

	public bool isDead { get; private set; }
	public int numKilled;
	public int numDeath;


	public float range { get; set; }

	private HealthBarUI healthBarUI;
    
    void Start()
    {
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		charID = photonView.viewID;
        maxHp = 100f;
        hp = 100f;
		score = 0;
		numDeath = 0;
		numKilled = 0;
		isDead = false;
    }

	void Update() {
		healthBarUI.SetHealthUI(hp,maxHp);
	}
		
    public void TakeDamage(float f)
    {
        hp -= f;
        if (hp <= 0 && !isDead) {
            OnDeath();
        }

    }

    private void OnDeath()
    {
        isDead = true;
    }

	public int AddScore(int s) {
		this.score += s;
		return this.score;
	}

	public int DeductScore(int s) {
		this.score = Mathf.Max(this.score - s, 0);
		return this.score;
	}
		
}
