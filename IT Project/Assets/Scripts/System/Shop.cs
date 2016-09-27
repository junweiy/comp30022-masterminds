using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shop {

	private HashSet<Item> items;

	public bool canPurchase(Item item, Character character) {
        return (character.Coin >= item.purchasePrice) && character.hasSpaceForItem();
	}

	public void purchase(Item item, Character character) {
		if (canPurchase (item, character)) {
			character.addItem (item);
			character.deductCoin (item.purchasePrice);
		}
	}

	public HashSet<Item> getPurchasableItems(Character character) {
		var ret = new HashSet<Item> ();
		foreach (Item i in items) {
			if (canPurchase(i, character)) {
				ret.Add (i);
			}
		}
		return ret;
	}

	public HashSet<Item> getPurchaseableItemsWithType(Character character, ItemTypeEnum type) {
		var ret = new HashSet<Item> ();
		foreach (Item i in getPurchasableItems(character)) {
			if (i.itemType == type) {
				ret.Add (i);
			}
		}
		return ret;
	}

   
	// create a empty shop
	public Shop() {
		this.items = new HashSet<Item> ();
	}

	// create a shop with provided items
	public Shop(IEnumerable<Item> items) {
		this.items = new HashSet<Item> (items);
	}

    public Shop(List<Item> items)
    {
        this.items = new HashSet<Item>(items);
    }

}
