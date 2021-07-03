using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_HitboxAttack : MonoBehaviour {
    private List<Collider2D> detected = new List<Collider2D>();
    private CT_Controller controller;

    private void Awake() {
        controller = GetComponentInParent<CT_Controller>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!detected.Contains(collision)) {
            detected.Add(collision);
            TakeAction(collision);
        }
    }

    private void TakeAction(Collider2D collision) {
        var damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null) {
            damageable.Damageable(1, controller.Core.Movement.FacingDirection * 3f, 5f);
        }
    }

    public void ClearListDetected() => detected.Clear();
}
