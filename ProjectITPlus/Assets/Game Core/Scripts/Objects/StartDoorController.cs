using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class StartDoorController : MonoBehaviour {
    [SerializeField] Animator animator;

    private void Start () {
        AudioManager.Instance.PlaySFX("Door Open");
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor () {
        yield return new WaitForSeconds(.8f);
        animator.SetTrigger("close");
        AudioManager.Instance.PlaySFX("Door Close");
    }
}
