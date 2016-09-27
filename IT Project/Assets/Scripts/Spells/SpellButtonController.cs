using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellButtonController : MonoBehaviour {

    public GameObject spellButton;
    public Spell spell;
    public Image icon;

    void Start()
    {
        Image[] images;
        images = spellButton.GetComponentsInChildren<Image>();
        icon = images[1];
    }
	public void onclick()
    {
        if (spell.currentCooldown >= spell.cooldown)
            {
                spell.currentCooldown = 0;
            }
    }
}
