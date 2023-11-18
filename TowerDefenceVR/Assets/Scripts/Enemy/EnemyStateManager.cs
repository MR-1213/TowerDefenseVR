using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;
using Oculus.Interaction.Samples;

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
    private GameObject[] magics = new GameObject[8];
    private GameObject selectedMagic;
    
    [Header("敵の特性")]
    public bool hasBarrier;
    private float chaseDistanceThreshold;
    private float attackDistanceThreshold;

    [Header("敵の追跡対象")]
    [SerializeField] private Transform targetCore;
    [SerializeField] private Transform playerTransform;

    [Header("手の位置")]
    [SerializeField] private Transform handTransform;

    [SerializeField] private GenerateMagic generateMagic;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private SphereCollider attackCollider;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private EnemyStatusManager statusManager;
    private Slider enemyHPSlider;
    private bool isCoreAttacking = false;

    private enum EnemyState{
        GoToCore,
        Chase,
        Attack,
        AttackIdle,
        Dying,
    }
    private EnemyState currentState = EnemyState.GoToCore;
    private bool stateEnter;
    private float stateTime;
    private Tween attackTween;
    private Attack_MazeCallback attackCallback;
    private Attack_SwordCallback attackSwordCallback;
    public bool isAnimStateEnd { get; set;} = false;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        enemyHPSlider = GetComponentInChildren<Slider>();

        for(int i = 0; i < magics.Length; i++)
        {
            magics[i] = generateMagic.enemyMagics[i];
        }

        if(enemyType == EnemyType.SwordEnemy)
        {
            attackSwordCallback = animator.GetBehaviour<Attack_SwordCallback>();
            chaseDistanceThreshold = 10.0f;
            attackDistanceThreshold = 2.0f;
        }
        else if(enemyType == EnemyType.MagicEnemy)
        {
            attackCallback = animator.GetBehaviour<Attack_MazeCallback>();
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

        statusManager = new EnemyStatusManager(50.0f);
        enemyHPSlider.minValue = 0.0f;
        enemyHPSlider.maxValue = 30.0f;
        enemyHPSlider.value = enemyHPSlider.maxValue;
    }

    private void OnEnable() 
    {
        ChangeState(EnemyState.GoToCore);
    }

    private void OnTriggerEnter(Collider other) 
    {
        //BoxColliderに剣が当たったらダメージを受ける
        if (other.gameObject.CompareTag("PlayerSword") && Vector3.Distance(transform.position, playerTransform.position) < 4.0f)
        {
            audioSource.PlayOneShot(enemyManager.GetAttackedSE());
            statusManager.SwordDamage();
            enemyHPSlider.DOValue(statusManager.HP, 0.5f);
            OVRInput.SetControllerVibration(0f, 0.5f, OVRInput.Controller.RTouch);
            DOVirtual.DelayedCall(1.0f, () => OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.RTouch));
            if(statusManager.HP == 0)
            {
                ChangeState(EnemyState.Dying);
            }
        }

        if(other.gameObject.CompareTag("TargetCore"))
        {
            isCoreAttacking = true;
            ChangeState(EnemyState.Attack);
        }
        
    }

    private void OnCollisionEnter(Collision col)
    {
        //BoxColliderに魔法が当たったらダメージを受ける
        if(col.gameObject.CompareTag("PlayerMagic"))
        {
            statusManager.MagicDamage();
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
            case EnemyState.GoToCore:
            {
                if(stateEnter)
                {
                    navMeshAgent.SetDestination(targetCore.position);
                    
                    animator.SetTrigger("CoreTrigger");
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
                    ChangeState(EnemyState.GoToCore);
                    return;
                }

                if(!navMeshAgent.pathPending && Vector3.Distance(transform.position, playerTransform.position) < attackDistanceThreshold)
                {
                    isCoreAttacking = false;
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
                    //NavMeshAgentを止める
                    if(isCoreAttacking)
                    {
                        navMeshAgent.isStopped = true;
                    }
                    
                }

                if(!isCoreAttacking && Vector3.Distance(transform.position, playerTransform.position) >= attackDistanceThreshold + 0.5f)
                {
                    ChangeState(EnemyState.Chase);
                    return;
                }

                ChangeState(EnemyState.AttackIdle);
                return;
            }

            case EnemyState.AttackIdle:
            {

                if(enemyType == EnemyType.SwordEnemy && isAnimStateEnd)
                {
                    Debug.Log("AttackIdle");
                    isAnimStateEnd = false;
                    CheckAttackTarget();
                    ChangeState(EnemyState.Attack);
                    return;
                }
                else if(enemyType == EnemyType.MagicEnemy && stateTime > 5.0f)
                {
                    CheckAttackTarget();
                    ChangeState(EnemyState.Attack);
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

            default:
            {
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

    private void AttackImpactEvent()
    {
        if(enemyType == EnemyType.MagicEnemy) return;
        
        //攻撃判定を有効にする
        attackCollider.enabled = true;

        //0.2秒後に攻撃判定を無効にする
        Invoke(nameof(DisableAttackCollider), 0.2f);
    }

    private void DisableAttackCollider()
    {
        attackCollider.enabled = false;
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
                break;
            }
        }
    }

    private void CheckAttackTarget()
    {
        if(isCoreAttacking)
        {
            Vector3 coreDirection = targetCore.position - transform.position;
            coreDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(coreDirection);
        }
        else
        {
            Vector3 playerDirection = playerTransform.position - transform.position;
            playerDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(playerDirection);
        }
    }

    public void GenerateMagic()
    {
        Instantiate(selectedMagic, handTransform.position, handTransform.rotation);
    }
}
