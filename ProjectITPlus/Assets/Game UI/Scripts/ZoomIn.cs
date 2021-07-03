using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ZoomIn : MonoBehaviour {
    private void OnEnable () {
        transform.DOScale(.9f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
