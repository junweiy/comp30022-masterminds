using UnityEngine;
using System.Collections;

public class TapToMove : MonoBehaviour {


    private NavMeshAgent navMeshAgent;


	// Use this for initialization
	void Start () {

        navMeshAgent = GetComponent<NavMeshAgent>();


    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1"))
        {

            Move();


        }

	
	}



    void Move()
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




