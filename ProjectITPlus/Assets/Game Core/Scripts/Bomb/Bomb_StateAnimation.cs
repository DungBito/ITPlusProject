using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_StateAnimation : StateMachineBehaviour {
    private Bomb_Controller controller;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (controller == null) {
            controller = animator.GetComponentInParent<Bomb_Controller>();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Pooler.Instance.AddToPool("Bomb", controller.gameObject);
    }
}
