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
    private int coin;

    private bool isDead;

    private List<Item> items;

    public Character()
    {
        maxHp = 100f;
        hp = 100f;
    }
	
    public void TakeDamage(float f)
    {
        hp -= f;
        if(hp <= 0 && !isDead)
        {
            OnDeath();
        }

    }

    private void OnDeath()
    {
        isDead = true;
    }

    public void deductCoin(int c)
    {
        coin -= c;
    }


    public void addItem(Item i)
    {

    }

    public bool hasSpaceForItem()
    {
        return items.Count <= MAXIMUM_NUMBER_OF_ITEM;
    }

    public float HP {
        get { return this.hp; }
        set { this.hp = value; }
    }

    public float MaxHP
    {
        get { return this.maxHp; }
        set { this.maxHp = value; }
    }

    public int Coin
    {
        get { return this.coin; }
        set { this.coin = value; }
    }

}
