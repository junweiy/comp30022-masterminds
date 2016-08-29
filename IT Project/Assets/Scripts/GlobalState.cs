using UnityEngine;
using System.Collections;

public class GlobalState {

	// Singleton implementation
	public static GlobalState instance;
	public static GlobalState getInstance() {
		if (instance == null) {
			instance = new GlobalState ();
		}
		return instance;
	}

	public Character currentChar {
		get {
			return currentChar;
		}
	}


}
