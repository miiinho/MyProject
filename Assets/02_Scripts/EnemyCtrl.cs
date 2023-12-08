using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : MonoBehaviour
{
    public Transform target;

    public float patrolRadius = 10f;
    public float patrolInterval = 5f;

    public enum State {Idle, Found, Chase, Attack, Damaged, Dead};
    State state = State.Idle;

    NavMeshAgent nma;

    public FieldOfView fieldOfView;
    public PlayerCtrl playerCtrl;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
        playerCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        if(state == State.Idle)
        {
            if(fieldOfView.visibleTargets.Count == 0)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                if(fieldOfView.visibleTargets.Count >= 1)
                {
                    state = State.Found;
                }

                if(playerCtrl.moveSpeed == 15f)
                {
                    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fieldOfView.largeRadius, fieldOfView.targetMask);
                    if(targetsInViewRadius.Length >= 1)
                    {
                        state = State.Found;
                    }
                }

                if (playerCtrl.moveSpeed == 10f)
                {
                    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fieldOfView.viewRadius, fieldOfView.targetMask);
                    if (targetsInViewRadius.Length >= 1)
                    {
                        state = State.Found;
                    }
                }
                break;

            case State.Found:
                state = State.Chase;
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

    IEnumerator Patrol()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);

        Vector3 finaPosition = hit.position;

        nma.SetDestination(finaPosition);

        yield return new WaitForSeconds(patrolInterval);

        StartCoroutine(Patrol());
    }
}
