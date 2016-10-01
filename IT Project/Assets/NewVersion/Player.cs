﻿using UnityEngine;
using System.Collections;

public class Player {

    public int id { get; private set; }

    public Vector3 spawn;
    GameObject p;

    public Player(int id, Vector3 spawn)
    {
        p = GameObject.Instantiate(Resources.Load("Player", typeof(GameObject))) as GameObject;
        p.transform.localPosition = spawn;
        this.id = id;
    }

    public void Move(Vector3 dest)
    {
        NavMeshAgent nma = p.GetComponent<NavMeshAgent>();
        nma.SetDestination(dest);
        nma.Resume();
    }

}