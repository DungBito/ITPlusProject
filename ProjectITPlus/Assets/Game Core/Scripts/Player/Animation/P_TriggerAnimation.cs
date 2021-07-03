using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_TriggerAnimation : MonoBehaviour {
    private P_Controller controller;

    void Start() {
        controller = GetComponentInParent<P_Controller>();
    }

    public void TriggerAnimation() {
        controller.StateMachine.CurrentState.SetFinishAnimation();
    }
}
