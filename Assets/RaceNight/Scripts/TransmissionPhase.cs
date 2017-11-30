using System;
using UnityEngine;

[Serializable]
public class TransmissionPhase {
    public float accel = 0;
    public float speedToNextPhase = -1.0f;
    public float maxVelocity = 0;
    public float tolerance = 0;
}
