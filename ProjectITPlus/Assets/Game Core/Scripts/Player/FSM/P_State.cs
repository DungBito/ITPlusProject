using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_State {
    protected P_Controller controller;
    protected P_Data data;
    private int hashParam;
    private bool isTriggerParam;

    protected Core core;
    protected P_StateMachine stateMachine;
    protected Animator animator;
    protected P_InputHandle inputHandle;

    protected float startTime;
    protected float exitTime;
    protected bool isFinishAnimation;

    public P_State(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) {
        this.controller = controller;
        this.data = data;
        this.hashParam = hashParam;
        this.isTriggerParam = isTriggerParam;

        core = controller.Core;
        stateMachine = controller.StateMachine;
        animator = controller.Animator;
        inputHandle = controller.InputHandle;
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
        Input();
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
        Input();
        Check();
    }

    public virtual void PhysicsUpdate() {
    }

    public virtual void Input() {
    }

    public virtual void Check() {
    }

    public void SetFinishAnimation() {
        isFinishAnimation = true;
    }
}
