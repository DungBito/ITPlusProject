using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_Hitbox : MonoBehaviour {
    private BP_HitboxPlayer hbPlayer;
    private BP_HitboxBomb hbBomb;

    private void Awake() {
        hbPlayer = GetComponentInChildren<BP_HitboxPlayer>();
        hbBomb = GetComponentInChildren<BP_HitboxBomb>();
    }

    public void ClearListDetected() {
        hbPlayer.ClearListDetected();
        hbBomb.ClearListDetected();
    }
}
