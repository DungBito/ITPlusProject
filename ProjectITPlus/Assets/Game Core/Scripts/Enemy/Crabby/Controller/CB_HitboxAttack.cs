using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_HitboxAttack : MonoBehaviour {
    private List<Collider2D> detected = new List<Collider2D>();

    private void OnTriggerStay2D(Collider2D collision) {
        if (!detected.Contains(collision)) {
            detected.Add(collision);
            TakeAction(collision);
        }
    }

    private void TakeAction(Collider2D collision) {
        var damageable = collision.GetComponentInParent<IDamageable>();
        var dir = (collision.transform.position - transform.parent.position).x >= 0 ? 1 : -1;
        if (damageable != null) {
            damageable.Damageable(1, dir * 3f, 5f);
        }
    }

    public void ClearListDetected() => detected.Clear();
}
