using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	public DisplayPlayerCoin coinNumber;

    public Transform spellSpawn;

    private Character character;

    private HealthBarUI healthBarUI;
	private NavMeshAgent navMeshAgent;

	// Use this for initialization
	void Start () {
		character = new Character();
		navMeshAgent = GetComponent<NavMeshAgent>();
        healthBarUI = GetComponent<HealthBarUI>();
    }
	
	// Update is called once per frame
	void Update () {
		healthBarUI.SetHealthUI(character.HP,character.MaxHP);
        coinNumber.updateCoin(character.Coin);
		if (Input.GetMouseButton (1)) {
			Move ();
		}

        if (Input.GetButton("Fire1"))
        {
            Move();
        }

        if (Input.GetKey("space"))
        {
            //Instantiate(fireBall, spellSpawn.position, spellSpawn.rotation);
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


}
