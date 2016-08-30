using UnityEngine;
using System.Collections;

public class ItemInfoPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateInfo(Item item) {
		if (item == null) {
			Debug.Log ("cannot update info of item (null)");
		}
		// TODO
	}

    public void onclick()
    {
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }
}
