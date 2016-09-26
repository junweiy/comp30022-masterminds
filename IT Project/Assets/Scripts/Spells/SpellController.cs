using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpellController : MonoBehaviour {
	// The name of object used to spawn spells
	private string SPELL_SPAWN_NAME = "SpellSpawn";
	// The character that controls casting spells
	private Character character;
	// Whether the character is the main character
	private bool isMainCharacter;
	// The image of spell range
	public Image spellRange;
	// A list of spells that the character currently have
	private List<Spell> spells;

	/* The function initialises the controller on a certain character.
	 */
	public void initialise(Character c) {
		character = c;
		this.isMainCharacter = false;
		spellRange.enabled = false;
		spells = c.spells;
	}

	/* The function sets the player with this spell controller as the main character.
	 */ 
	public void setAsMainCharacter(){
		isMainCharacter = true;
	}

	/* The function updates the game state every frame, which includes detection of user input and
	 * updating cool down time for all spells.
	 */ 
	void Update () {
		// Update cool down time for all spells
		foreach (Spell s in spells) {
			if (s.currentCooldown < s.cooldown) {
				s.currentCooldown++;
			}
		}
		// Detect user input of casting spells
		if (isMainCharacter) {
			// Need to change to accommodate touch screen
			if (Input.GetKeyDown ("1")) {
				Cast (spells[0]);
			}
			if (Input.GetKeyDown ("2")) {
				Cast (spells[1]);
			}
		}
	}
	/* This function will cast spell based on the spell number stored in the character */
	private void Cast(Spell s)
	{

		// Reject casting if cool down has not finished
		if (s.currentCooldown < s.cooldown) {
			return;
		}

		if (s.isInstantSpell) {
			// Find the transform of spell spawning point for instant spells
			Transform t = transform.Find (SPELL_SPAWN_NAME);
			s.ApplyEffect(character, transform, t.position);
		} else {
			// WuliPangPang please fix this
			spellRange.enabled = true;
			spellRange.transform.localScale *= s.range+character.range;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				s.ApplyEffect(character, transform, hit.point);
			}
			spellRange.enabled = false;
		}

		// Reset the cool down
		s.currentCooldown = 0;
	}

}
