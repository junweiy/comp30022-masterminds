using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */ 
public class Character {

    private const float DEFAULT_HP = 100f;
    private const int MAXIMUM_NUMBER_OF_ITEM = 6;

	public int baseAttack { get;set; }
    private float hp; 
	private float maxHp { get; set; }
	public int score { get; private set; }
    private int coin;

	public bool canMove { get; set; }
    private bool isDead;

    public List<Item> items;
	public List<Spell> spells { get; set; }

	public float range { get; set; }
    
    public Character()
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
		addSpell (new FireBall ());
		addSpell (new FireNova ());
    }



    /*****/

    public void addSpell(Spell i)
    {
        spells.Add(i);
    }

    /*****/


    public void addItem(Item i)
    {
        items.Add(i);
    }

    public bool hasSpaceForItem()
    {
        return items.Count <= MAXIMUM_NUMBER_OF_ITEM;
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
		GlobalState.instance.gameController.onCharacterDeath ();
    }

    /**
     *  Coin and relative function
     */
    public int Coin
    {
        get { return this.coin; }
        set { this.coin = value; }
    }

    public void deductCoin(int c)
    {
        coin -= c;
    }

	public int addScore(int s) {
		this.score += s;
		return this.score;
	}

	public int deductScore(int s) {
		this.score = Mathf.Max(this.score - s, 0);
		return this.score;
	}

}
