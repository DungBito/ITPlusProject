using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_State {
    protected BP_Controller controller;
    protected BP_Data data;
    private int hashParam;
    private bool isTriggerParam;

    protected Core core;
    protected BP_StateMachine stateMachine;
    protected Animator animator;

    protected float startTime;
    protected float exitTime;
    protected bool isFinishAnimation;

    public BP_State(BP_Controller controller, BP_Data data, int hashParam, bool isTriggerParam) {
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
