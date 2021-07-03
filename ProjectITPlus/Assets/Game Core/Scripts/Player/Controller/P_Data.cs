using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player")]
public class P_Data : ScriptableObject {
    [Header("Door In/Out")]
    public float doorSpeed;

    [Header("Run State")]
    public float runSpeed;

    [Header("Jump/Ground State")]
    public int amountOfJump;
    public float jumpForce;
    public float jumpSpeed;
    public float groundSpeed;

    [Header("In Air State")]
    public float inAirSpeed;
    public float fallMultiplier;
    public float timeToCheckChangeState;

    [Header("Throw State")]
    public float throwSpeed;
    public float maxTimeHoldThrow;
    public float maxForceThrow;
    public float delayThrowTime;
}
