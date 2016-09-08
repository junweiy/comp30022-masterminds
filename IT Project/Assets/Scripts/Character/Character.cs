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
    private float hp; 
    private float maxHp;
    private int score;
    private int coin = 20;

    private bool isDead;

    private List<Item> items;
    private List<Spell> spells;

	public float range { get; set; }
    
    public Character()
    {
        maxHp = 100f;
        hp = 100f;
        items = new List<Item>();
        spells = new List<Spell>();
		addSpell (new FireBall ());
    }



    /*****/

    public void addSpell(Spell i)
    {
        spells.Add(i);
    }

    public Spell getSpell(int i)
    {
        return spells.ToArray()[i];
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

}
