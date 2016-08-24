
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float maximumHealth = 100f;               // The maximum health of the character
    public Slider slider;                             // The slider to represent how much health the character currently has.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public Image fillImage;                          // The image fills the slider


    private float currentHealth;                      // How much health the tank currently has.
    private bool isDead;                              // is the character dead? health below 0


    private void Awake()
    {

    }


    private void OnEnable()
    {
        // When the tank is enabled, it starts with full health.
        currentHealth = maximumHealth;
        isDead = false;
        SetHealthUI();
        slider.value = currentHealth / maximumHealth;
        

    }


    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        currentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (currentHealth <= 0f && !isDead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        slider.value = currentHealth / maximumHealth;
        //Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, currentHealth / maximumHealth);
    }


    private void OnDeath()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}