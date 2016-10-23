using UnityEngine;

public class FireBallController : Photon.MonoBehaviour {
    // The tag of the character
    public const string CHARACTER_TAG = "Character";
    // The velocity of fire ball
    public const float VELOCITY = 300;
    // The range that can be chosen to cast within
    private const float RANGE = 600;

    // Character ID that cast the spell
    public int CharId;
	// Damage of spell
    public int Damage;
	// Distance traveled since cast 
    public float DistanceTravelled;
	// Whether damage is enabled (disabled in replay)
    public bool EnableDamage = true;


    private void Start() {
        SetVelocity();
        DistanceTravelled = 0;
    }

	// Update distance traveled and destroy the game object once
	// beyond range
    private void Update() {
        DistanceTravelled += VELOCITY*Time.deltaTime;
        if (DistanceTravelled >= RANGE) {
            Destroy(this.gameObject);
        }
    }

	// Set velocity of the fire ball
    private void SetVelocity() {
        this.GetComponent<Rigidbody>().velocity = VELOCITY*(this.transform.rotation*new Vector3(0, 0, 1));
    }


    /* The function detects if the fireball hits on any other player while flying. If it does, 
     * damage will be caused and the fireball will disappear.
     */

    public virtual void OnCollisionEnter(Collision collision) {
        Character c;
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == CHARACTER_TAG) {
            c = gameObject.GetComponent<Character>();
            if (!c.CharId.Equals(CharId)) {
                Destroy(this.gameObject);
                if (EnableDamage) {
                    c.TakeDamage(Damage);
                }
                if (c.IsDead) {
                    c.NumDeath++;
                    PhotonView.Find(CharId).gameObject.GetComponent<Character>().Killed();
                }
            }
        } else {
            Destroy(this.gameObject);
        }
    }
}