using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpellUIController : MonoBehaviour {

    public GameObject spellIcon;
    private GameObject spellBar_go;
    private Character character;
    private bool isMainCharacter;
    private List<Spell> spells;
    private GameObject [] icons;

    public void initialise(Character c)
    {
        character = c;
        spells = c.spells;
    }

    public void setAsMainCharacter()
    {
        isMainCharacter = true;
    }

    // Use this for initialization
    void Start () {
        spellBar_go = GameObject.FindGameObjectWithTag("SpellBar");
        //generate buttons
        if (isMainCharacter)
        {
            for (int i = 0; i < spells.Count; i++)
            {
                GameObject spellButton = (GameObject)Instantiate(spellIcon);
                Image[] images = spellButton.GetComponentsInChildren<Image>();
                images[0].sprite = (Sprite)Resources.Load(spells[i].iconPath, typeof(Sprite));
                images[1].sprite = (Sprite)Resources.Load(spells[i].iconPath, typeof(Sprite));
                spellButton.transform.SetParent(spellBar_go.transform, false);
                spellButton.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        icons = spellBar_go.GetComponentsInChildren<GameObject>();
        for (int i = 0; i < icons.Length; i++)
        {
            if (spells[i].currentCooldown < spells[i].cooldown)
            {
                Image[] images;
                images = icons[i].GetComponentsInChildren<Image>();
                Image spellImage = images[1];
                spells[i].currentCooldown += Time.deltaTime;
                spellImage.fillAmount = spells[i].currentCooldown / spells[i].cooldown;
            } 
        }
    }
}
