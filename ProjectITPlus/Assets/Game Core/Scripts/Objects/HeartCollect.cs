using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour {
    [SerializeField] Animator animator;

    private void OnTriggerEnter2D (Collider2D collision) {
        animator.SetTrigger("take");
        Invoke(nameof(Destroy), .27f);
        var controller = collision.GetComponentInParent<P_Controller>();
        if (controller != null) {
            controller.CollectHeart();
        }
    }

    private void Destroy () {
        Destroy(gameObject);
    }
}
