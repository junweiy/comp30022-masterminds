using UnityEngine;

public class FireNovaController : Photon.MonoBehaviour {
    // The tag of the character
    public const string CHARACTER_TAG = "Character";
    // Character view ID
    public int CharId;
    // The damage of spell at current level
    public int Damage;
    // The range of spell at current level
    public float Range;
    // The power of spell at current level
    public float Power;
    // The casting time of spell at current level
    public int CastingTime;

    public float TimePassed;


    /* The function utilised coroutine to achieve casting time effect.
     */

    private void Start() {
        TimePassed = 0;
    }

    private void Update() {
        TimePassed += Time.deltaTime;
        if (TimePassed >= CastingTime) {
            CastFireNova();
        }
    }

    public void CastFireNova() {
        // After casting time find all objects within casting range
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, Range);
        foreach (Collider hit in colliders) {
            if (!hit.CompareTag(CHARACTER_TAG)) {
                continue;
            }
            Character anotherCharacter = hit.GetComponent<Character>();
            if (anotherCharacter.CharId.Equals(CharId)) {
                continue;
            }
            // all players around will be pushed with certain amount of power
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(Power, transform.position, Range);
                anotherCharacter.TakeDamage(Damage);
                if (anotherCharacter.IsDead) {
                    anotherCharacter.NumDeath++;
                    PhotonView.Find(CharId).gameObject.GetComponent<Character>().Killed();
                }
            }
        }
        // The spell object is destroyed
        Destroy(this.gameObject);
    }
}