
using UnityEngine;
using UnityEngine.UI;


// This script will handle the health bar UI
public class HealthBar : MonoBehaviour
{
    public Slider slider;                             // The slider to represent how much health the character currently has.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public Image fillImage;                          // The image fills the slider

    private float currentHealth;                   // How much health the tank currently has.
    private float maximumHealth;                   // The maximum health of the character

    public void SetHealth(float current, float maximum)
    {
        // When the character is enabled, it starts with full health.
        this.maximumHealth = maximum;
        this.currentHealth = current;
        SetHealthUI();
    }
    

    private void SetHealthUI()
    {
        slider.value = currentHealth / maximumHealth;
        //Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        fillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, currentHealth / maximumHealth);
    }

    
}