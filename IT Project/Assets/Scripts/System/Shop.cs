using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Shop {

	private HashSet<Item> items;

	public bool CanPurchase(Item item, Character character) {
		return (character.coin >= item.purchasePrice) && character.HasSpaceForItem();
	}

	public void Purchase(Item item, Character character) {
		if (CanPurchase (item, character)) {
			character.AddItem (item);
			character.DeductCoin (item.purchasePrice);
		}
	}

	public HashSet<Item> GetPurchasableItems(Character character) {
		var ret = new HashSet<Item> ();
		foreach (Item i in items) {
			if (CanPurchase(i, character)) {
				ret.Add (i);
			}
		}
		return ret;
	}

	public HashSet<Item> getPurchaseableItemsWithType(Character character, ItemTypeEnum type) {
		var ret = new HashSet<Item> ();
		foreach (Item i in GetPurchasableItems(character)) {
			if (i.itemType == type) {
				ret.Add (i);
			}
		}
		return ret;
	}

    public void AddItem(Item item) {
        this.items.Add(item);
    }

   
	// create a empty shop
	public Shop() {
		this.items = new HashSet<Item> ();
	}

	// create a shop with provided items
	public Shop(IEnumerable<Item> items) {
		this.items = new HashSet<Item> (items);
	}

    public Shop(Item item)
    {
        this.items = new HashSet<Item>();
        this.items.Add(item);
    }

}
