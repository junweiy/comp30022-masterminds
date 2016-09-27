using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */ 
public class Character {

	// HP that character is initialised with
    public const float DEFAULT_HP = 100f;
	// Maximum number of items that character can carry
    public const int MAXIMUM_NUMBER_OF_ITEM = 6;
	// Default base attack point
	public const int DEFAULT_BASE_ATTACK = 0;
	// Default score
	public const int DEFAULT_SCORE = 0;
	// Default coin
	public const int DEFAULT_COIN = 0;


	// Base attack point 
	public int baseAttack { get; set; }
	// Current HP
	public float hp { get; private set; }
	// Upper limit of HP
	public float maxHp { get; set; }
	// Score gained during the game
	public int score { get; private set; }
	// Number of coins player gained during the game
	public int coin { get; private set; }
	// Whether the character is able to move
	public bool canMove { get; set; }
	// Whether the character is dead
	public bool isDead { get; private set; }
	// List of items that the character currently has
    public List<Item> items { get; private set; }
	// List of spells that the character currently has
	public List<Spell> spells { get; private set; }
	// The base maximum fly distance of spell projectile
	public float range { get; set; }
    
	// Type constructor of character
    public Character() {
		baseAttack = DEFAULT_BASE_ATTACK;
		maxHp = DEFAULT_HP;
		hp = DEFAULT_HP;
		score = DEFAULT_SCORE;
		coin = DEFAULT_COIN;
		isDead = false;
		canMove = true;
        items = new List<Item>();
		spells = new List<Spell> ();
		AddSpell (new FireBall ());
		AddSpell (new FireNova ());
    }

	// Method to add spell to the inventory of current player
    public void AddSpell(Spell i) {
		spells.Add (i); 
    }
		
	// Method to add item when there is place remaining
    public void AddItem(Item i) {
		if (items.Count < MAXIMUM_NUMBER_OF_ITEM) {
			items.Add(i);
		} else {
			throw new FullItemException ();
		}
    }

	// Whether there is more place to enable player to buy more item
    public bool HasSpaceForItem() {
        return items.Count < MAXIMUM_NUMBER_OF_ITEM;
    }

	// Method to take damage
    public void TakeDamage(float f)  {
        hp -= f;
        if (hp <= 0 && !isDead) {
            OnDeath();
        }
    }

	// Method to clean up the character after death
    private void OnDeath() {
        isDead = true;
		if (GlobalState.instance.gameController != null) {
			GlobalState.instance.gameController.onCharacterDeath ();
		}
    }
		
	// Method to add coin
	public void AddCoin(int c) {
		this.coin += c;
	}

	// Method to deduct coin
    public void DeductCoin(int c) {
		this.coin -= c;
    }

	// Method to add score
	public int AddScore(int s) {
		this.score += s;
		return this.score;
	}

	// Method to deduct score
	public int DeductScore(int s) {
		this.score = Mathf.Max(this.score - s, 0);
		return this.score;
	}
}
