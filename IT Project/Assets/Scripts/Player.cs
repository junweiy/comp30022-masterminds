using UnityEngine;
using System.Collections;

public class Player {

    public int id { get; private set; }

    public enum PlayerState
    {
        Idle,
        Moving,
        AwaitingCast,
        Died
    }
    private PlayerState state;
    private Vector3 spawn;
    private GameObject p;
    private NavMeshAgent nma;

	public int HP { get; private set; }
	public int MaxHP { get; private set; }


    public Player(int id, Vector3 spawn)
    {
        this.spawn = spawn;
        this.id = id;
    }

    public void Spawn()
    {
        p = GameObject.Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject;
        p.transform.localPosition = spawn;
        nma = p.GetComponent<NavMeshAgent>();
        state = PlayerState.Idle;
    }

    public void Move(Vector3 dest)
    {
        nma.SetDestination(dest);
        nma.Resume();
        state = PlayerState.Moving;
    }

    public Vector3 GetPosition()
    {
        return p.transform.localPosition;
    }


    public void Update()
    {
        
        if(state == PlayerState.Idle)
        {
            p.GetComponent<Animation>().Play("Move|Idle");
        }

        if (state == PlayerState.Moving)
        {
            p.GetComponent<Animation>().Play("Move|Move");
        }

        CheckMoving();

    }
    
    public void CheckMoving()
    {
        if (nma.remainingDistance <= nma.stoppingDistance)
        {
            if (!nma.hasPath || Mathf.Abs(nma.velocity.sqrMagnitude) < float.Epsilon)
            {
                if (state == PlayerState.Moving)
                {
                    state = PlayerState.Idle;
                }
            }
        }
    }
}
