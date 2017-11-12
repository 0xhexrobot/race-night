using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
    private float phase = 0;
    [SerializeField]
    private float amplitude = 0;
    public bool reachedSpeed = true;
    public float accel = 0.1f;

    void Update() {
        /*if(reachedSpeed) {
            float velocityPercentage = NormalVelocity.instance.velocity / NormalVelocity.instance.maxVelocity;
            phase += phaseChange;
            float newX = Mathf.Sin(phase) * amplitude * velocityPercentage;
            transform.localPosition = new Vector3(newX, 0, transform.localPosition.z);
        }*/
    }
}
