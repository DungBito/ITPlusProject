using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent {
    #region Init, Config
    //Config
    [Header("Face Direction")]
    [SerializeField] bool isFacingRight;

    //Component
    public Rigidbody2D Rigidbody { get; private set; }

    //Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; }

    //Workspace
    private Vector2 workspace;

    //Awake
    protected override void Awake() {
        base.Awake();
        Rigidbody = core.GetComponentInParent<Rigidbody2D>();
        FacingDirection = isFacingRight ? 1 : -1;
    }
    #endregion

    #region Logic Update
    public void LogicUpdate() {
        CurrentVelocity = Rigidbody.velocity;
    }
    #endregion

    #region Sets
    public void SetXVelocity(float veloToSet) {
        workspace.Set(veloToSet, CurrentVelocity.y);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetYVelocity(float veloToSet) {
        workspace.Set(CurrentVelocity.x, veloToSet);
        Rigidbody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocity(Vector2 veloToSet) {
        Rigidbody.velocity = veloToSet;
        CurrentVelocity = veloToSet;
    }

    public void SetPlusVelocity(Vector2 veloPlus) {
        Rigidbody.velocity += veloPlus;
    }

    public void SetZeroVelocity() {
        Rigidbody.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public void AddForce(Vector2 forceToAdd, ForceMode2D forceMode) {
        Rigidbody.AddForce(forceToAdd, forceMode);
    }

    public void CheckIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    public void Flip() {
        FacingDirection *= -1;
        Rigidbody.transform.Rotate(0, 180, 0);
    }
    #endregion

    #region Checks
    public bool IsNegativeYVelo => CurrentVelocity.y <= .01f;
    #endregion
}
