﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_PatrolState : BP_State {
    private float patrolSpeed;
    private float patrolTime;

    private bool isDetectedWall;
    private bool isDetectedLedge;
    private bool isDetectedBomb;
    private bool isDetectedPlayer;

    #region Constructor
    public BP_PatrolState(BP_Controller controller, BP_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        patrolSpeed = data.patrolSpeed;
        patrolTime = data.patrolTime;
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedWall = core.Collision.Wall;
        isDetectedLedge = core.Collision.Ledge;
        isDetectedBomb = core.Detect.Bomb;
        isDetectedPlayer = core.Detect.Player;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isDetectedWall || !isDetectedLedge) {
            controller.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(controller.IdleState);
        }
        else if (Time.time >= startTime + patrolTime) {
            controller.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(controller.IdleState);
        }
        else if ((isDetectedBomb || isDetectedPlayer) && controller.AttackState.CanAttack) {
            stateMachine.ChangeState(controller.AttackState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(patrolSpeed * core.Movement.FacingDirection);
    }
}
