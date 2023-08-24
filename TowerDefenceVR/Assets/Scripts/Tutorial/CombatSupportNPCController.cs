using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatSupportNPCController : MonoBehaviour
{
    public Transform destination;

    private NavMeshAgent agent;
    private Animator animator;

    private bool isAssembly = false;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
         if(Vector3.Distance(transform.position, destination.position) < 0.1f && isAssembly)
        {
            animator.SetInteger("TransitionNumber", 0);
        }
    }

    public void Assembly()
    {
        animator.SetInteger("TransitionNumber", 1);
        agent.destination = destination.position;
        agent.speed = 4.0f;
        isAssembly = true;
    }
}
