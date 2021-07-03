using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_InAirState : P_State {
    private int xInput;
    private bool jumpInput;
    private bool throwInput;

    private bool grounded;

    private bool isJumping;
    private float inAirSpeed;
    private float timeCheckChangeState;

    #region Constructor
    public P_InAirState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        inAirSpeed = data.inAirSpeed;
        timeCheckChangeState = data.timeToCheckChangeState;
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
        ResetAnimation();
    }

    public override void Input() {
        base.Input();
        xInput = inputHandle.XInput;
        jumpInput = inputHandle.JumpInput;
        throwInput = inputHandle.ThrowInput;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        CheckIsJumping();
        SetAnimation();
        if (jumpInput && controller.JumpState.CanJump) {
            inputHandle.UseJumpInput();
            stateMachine.ChangeState(controller.JumpState);
        }
        else if (grounded && core.Movement.IsNegativeYVelo && Time.time >= startTime + timeCheckChangeState) {
            stateMachine.ChangeState(controller.GroundState);
        }
        else if (throwInput && controller.ThrowState.CanThrow) {
            inputHandle.UseThrowInput();
            controller.ThrowState.SetLastThrowTime();
            Pooler.Instance.SpawnFromPool("Bomb", controller.BombSpawn);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(xInput * inAirSpeed);
    }

    private void CheckIsJumping() {
        if (isJumping && core.Movement.IsNegativeYVelo) {
            isJumping = false;
        }
        if (!isJumping) {
            core.Movement.SetPlusVelocity((data.fallMultiplier - 1) * Physics.gravity.y * Time.deltaTime * Vector2.up);
        }
    }

    public void SetJumping(bool boolToSet) {
        isJumping = boolToSet;
    }

    private void SetAnimation() {
        animator.SetFloat("yVelo", core.Movement.CurrentVelocity.y);
    }

    private void ResetAnimation() {
        animator.SetFloat("yVelo", 5f);
    }
}
