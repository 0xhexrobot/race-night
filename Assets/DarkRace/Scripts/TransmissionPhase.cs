using System;
using UnityEngine;

[Serializable]
public class TransmissionPhase : MonoBehaviour {
    public float accel = 0;
    public float speedToNextPhase = -1.0f;
    public float maxSpeed = 0;
    public float tolerance = 0;
}
