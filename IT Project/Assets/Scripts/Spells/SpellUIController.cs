using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpellUIController : MonoBehaviour {

    public GameObject spellIcon;
    public RectTransform spellBar;
    public GameObject bar;
    private Character character;
    private List<Spell> spells;
    private GameObject [] icons;


	// Use this for initialization
	void Start () {
        //generate buttons
        spells = character.spells;
        //...
        icons = bar.GetComponentsInChildren<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < icons.Length; i++)
        {
            if (spells[i].currentCooldown < spells[i].cooldown)
            {
                Image[] images;
                images = icons[i].GetComponentsInChildren<Image>();
                Image spellImage = images[1];
                spellImage.fillAmount = spells[i].currentCooldown / spells[i].cooldown;
            } 
        }
    }
}
