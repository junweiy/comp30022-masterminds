using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour {

	// the character model
    public Character character;
	private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;
	private bool isMainCharacter;
    private bool selectingSpellCasting;
    private int numberOfTouch;
    
	// Use this for initialization
	public void initialise(Character c,bool isMainCharacter) {
		character = c;
		this.isMainCharacter = isMainCharacter;
		this.healthBarUI = this.GetComponent<HealthBarUI> ();
		this.gameObject.tag = "Character";
		navMeshAgent = this.GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = GlobalState.isCurrentChar (character);
        selectingSpellCasting = false;
        numberOfTouch = 0;
    }

	public void setAsMainCharacter(){
		isMainCharacter = true;
	}

	// Update is called once per frame
	void Update () {

		healthBarUI.SetHealthUI(character.HP,character.MaxHP);

		if (isMainCharacter) {
            while (numberOfTouch < Input.touchCount)
            {
                Touch t = Input.GetTouch(numberOfTouch);
                Move(t);


                numberOfTouch++;
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


	private void Move(Touch t)
	{


		SpellController spellController = this.GetComponent<SpellController> ();
		if (spellController.spellRange.enabled == true) {
			spellController.spellRange.enabled = false;
		}
        

		Ray ray = Camera.main.ScreenPointToRay(t.position);
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
