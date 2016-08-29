using UnityEngine;
using System.Collections;

public class ItemButtonScript : MonoBehaviour {

	public ShopController controller;
	public Item item { get; set; }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick() {
		controller.test ();
		// TODO Enabled only item can be assigned an instance
		// controller.selectedItem = this.item;
	}
}
