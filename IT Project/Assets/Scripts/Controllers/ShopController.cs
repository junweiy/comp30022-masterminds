using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	private Shop shop;
	public ItemInfoPanelScript infoPanel;

	private Item _selectedItem;

	public Item selectedItem {
		get {
			return _selectedItem;
		}
		set {
			this._selectedItem = value;
			infoPanel.updateInfo (value);
		}
	}

	// Use this for initialization
	void Start () {
		this.shop = new Shop();
	}

	public bool CanPurchase(Item item) {
		return this.shop.canPurchase (item, GlobalState.instance.currentChar);
	}

	public void Purchase() {
		this.shop.purchase (selectedItem, GlobalState.instance.currentChar);
	}

	public void NextRound() {
		StateController.switchToNewRound ();
	}

	// Update is called once per frame
	void Update () {
	}

	public void test() {
		Debug.Log ("Hello");
	}
}
