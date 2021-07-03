using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Captain Data", menuName = "Data/Captain")]
public class CT_Data : ScriptableObject {
    [Header("Idle State")]
    public float idleTime;

    [Header("Patrol State")]
    public float patrolSpeed;
    public float patrolTime;

    [Header("Attack State")]
    public float delayAttackTime;

    [Header("Scare State")]
    public float timeScare;
    public float scareSpeed;
}
