using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEatAnimControl : StateMachineBehaviour
{
    // Animator º¯¼ö
    private Animator _eatAnim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _eatAnim = animator.GetComponent<Animator>();

        _eatAnim.SetBool("Eating", false);

        Transform B_horse = GameObject.Find("Horse").transform.Find("big_horse");
        Transform S_horse = GameObject.Find("Horse").transform.Find("small_horse");

        if (B_horse.gameObject.activeSelf)
        {
            Transform B0 = GameObject.Find("B_Carrot_Step0").transform.Find("BC0");
            Transform B1 = GameObject.Find("B_Carrot_Step1").transform.Find("BC1");
            Transform B2 = GameObject.Find("B_Carrot_Step2").transform.Find("BC2");
            Transform B3 = GameObject.Find("B_Carrot_Step3").transform.Find("BC3");

            if (B0.gameObject.activeSelf)
            {
                B0.gameObject.SetActive(false);
                B1.gameObject.SetActive(true);
            }
            else if (B1.gameObject.activeSelf)
            {
                B1.gameObject.SetActive(false);
                B2.gameObject.SetActive(true);
            }
            else if (B2.gameObject.activeSelf)
            {
                B2.gameObject.SetActive(false);
                B3.gameObject.SetActive(true);
            }
        }
        else if (S_horse.gameObject.activeSelf)
        {
            Transform S0 = GameObject.Find("S_Carrot_Step0").transform.Find("SC0");
            Transform S1 = GameObject.Find("S_Carrot_Step1").transform.Find("SC1");
            Transform S2 = GameObject.Find("S_Carrot_Step2").transform.Find("SC2");
            Transform S3 = GameObject.Find("S_Carrot_Step3").transform.Find("SC3");

            if (S0.gameObject.activeSelf)
            {
                S0.gameObject.SetActive(false);
                S1.gameObject.SetActive(true);
            }
            else if (S1.gameObject.activeSelf)
            {
                S1.gameObject.SetActive(false);
                S2.gameObject.SetActive(true);
            }
            else if (S2.gameObject.activeSelf)
            {
                S2.gameObject.SetActive(false);
                S3.gameObject.SetActive(true);
            }
        }
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
