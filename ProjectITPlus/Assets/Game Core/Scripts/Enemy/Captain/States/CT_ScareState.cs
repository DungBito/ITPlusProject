﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_ScareState : CT_State {
    private float timeScare;
    private float scareSpeed;

    private bool isDetectedWall;
    private bool isDetectedLedge;

    #region Constructor
    public CT_ScareState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        timeScare = data.timeScare;
        scareSpeed = data.scareSpeed;
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedWall = core.Collision.Wall;
        isDetectedLedge = core.Collision.Ledge;
    }

    public override void Enter() {
        base.Enter();
        core.Movement.Flip();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (Time.time >= startTime + timeScare) {
            stateMachine.ChangeState(controller.IdleState);
        }
        else if (isDetectedWall || !isDetectedLedge) {
            core.Movement.Flip();
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(scareSpeed * core.Movement.FacingDirection);
    }
}
