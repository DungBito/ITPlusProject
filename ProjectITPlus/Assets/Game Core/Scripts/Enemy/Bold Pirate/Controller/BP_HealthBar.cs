using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BP_HealthBar : MonoBehaviour {
    [SerializeField] Transform bar;

    public void SetSize (float sizeNormalize) {
        bar.DOScale(new Vector2(sizeNormalize, 1f), .2f);
    }
}
