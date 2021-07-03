using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_AttackState : PS_State {
    private float attackSpeed;
    private float attackMaxTime;

    private bool isDetectedWall;
    private bool isDetectedLedge;

    #region Constructor
    public PS_AttackState(PS_Controller controller, PS_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
        attackSpeed = data.attackSpeed;
        attackMaxTime = data.attackMaxTime;
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedWall = core.Collision.Wall;
        isDetectedLedge = core.Collision.Ledge;
    }

    public override void Enter() {
        base.Enter();
        controller.Attack.gameObject.SetActive(true);
    }

    public override void Exit() {
        base.Exit();
        controller.Attack.gameObject.SetActive(false);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isDetectedWall || !isDetectedLedge) {
            core.Movement.Flip();
            stateMachine.ChangeState(controller.IdleState);
        }
        else if (Time.time >= startTime + attackMaxTime) {
            stateMachine.ChangeState(controller.IdleState);
        }
        else if (controller.Attack.HitPlayer) {
            stateMachine.ChangeState(controller.IdleState);
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        core.Movement.SetXVelocity(core.Movement.FacingDirection * attackSpeed);
    }
}
