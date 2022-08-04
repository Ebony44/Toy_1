using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInitState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("IdleState", true);
        int randomIdle = Random.Range(0, 2);
        animator.SetInteger("IdleStates", randomIdle);
    }
}
