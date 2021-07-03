using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_ThrowState : P_State {
    private float throwSpeed;
    private float maxTimeHoldThrow;
    private float maxForceThrow;
    private float delayThrowTime;

    private int xInput;
    private bool throwInputStop;

    private float timeHold;
    private float forceAddToBomb;
    private bool isHolding;

    #region Constructor
    public P_ThrowState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        throwSpeed = data.throwSpeed;
        maxTimeHoldThrow = data.maxTimeHoldThrow;
        maxForceThrow = data.maxForceThrow;
        delayThrowTime = data.delayThrowTime;
    }
    #endregion

    public override void Check() {
        base.Check();
        core.Movement.CheckIfShouldFlip(xInput);
    }

    public override void Enter() {
        base.Enter();
        isHolding = true;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Input() {
        base.Input();
        xInput = inputHandle.XInput;
        throwInputStop = inputHandle.ThrowInputStop;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        SetAnimation();
        CheckHolding();
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(xInput * throwSpeed);
    }

    private void SetAnimation() {
        controller.Animator.SetFloat("xVelo", Mathf.Abs(core.Movement.CurrentVelocity.x));
    }

    private void CheckHolding() {
        if (isHolding) {
            if (throwInputStop) {
                timeHold = Time.time - startTime;
                isHolding = false;
            }
            else if (Time.time >= startTime + maxTimeHoldThrow) {
                timeHold = maxTimeHoldThrow;
                isHolding = false;
            }
        }
        else {
            forceAddToBomb = (timeHold / maxTimeHoldThrow) * maxForceThrow;
            var bomb = Pooler.Instance.SpawnFromPool("Bomb", controller.BombSpawn);
            bomb.GetComponent<Bomb_Controller>().AddForceWhenSpawn(core.Movement.FacingDirection, forceAddToBomb);
            stateMachine.ChangeState(controller.IdleState);
        }
    }

    public bool CanThrow {
        get => Time.time >= exitTime + delayThrowTime;
    }

    public void SetLastThrowTime() {
        exitTime = Time.time;
    }
}
