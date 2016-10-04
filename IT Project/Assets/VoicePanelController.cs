using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VoicePanelController : MonoBehaviour {

    public GameObject voiceButton;

	// Use this for initialization
	void Start () {
        GameObject button = (GameObject)Instantiate(voiceButton);
        button.transform.SetParent(GetComponent<Image>().rectTransform, false);
        button.transform.localScale = new Vector3(1, 1, 1);
	}
	
}
