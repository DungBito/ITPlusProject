using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_HitState : BP_State {
    private float timeToCheck = .15f;

    private bool isDetectedGround;

    #region Constructor
    public BP_HitState(BP_Controller controller, BP_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedGround = core.Collision.Grounded;
    }

    public override void Enter() {
        base.Enter();
        if (controller.IsDead) {
            controller.Capsule.direction = CapsuleDirection2D.Horizontal;
        }
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
