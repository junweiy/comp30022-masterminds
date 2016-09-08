using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// the character model
    public Character character;
	private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;
	private bool isMainCharacter;

	// Use this for initialization
	public void initialise(Character c,bool isMainCharacter) {
		character = c;
		this.isMainCharacter = isMainCharacter;
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		this.gameObject.tag = "Character";
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = GlobalState.isCurrentChar (character);

	}

	public void setAsMainCharacter(){
		isMainCharacter = true;
	}

	// Update is called once per frame
	void Update () {

		healthBarUI.SetHealthUI(character.HP,character.MaxHP);


		if (isMainCharacter) {
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
