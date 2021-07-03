using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_HitState : P_State {
    private float timeToCheck = .15f;

    private bool grounded;

    #region Constructor
    public P_HitState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        grounded = core.Collision.Grounded;
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

    public override void Input() {
        base.Input();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (grounded && core.Movement.IsNegativeYVelo && Time.time >= startTime + timeToCheck) {
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
