using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_IdleState : PS_State {
    private float idleTime;

    #region Constructor
    public PS_IdleState(PS_Controller controller, PS_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        idleTime = data.idleTime;
    }
    #endregion

    public override void Check() {
        base.Check();
    }

    public override void Enter() {
        base.Enter();
        core.Movement.SetZeroVelocity();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime) {
            stateMachine.ChangeState(controller.AttackState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        if (core.Movement.CurrentVelocity.x != 0) {
            core.Movement.SetZeroVelocity();
        }
    }
}
