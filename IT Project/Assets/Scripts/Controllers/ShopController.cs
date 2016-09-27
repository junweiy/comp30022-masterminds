using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	private Shop shop;
    private ItemInventory inventory;
    public GameObject shopSystem;
    public GameObject shopPane;
    public GameObject equipmentPane;
	public ItemInfoPanelScript infoPanel;

	private Item _selectedItem;

	public Item selectedItem {
		get {
			return _selectedItem;
		}
		set {
			this._selectedItem = value;
			//infoPanel.updateInfo (value);
		}
	}

	// Use this for initialization
	void Start () {
        this.inventory = shopSystem.GetComponent<ItemInventory>();
        this.inventory.initialise();
		this.shop = new Shop(inventory.items);
        var shopUIController = shopPane.GetComponent<ShopUIController>();
        Debug.Assert(GlobalState.instance.currentChar != null);
        shopUIController.initialise(this.shop, GlobalState.instance.currentChar);
	}

	public bool canPurchase(Item item) {
		return this.shop.canPurchase (item, GlobalState.instance.currentChar);
	}

	public void purchase() {
		this.shop.purchase (selectedItem, GlobalState.instance.currentChar);
	}

	public void nextRound() {
		StateController.switchToNewRound ();
	}

	// Update is called once per frame
	void Update () {
	}

	public void test() {
		Debug.Log ("Hello");
	}
}
