using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public Transform target;

    enum State {Idle, Found, Chase, Attack, Damaged, Dead};
    State state = State.Idle;

    NavMeshAgent nma;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;

            case State.Found:
                break;

            case State.Chase:
                nma.SetDestination(target.position);
                float dist = Vector3.Distance(transform.position, target.position);
                if (dist <= 1)
                {
                    state = State.Attack;
                }
                break;

            case State.Attack:
                break;

            case State.Damaged:
                break;

            case State.Dead:
                break;
        }

    }
}
