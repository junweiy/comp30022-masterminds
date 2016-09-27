using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {
	// Upper limit of HP
	public float maximumHealth;
	// Obstacle class
	private Obstacle obstacle;

	void Start () {
        obstacle = new Obstacle(maximumHealth);
        obstacle.AddOnDestoyAction(delegate {
            GameObject.Destroy(this.gameObject);
        });
	}

	public void TakeDamage(float f) {
        obstacle.TakeDamage(f);
	}

}
