using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_HitboxBomb : MonoBehaviour {
    private List<Collider2D> detected = new List<Collider2D>();
    private BP_Controller controller;

    private void Awake() {
        controller = GetComponentInParent<BP_Controller>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!detected.Contains(collision)) {
            detected.Add(collision);
            TakeAction(collision);
        }
    }

    private void TakeAction(Collider2D collision) {
        var damageable = collision.GetComponentInParent<IBombAction>();
        if (damageable != null) {
            damageable.Kick(controller.Core.Movement.FacingDirection);
        }
    }

    public void ClearListDetected() => detected.Clear();
}
