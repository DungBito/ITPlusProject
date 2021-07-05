using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_DeadState : CT_State {
    private float timeDead = 2f;
    private bool dead = false;

    private bool isDetectedGround;

    #region Constructor
    public CT_DeadState(CT_Controller controller, CT_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check() {
        base.Check();
        isDetectedGround = core.Collision.Grounded;
    }

    public override void Enter() {
        base.Enter();
        core.Movement.SetXVelocity(core.Movement.CurrentVelocity.x / 3);
        Pooler.Instance.SpawnFromPool("Fall", controller.AliveGO.transform);
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isFinishAnimation && isDetectedGround && !dead) {
            core.Movement.SetXVelocity(0f);
            startTime = Time.time;
            dead = true;
        }
        if (dead && Time.time >= timeDead + startTime) {
            controller.OnDead();
            controller.DeadGO.transform.SetPositionAndRotation(controller.AliveGO.transform.position, controller.AliveGO.transform.rotation);
            controller.AliveGO.SetActive(false);
            controller.DeadGO.SetActive(true);
            controller.Destroy();
        }
    }
}
