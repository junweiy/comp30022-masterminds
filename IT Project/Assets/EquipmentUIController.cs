using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EquipmentUIController : MonoBehaviour {

    private Character character;
    public GameObject armour;
    public GameObject weapon;
    public GameObject trinket;

    public void initialise(Character character)
    {
        this.character = character;
    }

    void Start()
    {
        Text[] armourTexts = armour.GetComponentsInChildren<Text>();
        Image armourImage = armour.GetComponent<Image>();
        Text[] weaponTexts = weapon.GetComponentsInChildren<Text>();
        Image weaponImage = weapon.GetComponent<Image>();
        Text[] trinketTexts = trinket.GetComponentsInChildren<Text>();
        Image trinketImage = trinket.GetComponent<Image>();

        foreach (Item item in character.items)
        {
            if (item.itemType == ItemTypeEnum.Armour)
            {
                armourTexts[0].text = item.itemName;
                armourTexts[1].text = item.description;
                armourImage.sprite = Resources.Load<Sprite>(item.iconPath);
            }
            else if (item.itemType == ItemTypeEnum.Weapon)
            {
                weaponTexts[0].text = item.itemName;
                weaponTexts[1].text = item.description;
                weaponImage.sprite = Resources.Load<Sprite>(item.iconPath);
            }
            else if (item.itemType == ItemTypeEnum.Trinket)
            {
                trinketTexts[0].text = item.itemName;
                trinketTexts[1].text = item.description;
                trinketImage.sprite = Resources.Load<Sprite>(item.iconPath);
            }
        }
    }
}
