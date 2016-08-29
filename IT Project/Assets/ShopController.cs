using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	private Shop shop;
	private Character character;

	// Use this for initialization
	void Start () {
		this.shop = new Shop ();
		this.character = GameController.playerCharacter;
	}

	public bool canPurchase(Item item) {
		return this.shop.canPurchase (item, character);
	}

	public void purchase(Item item) {
		this.shop.purchase (item, character);
	}

	public void nextRound() {
		StateController.switchToNewRound ();
	}


	// Update is called once per frame
	void Update () {
	}
}
