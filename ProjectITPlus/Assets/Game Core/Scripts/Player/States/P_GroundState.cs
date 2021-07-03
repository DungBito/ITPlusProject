using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundState : P_State {
    private int xInput;

    private float groundSpeed;

    #region Constructor
    public P_GroundState(P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        groundSpeed = data.groundSpeed;
    }
    #endregion

    public override void Check() {
        base.Check();
        core.Movement.CheckIfShouldFlip(xInput);
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Input() {
        base.Input();
        xInput = inputHandle.XInput;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isFinishAnimation || Time.time > startTime + .15f) {
            stateMachine.ChangeState(controller.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(xInput * groundSpeed);
    }
}
