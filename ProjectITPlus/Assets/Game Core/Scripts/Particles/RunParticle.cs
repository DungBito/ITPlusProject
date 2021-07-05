using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticle : MonoBehaviour {
    private void OnEnable () {
        StartCoroutine(AddToPool());
    }

    private IEnumerator AddToPool () {
        yield return new WaitForSeconds(.3f);
        Pooler.Instance.AddToPool("Run", gameObject);
    }
}
