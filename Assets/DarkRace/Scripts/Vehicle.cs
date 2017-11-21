using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {
    public float accel = 0.2f;
    [HideInInspector]
    public float velocity = 0;
    public float maxVelocity = 20.0f;
    private float realAccel = 0;

    void Update() {
        if(velocity > maxVelocity) {
            velocity = maxVelocity;
        } else {
            velocity += realAccel;
        }

        float newX = transform.position.x + velocity * Time.deltaTime;
        transform.position = new Vector3(newX, 0, transform.position.z);

        if(velocity > 0.1f) {
            velocity *= 0.98f;
        } else {
            velocity = 0;
        }
    }

    public void accelerate(float accelFactor) {
        realAccel = accel * accelFactor;
    }

    public void stopAccel() {
        realAccel = 0;
    }
}
