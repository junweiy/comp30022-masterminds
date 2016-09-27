using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemButtonScript : MonoBehaviour {

    private Character character;
	private Item item {get; set;}
    private Shop shop;
    public GameObject infoPanel;
    private GameObject canvas;
    private Transform window;

    public void initialise(Character character, Item item, Shop shop)
    {
        this.item = item;
        this.character = character;
        this.shop = shop;
    }

    
	public void onClick() {
		//controller.selectedItem = this.item;
        canvas = GameObject.FindGameObjectWithTag("ShopWindow");
        window = canvas.transform.Find("ShopWindow");
        GameObject panel = (GameObject)Instantiate(infoPanel);
        Transform nameTr = panel.transform.Find("ItemName");
        nameTr.GetComponent<Text>().text = item.itemName;
        Transform descriptionTr = panel.transform.Find("Description");
        descriptionTr.GetComponent<Text>().text = item.description;
        var itemInfoPanelScript = panel.GetComponent<ItemInfoPanelScript>();
        itemInfoPanelScript.initialise(item, character,shop);
        panel.transform.SetParent(window, false);
        panel.transform.localScale = new Vector3(1, 1, 1);
        infoPanel.SetActive(true);
    }
   
}
