using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherside : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 현재의 localScale 값을 가져옵니다.
        Vector3 currentScale = animator.transform.localScale;

        // x 방향의 값을 반전시킵니다.
        currentScale.x = -currentScale.x;

        // 반전된 localScale 값을 적용합니다.
        animator.transform.localScale = currentScale;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 현재의 localScale 값을 가져옵니다.
        Vector3 currentScale = animator.transform.localScale;

        // x 방향의 값을 반전시킵니다.
        currentScale.x = -currentScale.x;

        // 반전된 localScale 값을 적용합니다.
        animator.transform.localScale = currentScale;
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
