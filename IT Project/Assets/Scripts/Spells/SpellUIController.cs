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
                images[0] = (Image)Resources.Load(spells[i].iconPath, typeof(Texture2D));
                images[1] = (Image)Resources.Load(spells[i].iconPath, typeof(Texture2D));
                spellButton.transform.SetParent(spellBar_go.transform, false);
                spellButton.transform.localScale = new Vector3(1, 1, 1);
                icons[i] = spellButton;
                Debug.Log("icons " + i.ToString() + " is " + (icons[i] == null).ToString());
            }
        }
        
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
                spells[i].currentCooldown += Time.deltaTime;
                spellImage.fillAmount = spells[i].currentCooldown / spells[i].cooldown;
            } 
        }
    }
}
