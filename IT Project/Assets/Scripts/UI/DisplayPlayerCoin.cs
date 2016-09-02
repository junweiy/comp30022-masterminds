using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class DisplayPlayerCoin : MonoBehaviour {

    private Text text;

    public void Awake () {
        text = GetComponent<Text>();
	}
	
	public void updateCoin(int gold) {
		text.text = gold.ToString();
    }
}
