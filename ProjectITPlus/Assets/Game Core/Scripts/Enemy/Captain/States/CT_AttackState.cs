using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_AttackState : CT_State {
    private float delayAttackTime;

    #region Constructor
    public CT_AttackState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        delayAttackTime = data.delayAttackTime;
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
        if (isFinishAnimation) {
            stateMachine.ChangeState(controller.LookState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }

    public bool CanAttack {
        get => Time.time >= exitTime + delayAttackTime;
    }
}
