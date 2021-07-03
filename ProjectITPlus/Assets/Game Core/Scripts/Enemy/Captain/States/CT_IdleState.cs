using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_IdleState : CT_State {
    private float idleTime;
    private bool flipAfterIdle;

    private bool isDetectedBomb;
    private bool isDetectedPlayer;

    #region Constructor
    public CT_IdleState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        idleTime = data.idleTime;
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedPlayer = core.Detect.Player;
        isDetectedBomb = core.Detect.Bomb;
    }

    public override void Enter() {
        base.Enter();
        core.Movement.SetZeroVelocity();
    }

    public override void Exit() {
        base.Exit();
        if (flipAfterIdle) {
            flipAfterIdle = false;
            core.Movement.Flip();
        }
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime) {
            stateMachine.ChangeState(controller.PatrolState);
        }
        else if (isDetectedBomb) {
            stateMachine.ChangeState(controller.ScareState);
        }
        else if (isDetectedPlayer && controller.AttackState.CanAttack) {
            stateMachine.ChangeState(controller.AttackState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        if (core.Movement.CurrentVelocity.x != 0) {
            core.Movement.SetZeroVelocity();
        }
    }

    public void SetFlipAfterIdle(bool boolToSet) {
        flipAfterIdle = boolToSet;
    }
}
