using UnityEngine;
using System.Collections;

public interface Item {

	// get the price of item
	int getPrice();

	// get a short description of the item
	string getDescription();
}
