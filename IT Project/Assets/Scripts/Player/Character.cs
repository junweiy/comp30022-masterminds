using UnityEngine;
using System.Collections;


/*
 *  This class is the main class for the character. It stores all the information about the character
 *  
 * 
 */ 
public class Character : MonoBehaviour {

    public float maximumHealth; 
    public float currentHealth;
    public int score;
    public int goldEarn;

	public GameObject fireBall;
	public Transform spellSpawn;

    private bool isDead;

    private CharacterNavigation nav;
    private HealthBar health;
    // Use this for initialization
    void Start () {
        nav = GetComponent<CharacterNavigation>();
        health = GetComponent<HealthBar>();
        isDead = false;
		health.SetHealth(currentHealth, maximumHealth);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            nav.Move();
        }

		if (Input.GetKey ("space")) {
			Instantiate (fireBall, spellSpawn.position, spellSpawn.rotation);
		}

        //TakeDamage(0.1f);

    }

	public int getCoin() {
		return this.goldEarn;
	}

	public bool hasSpaceForItem(Item item) {
		return true; // TODO
	}

	public void addItem(Item item) {
		return; // TODO
	}

	public void deductCoin(int coin) {
		this.goldEarn -= coin;
	}

	public bool hasItem (Item item) {
		return false; // TODO
	}

    public void TakeDamage(float f)
    {
        currentHealth -= f;
        health.SetHealth(currentHealth, maximumHealth);
        if(currentHealth <= 0 && !isDead)
        {
            OnDeath();
        }

    }

	public string getName() {
		return "Character"; // TODO
	}

    private void OnDeath()
    {
        isDead = true;
		GlobalState.instance.gameController.onCharacterDeath ();
        Destroy(this);
    }

	// For debugging only
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name != "Ground" && collision.gameObject.name != "Lava") {
			Debug.Log (collision.gameObject.name);
		}
	}




}
