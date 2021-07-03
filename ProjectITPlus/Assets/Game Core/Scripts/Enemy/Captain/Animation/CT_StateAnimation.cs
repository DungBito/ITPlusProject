using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_StateAnimation : StateMachineBehaviour {
    private CT_Controller controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (controller == null) {
            controller = animator.GetComponentInParent<CT_Controller>();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller.StateMachine.CurrentState.SetFinishAnimation();
    }
}
