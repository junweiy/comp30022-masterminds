using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shop {

	private HashSet<Item> items;

	public bool canPurchase(Item item, Character character) {
		return (character.getCoin() >= item.getPrice()) &&
			character.hasSpaceForItem(item) &&
			character.hasItem(item);
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
		//HashSet<Item> a = new HashSet<Item> ();
		//Debug.Log (a);
		this.items = new HashSet<Item> ();
	}
		

	// create a shop with provided items
	public Shop(IEnumerable<Item> items) {
		this.items = new HashSet<Item> (items);
	}

}
