using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public DisplayPlayerCoin coinNumber;

    public Transform spellSpawn;
    public GameObject fireball;

    public Character character;
    private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
//		character = new Character();
//		healthBarUI = this.gameObject.AddComponent<HealthBarUI>();
		healthBarUI = this.gameObject.GetComponent<HealthBarUI>();
    }

	public void setAsPlayed() {
		GlobalState.instance.currentChar = this.character;
		navMeshAgent = this.gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (coinNumber);
		healthBarUI.SetHealthUI(character.HP,character.MaxHP);
        coinNumber.updateCoin(character.Coin);
		if (Input.GetMouseButton (1)) {
			Move ();
		}

        if (Input.GetButton("Fire1"))
        {
            Move();
        }

        if (Input.GetKey("1"))
        {
            Cast(1);
        }
        if (Input.GetKey("2"))
        {
            Cast(2);
        }
        if (Input.GetKey("3"))
        {
            Cast(3);
        }
        if (Input.GetKey("4"))
        {
            Cast(4);
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
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100))
		{
			navMeshAgent.destination = hit.point;
			navMeshAgent.Resume();

		}
	}

    /* This function will cast spell based on the spell number stored in the character */
    private void Cast(int i)
    {
        Spell s = character.getSpell(i-1);
        if (s.isInstant())
        {
            s.applyEffect(character,this.transform);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                s.applyEffect(character, this.transform, hit.point);
            }
        }
    }


    public Character getCharacter()
    {
        return character;
    }

}
