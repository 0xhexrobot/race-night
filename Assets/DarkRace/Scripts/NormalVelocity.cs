using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalVelocity : MonoBehaviour {
    [SerializeField]
    private float velocity = 0;

    void Update() {
        transform.Translate(velocity * Time.deltaTime, 0, 0);
    }
}
