using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SupportNPCController : MonoBehaviour
{
    [Tooltip("目的地のリスト")]
    public GameObject destinations;
    private List<Transform> destinationList = new List<Transform>();

    [Tooltip("NPCの向く位置のリスト")]
    public GameObject lookAtPositions;
    private List<Transform> lookAtPositionList = new List<Transform>();
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
        foreach(Transform destination in destinations.GetComponentsInChildren<Transform>())
        {
            if(destination != destinations.transform)
            {
                destinationList.Add(destination);
            }
        }

        foreach(Transform lookAtPosition in lookAtPositions.GetComponentsInChildren<Transform>())
        {
            if(destinations != lookAtPositions.transform)
            {
                lookAtPositionList.Add(lookAtPosition);
            }
        }

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

        if(destinationList.Count > 0)
        {
            if (Vector3.Distance(transform.position, destinationList[0].position) < 0.1f)
            {
                Debug.Log("ArrivedDestination");
                animator.SetInteger("TransitionNumber", 0);
                NextDestination();
                NPCAction(2);
            }
        }
        else if(lookAtPositionList.Count > 0)
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
        destinationList.RemoveAt(0);
    }

    private void NextLookAtPosition()
    {
        lookAtPositionList.RemoveAt(0);
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
                if(destinationList.Count > 0)
                {
                    animator.SetInteger("TransitionNumber", 1);
                    agent.destination = destinationList[0].position;
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
                transform.LookAt(lookAtPositionList[0]);
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
