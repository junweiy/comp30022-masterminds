
using UnityEngine;
using UnityEngine.UI;


// This script will handle the health bar UI
public class HealthBarUI : MonoBehaviour
{
    public Slider slider;                             // The slider to represent how much health the character currently has.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public Image fillImage;                          // The image fills the slider
    
    public void SetHealthUI(float current, float maximum)
    {
		slider.value = current / maximum;
        //Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
		fillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, current / maximum);
    }

    
}