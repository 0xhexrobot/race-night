using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    private const float TOLERANCE = 30;
    private PlayerControl playerControl = null;

    void Start() {
        playerControl = Object.FindObjectOfType<PlayerControl>();
    }

    void Update() {
        if(transform.position.x + TOLERANCE < playerControl.transform.position.x) {
            ObjectPool.instance.poolObject(gameObject);
        }
    }
}
