using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pink Star Data", menuName = "Data/Pink Star")]
public class PS_Data : ScriptableObject {
    [Header("Idle State")]
    public float idleTime;

    [Header("Attack State")]
    public float attackSpeed;
    public float attackMaxTime;
}
