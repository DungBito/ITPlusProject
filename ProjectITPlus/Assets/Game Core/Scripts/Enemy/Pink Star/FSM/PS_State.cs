using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_State {
    protected PS_Controller controller;
    protected PS_Data data;
    private int hashParam;
    private bool isTriggerParam;

    protected Core core;
    protected PS_StateMachine stateMachine;
    protected Animator animator;

    protected float startTime;
    protected float exitTime;
    protected bool isFinishAnimation;

    public PS_State(PS_Controller controller, PS_Data data, int hashParam, bool isTriggerParam) {
        this.controller = controller;
        this.data = data;
        this.hashParam = hashParam;
        this.isTriggerParam = isTriggerParam;

        core = controller.Core;
        stateMachine = controller.StateMachine;
        animator = controller.Animator;
    }

    public virtual void Enter() {
        startTime = Time.time;
        isFinishAnimation = false;
        if (isTriggerParam) {
            animator.SetTrigger(hashParam);
        }
        else {
            animator.SetBool(hashParam, true);
        }
        Check();
    }

    public virtual void Exit() {
        exitTime = Time.time;
        if (isTriggerParam) {
            animator.ResetTrigger(hashParam);
        }
        else {
            animator.SetBool(hashParam, false);
        }
    }

    public virtual void LogicUpdate() {
        Check();
    }

    public virtual void PhysicsUpdate() {
    }

    public virtual void Check() {
    }

    public void SetFinishAnimation() {
        isFinishAnimation = true;
    }
}
