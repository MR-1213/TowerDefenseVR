using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private enum EnemyState{
        Idle,
        Chase,
        Attack,
        Dying,
    }
    private EnemyState currentState = EnemyState.Idle;
    private bool stateEnter;
    private float stateTime;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            ChangeState(EnemyState.Dying);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        stateEnter = true;
        stateTime = 0;
    }

    private void Update() 
    {
        stateTime += Time.deltaTime;

        switch(currentState)
        {
            case EnemyState.Idle:
            {
                if(stateEnter)
                {
                    animator.SetTrigger("IdleTrigger");
                }

                if(Vector3.Distance(transform.position, playerTransform.position) < 6.0f)
                {
                    ChangeState(EnemyState.Chase);
                    return;
                }

                return;
            }

            case EnemyState.Chase:
            {
                if(stateEnter)
                {
                    navMeshAgent.SetDestination(playerTransform.position);
                    animator.SetTrigger("ChaseTrigger");
                }

                if(Vector3.Distance(transform.position, playerTransform.position) >= 6.0f)
                {
                    ChangeState(EnemyState.Idle);
                    return;
                }

                if(!navMeshAgent.pathPending && Vector3.Distance(transform.position, playerTransform.position) < 2.0f)
                {
                    ChangeState(EnemyState.Attack);
                    return;
                }

                return;
            }

            case EnemyState.Attack:
            {
                if(stateEnter)
                {
                    animator.SetTrigger("AttackTrigger");
                }

                if(Vector3.Distance(transform.position, playerTransform.position) >= 2.5f)
                {
                    ChangeState(EnemyState.Chase);
                    return;
                }

                return;
            }

            case EnemyState.Dying:
            {
                if(stateEnter)
                {
                    animator.SetTrigger("DyingTrigger");
                }

                if(stateTime > 10.0f)
                {
                    Destroy(this.gameObject);
                }

                return;
            }
        }
    }

    private void LateUpdate() 
    {
        if(stateTime != 0)
        {
            stateEnter = false;
        }
    }
}
