﻿using System;
using UnityEngine;

public class Transmission : MonoBehaviour {
    public TransmissionPhase[] transmissionPhases;
    [HideInInspector]
    public float transmissionValue = 0;
    private float radians = 0;
    private int currentPhase = 0;

    public void update() {
        radians += 0.05f;
        transmissionValue = Mathf.Sin(radians);
    }

    public void applyAxis(float change) {
        transmissionValue += change;
        transmissionValue = Mathf.Clamp(transmissionValue, -1.0f, 1.0f);
    }

    public bool hasNextPhase() {
        return currentPhase < transmissionPhases.Length - 1;
    }

    public void upgradeToNextPhase() {
        if(hasNextPhase()) {
            currentPhase++;
        } else {
            throw new InvalidOperationException("Cannot upgrade. Currently at last phase.");
        }
    }

    public bool hasPrevPhase() {
        return currentPhase > 0;
    }

    public void downgradeToPrevPhase() {
        if(currentPhase > 0) {
            currentPhase--;
        } else {
            throw new InvalidOperationException("Currently at first phase.");
        }
    }

    public int getCurrentPhase() {
        return currentPhase;
    }

    public float getAccel() {
        return transmissionPhases[currentPhase].accel;
    }

    public float getSpeedToNextPhase() {
        return transmissionPhases[currentPhase].speedToNextPhase;
    }

    public float getMaxSpeed() {
        return transmissionPhases[currentPhase].maxVelocity;
    }

    public float getTolerance() {
        return transmissionPhases[currentPhase].tolerance;
    }

    public TransmissionPhase getTransmissionPhase() {
        return transmissionPhases[currentPhase];
    }
}
