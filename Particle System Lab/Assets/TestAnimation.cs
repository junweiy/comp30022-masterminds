using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if(Input.GetKey(KeyCode.M)){
			Debug.Log ("Pressed Move");
			this.GetComponent<Animation>().Play("Move|Move");
		}

		if(Input.GetKey(KeyCode.I)){
			Debug.Log ("Pressed Idle");
			this.GetComponent<Animation>().Play("Move|Idle");
		}

		if(Input.GetKey(KeyCode.C)){
			Debug.Log ("Pressed Cast");
			this.GetComponent<Animation>().Play("Move|Cast");
		}


	}
}
