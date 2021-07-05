using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using System;

public class HeartBarController : MonoBehaviour {
    [SerializeField] GameObject[] hearts;
    private int heartLeft;

    private void Awake () {
        this.RegisterListener(EventID.OnPlay, (param) => OnPlay());
        this.RegisterListener(EventID.PlayerTakeDamage, (param) => OnPlayerTakeDamage());
        this.RegisterListener(EventID.PlayerCollectHeart, (param) => OnPlayerCollectHeart());
    }

    private void OnPlay () {
        foreach (var item in hearts) {
            item.SetActive(true);
        }
        heartLeft = hearts.Length;
    }

    private void OnPlayerTakeDamage () {
        if (heartLeft > 0) {
            heartLeft -= 1;
            hearts[heartLeft].SetActive(false);
        }
    }

    private void OnPlayerCollectHeart () {
        if (heartLeft < hearts.Length ) {
            hearts[heartLeft].SetActive(true);
            heartLeft += 1;
        }
    }
}
