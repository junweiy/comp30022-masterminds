using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReplayItemButtonScript : MonoBehaviour {

    public GameObject textField;

    public void setText(string s) {
        textField.GetComponent<Text>().text = s;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
