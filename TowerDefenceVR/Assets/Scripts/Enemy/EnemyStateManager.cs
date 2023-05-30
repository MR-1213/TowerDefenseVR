using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 敵の状態を管理するクラス
/// </summary>
public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private EnemyStatusManager statusManager;
    private Slider enemyHPSlider;

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
        audioSource = GetComponent<AudioSource>();
        enemyHPSlider = GetComponentInChildren<Slider>();

        statusManager = new EnemyStatusManager(30.0f);
        enemyHPSlider.minValue = 0.0f;
        enemyHPSlider.maxValue = 30.0f;
        enemyHPSlider.value = enemyHPSlider.maxValue;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            Debug.Log("PlayerSwordがヒットしました。");
            statusManager.NormalDamage();
            enemyHPSlider.DOValue(statusManager.HP, 0.5f);
            OVRInput.SetControllerVibration(0f, 0.5f, OVRInput.Controller.RTouch);
            DOVirtual.DelayedCall(1.5f, () => OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch));
            if(statusManager.HP == 0)
            {
                ChangeState(EnemyState.Dying);
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("PlayerMagic"))
        {
            Debug.Log("PlayerMagicがヒットしました。");
            statusManager.NormalDamage();
            enemyHPSlider.DOValue(statusManager.HP, 0.5f);
            if(statusManager.HP == 0)
            {
                ChangeState(EnemyState.Dying);
            }
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

                if(Vector3.Distance(transform.position, playerTransform.position) < 10.0f)
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

                if(Vector3.Distance(transform.position, playerTransform.position) >= 10.0f)
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

                if(stateTime > 6.0f)
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
