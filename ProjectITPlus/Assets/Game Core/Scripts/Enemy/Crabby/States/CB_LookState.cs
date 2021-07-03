using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_LookState : CB_State {
    private bool isDetectedPlayerMax;

    #region Constructor
    public CB_LookState(CB_Controller controller, CB_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedPlayerMax = core.Detect.MaxPlayer;
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
            if (isDetectedPlayerMax) {
                stateMachine.ChangeState(controller.PatrolState);
            }
            else {
                stateMachine.ChangeState(controller.IdleState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
