using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_State {
    protected CT_Controller controller;
    protected CT_Data data;
    private int hashParam;
    private bool isTriggerParam;

    protected Core core;
    protected CT_StateMachine stateMachine;
    protected Animator animator;

    protected float startTime;
    protected float exitTime;
    protected bool isFinishAnimation;

    public CT_State(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) {
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
