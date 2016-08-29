using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public float maximumHealth; 
	private float currentHealth;

	void Start () {
		currentHealth = maximumHealth;
	}

	public void TakeDamage(float f)
	{
		currentHealth -= f;
		if(currentHealth <= 0) { Destroy(this); }

	}

}
