using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Explosion : MonoBehaviour {
    private List<Collider2D> detected = new List<Collider2D>();
    private float direction;
    private float force;

    private void OnTriggerStay2D(Collider2D collision) {
        if (!detected.Contains(collision)) {
            detected.Add(collision);
            TakeAction(collision);
        }
    }

    private void TakeAction(Collider2D collision) {
        var pos = collision.transform.position - transform.position;
        direction = pos.normalized.x;
        force = .75f / Mathf.Abs(pos.magnitude);
        if (collision.CompareTag("Bomb")) {
            var action = collision.GetComponentInParent<IBombAction>();
            if (action != null) {
                action.Push(direction, force);
            }
        }
        else if (collision.CompareTag("Other")) {
            var rigid = collision.GetComponent<Rigidbody2D>();
            rigid.AddForce(new Vector2(direction * force, 1.5f * force), ForceMode2D.Impulse);
            rigid.AddTorque(-direction, ForceMode2D.Impulse);
        }
        else {
            var damageable = collision.GetComponentInParent<IDamageable>();
            if (damageable != null) {
                damageable.Damageable(1, direction * 3.5f * force, 5.5f * force);
            }
        }
    }

    public void ClearListDetected() => detected.Clear();
}
