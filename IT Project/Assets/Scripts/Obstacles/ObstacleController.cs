using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public float maximumHealth; 
	private Obstacle obstacle;

	void Start () {
        obstacle = new Obstacle(maximumHealth);
        obstacle.addOnDestoyAction(delegate {
            GameObject.Destroy(this.gameObject);
        });
	}

	public void TakeDamage(float f) {
        obstacle.takeDamage(f);
	}

}
