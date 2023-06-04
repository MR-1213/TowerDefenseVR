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
    public enum EnemyType
    {
        SwordEnemy,
        MagicEnemy,
    }
    [Header("敵の種類")]
    public EnemyType enemyType;
    public enum MagicElement
    {
        Fire,
        Water,
        Ice,
        Wind,
        Thunder,
        Ground,
        Light,
        Dark,
    }
    [Header("敵の魔法属性")]
    public MagicElement magicElement;
    [SerializeField] private GameObject[] magics;
    private GameObject selectedMagic;
    
    [Header("敵の特性")]
    public bool hasBarrier;
    private float chaseDistanceThreshold;
    private float attackDistanceThreshold;

    [Header("敵の追跡対象")]
    [SerializeField] private Transform playerTransform;

    [Header("手の位置")]
    [SerializeField] private Transform handTransform;
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
    private Tween attackTween;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyHPSlider = GetComponentInChildren<Slider>();

        if(enemyType == EnemyType.SwordEnemy)
        {
            chaseDistanceThreshold = 10.0f;
            attackDistanceThreshold = 2.0f;
        }
        else if(enemyType == EnemyType.MagicEnemy)
        {
            chaseDistanceThreshold = 15.0f;
            attackDistanceThreshold = 10.0f;
            switch(magicElement)
            {
                case MagicElement.Fire:
                {
                    selectedMagic = magics[0];
                    break;
                }

                case MagicElement.Water:
                {
                    selectedMagic = magics[1];
                    break;
                }

                case MagicElement.Ice:
                {
                    selectedMagic = magics[2];
                    break;
                }

                case MagicElement.Wind:
                {
                    selectedMagic = magics[3];
                    break;
                }

                case MagicElement.Thunder:
                {
                    selectedMagic = magics[4];
                    break;
                }

                case MagicElement.Ground:
                {
                    selectedMagic = magics[5];
                    break;
                }

                case MagicElement.Light:
                {
                    selectedMagic = magics[6];
                    break;
                }

                case MagicElement.Dark:
                {
                    selectedMagic = magics[7];
                    break;
                }
            }
        }

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
            DOVirtual.DelayedCall(1.0f, () => OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch));
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

                if(Vector3.Distance(transform.position, playerTransform.position) < chaseDistanceThreshold)
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

                if(Vector3.Distance(transform.position, playerTransform.position) >= chaseDistanceThreshold)
                {
                    ChangeState(EnemyState.Idle);
                    return;
                }

                if(!navMeshAgent.pathPending && Vector3.Distance(transform.position, playerTransform.position) < attackDistanceThreshold)
                {
                    ChangeState(EnemyState.Attack);
                    return;
                }

                navMeshAgent.SetDestination(playerTransform.position);
                return;
            }

            case EnemyState.Attack:
            {
                if(stateEnter)
                {
                    CheckEnemyType();
                }

                if(Vector3.Distance(transform.position, playerTransform.position) >= attackDistanceThreshold + 0.5f)
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
                    attackTween.Kill();
                    animator.SetTrigger("DyingTrigger");
                }

                if(stateTime > 4.0f)
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

    private void CheckEnemyType()
    {
        switch(enemyType)
        {
            case EnemyType.SwordEnemy:
            {
                animator.SetTrigger("AttackTrigger");
                break;
            }

            case EnemyType.MagicEnemy:
            {
                animator.SetTrigger("AttackTrigger");
                attackTween = DOVirtual.DelayedCall(2.0f, () => {
                    Instantiate(selectedMagic, handTransform.position, handTransform.rotation);
                })
                .SetLoops(-1, LoopType.Restart);
                break;
            }
        }
    }
}
