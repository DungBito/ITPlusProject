using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_DeadState : P_State {
    private bool grounded;

    #region Constructor
    public P_DeadState (P_Controller controller, P_Data data, int hashParam, bool isTriggerParam) : base(controller, data, hashParam, isTriggerParam) {
    }
    #endregion

    public override void Check () {
        base.Check();
        grounded = core.Collision.Grounded;
    }

    public override void Enter () {
        base.Enter();
        core.Movement.SetXVelocity(core.Movement.CurrentVelocity.x / 2);
        controller.StartCoroutine(Dead());
        Pooler.Instance.SpawnFromPool("Fall", controller.AliveGO.transform);
    }

    public override void PhysicsUpdate () {
        base.PhysicsUpdate();
        if (isFinishAnimation && grounded) {
            core.Movement.SetXVelocity(0f);
        }
    }

    private IEnumerator Dead () {
        yield return new WaitForSeconds(2f);
        controller.OnDead();
        controller.DeadGO.transform.SetPositionAndRotation(controller.AliveGO.transform.position, controller.AliveGO.transform.rotation);
        controller.DeadGO.SetActive(true);
        controller.AliveGO.SetActive(false);
        controller.Destroy();
    }
}
