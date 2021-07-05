using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_GroundState : CT_State {
    #region Constructor
    public CT_GroundState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Enter() {
        base.Enter();
        core.Movement.SetXVelocity(core.Movement.CurrentVelocity.x / 3);
        Pooler.Instance.SpawnFromPool("Fall", controller.AliveGO.transform);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isFinishAnimation) {
            stateMachine.ChangeState(controller.IdleState);
        }
    }
}
