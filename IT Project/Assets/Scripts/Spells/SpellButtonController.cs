using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellButtonController : MonoBehaviour {

    public GameObject spellIcon;
    public Spell spell;
    public Image icon;

    void Start()
    {
        //Image[] images;
        //images = spellButton.GetComponentsInChildren<Image>();
        //icon = images[1];
    }
	public void onclick()
    {
        if (spell.currentCooldown >= spell.cooldown)
            {
                spell.currentCooldown = 0;
            }
    }

    void Update()
    {
        if (spell.currentCooldown < spell.cooldown)
        {
            Image[] images;
            images = icons[i].GetComponentsInChildren<Image>();
            Image spellImage = images[1];
            spells[i].currentCooldown += Time.deltaTime;
            spellImage.fillAmount = spells[i].currentCooldown / spells[i].cooldown;
        }
    }
}
