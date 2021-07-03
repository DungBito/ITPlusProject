using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionGround : CoreComponent {
    #region Init, Config
    [Header("Ground")]
    [SerializeField] bool checkGround = false;
    [ConditionalHide("checkGround", HideInInspector = true)] [SerializeField] Transform groundCheck;
    [ConditionalHide("checkGround", HideInInspector = true)] [SerializeField] float distanceBettwenLeg;
    [ConditionalHide("checkGround", HideInInspector = true)] [SerializeField] float groundCheckDistance;

    [Header("Ledge")]
    [SerializeField] bool checkLedge = false;
    [ConditionalHide("checkLedge", HideInInspector = true)] [SerializeField] Transform ledgeCheck;
    [ConditionalHide("checkLedge", HideInInspector = true)] [SerializeField] float ledgeCheckDistance;

    [Header("Wall")]
    [SerializeField] bool checkWall = false;
    [ConditionalHide("checkWall", HideInInspector = true)] [SerializeField] Transform wallCheck;
    [ConditionalHide("checkWall", HideInInspector = true)] [SerializeField] float wallCheckDistance;

    [Header("LayerMask")]
    [SerializeField] LayerMask whatIsGround;
    #endregion

    #region Checks
    public bool Grounded {
        get => (Physics2D.Raycast(groundCheck.position + (Vector3.right * distanceBettwenLeg), Vector2.down, groundCheckDistance, whatIsGround)
            || Physics2D.Raycast(groundCheck.position - (Vector3.right * distanceBettwenLeg), Vector2.down, groundCheckDistance, whatIsGround))
            && checkGround;
    }

    public bool Ledge {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.down, ledgeCheckDistance, whatIsGround)
            && checkLedge;
    }

    public bool Wall {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround)
            && checkWall;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos() {
        if (groundCheck != null) {
            Gizmos.DrawLine(groundCheck.position + Vector3.right * distanceBettwenLeg, groundCheck.position + Vector3.right * distanceBettwenLeg + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(groundCheck.position - Vector3.right * distanceBettwenLeg, groundCheck.position - Vector3.right * distanceBettwenLeg + Vector3.down * groundCheckDistance);
        }
        if (wallCheck != null && core != null) {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + core.Movement.FacingDirection * wallCheckDistance * Vector3.right);
        }
        if (ledgeCheck != null) {
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.down * ledgeCheckDistance);
        }
    }
    #endregion
}
