using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_HitState : CB_State {
    private float timeToCheck = .15f;

    private bool isDetectedGround;

    #region Constructor
    public CB_HitState(CB_Controller controller, CB_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedGround = core.Collision.Grounded;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isDetectedGround && core.Movement.IsNegativeYVelo && Time.time >= startTime + timeToCheck) {
            if (controller.IsDead) {
                stateMachine.ChangeState(controller.DeadState);
            }
            else {
                stateMachine.ChangeState(controller.GroundState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
