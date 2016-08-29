using UnityEngine;
using System.Collections;

public class GlobalState {

	// Singleton implementation
	public static GlobalState instance;
	public static GlobalState getInstance() {
		if (instance == null) {
			instance = new GlobalState ();
			instance.currentChar = new Character (); // TODO testing only
		}
		return instance;
	}

	public Character currentChar { get;	private set; }


}
