
using UnityEngine;
using UnityEngine.UI;


// This script will handle the health bar UI
public class HealthBarUI : MonoBehaviour
{
	// slider representing how much health the character currently has
    public Slider slider;
	// The color the health bar will be when on full health
    public Color FullHealthColor = Color.green;
	// The color the health bar will be when on no health.
    public Color ZeroHealthColor = Color.red;
	// The image fills the slider
    public Image fillImage;
    
    public void SetHealthUI(float current, float maximum) {
		slider.value = current / maximum;
        //Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
		fillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, current / maximum);
    }

    
}