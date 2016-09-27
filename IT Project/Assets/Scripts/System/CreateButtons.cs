using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateButtons : MonoBehaviour {

    public GameObject shopIcon;
    public ShopController controller;
    private LifeNecklace neck;
    private Shop shop;
    public RectTransform spellPanel;
    public RectTransform equipmentPanel;
    public RectTransform upgradePanel;


    void Start()
    {
        neck = new LifeNecklace();
        shop = new Shop(neck);

        for (int i = 0; i < 10; i++)
        {
            GameObject goButton = (GameObject)Instantiate(shopIcon);
            goButton.transform.SetParent(upgradePanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);

            //Button tempButton = goButton.GetComponent<Button>();
            //int tempInt = i;

            //tempButton.onClick.AddListener(() => ButtonClicked(tempInt));
        }

		foreach (Item item in shop.GetPurchasableItems(GlobalState.instance.currentChar))
        {
            GameObject goButton = (GameObject)Instantiate(shopIcon);
            goButton.GetComponentInChildren<Text>().text = item.itemName;
            goButton.transform.SetParent(equipmentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);
        }


    }

}
