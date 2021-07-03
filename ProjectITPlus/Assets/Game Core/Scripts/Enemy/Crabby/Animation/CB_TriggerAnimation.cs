using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_TriggerAnimation : MonoBehaviour {
    private CB_Controller controller;

    void Start() {
        controller = GetComponentInParent<CB_Controller>();
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
