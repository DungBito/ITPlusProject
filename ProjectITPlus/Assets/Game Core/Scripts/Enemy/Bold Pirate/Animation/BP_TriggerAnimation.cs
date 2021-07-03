using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_TriggerAnimation : MonoBehaviour {
    private BP_Controller controller;

    void Start() {
        controller = GetComponentInParent<BP_Controller>();
    }

    public void TriggerAnimation() {
        controller.StateMachine.CurrentState.SetFinishAnimation();
    }

    public void EnableAttackTrigger() {
        controller.Attack.gameObject.SetActive(true);
    }

    public void DisableAttackTrigger() {
        controller.Attack.gameObject.SetActive(false);
    }

    public void ClearDetected() {
        controller.Attack.ClearListDetected();
    }
}
