using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour {

    // The shop
    private Shop shop;
    // The main character
    private Character character;
    // Icon prefab of item
    public GameObject shopIcon;
    // Panel of spell list
    public RectTransform spellPanel;
    // Panel of equipment list
    public RectTransform equipmentPanel;
    // Panel of upgrade list
    public RectTransform upgradePanel;

    public void initialise(Shop shop, Character character)
    {
        this.shop = shop;
        this.character = character;
        Debug.Assert(this.character != null);
    }

    void Start()
    {
        // Get all purchaseable items and generate icons in proper list
        foreach (Item item in shop.getPurchasableItems(this.character))
        {
            GameObject goButton = (GameObject)Instantiate(shopIcon);
            goButton.GetComponentInChildren<Text>().text = item.itemName;
            goButton.GetComponent<Image>().sprite = Resources.Load<Sprite>(item.iconPath);
            var itemButtonController = goButton.GetComponent<ItemButtonScript>();
            itemButtonController.initialise(character, item, shop);
            if (item.itemType == ItemTypeEnum.Spell)
            {
                goButton.transform.SetParent(spellPanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (item.itemType == ItemTypeEnum.Upgrade)
            {
                goButton.transform.SetParent(upgradePanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                goButton.transform.SetParent(equipmentPanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);
            }
            
            
        }


    }

}
