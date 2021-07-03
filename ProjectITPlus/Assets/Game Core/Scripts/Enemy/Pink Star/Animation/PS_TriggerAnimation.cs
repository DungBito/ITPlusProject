using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_TriggerAnimation : MonoBehaviour {
    private PS_Controller controller;

    void Start() {
        controller = GetComponentInParent<PS_Controller>();
    }

    public void TriggerAnimation() {
        controller.StateMachine.CurrentState.SetFinishAnimation();
    }
}
