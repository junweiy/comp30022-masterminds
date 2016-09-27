using UnityEngine;
using System.Collections;


// TEMPORARY CLASS
public class KeyboardMovement : MonoBehaviour {

    public float velocity = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Move front
        if (Input.GetKey("w"))
        {
            this.transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }

        //Move back
        if (Input.GetKey("s"))
        {
            this.transform.Translate(Vector3.back * velocity * Time.deltaTime);
        }

        //Move left
        if (Input.GetKey("a"))
        {
            this.transform.Translate(Vector3.left * velocity * Time.deltaTime);
        }

        //Move right
        if (Input.GetKey("d"))
        {
            this.transform.Translate(Vector3.right * velocity * Time.deltaTime);
        }




    }
}
