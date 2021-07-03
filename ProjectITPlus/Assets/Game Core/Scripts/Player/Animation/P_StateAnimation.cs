using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_StateAnimation : StateMachineBehaviour {
    private P_Controller controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (controller == null) {
            controller = animator.GetComponentInParent<P_Controller>();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller.StateMachine.CurrentState.SetFinishAnimation();
    }
}
