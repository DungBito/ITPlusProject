using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DoorInState : P_State {
    #region Constructor
    public P_DoorInState (P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void LogicUpdate () {
        base.LogicUpdate();
        if (isFinishAnimation) {
            core.Movement.SetZeroVelocity();
        }
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(core.Movement.FacingDirection * .5f);
    }
}
