using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Character.
/// This is the model class of Character. It contains information about the character
/// and the logic.
/// </summary>
public class Character {

    public const float DEFAULT_HP = 100f;
    public const int MAXIMUM_NUMBER_OF_ITEM = 6;

	public int baseAttack { get; set; }
	public float castingRange { get; set; }
	public float maxHp { get; set; }
	public float hp { get; set; } 
	public int score { get; private set; }
	public bool canMove { get; set; }
	public List<Spell> spells { get; set; }
	public int coin { get; set; }
    public Profile player { get; set; }

	public List<Character> killedCharacter{ get; set;}
	public Character lastDamagedCharacter{ get; set; }
	public int numKill { get; private set; }
	public int numDeath { get; private set; }

	private string SPELL_SPAWN_NAME = "SpellSpawn";
	private List<Item> items;
	//The state of the character, whether he is aming to cast or casting or selecting
	private enum State {Waiting, Aiming, Casting, Moving, Died}; 
	private State state;
	private Spell selectedSpell;

    public Character()
    {
		baseAttack = 0;
        maxHp = 100f;
        hp = 100f;
		score = 0;
		coin = 0;
		numKill = 0;
		numDeath = 0;
        items = new List<Item>();
		spells = new List<Spell> ();
		killedCharacter = new List<Character> ();
		addSpell (new FireBall ());
		addSpell (new FireNova ());
    }


    public void addSpell(Spell i)
    {
        spells.Add(i);
    }
		
    public void addItem(Item i)
    {
        items.Add(i);
    }

    public bool hasSpaceForItem()
    {
        return items.Count <= MAXIMUM_NUMBER_OF_ITEM;
    }
		
	public void takeDamage(float f,Character fromCharacter)
    {
		hp -= f;
		lastDamagedCharacter = fromCharacter;
		if (hp <= 0 && state != State.Died)
		{
			onDeath();
		}
    }

	public void loseHealth(float f){
		hp -= f;
		if (hp <= 0 && state != State.Died)
		{
			onDeath();
		}
	}

	public void recordKill(Character c){
		killedCharacter.Add (c);
		addScore (1);
		numKill += 1;
	}

    private void onDeath()
    {
		lastDamagedCharacter.recordKill (this);
		state = State.Died;
		GlobalState.instance.gameController.onCharacterDeath ();
		addScore (-3);
		numDeath += 1;
    }

    /**
     *  Coin and relative function
     */

    public void deductCoin(int c)
    {
        coin -= c;
    }

	public int addScore(int s) {
		this.score += s;
		return this.score;
	}

	public bool canCast(){
		return state == State.Aiming;
	}

	public Spell getSelectedSpell(){
		return selectedSpell;
	}

	public void selectSpell(Spell s){
		//select again to cancel select
		if (state == State.Aiming) {
			state = State.Waiting;
			return;
		}
		// Reject casting if cool down has not finished
		if (s.currentCooldown < s.cooldown) {
			throw new UnityException("Cooldown");
		}
		selectedSpell = s;
		state = State.Aiming;
	}

	public void cast(Spell s, Transform transform, Vector3 destination) {
		if (state == State.Died) {
			throw new UnityException ("Died");
		}

		if (state == State.Casting){
			throw new UnityException ("Casting");
		}

		// Reject casting if cool down has not finished
		if (s.currentCooldown < s.cooldown) {
			throw new UnityException("Cooldown");
		}
		s.applyEffect(this, transform, destination);
		s.currentCooldown = 0;
		state = State.Waiting;
		selectedSpell = null;
	}

	public void castSelected(Transform transform,Vector3 destination){
		cast (selectedSpell, transform, destination);
	}

}
