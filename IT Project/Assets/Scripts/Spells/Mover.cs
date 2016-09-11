using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	// The rigid body component of the object
	private Rigidbody rb;
	// Desired moving speed
	public float speed;

	/* The function set the velocity of the object to a constant such that the object
	 * will keep moving forward with the spped.
	 */
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}

}
