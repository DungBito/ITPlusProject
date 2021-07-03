using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bold Pirate Data", menuName = "Data/Bold Pirate")]
public class BP_Data : ScriptableObject {
    [Header("Idle State")]
    public float idleTime;

    [Header("Patrol State")]
    public float patrolSpeed;
    public float patrolTime;

    [Header("Attack State")]
    public float delayAttackTime;
}
