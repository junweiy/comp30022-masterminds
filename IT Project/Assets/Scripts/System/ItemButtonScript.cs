using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemButtonScript : MonoBehaviour {

	public ShopController controller;
	public Item item {get; set;}
    public GameObject infoPanel;
    private GameObject canvas;
    private Transform window;


	// Use this for initialization
	void Start () {
		item = new LifeNecklace (); // TODO to be deleted after proper item button generation
	}
	
	// Update is called once per frame
	void Update () {
	
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
        panel.transform.SetParent(window, false);
        panel.transform.localScale = new Vector3(1, 1, 1);
        infoPanel.SetActive(true);
    }
   
}
