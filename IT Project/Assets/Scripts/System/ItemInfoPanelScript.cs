using UnityEngine;
using System.Collections;

public class ItemInfoPanelScript : MonoBehaviour {

    private Item item;
    private Character character;
    private Shop shop;

    public void initialise(Item item, Character character, Shop shop)
    {
        this.item = item;
        this.character = character;
        this.shop = shop;
    }

    public void buyButton()
    {
        shop.purchase(item, character);
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }

    public void closeButton()
    {
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }
}
