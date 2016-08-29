using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	private Shop shop;
	public ItemInfoPanelScript infoPanel;

	public Item selectedItem {
		get {
			return selectedItem;
		}
		set {
			this.selectedItem = value;
			infoPanel.updateInfo (value);
		}
	}

	// Use this for initialization
	void Start () {
	}

	public bool canPurchase(Item item) {
		return this.shop.canPurchase (item, GlobalState.getInstance().currentChar);
	}

	public void purchase() {
		this.shop.purchase (selectedItem, GlobalState.getInstance().currentChar);
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
