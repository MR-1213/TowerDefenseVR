using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SupportNPCController : MonoBehaviour
{
    [Tooltip("目的地のリスト")]
    public List<Transform> destinations;
    [Tooltip("NPCの向く位置のリスト")]
    public List<Transform> lookAtPositions;
    [SerializeField] private TutorialManager tutorialManager;
    private NavMeshAgent agent;
    private Animator animator;
    private SphereCollider sphereCollider;
    private bool isArrivedDestination = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Start()
    {
        animator.SetInteger("TransitionNumber", 0);
    }

    private void OnAnimatorMove()
    {
        transform.position = GetComponent<Animator>().rootPosition;
    }

    private void Update()
    {
        if(animator.GetInteger("TransitionNumber") == 1)
        {
            transform.LookAt(agent.steeringTarget + transform.forward);
            agent.nextPosition = transform.position;
        }

        if(destinations.Count > 0)
        {
            if (Vector3.Distance(transform.position, destinations[0].position) < 0.1f)
            {
                Debug.Log("ArrivedDestination");
                animator.SetInteger("TransitionNumber", 0);
                NextDestination();
                NPCAction(2);
            }
        }
        else if(lookAtPositions.Count > 0)
        {
            /*
            if(animator.GetInteger("TransitionNumber") == 3)
            {
                //Talkののときは常にlookAtPosition[0]の方向を向く
                transform.LookAt(lookAtPositions[0]);
            }
            */
        }
        
    }

    private void NextDestination()
    {
        destinations.RemoveAt(0);
    }

    private void NextLookAtPosition()
    {
        lookAtPositions.RemoveAt(0);
    }

    public void NPCAction(int transitionNum)
    {
        switch(transitionNum)
        {
            case 0:
                //Idle
                animator.SetInteger("TransitionNumber", 0);
                break;
            case 1:
                //Walk
                if(destinations.Count > 0)
                {
                    animator.SetInteger("TransitionNumber", 1);
                    agent.destination = destinations[0].position;
                    break;
                }
                else
                {
                    NPCAction(0);
                    break;
                }
            case 2:
                //Wait
                isArrivedDestination = true;
                tutorialManager.MovedTrigger = false;
                break;
            case 3:
                //Talk
                transform.LookAt(lookAtPositions[0]);
                animator.SetInteger("TransitionNumber", 3);
                NextLookAtPosition();
                break;
            default:
                break;   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && isArrivedDestination)
        {
            isArrivedDestination = false;
            tutorialManager.MovedTrigger = true;
        }
    }
}
