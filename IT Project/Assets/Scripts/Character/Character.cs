using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */ 
public class Character : NetworkBehaviour {

    private const float DEFAULT_HP = 100f;
    public const int MAXIMUM_NUMBER_OF_ITEM = 6;

	public int baseAttack { get;set; }
	[SyncVar]
    private float hp; 
	private float maxHp { get; set; }
	public int score { get; private set; }
	public int coin { get; private set; }

	public bool canMove { get; set; }
	public bool isDead { get; private set; }

    public List<Item> items { get; private set; }
	public List<Spell> spells { get; set; }

	public float range { get; set; }
    
    void Start()
    {
		baseAttack = 0;
        maxHp = 100f;
        hp = 100f;
		score = 0;
		coin = 0;
		isDead = false;
		canMove = true;
        items = new List<Item>();
		spells = new List<Spell> ();
		AddSpell (new FireBall ());
		AddSpell (new FireNova ());
    }



    /*****/

    public void AddSpell(Spell i)
    {
		spells.Add (i);
        
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
        if (hp <= 0 && !isDead)
        {
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
