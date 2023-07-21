using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SupportNPCController : MonoBehaviour
{
    [Tooltip("目的地のリスト")]
    public List<Transform> destinations;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        transform.LookAt(agent.steeringTarget + transform.forward);
        agent.nextPosition = transform.position;

        if (agent.remainingDistance < 1.0f && !agent.pathPending)
        {
            animator.SetInteger("TransitionNumber", 0);
        }
    }

    private void NextTarget()
    {
        destinations.RemoveAt(0);
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
                    NextTarget();
                    break;
                }
                else
                {
                    NPCAction(0);
                    break;
                }
            default:
                break;   
        }
    }
}
