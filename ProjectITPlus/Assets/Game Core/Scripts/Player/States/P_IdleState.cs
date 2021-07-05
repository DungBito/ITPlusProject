using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_IdleState : P_State {
    private int xInput;
    private bool jumpInput;
    private bool throwInput;

    private bool grounded;

    #region Constructor
    public P_IdleState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        grounded = core.Collision.Grounded;
    }

    public override void Enter() {
        base.Enter();
        core.Movement.SetZeroVelocity();
        controller.JumpState.ResetAmountOfJumpLeft();
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
        else if (xInput != 0) {
            stateMachine.ChangeState(controller.RunState);
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
        if (core.Movement.CurrentVelocity.x != 0) {
            core.Movement.SetZeroVelocity();
        }
    }
}
