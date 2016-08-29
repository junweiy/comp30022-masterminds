using UnityEngine;
using System.Collections;

public class ItemButtonScript : MonoBehaviour {

	public ShopController controller;
	private Item item;

	// Use this for initialization
	void Start () {
		item = new LifeNecklace (); // TODO to be deleted after proper item button generation
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick() {
		controller.selectedItem = this.item;
	}
}
