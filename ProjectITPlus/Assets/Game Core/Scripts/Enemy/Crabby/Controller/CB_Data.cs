using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crabby Data", menuName = "Data/Crabby")]
public class CB_Data : ScriptableObject {
    [Header("Idle State")]
    public float idleTime;

    [Header("Patrol State")]
    public float patrolSpeed;
    public float patrolTime;

    [Header("Attack State")]
    public float delayAttackTime;
}
