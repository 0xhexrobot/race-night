using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transmission))]
public class Vehicle : MonoBehaviour {
    [HideInInspector]
    public float velocity = 0;
    [SerializeField]
    private Transform[] tires;
    private float realAccel = 0;
    private TransmissionPhase transmissionPhase;
    private float accelFactor = 0;

    void Start() {
        transmissionPhase = GetComponent<Transmission>().getTransmissionPhase();
    }

    void Update() {
        if(velocity > transmissionPhase.maxVelocity) {
            velocity = transmissionPhase.maxVelocity;
        } else {
            velocity += realAccel;
        }

        //accelerate
        realAccel = transmissionPhase.accel * accelFactor;

        float newX = transform.position.x + velocity * Time.deltaTime;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        foreach(Transform tireTransform in tires) {
            tireTransform.Rotate(velocity * Time.deltaTime * 180.0f, 0, 0);
        }

        if(velocity > 0.1f) {
            velocity *= 0.99f;
        } else {
            velocity = 0;
        }
    }

    public bool tryToChangeTransmissionPhase() {
        bool changedPhase = false;
        Transmission transmission = GetComponent<Transmission>();

        if(velocity >= transmissionPhase.speedToNextPhase && transmission.hasNextPhase()) {
            transmission.upgradeToNextPhase();
            changedPhase = true;

            if(GetComponent<PlayerControl>() != null) {
                UIManager.instance.uiTransmission.success();
            }
        } else {
            if(transmission.hasPrevPhase()) {
                transmission.downgradeToPrevPhase();
                changedPhase = true;
            }

            // velocity penalty
            velocity *= 0.9f;

            if(GetComponent<PlayerControl>() != null) {
                UIManager.instance.uiTransmission.fail();
            }
        }

        transmissionPhase = transmission.getTransmissionPhase();

        return changedPhase;
    }

    public void downgradeTransmissionPhase() {
        Transmission transmission = GetComponent<Transmission>();

        if(transmission.hasPrevPhase()) {
            transmission.downgradeToPrevPhase();
        }

        // velocity penalty
        velocity *= 0.9f;

        transmissionPhase = transmission.getTransmissionPhase();
    }

    public void updateAccelFactor(float accelFactor) {
        this.accelFactor = accelFactor;
    }

    public void stopAccel() {
        accelFactor = 0;
        realAccel = 0;
    }

    public void setTransmissionPhase(TransmissionPhase transmissionPhase) {
        this.transmissionPhase = transmissionPhase;
    }
}
