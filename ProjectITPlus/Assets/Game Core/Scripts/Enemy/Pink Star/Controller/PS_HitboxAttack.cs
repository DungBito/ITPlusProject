using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_HitboxAttack : MonoBehaviour {
    private PS_Controller controller;

    private void Awake() {
        controller = GetComponentInParent<PS_Controller>();
    }

    public bool HitPlayer { get; private set; }

    private void OnEnable() {
        HitPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null) {
            damageable.Damageable(1, controller.Core.Movement.FacingDirection * 3f, 5f);
        }
        HitPlayer = true;
    }
}
