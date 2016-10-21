
using UnityEngine;
using UnityEngine.UI;


// This script will handle the health bar UI
public class HealthBarUi : MonoBehaviour
{
    public Slider Slider;                             // The slider to represent how much health the character currently has.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public Image FillImage;                          // The image fills the slider
    
    public void SetHealthUi(float current, float maximum)
    {
		Slider.value = current / maximum;
        //Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
		FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, current / maximum);
    }

    
}