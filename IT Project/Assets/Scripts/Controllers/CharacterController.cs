using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// the character model
	public Character character {get; set;}

	public bool isMainCharacter { get; set;}

	// The name of object used to spawn spells
	private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;
	public Image spellRange;

	private string SPELL_SPAWN_NAME = "SpellSpawn";


	// Use this for initialization
	public void initialise(Character c) {
		character = c;
		this.isMainCharacter = false;
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		this.gameObject.tag = "Character";
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = GlobalState.isCurrentChar (character);
	}

	public void setAsMainCharacter(){
		isMainCharacter = true;
	}

	/* The function causes the player a sudden stop when the player is navigating to a point, which
	 * is used to fit the spell FireNova.
	 */ 
	public void stopMoving() {
		NavMeshAgent nma = this.GetComponent<NavMeshAgent> ();
		nma.velocity = new Vector3(0,0,0);
		nma.Stop ();
	}

	// Update is called once per frame
	void Update () {

		healthBarUI.SetHealthUI(character.hp,character.maxHp);

		// Update cool down time for all spells
		foreach (Spell s in character.spells) {
			if (s.currentCooldown < s.cooldown) {
				s.currentCooldown++;
			}
		}

		if (isMainCharacter) {

			if (Input.GetKeyDown ("1")) {
				cast (character.spells[0]);
			}
			if (Input.GetKeyDown ("2")) {
				cast (character.spells[1]);
			}

			if (Input.GetMouseButton (0) || Input.GetMouseButton(1)) {

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (!Physics.Raycast (ray, out hit, 100)) {
					throw new UnityException ("Ray cast not hit");
				}

				if (character.canCast ()) {
					character.castSelected (transform,hit.point);
				}
				else {
					move (hit.point);
				}
			}

			if (character.canCast ()) {
				float range = character.getSelectedSpell().range + character.castingRange;
				displaySpellCastRange (range);
			} else {
				hideSpellCastRange ();
			}

		}

    }
    // For debugging only
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Ground" && collision.gameObject.name != "Lava")
        {
            Debug.Log(collision.gameObject.name);
        }
    }


	private void move(Vector3 destination)
	{
		navMeshAgent.destination = destination;
		navMeshAgent.Resume ();

	}

	/* This function will cast spell based on the spell number stored in the character */
	private void cast(Spell s)
	{
		if (s.isInstantSpell) {
			// Find the transform of spell spawning point for instant spells
			// and cast
			Transform t = transform.Find (SPELL_SPAWN_NAME);
			character.cast (s,transform,t.position);
		} else {
			character.selectSpell(s);
		}

	}

	public void displaySpellCastRange(float range){
		spellRange.enabled = true;
		spellRange.color = new Color (0f,1f,1f,0.15f);
		spellRange.rectTransform.localScale = new Vector3(range,range,1f);
	}


	public void hideSpellCastRange(){
		spellRange.enabled = false;
	}

    public Character getCharacter()
    {
        return character;
    }

}
