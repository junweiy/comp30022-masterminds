using UnityEngine;
using System.Collections;

public class ItemInfoPanelScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateInfo(Item item) {
		if (item == null) {
			Debug.Log ("cannot update info of item (null)");
		}
		// TODO
	}

    public void onClick()
    {
        Destroy(GameObject.Find("InfoPanel(Clone)"));
    }
}
