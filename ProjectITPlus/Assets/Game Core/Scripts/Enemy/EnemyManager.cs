using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class EnemyManager : MonoBehaviour {
    [SerializeField] int enemyCount;
    private int enemyLeft;
    private bool isPostEvent;

    private void Awake () {
        this.RegisterListener(EventID.EnemyDead, (param) => OnEnemyDead());
    }

    private void Start () {
        enemyLeft = enemyCount;
        isPostEvent = false;
    }

    private void OnEnemyDead () {
        enemyLeft--;
        if (enemyLeft <= 0 && !isPostEvent) {
            isPostEvent = true;
            this.PostEvent(EventID.OnClearEnemy);
        }
    }
}
