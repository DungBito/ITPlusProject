﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_RunState : P_State {
    private int xInput;
    private bool jumpInput;
    private bool throwInput;

    private bool grounded;

    private float runSpeed;

    #region Constructor
    public P_RunState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        runSpeed = data.runSpeed;
    }
    #endregion

    public override void Check() {
        base.Check();
        core.Movement.CheckIfShouldFlip(xInput);
        grounded = core.Collision.Grounded;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Input() {
        base.Input();
        xInput = inputHandle.XInput;
        jumpInput = inputHandle.JumpInput;
        throwInput = inputHandle.ThrowInput;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (!grounded) {
            controller.InAirState.SetJumping(false);
            stateMachine.ChangeState(controller.InAirState);
        }
        else if (xInput == 0) {
            stateMachine.ChangeState(controller.IdleState);
        }
        else if (jumpInput && controller.JumpState.CanJump) {
            inputHandle.UseJumpInput();
            stateMachine.ChangeState(controller.JumpState);
        }
        else if (throwInput && controller.ThrowState.CanThrow) {
            inputHandle.UseThrowInput();
            stateMachine.ChangeState(controller.ThrowState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(xInput * runSpeed);
    }
}
