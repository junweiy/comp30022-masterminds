using UnityEngine.UI;
using System.Collections;
using UnityEngine;

public class DisplayPlayerGold : MonoBehaviour {

    private Text text;
    public Character character;
    public void Awake () {
        text = GetComponent<Text>();
	}
	
	public void FixedUpdate () {
        text.text = character.goldEarn.ToString();
    }
}
