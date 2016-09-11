using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// the character model
	public Character character {get; set;}
	private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;
	public bool isMainCharacter { get; set;}

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

		healthBarUI.SetHealthUI(character.HP,character.MaxHP);


		if (isMainCharacter && character.canMove) {
			if (Input.GetMouseButton (1)) {
				Move ();
			}

			if (Input.GetButton ("Fire1")) {
				Move ();
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


	private void Move()
	{
		SpellController spellController = this.GetComponent<SpellController> ();
		if (spellController.spellRange.enabled == true) {
			spellController.spellRange.enabled = false;
		}

		// quick fix only
		if (navMeshAgent == null) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
			navMeshAgent.destination = hit.point;
			navMeshAgent.Resume();

		}

	}

    

    public Character getCharacter()
    {
        return character;
    }

}
