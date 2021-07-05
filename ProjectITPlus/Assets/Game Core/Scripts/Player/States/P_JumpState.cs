using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_JumpState : P_State {
    private int xInput;

    private float jumpSpeed;
    private float jumpForce;
    private int maxAmountOfJump;
    private int amountOfJumpLeft;

    #region Constructor
    public P_JumpState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        maxAmountOfJump = data.amountOfJump;
        jumpSpeed = data.jumpSpeed;
        jumpForce = data.jumpForce;
    }
    #endregion

    public override void Check() {
        base.Check();
        core.Movement.CheckIfShouldFlip(xInput);
    }

    public override void Enter() {
        base.Enter();
        Pooler.Instance.SpawnFromPool("Jump", controller.AliveGO.transform);
    }

    public override void Exit() {
        base.Exit();
        DecreaseAmountOfJumpLeft();
        controller.InAirState.SetJumping(true);
        core.Movement.SetYVelocity(0f);
        core.Movement.SetYVelocity(jumpForce);
    }

    public override void Input() {
        base.Input();
        xInput = inputHandle.XInput;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isFinishAnimation) {
            stateMachine.ChangeState(controller.InAirState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(xInput * jumpSpeed);
    }

    public bool CanJump {
        get => amountOfJumpLeft > 0;
    }

    public void DecreaseAmountOfJumpLeft() {
        amountOfJumpLeft--;
    }

    public void ResetAmountOfJumpLeft() {
        amountOfJumpLeft = maxAmountOfJump;
    }

    public void IncreaseMaxAmountOfJump() {
        maxAmountOfJump++;
        ResetAmountOfJumpLeft();
    }
}
