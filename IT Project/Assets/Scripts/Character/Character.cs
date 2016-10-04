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

	public float hp;
	private float maxHp { get; set; }
	public int score { get; private set; }
	public int coin { get; private set; }

	public bool canMove { get; set; }
	public bool isDead { get; private set; }
	public int numKilled;
	public int numDeath;

    public List<Item> items { get; private set; }

	public float range { get; set; }

	private HealthBarUI healthBarUI;
    
    void Start()
    {
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		charID = photonView.viewID;
        maxHp = 100f;
        hp = 100f;
		score = 0;
		coin = 0;
		numDeath = 0;
		numKilled = 0;
		isDead = false;
		canMove = true;
        items = new List<Item>();
    }

	void Update() {
		healthBarUI.SetHealthUI(HP,MaxHP);
	}


    /*****/


    public void AddItem(Item i)
    {
		if (items.Count < MAXIMUM_NUMBER_OF_ITEM) {
			items.Add(i);
		} else {
			throw new FullItemException ();
		}
        
    }

    public bool HasSpaceForItem()
    {
        return items.Count < MAXIMUM_NUMBER_OF_ITEM;
    }

    /*****/
    public float HP {
        get { return this.hp; }
        set { this.hp = value; }
    }

    public float MaxHP
    {
        get { return this.maxHp; }
        set { this.maxHp = value; }
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

    /**
     *  Coin and relative function
     */

	public void AddCoin(int c) {
		this.coin += c;
	}

    public void DeductCoin(int c)
    {
		this.coin -= c;
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
