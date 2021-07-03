using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_LookState : CT_State {
    private bool isDetectedBomb;
    private bool isDetectedPlayerMax;

    #region Constructor
    public CT_LookState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedBomb = core.Detect.Bomb;
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
            if (isDetectedBomb) {
                stateMachine.ChangeState(controller.ScareState);
            }
            else if (isDetectedPlayerMax) {
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
