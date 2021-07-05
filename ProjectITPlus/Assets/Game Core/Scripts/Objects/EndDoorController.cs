using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using Audio;

public class EndDoorController : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D box;

    private void Awake () {
        this.RegisterListener(EventID.OnClearEnemy, (param) => OnClearEnemy());
    }

    private void Start () {
        box.enabled = false;
    }

    private void OnClearEnemy () {
        AudioManager.Instance.PlaySFX("Door Open");
        animator.SetTrigger("open");
        box.enabled = true;
    }

    private void OnTriggerEnter2D (Collider2D collision) {
        var controller = collision.GetComponentInParent<P_Controller>();
        controller.StateMachine.ChangeState(controller.DoorInState);
        StartCoroutine(CloseDoor());
    }

    private IEnumerator CloseDoor () {
        yield return new WaitForSeconds(.8f);
        animator.SetTrigger("close");
        AudioManager.Instance.PlaySFX("Door Close");
        StartCoroutine(PostEventUI());
    }

    private IEnumerator PostEventUI () {
        yield return new WaitForSeconds(1.5f);
        this.PostEvent(EventID.OnWin);
    }
}
