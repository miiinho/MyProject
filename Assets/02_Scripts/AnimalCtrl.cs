using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalCtrl : MonoBehaviour
{
    public Transform target;

    public float patrolRadius = 10f;
    public float patrolInterval = 5f;

    public enum State { Idle, Run, Dead };
    State state = State.Idle;

    NavMeshAgent nma;

    public FieldOfView fieldOfView;
    public PlayerCtrl playerCtrl;

    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        fieldOfView = GetComponent<FieldOfView>();
        playerCtrl = GameObject.FindWithTag("Player").GetComponent<PlayerCtrl>();

        if (state == State.Idle)
        {
            if (fieldOfView.visibleTargets.Count == 0)
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
                if (fieldOfView.visibleTargets.Count >= 1)
                {
                    state = State.Run;
                }

                if (playerCtrl.moveSpeed == 15f)
                {
                    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fieldOfView.largeRadius, fieldOfView.targetMask);
                    if (targetsInViewRadius.Length >= 1)
                    {
                        state = State.Run;
                    }
                }

                if (playerCtrl.moveSpeed == 10f)
                {
                    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, fieldOfView.viewRadius, fieldOfView.targetMask);
                    if (targetsInViewRadius.Length >= 1)
                    {
                        state = State.Run;
                    }
                }
                break;

            case State.Run:
                Vector3 runawayDirection = transform.position - target.position;
                Vector3 runawayPosition = transform.position + runawayDirection.normalized * 20f;
                nma.SetDestination(runawayPosition);
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
