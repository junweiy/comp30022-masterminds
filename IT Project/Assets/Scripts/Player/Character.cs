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

    private bool isDead;

    private CharacterNavigation nav;
    private HealthBar health;
    // Use this for initialization
    void Start () {
        nav = GetComponent<CharacterNavigation>();
        health = GetComponent<HealthBar>();
        isDead = false;
        health.SetHealth(maximumHealth, currentHealth);
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {
            nav.Move();
        }

        TakeDamage(0.1f);

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

    private void OnDeath()
    {
        isDead = true;
        Destroy(this);
    }




}
