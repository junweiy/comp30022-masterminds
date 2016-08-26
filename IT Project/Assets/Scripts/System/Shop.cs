using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shop : MonoBehaviour {

	private HashSet<Item> items {
		get { return items; }
		set { items = value; }
	}


	public bool canPurchase(Item item, Character character) {
		return (character.getCoin() >= item.getPrice()) && character.hasSpaceForItem(item);
	}

	public void purchase(Item item, Character character) {
		if (canPurchase (item, character)) {
			character.addItem (item);
			character.deductCoin (item.getPrice ());
		}
	}

	public HashSet<Item> getPurchasableItems(Character character) {
		return (HashSet<Item>) this.items.Where(i => this.canPurchase(i, character));
	}

	// create a empty shop
	public Shop() {
		this.items = new HashSet<Item> ();
	}

	// create a shop with provided items
	public Shop(IEnumerable<Item> items) {
		this.items = new HashSet<Item> (items);
	}

}
