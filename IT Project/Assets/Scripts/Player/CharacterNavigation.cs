using UnityEngine;
using System.Collections;

public class CharacterNavigation : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

	// Use this for initialization
	public void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
	
    public void Move()
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




