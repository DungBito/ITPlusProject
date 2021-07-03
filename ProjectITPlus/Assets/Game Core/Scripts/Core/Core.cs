using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {
    [SerializeField] bool collision;
    [SerializeField] bool detect;
    [SerializeField] bool movement;

    public CollisionGround Collision { get; private set; }
    public DetectEntity Detect { get; private set; }
    public Movement Movement { get; private set; }

    private void Awake() {
        if (collision) {
            Collision = GetComponentInChildren<CollisionGround>();
        }
        if (detect) {
            Detect = GetComponentInChildren<DetectEntity>();
        }
        if (movement) {
            Movement = GetComponentInChildren<Movement>();
        }
    }

    public void LogicUpdate() {
        if (movement) {
            Movement.LogicUpdate();
        }
    }
}
