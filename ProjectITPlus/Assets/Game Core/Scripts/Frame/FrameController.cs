using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour {
    [SerializeField] GameObject virtualCam;
    [SerializeField] GameObject[] activeWhenFrame;

    [SerializeField] bool activeOnStart;

    private void Start () {
        if (activeOnStart) {
            foreach (var item in activeWhenFrame) {
                item.SetActive(true);
            }
        }
        else {
            foreach (var item in activeWhenFrame) {
                item.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        virtualCam.SetActive(true);
        foreach (var item in activeWhenFrame) {
            item.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        virtualCam.SetActive(false);
        foreach (var item in activeWhenFrame) {
            item.SetActive(false);
        }
    }
}
