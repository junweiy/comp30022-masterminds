using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellIconController : MonoBehaviour {

    public GameObject spellIcon;
    private Spell spell { set; get; }
    private Sprite icon { set; get; }
    private Image[] images;

    // Initialise the spell icon
//    public void initialise(Spell spell)
//    {
//        this.spell = spell;
//        this.icon = Resources.Load<Sprite>(spell.iconPath);
//        images = spellIcon.GetComponentsInChildren<Image>();
//        images[0].sprite = icon;
//        images[1].sprite = icon;
//        images[1].type = Image.Type.Filled;
//        images[1].fillMethod = Image.FillMethod.Radial360;
//        images[1].fillOrigin = (int)Image.Origin360.Top;
//    }

    // On click event
	public void onclick()
    {
        if (spell.currentCooldown >= spell.cooldown)
            {
                spell.currentCooldown = 0;
            }
    }

    // Update the display of image
    void Update()
    {
        if (spell.currentCooldown < spell.cooldown)
        {
            images[1].fillAmount = spell.currentCooldown / spell.cooldown;
        }
    }
}
