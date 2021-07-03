using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEntity : CoreComponent {
    [Header("Player")]
    [SerializeField] bool checkPlayer = false;
    [ConditionalHide("checkPlayer", HideInInspector = true)] [SerializeField] Transform playerCheck;
    [ConditionalHide("checkPlayer", HideInInspector = true)] [SerializeField] float playerCheckDistance;
    [ConditionalHide("checkPlayer", HideInInspector = true)] [SerializeField] float playerMaxCheckDistance;

    [Header("Bomb")]
    [SerializeField] bool checkBomb = false;
    [ConditionalHide("checkBomb", HideInInspector = true)] [SerializeField] Transform bombCheck;
    [ConditionalHide("checkBomb", HideInInspector = true)] [SerializeField] float bombCheckDistance;

    [Header("LayerMask")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask whatIsBomb;

    #region Checks
    public bool Player {
        get => Physics2D.Raycast(playerCheck.position, Vector2.right * core.Movement.FacingDirection, playerCheckDistance, whatIsPlayer)
            && checkPlayer;
    }

    public bool MaxPlayer {
        get => Physics2D.Raycast(playerCheck.position, Vector2.right * core.Movement.FacingDirection, playerMaxCheckDistance, whatIsPlayer)
            && checkPlayer;
    }

    public bool Bomb {
        get => Physics2D.Raycast(bombCheck.position, Vector2.right * core.Movement.FacingDirection, bombCheckDistance, whatIsBomb)
            && checkBomb;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos() {
        if (playerCheck != null && core != null) {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + core.Movement.FacingDirection * playerMaxCheckDistance * Vector3.right);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + core.Movement.FacingDirection * playerCheckDistance * Vector3.right);
        }
        if (bombCheck != null && core != null) {
            Gizmos.DrawLine(bombCheck.position, bombCheck.position + core.Movement.FacingDirection * bombCheckDistance * Vector3.right);
        }
    }
    #endregion
}
