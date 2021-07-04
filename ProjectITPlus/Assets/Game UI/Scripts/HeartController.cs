using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class HeartController : MonoBehaviour {
    [SerializeField] GameObject[] hearts;
    private int heartLeft;

    private void Awake () {
        this.RegisterListener(EventID.OnPlay, (param) => OnPlay());
        this.RegisterListener(EventID.PlayerTakeDamage, (param) => OnPlayerTakeDamage());
    }

    private void OnPlay () {
        foreach (var item in hearts) {
            item.SetActive(true);
        }
        heartLeft = hearts.Length;
    }

    private void OnPlayerTakeDamage () {
        heartLeft -= 1;
        if (heartLeft >= 0) {
            hearts[heartLeft].SetActive(false);
        }
    }
}
