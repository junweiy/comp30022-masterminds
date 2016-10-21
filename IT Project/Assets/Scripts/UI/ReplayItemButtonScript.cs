using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReplayItemButtonScript : MonoBehaviour {

    public GameObject TextField;

    public void SetText(string s) {
        TextField.GetComponent<Text>().text = s;
    }

	// Use this for initialization
    private void Start () {

	}
	
	// Update is called once per frame
    private void Update () {
	
	}
}
