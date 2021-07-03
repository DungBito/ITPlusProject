using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_GroundState : PS_State {
    #region Constructor
    public PS_GroundState(PS_Controller controller, PS_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
    }

    public override void Enter() {
        base.Enter();
        core.Movement.SetXVelocity(core.Movement.CurrentVelocity.x / 3);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isFinishAnimation) {
            stateMachine.ChangeState(controller.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
