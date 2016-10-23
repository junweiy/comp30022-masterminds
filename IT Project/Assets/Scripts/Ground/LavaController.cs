using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LavaController : MonoBehaviour {
    // Health Point dropped every time
    public int HealthDropPerTime;
    // Time between HP dropping in seconds
    public int HealthDropSecondsInterval;
    private List<Character> _characterList;


    private void Start() {
        _characterList = new List<Character>();
    }

	// Detect and update lists of characters that are in lava
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Character") {
            _characterList.Add(collision.gameObject.GetComponent<CharacterController>().Character);
            StartCoroutine(Damage(collision.gameObject.GetComponent<CharacterController>().Character));
        }
    }

	// Remove the character from the list of characters that are in lava
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "Character") {
            _characterList.Remove(collision.gameObject.GetComponent<CharacterController>().Character);
        }
    }

	// Cause damage to character in lava and handle if death of player trigger termination of the game
    private IEnumerator Damage(Character player) {
        yield return new WaitForSeconds(HealthDropSecondsInterval);
        while (_characterList.Contains(player)) {
            player.TakeDamage(HealthDropPerTime);
            if (GameController.CheckIfGameEnds() && !GameObjectFinder.FindMainCharacter().IsDead) {
                GameObjectFinder.FindMainCharacter().UpdateProfile(true);
            }
            yield return new WaitForSeconds(HealthDropSecondsInterval);
        }
    }

    
}