using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private Animator animator;
    private void Start() 
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            animator.SetTrigger("DyingTrigger");
        }
    }
}
