using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_MazeCallback : StateMachineBehaviour
{
    private bool isNewAnimState = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isNewAnimState = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //現在のアニメーションステート名を取得
        var clipInformation = animator.GetCurrentAnimatorClipInfo(0)[0];
        var stateInformation = animator.GetCurrentAnimatorStateInfo(0);
            
        var currentAnimTime = clipInformation.clip.length * stateInformation.normalizedTime;
        if(currentAnimTime > 1.5f && currentAnimTime < 1.6f && isNewAnimState)
        {
            isNewAnimState = false;
            animator.gameObject.GetComponent<EnemyStateManager>().GenerateMagic();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
